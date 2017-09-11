using TelegramBot.Data;
using TelegramBot.Data.Account;


namespace TelegramBot.Net
{
    public interface IPacket
    {
        void Serialize(Client client, StepsEventArgs e);
        void Deserialize(Client client, StepsEventArgs e);
    }
}
