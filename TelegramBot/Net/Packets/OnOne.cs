using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;
using TelegramBot.IO.Menu;


namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.One, 0)]
    internal class OnOne: IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                 client.HideMenu("Здравствуйте!\nНажмите на аудио-файл для его прослушивания.");
                // await e.Network.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADFAADzsVBSg_tFoIB7sd3Ag", 194, "100lbov",
                //      "Listen");
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId,"Слушаем ");
                client.Access.AddTime(Access.AccessType.Step, 20);
                var keyboard = TelegramCode.GetNextInlineKeyboard(client.Language);
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                    "«Как заработать деньги в социальных сетях и не только»\nавтор - Илья Столбов \n«Приветствие»",replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("One Serialize", ex, client);
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
                        client.Step.Update(TypeSteps.GetMobile, 0);
                        e.Network.Connection.OnSerialize(client);
                    }
                    else
                        e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                            "Следующая аудиоглава будет вам отправлена исключительно после того, как вы ознакомитесь с предыдущей.");
                }
            }
            catch (Exception ex)
            {
                Log.AddError("One Deserialize", ex, client);
            }
        }
    }
}
