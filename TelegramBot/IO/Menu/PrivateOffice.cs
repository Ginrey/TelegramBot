using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot;
using TelegramBot.Data.Account;
using TelegramBot.IO.Localizations;
using TelegramBot.IO.Menu;


namespace TelegramBot.IO.Menu
{
   public static class PrivateOffice
    {
        public static Session Session { get; set; }

        public static void ShowMainMenu(this Client client, string text = Config.MenuList.CompleteEnter)
        {
            text = Localization.Get(client.Language, text);
            List<KeyboardButton[]> listMenu = TelegramCode.CreateKeyboardButtons(client.Language,
                Config.MenuList.Bentleysmmbot,
                Config.MenuList.Url,
                Config.MenuList.Reviews,
                Config.MenuList.Teach,
                Config.MenuList.Territory,
                Config.MenuList.Clubs,
                Config.MenuList.Files,
                Config.MenuList.Questions);

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            Session.Bot?.SendTextMessageAsync(client.TelegramId, string.Format(text, client.Information.Representative.Name),
                replyMarkup: keyboard);
        }

        public static async void HideMenu(this Client client, string text = "Hide")
        {
            text = Localization.Get(client.Language, text);
            ReplyKeyboardRemove keyboard = new ReplyKeyboardRemove();
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text,
                replyMarkup: keyboard);
        }

        public static async void ShowRegMenu(this Client client, string text = "Меню")
        {
            ReplyKeyboardMarkup keyboard =  new ReplyKeyboardMarkup(TelegramCode.CreateKeyboardButtons(client.Language,
                        Config.MenuList.AcceptReg,
                        Config.MenuList.Questions).ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text, replyMarkup: keyboard);
        }

        public static void DontShow(this Client client)
        {
            Session.Bot?.SendTextMessageAsync(client.TelegramId, "Раздел наполняется. Время завершения 15.04.2017");
        }

        public static async void SendAllFiles(this Client client)
        {
            await Session.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADFAADzsVBSg_tFoIB7sd3Ag", 194, "100lbov","Listen");
            await Session.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADWgADO4AwShaD7itlf5KUAg", 636, "100lbov","Listen");
            await Session.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADWwADO4AwSjHVA3ZjxqCKAg", 398, "100lbov","Listen");
            await Session.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADXAADO4AwSr0KwrRyoMUXAg", 212, "100lbov","Listen");
            await Session.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADYAADO4AwSgNTvHFOFamDAg", 268, "100lbov","Listen");
            await Session.Bot?.SendVideoAsync(client.TelegramId, "BAADAgADBQADoV44Soq8vr55rQOxAg");
            await Session.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADYgADO4AwShBeWIs2PLQyAg", 506, "100lbov","Listen");
            await Session.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADGAADzsVBSvT5vtMBgkgBAg", 152, "100lbov","Listen");
            await Session.Bot?.SendVideoAsync(client.TelegramId, "BAADAgADEgADoV44SnBgwPiPTQqhAg");
            await Session.Bot?.SendVideoAsync(client.TelegramId, "BAADAgADBAADoV44SiBwGRerpmvhAg");
            await Session.Bot?.SendAudioAsync(client.TelegramId, "CQADAgADZAADO4AwStenXfVbjerIAg", 822, "100lbov","Listen");
        }

        public static void ShowMyUrl(this Client client)
        {
            Session.Bot?.SendTextMessageAsync(client.TelegramId,
                "t.me/Bentleysmmbot?start=" + client.Information.Representative.Id);
        }

        public static async void ShowReviews(this Client client, string text = "Обзоры")
        {
            List<KeyboardButton[]> listMenu = TelegramCode.CreateKeyboardButtons(client.Language,
                Config.MenuList.AboutVilavi,
                Config.MenuList.AboutT8,
                Config.MenuList.AboutStruct,
                Config.MenuList.AboutPrivateOffice,
                Config.MenuList.BackToMenu);
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text, replyMarkup: keyboard);
        }

        public static async void ShowAboutVilavi(this Client client, string text = "Обзоры")
        {
            List<KeyboardButton[]> listMenu = TelegramCode.CreateKeyboardButtons(client.Language,
                Config.MenuList.AboutHistory,
                Config.MenuList.Contacts,
                Config.MenuList.Reviews,
                Config.MenuList.BackToMenu);
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text, replyMarkup: keyboard);
        }

        public static async void ShowAboutT8(this Client client, string text = "Обзоры")
        {
            List<KeyboardButton[]> listMenu = TelegramCode.CreateKeyboardButtons(client.Language,
                Config.MenuList.AboutProduct,
                Config.MenuList.T8Internet,
                Config.MenuList.Reviews,
                Config.MenuList.BackToMenu);
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text, replyMarkup: keyboard);
        }

        public static async void ShowTeaches(this Client client, string text = "Обучения")
        {
            List<KeyboardButton[]> listMenu = TelegramCode.CreateKeyboardButtons(client.Language,
                Config.MenuList.TeachFirstStep,
                Config.MenuList.TeachMoneyPlan,
                Config.MenuList.TeachAutobonus,
                Config.MenuList.TeachLeaders,
                Config.MenuList.TeachTools,
                Config.MenuList.BackToMenu);
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text, replyMarkup: keyboard);
        }
        /*
        public static async void ShowTeachesTools(this Client client, string text = "Обучения")
        {
            List<KeyboardButton[]> listMenu = new List<KeyboardButton[]>
            {
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ForPartners))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ForTeam))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ForRegion))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))}
            };
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text, replyMarkup: keyboard);
        }

        public static async void ShowListTerritory(this Client client, string text = "Территории")
        {
            List<KeyboardButton[]> listMenu = new List<KeyboardButton[]>
            {
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityBelarus))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKazakhstan))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKyrgyzstan))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityRussia))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityUkraine))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))}
            };
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text, replyMarkup: keyboard);
        }

        public static async void ShowTerritory(this Client client, string text = "Территории")
        {
            List<KeyboardButton[]> listMenu = new List<KeyboardButton[]>();

            if (text == Config.MenuList.CityBelarus)
            {
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityBelarusChanal))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityBelarusCity))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityBelarusChat))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityBelarusLeaders))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.Territory))});
                listMenu.Add(
                    new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))});
            }

            if (text == Config.MenuList.CityKazakhstan)
            {
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKazakhstanChanal))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKazakhstanCity))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKazakhstanChat))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKazakhstanLeaders))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.Territory))});
                listMenu.Add(
                    new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))});
            }
            if (text == Config.MenuList.CityKyrgyzstan)
            {
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKyrgyzstanChanal))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKyrgyzstanCity))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKyrgyzstanChat))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityKyrgyzstanLeaders))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.Territory))});
                listMenu.Add(
                    new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))});
            }
            if (text == Config.MenuList.CityRussia)
            {
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityRussiaChanal))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityRussiaCity))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityRussiaChat))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityRussiaLeaders))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.Territory))});
                listMenu.Add(
                    new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))});
            }
            if (text == Config.MenuList.CityUkraine)
            {
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityUkraineChanal))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityUkraineCity))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityUkraineChat))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.CityUkraineLeaders))});
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.Territory))});
                listMenu.Add(
                    new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))});
            }

            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, Session.Language.Get(client.Language, text),
                replyMarkup: keyboard);
        }

        public static async void ShowClub(this Client client, Clubs club)
        {
            List<KeyboardButton[]> listMenu = new List<KeyboardButton[]>
            {
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubsPlans))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubsEvents))}
            };
            if (club >= Clubs.Gold)
                listMenu.Add(
                    new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubsPromo))});
            if (club >= Clubs.Crystal)
                listMenu.Add(
                    new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubsParty))});
            if (club >= Clubs.Diamond)
                listMenu.Add(new[]
                    {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubsClosedParty))});
            listMenu.Add(new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))});
            client.Temp = club;
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, Enum.GetName(typeof(Clubs), club),
                replyMarkup: keyboard);
        }

        public static async void ShowListClubs(this Client client, string text = "Клубы")
        {
            List<KeyboardButton[]> listMenu = new List<KeyboardButton[]>
            {
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubPremier))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubGold))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubCrystal))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.ClubDiamond))},
                new[] {new KeyboardButton(Session.Language.Get(client.Language, Config.MenuList.BackToMenu))}
            };
            ReplyKeyboardMarkup keyboard = new ReplyKeyboardMarkup(listMenu.ToArray());
            await Session.Bot?.SendTextMessageAsync(client.TelegramId, text, replyMarkup: keyboard);
        }*/

        public static void HelpPlease(this Client client)
        {
            if (client.AdditionInfo.IdFromReferal != 0)
            {
                long tid;
                Session.Sql.GetTelegramId(client.AdditionInfo.IdFromReferal, out tid);
                if (tid != 0)
                {
                    Session.Bot?.SendTextMessageAsync(tid,
                        $"Пользователь просит вашей помощи! Его номер: {client.AdditionInfo.Mobile}");
                }
            }
            Session.Bot?.SendTextMessageAsync(client.TelegramId,
                "Запрос о помощи был успешно отправлен. В ближайшее время с вами свяжется представитель компании и ответит на все ваши вопросы.");
        }

    }
}
