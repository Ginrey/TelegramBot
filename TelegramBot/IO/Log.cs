using System;
using System.IO;
using TelegramBot.Data.Account;

namespace TelegramBot.IO
{
    public static class Log
    {
        public static bool Enabled { get; set; } = true;
        static readonly object Locking = new object();
        static string _log = "";

        public static void Logger(this Client client, string msg, bool writeInConsole = false, ConsoleColor color = ConsoleColor.Gray)
        {
           File.AppendAllText($"Logger/{client.TelegramId}.txt", $"{DateTime.Now} {msg}\r\n");
            if (writeInConsole) WriteLine($"{client.TelegramId} : {msg}", color);
        }
        public static void WriteLine(string msg, ConsoleColor color = ConsoleColor.Gray, bool input = true)
        {
            input = input && Configuration.IsLog;
            Console.ForegroundColor = color;
            string text = $"[{DateTime.Now}] {msg}";
            if (input) AddInfo(text);
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        public static void AddInfo(string msg)
        {
            lock (Locking)
            {
                _log += msg;
                Flush();
            }
        }
        public static void AddError(string msg, Exception ex, Client client = null)
        {
            if (!Enabled) return;
            try
            {
                string stack = ex.StackTrace ?? "";
                lock (Locking)
                {
                    string telegramId = client != null ? $"({client.TelegramId})" : "";
                    _log += ($"{DateTime.Now.ToShortDateString()} {telegramId} {msg}: {ex.Message}\r\n {stack}\r\n");
                    WriteLine(msg, ConsoleColor.Red, false);
                    Flush();
                }
            }
            catch
            {
                // ignored
            }
        }

        static void Flush()
        {
            try
            {
                File.AppendAllText("Log.txt", _log+"\r\n");
                _log = "";
            }
            catch
            {
                // ignored
            }
        }
    }
}