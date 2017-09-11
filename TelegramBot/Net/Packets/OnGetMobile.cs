using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;


namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.GetMobile, 0)]
    public class OnGetMobile : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(new[]
            {
                new[] {new KeyboardButton("Отправить номер телефона") {RequestContact = true}}
            });

            await e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                "Для того чтобы мы могли с вами связаиться подтвердите номер телефона.\n" +
                "Нажмите на кнопку для подтверждения номера телефона", replyMarkup: keyboard);
        }

        public async void Deserialize(Client client, StepsEventArgs e)
        {
            try
            {
                if (e.Message.Type == Telegram.Bot.Types.Enums.MessageType.ContactMessage)
                {
                    client.AdditionInfo.Mobile = e.Message.Contact.PhoneNumber;
                    client.AdditionInfo.Mobile = "+" + client.AdditionInfo.Mobile.TrimStart('+');
                    e.Network.Sql.UpdateTempTaygaMobile(client.TelegramId, client.AdditionInfo.Mobile);
                    if (client.AdditionInfo.IdFromReferal!=0)
                    {
                        long tid;
                        if(e.Network.Sql.GetTelegramId(client.AdditionInfo.IdFromReferal, out tid))
                            {
                                string sendText = $"Человек с номером {client.AdditionInfo.Mobile} перешел по вашей ссылке\n";
                                if (!string.IsNullOrEmpty(client.AdditionInfo.Username))
                                    sendText += $"С ним сожно связаться по ссылке: https://t.me/{client.AdditionInfo.Username}/";
                               await e.Network.Bot?.SendTextMessageAsync(tid, sendText);
                            }
                    }
                    client.Step = client.AdditionInfo.BigStep ? client.Step.Update(TypeSteps.Nine, 0) : client.Step.Update(TypeSteps.Two, 0);
                    e.Network.Connection.OnSerialize(client);
                }
                else
                  await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "Неверные данные, отправляйте номер нажатием на кнопку");
            }
            catch (Exception ex)
            {
                Log.AddError("GetMobile Deserialize", ex, client);
            }
        }
    }
}
