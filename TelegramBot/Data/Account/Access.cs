using System;

namespace TelegramBot.Data.Account
{
   public class Access
    {
        public enum AccessType
        {
            Message,
            Step
        }
        DateTime LastMessageTime { get; set; }
        DateTime LastStepTime { get; set; }

        public Access()
        {
            LastMessageTime = LastStepTime = DateTime.Now;
        }
        public void AddTime(AccessType accessType, int sec)
        {
            switch (accessType)
            {
                case AccessType.Message:
                    LastMessageTime = DateTime.Now.AddSeconds(sec);
                    break;
                case AccessType.Step:
                    LastStepTime = DateTime.Now.AddSeconds(sec);
                    break;
            }
        }
        public bool CheckAccess(AccessType accessType)
        {
            switch(accessType)
            {
                case AccessType.Message:
                    if (LastMessageTime.Subtract(DateTime.Now) > TimeSpan.Zero) return false;
                    break;
                case AccessType.Step:
                    if (LastStepTime.Subtract(DateTime.Now) > TimeSpan.Zero) return false;
                    break;
            }
            return true;
        }
    }
}
