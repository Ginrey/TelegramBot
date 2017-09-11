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
    [PacketIdentifier(TypeSteps.Eight, 0)]
    internal class OnEight : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                await e.Network.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADGAADzsVBSvT5vtMBgkgBAg", 152, "100lbov", "Listen");
                client.Access.AddTime(Access.AccessType.Step, 152);
                var keyboard = TelegramCode.GetNextInlineKeyboard(client.Language, true);
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "«Концепт бизнеса: Простота, Легкость, Результат»", replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("Eight Serialize", ex, client);
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
                        client.Step.Update(TypeSteps.Nine, 0);
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
                Log.AddError("Eight Deserialize", ex, client);
            }
        }
    }
}
