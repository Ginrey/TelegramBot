using System;
using System.Collections.Generic;
using System.Reflection;
using TelegramBot.Data;
using TelegramBot.IO;

namespace TelegramBot.Net
{
    public class PacketsRegistry
    {
        static Assembly[] currentAssembly = {Assembly.GetExecutingAssembly()};

        Dictionary<PacketIdentifier, Type> packets = new Dictionary<PacketIdentifier, Type>();

        public PacketsRegistry()
        {
            Register(currentAssembly, true);
            Log.WriteLine("Packets was loaded", ConsoleColor.Yellow);
        }

        public void Register(Assembly[] assemblies, bool includeGlobal)
        {
            var types = new List<Type>();
            foreach (Assembly assembly in assemblies)
            foreach (var type in assembly.GetTypes())
            {
                if (typeof(IPacket).IsAssignableFrom(type))
                        types.Add(type);
            }
            if (includeGlobal) Register(types);
        }

        public void Register(IEnumerable<Type> types)
        {
            foreach (var type in types)
            {
                Register(type);
            }
        }

        public void Register(Type type)
        {
            var packetIds = (PacketIdentifier[]) type.GetCustomAttributes(typeof(PacketIdentifier), false);
            foreach (var packetId in packetIds)
            {
                Register(type, packetId);
            }
        }

        public void Register(Type type, PacketIdentifier packetId)
        {
            packets[packetId] = type;
        }

        internal IPacket GetPacket(TypeSteps type, int deep)
        {
            return GetPacket(new PacketIdentifier(type, deep));
        }

        internal IPacket GetPacket(PacketIdentifier packetId)
        {
            return GetPacketType(packetId).GetConstructor(Type.EmptyTypes).Invoke(null) as IPacket;
        }

        public bool TryGetPacket(TypeSteps type, int deep, out IPacket packet)
        {
            return TryGetPacket(new PacketIdentifier(type, deep), out packet);
        }

        public bool TryGetPacket(PacketIdentifier packetId, out IPacket packet)
        {
            Type type;
            if (TryGetPacketType(packetId, out type))
            {
                packet = type.GetConstructor(Type.EmptyTypes).Invoke(null) as IPacket;
                return true;
            }
            packet = null;
            return false;
        }

        public Type GetPacketType(PacketIdentifier packetId)
        {
            Type type;
            if (!TryGetPacketType(packetId, out type))
            {
                throw new Exception("Unknown packet");
            }
            return type;
        }

        public bool TryGetPacketType(PacketIdentifier packetId, out Type type)
        {
            return packets.TryGetValue(packetId, out type);
        }
    }
}