using System;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;
using TelegramBot.IO.Menu;


namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.Eight, 0)]
    internal class OnRegistration : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                if (client.AdditionInfo.QuestionList.Count == 0)
                {

                    if (client.AdditionInfo.IdFromReferal == 0) client.AdditionInfo.IdFromReferal = 845797;
                    long id;
                    if (client.AdditionInfo.IdFromReferal == 888888 || client.AdditionInfo.Rocket)
                        if (e.Network.WebService.GetIdForRegister(out id, client.AdditionInfo.IdFromReferal))
                        {
                            await e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                                "Скопируйте данный ID и используйте его при регистрации");
                            client.AdditionInfo.IdFromReferal = id;
                            e.Network.Sql.UpdateFromReferal(client.TelegramId, id);
                            e.Network.Sql.UpdateFromReferalRocket(client.TelegramId, false);

                            client.AdditionInfo.Rocket = false;
                        }
                        else
                        {
                            await e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                                "Свободных ID сейчас нет в наличие. Подождите немного, вам автоматически поступит нужный идентификатор для регистрации");
                          //  if (!TimerUpdate.ListTelegramId.Contains(client.TelegramId))
                           //     TimerUpdate.ListTelegramId.Add(client.TelegramId);
                        }
                    if (!client.AdditionInfo.Rocket)
                        await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, client.AdditionInfo.IdFromReferal.ToString());
                    client.ShowRegMenu(@"Перейдите для регистрации по ссылке
https://office.vilavi.com/register/

Для подтверждения регистрации
- нажмите кнопку в меню:
«подтвердить регистрацию»
ОБЯЗАТЕЛЬНО ПОДТВЕРДИ ВАШ НОМЕР АЙДИ ПОСЛЕ РЕГИСТРАЦИИ - ЗДЕСЬ В ТЕЛЕГРАМ БОТЕ");
                }
                else
                    client.ShowRegMenu();
                var keyboard = new InlineKeyboardMarkup(new[]
                {
                    new InlineKeyboardButton("ПОДТВЕРДИТЬ РЕГИСТРАЦИЮ", "/AcceptReg")
                });
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "------------------------------",
                    replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("Nine Serialize", ex, client);
            }
        }

        public void Deserialize(Client client, StepsEventArgs e)
        {
        }
    }
}
