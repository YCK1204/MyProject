using Google.FlatBuffers;
using Server.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Room
{
    public class RoomManager
    {
        Dictionary<UInt64, Room> _rooms = new Dictionary<UInt64, Room>();
        object _lock = new object();
        UInt64 _id = 0;
        public Room Generate(ClientSession session, C_CreateRoom roomInfo)
        {
            Room room = null;
            lock (_lock)
            {
                room.ID = ++_id;
                room.Theme = roomInfo.Theme;
                room.Title = roomInfo.RoomTitle;
                room.Password = roomInfo.Password;

                _rooms.Add(room.ID, room);
            }
            return room;
        }
        public Room Find(UInt64 id)
        {
            Room room = null;
            lock (_lock)
            {
                _rooms.TryGetValue(id, out room);
            }
            return room;
        }
        public bool Remove(UInt64 id) 
        {
            lock (_lock)
            {
                return _rooms.Remove(id);
            }
        }
        public Offset<S_RoomInfo> CreateRoomInfos(FlatBufferBuilder builder)
        {
            List<Offset<RoomInfo>> roomInfos = new List<Offset<RoomInfo>>();
            lock (_lock)
            {
                
                foreach (var room in _rooms.Values) 
                {
                    var roomInfo = room.GetRoomInfo(builder);
                    roomInfos.Add(roomInfo);
                }
            }
            var infos = builder.CreateVectorOfTables(roomInfos.ToArray());
            var data = S_RoomInfo.CreateS_RoomInfo(builder, infos);

            return data;
        }
    }
}
