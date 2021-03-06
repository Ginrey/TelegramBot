﻿using System;
using TelegramBot.Data;
using TelegramBot.Data.Account;
using TelegramBot.IO;
using TelegramBot.IO.Menu;

namespace TelegramBot.Net.Packets
{
    [PacketIdentifier(TypeSteps.Two, 0)]
   internal class OnTwo : IPacket
    {
        public async void Serialize(Client client, StepsEventArgs e)
        {
            try
            {
                client.HideMenu("Спасибо, приятного прослушивания");
                await e.Network.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADWgADO4AwShaD7itlf5KUAg", 636, "100lbov","Listen");
                client.Access.AddTime(Access.AccessType.Step, 636);
                var keyboard = TelegramCode.GetNextInlineKeyboard(client.Language);
                await e.Network.Bot?.SendTextMessageAsync(client.TelegramId, "«Заблуждения блогеров»",replyMarkup: keyboard);
            }
            catch (Exception ex)
            {
                Log.AddError("Two Serialize", ex, client);
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
                        client.Step.Update(TypeSteps.Three, 0);
                        e.Network.Connection.OnSerialize(client);
                    }
                    else
                        e.Network.Bot?.SendTextMessageAsync(client.TelegramId,
                            "Следующая аудиоглава будет вам отправлена исключительно после того, как вы ознакомитесь с предыдущей.");
                }
            }
            catch (Exception ex)
            {
                Log.AddError("Two Deserialize", ex, client);
            }
        }
    }
}
