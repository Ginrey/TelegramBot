using System.Collections.Generic;
using TelegramBot.Data.Account;

namespace TelegramBot.IO.Managers
{
    public class ClientManager : IManager<Client>
    {
        Dictionary<long, Client> _clients = new Dictionary<long, Client>();
        object locked = new object();
        public void Add(Client client)
        {
            lock (locked)
            {
                if (!_clients.ContainsKey(client.TelegramId)) _clients.Add(client.TelegramId, client);
            }
        }

        public Client Get()
        {
            return null;
        }

        public Client Get(long telegramId)
        {
            lock (locked)
            {
                if (_clients.TryGetValue(telegramId, out Client value))
                {
                    value.IsNew = false;
                    return value;
                }
                value = new Client {TelegramId = telegramId, IsNew = true};
                Add(value);
                return value;
            }
        }
    }
}
