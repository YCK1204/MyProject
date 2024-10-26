using Google.FlatBuffers;
using Server.Game.Object;
using Server.Game.Room;
using Server.Managers;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Server.Session
{
    public class ClientSession : PacketSession
    {
        public int ID { get; set; }
        public FlatBufferBuilder Builder = new FlatBufferBuilder(1024);
        public Player MyPlayer { get; set; }
        public GameRoom Room
        {
            get
            {
                if (MyPlayer == null)
                    return null;
                return MyPlayer.Room;
            }
            set
            {
                MyPlayer.Room = value;
            }
        }
        // 1. 플레이어 소켓 연결 시 플레이어 생성(데이터만) O
        // 2. 클라의 플레이어가 방 생성 요청 O
        // 3. 방 생성  O
        // 4. 클라의 방 입장 요청 O
        // 5. 방 입장 O
        // 6. 클라의 스폰 요청 <- 없어도 될듯? 개선 필요
        // 7. 스폰 broadcast
        public override void OnConnect(EndPoint endPoint)
        {
            MyPlayer = Manager.Object.Generate<Player>();
            MyPlayer.Session = this;
            Console.WriteLine($"OnConnect");
        }
        public override void OnDisconnect(EndPoint endPoint)
        {
            // room 떠나는것
            // SessionMAnager에서 처리 추가 필요

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
