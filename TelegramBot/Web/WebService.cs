using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using HtmlAgilityPack;
using TelegramBot.Data.Account;
using TelegramBot.IO;


namespace TelegramBot.Web
{
    public class WebService
    {
        public WebService(string login = "", string password = "", string proxy = "",
            string passwordProxy = "")
        {
            Login = login;
            Password = password;
            if (!string.IsNullOrEmpty(proxy))
            {
                Proxy = new WebProxy(proxy)
                {
                    Credentials = new NetworkCredential(passwordProxy.Split(':')[0], passwordProxy.Split(':')[1])
                };
            }
            WebClient = new MyWebClient(proxy: Proxy);
        }

        public string Login { get; set; }
        public string Password { get; set; }
        public WebProxy Proxy { get; set; }
        public bool IsDone { get; set; }
        MyWebClient WebClient { get; }


        public void Auth()
        {
            try
            {
                if (IsDone) return;
                WebClient.ResetHeadersGet();
                WebClient.UploadString("https://office.vilavi.com/");
                WebClient.ResetHeadersPost();

                Dictionary<string, string> code = new Dictionary<string, string>
                {
                    {"postOn", "1"},
                    {"userLogin", Login},
                    {"userPassword", Password}
                };
                WebClient.ResetHeadersPost();
                string data = WebClient.UploadString("https://office.vilavi.com/", code);
                IsDone = data.Contains("\"submitOn\":true");
                Log.WriteLine(IsDone ? "Web service: Login complete" : "Web service: Login error",
                    IsDone ? ConsoleColor.Yellow : ConsoleColor.Red);
            }
            catch (Exception ex)
            {
                Log.AddError("Auth error", ex);
            }
        }

        public string GetProfile()
        {
            return WebClient.UploadString("https://office.vilavi.com/profile/");
        }

        public bool GetIdForRegister(out long id, long binId = -1)
        {
            if (binId < 0)
            return long.TryParse(WebClient.UploadString("http://voitivit.ru/get_ids_for_traf.php"), out id);
            return long.TryParse(WebClient.UploadString("http://voitivit.ru/get_ids_for_traf.php?id=" + binId), out id);
        }

        public bool InsertIdToWeb(long id, long fromId)
        {
            string data = "";
            try
            {
                Dictionary<string, string> code = new Dictionary<string, string>
                {
                    {"id", id.ToString()},
                    {"id_pod", fromId.ToString()}
                };
                data = WebClient.UploadStringGET("http://voitivit.ru/insert_new.php", WebClient.GetArgs(code));

                return Convert.ToBoolean(data);
            }
            catch (Exception ex)
            {
                Log.AddError($"InsertIdToWeb ({data})", ex);
                return false;
            }
        }

        public List<AccountGroup> GetTable(
            string url = "https://office.vilavi.com/group?fltBcNum=-1&fltBcDir=all&fltLevels=0;all;1;2;3;4;5;6;7&sortField=&sortMode=",
            long id = -1, int count = 100)
        {
            List<AccountGroup> list = new List<AccountGroup>();
            string html = WebClient.UploadString(url);
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var nodes = doc.DocumentNode.SelectNodes("//table[@class=\"data_table\"]");

            var headers = nodes[0]
                .Elements("tr")
                .ToList()
                .Select(item => item.InnerHtml)
                .ToList();

            headers.RemoveRange(0, 2);
            headers.RemoveAt(headers.Count - 1);
            int index = 0;
            foreach (var header in headers)
            {
                index++;
                doc.LoadHtml(header);
                string[] info = doc.DocumentNode.SelectNodes("//td").Select(td => td.InnerText.Trim()).ToArray();
                string[] repres = info[1].Split(new[] {" "}, StringSplitOptions.RemoveEmptyEntries);
                list.Add(new AccountGroup
                {
                    Generation = int.Parse(info[0]),
                    Representative = new Representative
                    {
                        Id = int.Parse(repres[0]),
                        Name = repres[2],
                        Surename = repres[1],
                        Patronymic = repres.Length > 5 ? repres[3] : "",
                        Mobile = repres[repres.Length - 1]
                    },
                    Rank = info[2],
                    Location = info[3],
                    Pay = int.Parse(info[4]),
                    Activity = info[5],
                    DateOfEntry = info[6]
                });
                if (list.Count > count || list.Last().Representative.Id == id)
                    return list;
            }
            return list;
        }
    }
}

