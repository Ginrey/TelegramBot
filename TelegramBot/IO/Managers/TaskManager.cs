using System;
using System.Threading.Tasks;

namespace TelegramBot.IO.Managers
{
   public class TaskManager : IManager<Action>
    {
        public void Add(Action item)
        {
            Task.Factory.StartNew(item);
        }

        public Action Get()
        {
            return null;
        }

        public Action Get(long id)
        {
            return null;
        }
    }
}
