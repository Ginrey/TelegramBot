using System;


namespace TelegramBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Session session = new Session();
            session.Configuration.SetBotTokenConnection(TypeOfStart.Debug, "310136073:AAHEP0i318aIkB8y3lAOTtwYxMf7jwtp51w");
            session.Configuration.SetBotTokenConnection(TypeOfStart.Release, "355305918:AAHxGZ1zXVzz38zIpC5GOcs5RZrTFUflWz4");
            session.Configuration.SetSqlConnectionString(TypeOfStart.Debug, "SERVER=DESKTOP-VBFBI8T;DATABASE=Tayga;Trusted_Connection=True");
            session.Configuration.SetSqlConnectionString(TypeOfStart.Release, "SERVER=WIN-344VU98D3RU;DATABASE=Tayga;Trusted_Connection=True");
            session.ManagerWeb.Add(new Web.WebService("845797", "bot1T8"));
            session.Inizialize();
            session.Connect();
            Console.ReadLine();
        }
    }
}
