using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;
using TelegramBot.IO.Menu;


namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.Seven, 0)]
    internal class OnSeven : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                await e.Network.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADYgADO4AwShBeWIs2PLQyAg", 506, "100lbov","Listen");
                client.Access.AddTime(Access.AccessType.Step, 506);
                var keyboard = TelegramCode.GetNextInlineKeyboard(client.Language, true);
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "«Гениальный маркетинговый продукт»", replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("Seven Serialize", ex, client);
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
                        client.Step.Update(TypeSteps.Eight, 0);
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
                Log.AddError("Seven Deserialize", ex, client);
            }
        }
    }
}
