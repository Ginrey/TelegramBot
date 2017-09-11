using System.Collections.Generic;

namespace TelegramBot.Data.Account
{
    public class AdditionInfo
    {
        public bool Rocket { get; set; }
        public bool BigStep { get; set; }
        public long IdFromReferal { get; set; }
        public string Mobile { get; set; }
        public string Username { get; set; }
        public List<string> QuestionList { get; set; } = new List<string>();
    }
}
