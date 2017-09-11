using System;
using Telegram.Bot.Types;

namespace TelegramBot.Data
{
    public class StepsEventArgs: EventArgs
    {
        public StepsEventArgs(Message message,  Session network)
        {
            Message = message;
            Network = network;
        }
        public Session Network { get; set; }
        public Message Message { get; set; }
    }
}
