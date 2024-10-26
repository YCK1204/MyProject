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
        public int MapId { get; set; }
        Map map;
        public string Password { get; set; }
        public int MemberCount { get; set; }
        public int CurMemberCount { get; set; } = 0;
        object _lock = new object();
        Dictionary<int, Player> _players = new Dictionary<int, Player>();
        public bool Enter(Player player)
        {
            lock (_lock)
            {
                if (CurMemberCount >= MemberCount)
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
    }
}
