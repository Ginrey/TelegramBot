using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TelegramBot.IO.Localizations
{
    public enum Language
    {
        Russian,
        English
    }

    public static class Localization
    {
        static readonly Dictionary<Language, Dictionary<string, string>> Translate = new Dictionary<Language, Dictionary<string, string>>();
        static readonly Dictionary<string, string> ReverseTranslate = new Dictionary<string, string>();

        public static void LoadLocalization()
        {
            FileSystemWatcher watcher = new FileSystemWatcher("Localization");
            watcher.Changed += Watcher_Changed;
            watcher.NotifyFilter = NotifyFilters.LastWrite;
            watcher.Filter = "*.lng";
            watcher.EnableRaisingEvents = true;
            FindLanguages();
            Log.WriteLine("Languages was loaded", ConsoleColor.Yellow);
        }

        private static void Watcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                Translate.Clear();
                ReverseTranslate.Clear();
                FindLanguages();
                Log.WriteLine("Languages was updated", ConsoleColor.Yellow);
            }
        }

        static void FindLanguages()
        {
            foreach (Language lang in Enum.GetValues(typeof(Language)))
            {
                string path = "Localization/" + lang + ".lng";
                if (!File.Exists(path)) continue;
                LoadLanguages(lang, File.ReadAllLines(path));
            }
        }

        static void LoadLanguages(Language lang, string[] lines)
        {
            Dictionary<string, string> translate = new Dictionary<string, string>();
            foreach(var line in lines)
            {
                var array = GetLines(line.Replace("\\n", "\n"));
                if(!string.IsNullOrEmpty(array[0]))
                translate.Add(array[0], array[1]);
            }
            Translate.Add(lang, translate);

            foreach (var line in translate)
            {
              ReverseTranslate[line.Value] = line.Key;
            }
        }

        static string[] GetLines(string line)
        {
            string[] lines = line.Split('=');
            string[] results = new string[2];
            results[0] = lines[0].TrimEnd();
            lines[0] = "";
            results[1] = string.Join("=", lines).Trim().TrimStart('=', ' ');
            return results;
        }
        public static string Get(Language lang, string key)
        {
            if (!Translate.ContainsKey(lang)) return "";
            if (!Translate[lang].ContainsKey(key)) return key;
            return Translate[lang][key];
        }

        public static string GetKey(string key)
        {
            if (!ReverseTranslate.ContainsKey(key)) return key;
            return ReverseTranslate[key];
        }
    }
}
