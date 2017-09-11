using Telegram.Bot;
using TelegramBot.Data.SQL;
using TelegramBot.IO.Localizations;
using TelegramBot.IO.Managers;
using TelegramBot.Web;


namespace TelegramBot.Net
{
    public abstract class Network 
    {
        public Session Session { get; set; }
        public TelegramBotClient Bot { get; set; }
        public Configuration Configuration { get; set; }
        public Connection Connection { get; set; }
        public PacketsRegistry PacketRegistry { get; set; }
        public SqlManager ManagerSql { get; set; }
        public MySqlDatabase Sql => ManagerSql?.Get();
        public WebService WebService => ManagerWeb?.Get();
        public ClientManager ManagerClient { get; set; }
        public TaskManager ManagerTask { get; set; }
        public WebManager ManagerWeb { get; set; }
        public virtual void Connect(){}
        public virtual void Disconnect(){} 

    }
}
