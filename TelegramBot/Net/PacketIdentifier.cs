using System;
using TelegramBot.Data;


namespace TelegramBot.Net
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class PacketIdentifier : Attribute
    {
        public int Deep { get; set; }
        public TypeSteps TypeSteps { get; set; }

        public PacketIdentifier(TypeSteps type, int deep)
        {
            Update(type, deep);
        }

        public PacketIdentifier Update(TypeSteps step, int deep)
        {
            TypeSteps = step;
            Deep = deep;
            return this;
        }

        public override bool Equals(object obj)
        {
            PacketIdentifier identifier = obj as PacketIdentifier;
            if (identifier == null) return base.Equals(obj);
            var pid = identifier;
            return pid.Deep == Deep && pid.TypeSteps == TypeSteps;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = base.GetHashCode();
                hashCode = (hashCode * 397) ^ Deep;
                hashCode = (hashCode * 397) ^ (int) TypeSteps;
                return hashCode;
            }
        }
    }
}