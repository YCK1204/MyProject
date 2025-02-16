using Google.FlatBuffers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Room
{
    public class Room
    {
        public UInt64 ID { get; set; }
        public string Title { get; set; }
        public byte Theme { get; set; }
        public string Password { get; set; }
        public byte CurMemberCount { get; set; } = 1;
        public Server.Session.UserInfo Owner { get; set; }
        public Offset<RoomInfo> GetRoomInfo(FlatBufferBuilder builder)
        {
            var title = builder.CreateString(Title);
            var password = builder.CreateString(Password);

            // 나중에 owner가 null일 때 생각해봐야함
            var ownerInfo = Owner.GetUserAccountInfo(builder);
            var roomInfo = RoomInfo.CreateRoomInfo(builder, title, ID, CurMemberCount, Theme, password, ownerInfo);

            return roomInfo;
        }
    }
}
