using Google.FlatBuffers;
using Server.Game.Object;
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
        C_Spawn packet = C_Spawn.GetRootAsC_Spawn(buffer);


        try
        {
            PosInfo posInfo = packet.Pos.Value;
            Console.WriteLine($"C_SpawnHandler dir : {posInfo.Dir}, x : {posInfo.X}, y : {posInfo.Y}");
            FlatBufferBuilder builder = new FlatBufferBuilder(1);
            var posInfoData = PosInfo.CreatePosInfo(builder, posInfo.Dir, posInfo.X, posInfo.Y);

            Player player = clientSession.MyPlayer;
            GameRoom room = player.Room;

            var data = S_Spawn.CreateS_Spawn(builder, player.ID, posInfoData);
            builder.Finish(data.Value);
            var bytes = builder.SizedByteArray();
            var pkt = Manager.Packet.CreatePacket(bytes, PacketType.S_Spawn);
            room.Broadcast(pkt);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
    public static void C_LeaveRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;
    }
    public static void C_EnterRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;

        var packet = C_EnterRoom.GetRootAsC_EnterRoom(buffer);

        FlatBufferBuilder builder = new FlatBufferBuilder(1);

        EnterRoomError error = EnterRoomError.SUCCESS;

        try
        {
            int roomId = packet.RoomId;
            string password = null;
            if (packet.Password != null)
                password = packet.Password;

            GameRoom room = Manager.Room.Find(roomId);
            if (room == null)
                error = EnterRoomError.NOT_FOUND;
            else
            {
                if (room.Enter(clientSession.MyPlayer) == false)
                    error = EnterRoomError.FULL;
                else
                    clientSession.Room = room;
                Console.WriteLine($"C_EnterRoomHandler roomId : {roomId}, passowrd : {password}");
            }
        }
        catch (Exception e)
        {
            error = EnterRoomError.UNKNOWN;
            Console.WriteLine(e);
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

        FlatBufferBuilder builder = new FlatBufferBuilder(1);
        CreateRoomError error = CreateRoomError.SUCCESS;

        try
        {
            int roomId = packet.RoomId;
            int memberCount = packet.MemberCount;
            string password = packet.Passowrd;

            Console.WriteLine($"C_CreateRoomHandler roomId : {roomId}, memberCount : {memberCount}, passoword : {password}");
            GameRoom room = Manager.Room.Generate(roomId);
            if (room == null)
                error = CreateRoomError.OVERLAPPED;
            else if (roomId < 0 || roomId > 1000)
                error = CreateRoomError.INVALID_ID;

            if (room != null)
            {
                room.MemberCount = memberCount;
                room.Password = password;
                room.ID = roomId;
            }
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