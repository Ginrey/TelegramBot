using System;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;

namespace TelegramBot.Net
{
    public delegate void ReceiveMessageDelegate(Client client, Message message);

    public class Connection
    {
        public ReceiveMessageDelegate ReceiveMessageComplete { get; set; }
        public Session Session { get; set; }

        public Connection(Session session)
        {
            Session = session;
            ReceiveMessageComplete += OnStart;
            ReceiveMessageComplete += OnDeserialize;
        }

        void GetAllConfigurationClient(Client client)
        {
            if (Session.Sql.IsPresentTempTelegram(client.TelegramId))
            {
                int intStep;
                long idFrom;
                bool rocket;
                if (Session.Sql.GetTempTaygaStep(client.TelegramId, out intStep))
                    client.Step = new PacketIdentifier((TypeSteps) intStep, 0);
                if (Session.Sql.GetFromReferal(client.TelegramId, out idFrom, out rocket))
                {
                    client.AdditionInfo.Rocket = rocket;
                    client.AdditionInfo.IdFromReferal = idFrom;
                }
            }
            else
            {
                Session.Sql.InsertTempTayga(client.TelegramId);
            }
        }

        internal void OnMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var message = messageEventArgs.Message;
            var client = Session.ManagerClient.Get(message.Chat.Id);
            if (!client.Access.CheckAccess(Access.AccessType.Message)) return;
            client.Access.AddTime(Access.AccessType.Message, 1);
            if (client.IsNew)
            {
                GetAllConfigurationClient(client);
            }
            ReceiveMessageComplete?.Invoke(client, message);
        }

        void OnStart(Client client, Message msg)
        {
            try
            {
                Session.Bot?.SendTextMessageAsync(client.TelegramId,  "Сервис закрыт на технический перерыв");
                return;
                if (msg.Text == null || !msg.Text.StartsWith("/start")) return;
                var lines = msg.Text.Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                string fromreferal = lines.Length == 2 ? lines[1] : "";

                client.UpdateInfo(fromreferal, msg.From.Username);
                if (lines.Length == 2 && client.AdditionInfo.IdFromReferal != 0)
                {
                    Session.Sql.InsertFromReferal(client.TelegramId, client.AdditionInfo.IdFromReferal,
                        client.AdditionInfo.Rocket);
                    Session.Sql.InsertTempTayga(client.TelegramId);
                    if (client.AdditionInfo.Rocket)
                        Session.Sql.UpdateFromReferal(client.TelegramId, client.AdditionInfo.IdFromReferal);
                }
                if (client.AdditionInfo.Rocket)
                {
                    Session.Sql.UpdateFromReferalRocket(client.TelegramId, true);
                    Console.WriteLine("[{0}] {1} Rocket user", DateTime.Now, client.TelegramId);
                }
                if (client.AdditionInfo.BigStep) client.Step.Update(TypeSteps.One, 0);

                if (client.AdditionInfo.IdFromReferal == 0)
                {
                    Session.Bot?.SendTextMessageAsync(client.TelegramId,
                        "Используйте реферальную ссылку для начала работы в боте");
                    return;
                }
                OnSerialize(client);
            }
            catch (Exception ex)
            {
                Log.AddError("OnStart", ex);
            }
        }

        public void OnSerialize(Client client, bool isSave = true)
        {
            Session.ManagerTask.Add(() =>
            {
                if (Session.PacketRegistry.TryGetPacket(client.Step, out IPacket progress))
                {
                    if(isSave)
                    Session.Sql.UpdateTempTaygaStep(client.TelegramId, (int) client.Step.TypeSteps);
                    client.Logger($"Jump to step: {client.Step.TypeSteps}", true, ConsoleColor.Green);
                    progress.Serialize(client, new StepsEventArgs(null, Session));
                }
                else
                    Log.WriteLine("OnSerialize(Packet don't found)", ConsoleColor.DarkMagenta, false);
            });
        }
        void OnDeserialize(Client client, Message msg)
        {
            Session.Bot?.SendTextMessageAsync(client.TelegramId, "Сервис закрыт на технический перерыв");
            return;
            if (msg.Text!= null &&  msg.Text.StartsWith("/start")) return;
            Session.ManagerTask.Add(() =>
            {
                if (Session.PacketRegistry.TryGetPacket(client.Step, out IPacket progress))
                {
                    client.Logger($"Send info to step: {client.Step.TypeSteps}", true, ConsoleColor.Green);
                    progress.Deserialize(client, new StepsEventArgs(msg, Session));
                }
                else
                    Log.WriteLine("OnDeserialize(Packet don't found)", ConsoleColor.DarkMagenta, false);
            });
        }

        internal void OnInlineQueryReceived(object sender, CallbackQueryEventArgs messageEventArgs)
        {
            var message = messageEventArgs.CallbackQuery.Message;
            message.Text = messageEventArgs.CallbackQuery.Data;
            OnMessageReceived(sender, new MessageEventArgs(message));
        }
    }
}
