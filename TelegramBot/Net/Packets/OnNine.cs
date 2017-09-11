using System;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;
using TelegramBot.IO.Menu;


namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.Eight, 0)]
    internal class OnNine : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                await e.Network.Bot?.SendVideoAsync(client.TelegramId, "BAADAgADEgADoV44SnBgwPiPTQqhAg");
                var keyboard = TelegramCode.GetNextInlineKeyboard(client.Language, true);
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "«Регистрация»",replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("Nine Serialize", ex, client);
            }
        }

        public void Deserialize(Client client, StepsEventArgs e)
        {
            try
            {
                if (e.Message.Text.StartsWith("/Next"))
                {
                    client.Step.Update(TypeSteps.Regisration, 0);
                    e.Network.Connection.OnSerialize(client);
                }
                if (e.Message.Text.StartsWith("/Help"))
                {
                    client.HelpPlease();
                }
            }
            catch (Exception ex)
            {
                Log.AddError("Nine Deserialize", ex, client);
            }
        }
    }
}
