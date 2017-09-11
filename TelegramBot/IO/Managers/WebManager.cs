using System;
using System.Collections.Generic;
using TelegramBot.Web;


namespace TelegramBot.IO.Managers
{
    public class WebManager: IManager<WebService>
    {
        Queue<WebService> _query = new Queue<WebService>();
        object locked = new object();
        public void Add(WebService web)
        {
            lock (locked)
            {
                _query.Enqueue(web);
            }
        }

        public WebService Get()
        {
            lock(locked)
            {
                WebService value = _query.Dequeue();
                _query.Enqueue(value);
                return value;
            }
        }
        [Obsolete("Не используется в этом классе. Использовать Get()")]
        public WebService Get(long id)
        {
            return null;
        }
    }
}
