using Server.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Managers
{
    public class Manager
    {
        MapManager _map = new MapManager();
        SessionManager _session = new SessionManager();
        PacketManager _packet = new PacketManager();
        static Manager _instance = new Manager();
        public static Manager Instance { get { return _instance; } }
        public static MapManager Map { get { return Instance._map; } }
        public static SessionManager Session { get { return Instance._session; } }
        public static PacketManager Packet { get { return Instance._packet; } }
    }
}
