using System;
using Telegram.Bot;
using TelegramBot.IO;
using TelegramBot.IO.Localizations;
using TelegramBot.IO.Menu;
using TelegramBot.Net;

namespace TelegramBot
{
    public class Session : Network
    {
        public bool IsWork { get; set; }
        public Session()
        {
            Configuration.IsLog = true;
            Configuration = new Configuration();
            PacketRegistry = new PacketsRegistry();
            Localization.LoadLocalization();
            ManagerClient = new IO.Managers.ClientManager();
            ManagerTask = new IO.Managers.TaskManager();
            ManagerWeb = new IO.Managers.WebManager();
            Connection = new Connection(this);
            PrivateOffice.Session = this;
            Session = this;
        }
        public void Inizialize()
        {
            Bot = new TelegramBotClient(Configuration.BotTokenConnection);
            Bot.OnMessage += Connection.OnMessageReceived;
            Bot.OnCallbackQuery += Connection.OnInlineQueryReceived;
            ManagerSql = new IO.Managers.SqlManager(Configuration.SqlConnectionString, 100);
            ManagerSql.RunDatabase();
            WebService.Auth();
            Log.WriteLine("Sql was initialization", ConsoleColor.Yellow);
        }
        public override void Connect()
        {
            if (IsWork) return;
            Bot.StartReceiving();
            Console.Title = Bot.GetMeAsync().Result.Username;
            IsWork = true;
            Log.WriteLine("Session was started", ConsoleColor.Yellow);
        }
        public override void Disconnect()
        {
            Bot.StopReceiving();
            IsWork = false;
        }
    }
}
