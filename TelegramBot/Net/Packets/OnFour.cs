using System;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;
using TelegramBot.IO.Menu;


namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.Four, 0)]
    internal class OnFour : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                await e.Network.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADXAADO4AwSr0KwrRyoMUXAg", 212, "100lbov", "Listen");
                client.Access.AddTime(Access.AccessType.Step, 212);
                var keyboard = TelegramCode.GetNextInlineKeyboard(client.Language);
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "«Идеальный товар»",
                    replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("Four Serialize", ex, client);
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
                        client.Step.Update(TypeSteps.Five, 0);
                        e.Network.Connection.OnSerialize(client);
                    }
                    else
                        e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                            "Следующая аудиоглава будет вам отправлена исключительно после того, как вы ознакомитесь с предыдущей.");
                }
            }
            catch (Exception ex)
            {
                Log.AddError("Four Deserialize", ex, client);
            }
        }
    }
}
