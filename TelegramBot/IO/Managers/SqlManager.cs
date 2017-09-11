using System;
using System.Collections.Generic;
using TelegramBot.Data.SQL;

namespace TelegramBot.IO.Managers
{
   public class SqlManager: IManager<MySqlDatabase>
    {
        Queue<MySqlDatabase> _query = new Queue<MySqlDatabase>();
        object locked = new object();
        public SqlManager(string sqlConnection ,int count)
        {
            for (int i = 0; i < count; i++)
             Add(new MySqlDatabase(sqlConnection));
        }
        public void Add(MySqlDatabase item)
        {
            lock (locked)
            {
                _query.Enqueue(item);
            }
        }

        public MySqlDatabase Get()
        {
            lock (locked)
            {
                MySqlDatabase value = _query.Dequeue();
                _query.Enqueue(value);
                return value;
            }
        }

        [Obsolete("Не используется в этом классе. Использовать Get()")]
        public MySqlDatabase Get(long id)
        {
            return null;
        }
        public void RunDatabase()
        {
            lock (locked)
            {
                foreach (var db in _query) db.Connect();
            }
        }
    }
}
