namespace TelegramBot.Data.Account
{
    public class AccountGroup
    {
        public int Generation { get; set; }
        public Representative Representative { get; set; } = new Representative();
        public string Rank { get; set; }
        public string Location { get; set; }
        public int Pay { get; set; }
        public string Activity { get; set; }
        public string DateOfEntry { get; set; }
    }

    public class Representative
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Surename { get; set; }
        public string Patronymic { get; set; }
        public string Mobile { get; set; }

        public string FullName => $"{Surename} {Name} {Patronymic}";

        public Representative(long id = -1, string name ="", string surename = "", string patronimyc = "", string mobile = "")
        {
            Id = id;
            Name = name;
            Surename = surename;
            Patronymic = patronimyc;
            Mobile = mobile;
        }
    }
}