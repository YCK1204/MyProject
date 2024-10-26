using Google.FlatBuffers;
using Server.Game.Room;
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
    public static void C_SpawnHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;
    }

    public static void C_LeaveRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;
    }
    public static void C_EnterRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;
        C_EnterRoom packet = C_EnterRoom.GetRootAsC_EnterRoom(buffer);

        FlatBufferBuilder builder = clientSession.Builder;
        EnterRoomError error = EnterRoomError.SUCCESS;

        try
        {
            int roomId = packet.RoomId;
            string password = packet.Password;

            GameRoom room = Manager.Room.Find(roomId);
            if (room == null)
                error = EnterRoomError.NOT_FOUND;
            else if (room.Enter(clientSession.MyPlayer) == false)
                error = EnterRoomError.FULL;
            Console.WriteLine($"C_EnterRoomHandler roomId : {roomId}, passowrd : {password}");
        }
        catch (Exception e)
        {
            error = EnterRoomError.UNKNOWN;
        }
        var data = S_EnterRoom.CreateS_EnterRoom(builder, error);
        builder.Finish(data.Value);
        var bytes = builder.SizedByteArray();
        var pkt = Manager.Packet.CreatePacket(bytes, PacketType.S_EnterRoom);
        clientSession.Send(pkt);
    }
    public static void C_CreateRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;
        C_CreateRoom packet = C_CreateRoom.GetRootAsC_CreateRoom(buffer);

        FlatBufferBuilder builder = clientSession.Builder;
        CreateRoomError error = CreateRoomError.SUCCESS;

        try
        {
            int roomId = packet.RoomId;
            int memberCount = packet.MemberCount;
            string password = packet.Passowrd;

            Console.WriteLine($"C_CreateRoomHandler roomId : {roomId}, memberCount : {memberCount}, passoword : {password}");
            GameRoom room = Manager.Room.Generate(roomId);
            if (room != null)
                error = CreateRoomError.OVERLAPPED;
            else if (roomId < 0 || roomId > 1000)
                error = CreateRoomError.INVALID_ID;
        }
        catch (Exception e)
        {
            error = CreateRoomError.UNKNOWN;
        }
        var data = S_CreateRoom.CreateS_CreateRoom(builder, error);
        builder.Finish(data.Value);
        var bytes = builder.SizedByteArray();
        var pkt = Manager.Packet.CreatePacket(bytes, PacketType.S_CreateRoom);
        clientSession.Send(pkt);
    }

    public static void C_RoomListHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;
        C_RoomList packet = C_RoomList.GetRootAsC_RoomList(buffer);
    }
}