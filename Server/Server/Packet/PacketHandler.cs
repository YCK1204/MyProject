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
        FlatBufferBuilder builder = new FlatBufferBuilder(1);

        try
        {
            PosInfo posInfo = packet.Pos.Value;
            Console.WriteLine($"C_SpawnHandler dir : {posInfo.Dir}, x : {posInfo.X}, y : {posInfo.Y}");
            var posInfoData = PosInfo.CreatePosInfo(builder, posInfo.Dir, posInfo.X, posInfo.Y);

            Player player = clientSession.MyPlayer;
            GameRoom room = player.Room;

            room.HandleSpawn(packet, clientSession);
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
            string password = packet.Password;

            GameRoom room = Manager.Room.Find(roomId);
            if (room == null)
                error = EnterRoomError.NOT_FOUND; // 방을 못찾은 경우(없는 경우)
            else
            {
                if (room.Enter(clientSession.MyPlayer) == false) // 이미 들어가 있거나 방이 꽉찬 경우
                    error = EnterRoomError.FULL;
                else
                    clientSession.Room = room; // 입장 성공
                Console.WriteLine($"C_EnterRoomHandler roomId : {roomId}, passowrd : {password}");
            }
        }
        catch (Exception e)
        {
            error = EnterRoomError.UNKNOWN;
            Console.WriteLine(e);
        }
        var data = S_EnterRoom.CreateS_EnterRoom(builder, error);
        var pkt = Manager.Packet.CreatePacket(data, builder, PacketType.S_EnterRoom);
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
            int gameLevel = packet.GameLevel;
            string password = packet.Passowrd;

            Console.WriteLine($"C_CreateRoomHandler roomId : {roomId}, memberCount : {memberCount}, passoword : {password}, gameLevel : {gameLevel}");


            if (roomId < 0 || roomId > 1000)
                error = CreateRoomError.INVALID_ID; // 방 번호 유효성 검사, 유효한 방 번호 기준은 나중에 결정
            else if (gameLevel <= 0 || gameLevel > 10)
                error = CreateRoomError.INVALID_GAME_LEVEL;
            else
            {
                GameRoom room = Manager.Room.Generate(roomId);
                if (room == null)
                    error = CreateRoomError.OVERLAPPED; // 방생성 실패, Generate 실패는 방 번호 중복
                else
                    room.Init(gameLevel, roomId, memberCount, password);
            }
        }
        catch (Exception e)
        {
            error = CreateRoomError.UNKNOWN;
            Console.WriteLine(e);
        }
        var data = S_CreateRoom.CreateS_CreateRoom(builder, error);
        var pkt = Manager.Packet.CreatePacket(data, builder, PacketType.S_CreateRoom);
        clientSession.Send(pkt);
    }

    public static void C_RoomListHandler(PacketSession session, ByteBuffer buffer)
    {
        ClientSession clientSession = session as ClientSession;
        C_RoomList packet = C_RoomList.GetRootAsC_RoomList(buffer);
    }
}