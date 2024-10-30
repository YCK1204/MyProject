using Server.Game.Object;
using Server.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        Dictionary<int, Player> _players = new Dictionary<int, Player>();
        int _mapId = 0;
        public void Init(int gameLevel, int id, int memberCount, string password)
        {
            ID = id;
            Password = password;
            MemberCount = memberCount;
            _mapId = gameLevel;
            _map.LoadMap(_mapId);
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
            Vector2 cellPos =  _map.PosToCell(posVec2);
            Console.WriteLine($"cellpos x : {cellPos.x}, y : {cellPos.y}");
            Vector2 originalPos = _map.CellToPos(cellPos);
            Console.WriteLine($"original x : {originalPos.x}, y : {originalPos.y}");

        }
    }
}
