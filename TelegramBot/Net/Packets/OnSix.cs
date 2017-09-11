using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;
using TelegramBot.IO.Localizations;
using TelegramBot.IO.Menu;


namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.Six, 0)]
    internal class OnSix : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                await e.Network.Bot?.SendVideoAsync(client.TelegramId, "BAADAgADBQADoV44Soq8vr55rQOxAg");
                client.Access.AddTime(Access.AccessType.Step, 133);
                var keyboard = TelegramCode.GetNextInlineKeyboard(client.Language, true);
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "«Презентация»", replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("Six Serialize", ex, client);
            }
        }

        public void Deserialize(Client client, StepsEventArgs e)
        {
            try
            {
                if (e.Message.Text.StartsWith("/Next"))
                {
                    if (client.Access.CheckAccess(Access.AccessType.Step))
                    {
                        client.Step.Update(TypeSteps.Seven, 0);
                        e.Network.Connection.OnSerialize(client);
                    }
                    else
                        e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                            "Следующая аудиоглава будет вам отправлена исключительно после того, как вы ознакомитесь с предыдущей.");
                }
                if (e.Message.Text.StartsWith("/Help"))
                {
                    client.HelpPlease();
                }
            }
            catch (Exception ex)
            {
                Log.AddError("Six Deserialize", ex, client);
            }
        }
    }
}
