using Google.FlatBuffers;
using Server.Game.Object;
using Server.Session;

namespace Server.Game.Room
{
    public class GameRoom
    {
        public int ID { get; set; }
        Map _map = new Map();
        public string Password { get; set; }
        public int MemberCount { get; set; }
        public int CurMemberCount { get; set; } = 0;
        object _lock = new object();
        public int Level { get; set; }
        Dictionary<int, Player> _players = new Dictionary<int, Player>();
        public void Init(int gameLevel, int id, int memberCount, string password)
        {
            ID = id;
            Password = password;
            MemberCount = memberCount;
            Level = gameLevel;
            _map.LoadMap(Level);
        }
        public bool Enter(Player player)
        {
            lock (_lock)
            {
                if (CurMemberCount >= MemberCount)
                    return false;
                if (_players.ContainsKey(player.ID))
                    return false;
                CurMemberCount++;
                _players.Add(player.ID, player);
            }
            return true;
        }
        public void Leave(Player player)
        {
            lock (_lock)
            {
                CurMemberCount--;
                _players.Remove(player.ID);
            }
        }
        public Player FindUser(int id)
        {
            Player player = null;
            lock (_lock)
            {
                if (_players.ContainsKey(id))
                    player = _players[id];
            }
            return player;
        }
        public Player FindUser(Player player)
        {
            return FindUser(player.ID);
        }
        public void Broadcast(byte[] packet)
        {
            lock (_lock)
            {
                foreach (Player player in _players.Values)
                {
                    player.Session.Send(packet);
                }
            }
        }
        public void HandleSpawn(C_Spawn packet, ClientSession session)
        {
            PosInfo posInfo = packet.Pos.Value;
            Vector2 posVec2 = new Vector2(posInfo.X, posInfo.Y);
            Vector2 cellPos = _map.PosToCell(posVec2);
            Console.WriteLine($"cellpos x : {cellPos.x}, y : {cellPos.y}");
            Vector2 originalPos = _map.CellToPos(cellPos);
            Console.WriteLine($"original x : {originalPos.x}, y : {originalPos.y}");

        }

        public Offset<RoomInfo> CreateRoomInfo(FlatBufferBuilder builder)
        {
            lock (_lock)
            {
                var ps = builder.CreateString(Password);
                var data = RoomInfo.CreateRoomInfo(builder, ID, Level, ps, MemberCount, CurMemberCount);
                return data;
            }
        }
    }
}
