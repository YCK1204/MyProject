using Server.Game.Room;
using Server.Session;
using ObjectManager = Server.Game.Object.ObjectManager;

namespace Server.Managers
{
    public class Manager
    {
        MapManager _map = new MapManager();
        SessionManager _session = new SessionManager();
        PacketManager _packet = new PacketManager();
        GameRoomManager _room = new GameRoomManager();
        ObjectManager _object = new ObjectManager();
        static Manager _instance = new Manager();
        public static Manager Instance { get { return _instance; } }
        public static MapManager Map { get { return Instance._map; } }
        public static SessionManager Session { get { return Instance._session; } }
        public static PacketManager Packet { get { return Instance._packet; } }
        public static GameRoomManager Room { get { return Instance._room; } }
        public static ObjectManager Object { get { return Instance._object; } }
    }
}
