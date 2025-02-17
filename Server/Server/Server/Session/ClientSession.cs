using Google.FlatBuffers;
using Server.Managers;
using ServerCore;
using System.Net;

namespace Server.Session
{
    public class UserInfo // 스팀 계정 정보
    {
        public string Nickname { get; set; }
        public UInt64 ID { get; set; }

        public Offset<UserAccountInfo> GetUserAccountInfo(FlatBufferBuilder builder)
        {
            var nicknameOffset = builder.CreateString(Nickname);

            var data = UserAccountInfo.CreateUserAccountInfo(builder, nicknameOffset, ID);
            return data;
        }
    }
    public class ClientSession : PacketSession
    {
        public Server.Session.UserInfo UserInfo { get; set; } = null;
        public override void OnConnect(EndPoint endPoint)
        {
            Manager.Session.Push(this);
            Console.WriteLine($"OnConnect");
        }
        public override void OnDisconnect(EndPoint endPoint)
        {
            Manager.Session.Remove(UserInfo.ID);
            Manager.Pool.Push<ClientSession>(this);
            Console.WriteLine($"OnDisconnect");
        }
        public override void OnRecvPacket(ArraySegment<byte> data)
        {
            Manager.Packet.OnRecvPacket(this, data);
        }
        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"OnSend");
        }
    }
}
