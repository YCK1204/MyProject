using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public class RoomManager
    {
        Dictionary<int, Room> _rooms = new Dictionary<int, Room>();
        object _lock = new object();
        int _id = 0;
        public Room Generate()
        {
            Room room;
            lock (_lock)
            {
                room = new Room();
                room.ID = ++_id;
                _rooms.Add(room.ID, room);
            }
            return room;
        }
        public Room Find(int id)
        {
            Room room = null;
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
    }
}
