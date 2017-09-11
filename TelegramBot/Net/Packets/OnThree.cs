using System;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;
using TelegramBot.IO.Menu;


namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.Three, 0)]
    internal class OnThree : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                await e.Network.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADWwADO4AwSjHVA3ZjxqCKAg", 398, "100lbov", "Listen");
                client.Access.AddTime(Access.AccessType.Step, 398);
                var keyboard = TelegramCode.GetNextInlineKeyboard(client.Language);
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "«Задачи и их решения»", replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("Three Serialize", ex, client);
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
                        client.Step.Update(TypeSteps.Four, 0);
                        e.Network.Connection.OnSerialize(client);
                    }
                    else
                        e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                            "Следующая аудиоглава будет вам отправлена исключительно после того, как вы ознакомитесь с предыдущей.");
                }
            }
            catch (Exception ex)
            {
                Log.AddError("Three Deserialize", ex, client);
            }
        }
    }
}
