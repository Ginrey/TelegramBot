namespace TelegramBot.IO
{
    interface IManager<T>
    {
        void Add(T item);
        T Get();
        T Get(long id);
    }
}
