using Google.FlatBuffers;
using Server.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.Room
{
    public class GameRoomManager
    {
        Dictionary<int, GameRoom> _rooms = new Dictionary<int, GameRoom>();
        object _lock = new object();
        public GameRoom Generate(int id)
        {
            lock (_lock)
            {
                if (_rooms.ContainsKey(id) == false)
                {
                    GameRoom room = new GameRoom();
                    room.ID = id;
                    _rooms.Add(id, room);
                    return room;
                }
            }
            return null;
        }
        public GameRoom Find(int id)
        {
            GameRoom room = null;
            lock (_lock)
            {
                if (_rooms.ContainsKey(id))
                    room = _rooms[id];
            }
            return room;
        }
        public bool Remove(int id)
        {
            lock (_lock)
            {
                return _rooms.Remove(id);
            }
        }
        public VectorOffset GetRoomList(FlatBufferBuilder builder, List<Offset<RoomInfo>> list)
        {
            lock (_lock)
            {
                foreach (GameRoom room in _rooms.Values)
                {
                    list.Add(room.CreateRoomInfo(builder));
                }
                var vector = S_RoomList.CreateRoomsVector(builder, list.ToArray());
                return vector;
            }
        }
    }
}
