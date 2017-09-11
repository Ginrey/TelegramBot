using TelegramBot.IO.Localizations;
using TelegramBot.Net;

namespace TelegramBot.Data.Account
{
    public class Client : Debug
    {
        public long TelegramId { get; set; }
        public Access Access { get; } = new Access();
        public AccountGroup Information { get; set; }
        public AdditionInfo AdditionInfo { get; } = new AdditionInfo();
        public PacketIdentifier Step { get; set; } = new PacketIdentifier(TypeSteps.One, 0);
        public Language Language { get; set; }
        
        public void UpdateInfo(string fromreferal= "", string username = "")
        {
            if (!string.IsNullOrEmpty(fromreferal))
            {
                AdditionInfo.Rocket = fromreferal.Contains("R");
                AdditionInfo.BigStep = fromreferal.Contains("RR");
                long additionInfoFromReferal;
                if (long.TryParse(fromreferal.TrimEnd('R'), out additionInfoFromReferal))
                    AdditionInfo.IdFromReferal = additionInfoFromReferal;
            }
            AdditionInfo.Username = username;
        }
    }
}
