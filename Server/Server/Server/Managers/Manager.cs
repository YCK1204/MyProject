using Server.Room;
using Server.Session;
using Server.Utils;

namespace Server.Managers
{
    public class Manager
    {
        SessionManager _session = new SessionManager();
        PacketManager _packet = new PacketManager();
        PoolManager _poolManager = new PoolManager();
        RoomManager _room = new RoomManager();
        static Manager _instance = new Manager();
        public static Manager Instance { get { return _instance; } }
        public static SessionManager Session { get { return Instance._session; } }
        public static PacketManager Packet { get { return Instance._packet; } }
        public static PoolManager Pool { get { return Instance._poolManager; } }
        public static RoomManager Room { get { return Instance._room; } }
    }
}
