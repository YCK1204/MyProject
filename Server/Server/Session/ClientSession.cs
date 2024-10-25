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
        public override void OnConnect(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnect");
        }

        public override void OnDisconnect(EndPoint endPoint)
        {
            Console.WriteLine($"OnDisconnect");
        }

        public override void OnRecvPacket(ArraySegment<byte> data)
        {
            Console.WriteLine($"OnRecvPacket");
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"OnSend");
        }
    }
}
