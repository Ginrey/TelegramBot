namespace TelegramBot
{
    public enum TypeOfStart
    {
        Debug,
        Release
    }
    public class Configuration
    {
        string _sqlReleaseConnection, _sqlDebugConnecion;
        string _botReleaseToken, _botDebugToken;
        public  static bool IsLog { get; set; }
        public bool IsDebug
        {
            get
            {
#if DEBUG
                return true;
#endif
                return false;
            }
        }
        public string SqlConnectionString => IsDebug ? _sqlDebugConnecion : _sqlReleaseConnection;
        public string BotTokenConnection => IsDebug ? _botDebugToken : _botReleaseToken;
        public void SetSqlConnectionString(TypeOfStart type, string sqlConnectionString)
        {
            switch(type)
            {
                case TypeOfStart.Debug: _sqlDebugConnecion = sqlConnectionString; break;
                case TypeOfStart.Release: _sqlReleaseConnection = sqlConnectionString; break;
            }
        }

        public void SetBotTokenConnection(TypeOfStart type, string botTokenConnection)
        {
            switch (type)
            {
                case TypeOfStart.Debug:
                    _botDebugToken = botTokenConnection;
                    break;
                case TypeOfStart.Release:
                    _botReleaseToken = botTokenConnection;
                    break;
            }
        }
    }
}
