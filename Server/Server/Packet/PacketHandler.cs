using Google.FlatBuffers;
using Server.Managers;
using Server.Session;
using ServerCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class PacketHandler
{
    public static void S_TestHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;
        S_Test s_Test = S_Test.GetRootAsS_Test(buffer);
        Console.WriteLine(s_Test.Test);
        FlatBufferBuilder builder = new FlatBufferBuilder(20);
        var str = builder.CreateString(s_Test.Test);
        var pkt = C_Test.CreateC_Test(builder, str);
        builder.Finish(pkt.Value);
        var data = builder.SizedByteArray();
        var d = Manager.Packet.CreatePacket(data, PacketType.C_Test);
        clientSession.Send(d);
    }
}
