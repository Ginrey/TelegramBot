using System.Collections.Generic;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.IO.Localizations;

namespace TelegramBot.IO.Menu
{
  public static class TelegramCode
    {
        public static List<KeyboardButton[]> CreateKeyboardButtons(Language language, params string[] list)
        {
            List<KeyboardButton[]> listMenu = new List<KeyboardButton[]>();
            foreach (var item in list)
                listMenu.Add(new[] {new KeyboardButton(Localization.Get(language, item))});
            return listMenu;
        }

        public static InlineKeyboardMarkup GetNextInlineKeyboard(Language language, bool withHelp = false)
        {
            List<InlineKeyboardButton> listMenu = new List<InlineKeyboardButton>();
            listMenu.Add(new InlineKeyboardButton(Localization.Get(language, "ofs_next"), "/Next"));
            if(withHelp)
                listMenu.Add(new InlineKeyboardButton(Localization.Get(language, "ofs_can_question"), "/Help"));
            return new InlineKeyboardMarkup(listMenu.ToArray());
        }
    }
}
