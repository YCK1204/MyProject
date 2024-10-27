using Google.FlatBuffers;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LightTransport;

public class PacketHandler
{
    public static void S_SpawnHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;
        S_Spawn packet = S_Spawn.GetRootAsS_Spawn(buffer);

        PosInfo posInfo = packet.Pos.Value;
        Debug.Log($"S_SpawnHandler dir {posInfo.Dir}");
    }
    public static void S_LeaveRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;
    }
    static void RequestSpawn(ServerSession session)
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(1);
        var posInfo = PosInfo.CreatePosInfo(builder, Dir.DOWN, 10, 10);
        var data = C_Spawn.CreateC_Spawn(builder, posInfo);
        builder.Finish(data.Value);
        var bytes = builder.SizedByteArray();

        var pkt = Network.NetworkManager.packet.CreatePacket(bytes, PacketType.C_Spawn);

        session.Send(pkt);
    }
    public static void S_EnterRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;

        S_EnterRoom packet = S_EnterRoom.GetRootAsS_EnterRoom(buffer);
        EnterRoomError result = packet.Ok;

        switch (result)
        {
            case EnterRoomError.SUCCESS:
                RequestSpawn(serverSession);
                break;
            case EnterRoomError.FULL:
                break;
            case EnterRoomError.NOT_FOUND:
                break;
            case EnterRoomError.UNKNOWN:
                break;
        }
    }
    static void EnterRoom(ServerSession session)
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(1);
        var data = C_EnterRoom.CreateC_EnterRoom(builder, 100);
        builder.Finish(data.Value);
        var ddd = builder.SizedByteArray();
        var bb = new ByteBuffer(ddd);
        C_EnterRoom c = C_EnterRoom.GetRootAsC_EnterRoom(bb);
        var dd = builder.DataBuffer.ToArray(0, builder.DataBuffer.Length);
        var pkt = Network.NetworkManager.packet.CreatePacket(ddd, PacketType.C_EnterRoom);

        session.Send(pkt);
    }
    public static void S_CreateRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;
        S_CreateRoom ret = S_CreateRoom.GetRootAsS_CreateRoom(buffer);

        switch (ret.Ok)
        {
            case CreateRoomError.SUCCESS:
                Debug.Log($"방 생성 성공!!");
                EnterRoom(serverSession);
                break;
            case CreateRoomError.OVERLAPPED:
                Debug.Log($"방 생성 실패 : 번호 중복");
                break;
            case CreateRoomError.INVALID_ID:
                Debug.Log($"방 생성 실패 : 정상적이지 않은 방번호로 생성 시도");
                break;
            case CreateRoomError.UNKNOWN:
                Debug.Log($"방 생성 실패 : 이유 알 수 없음");
                break;
        }
    }

    public static void S_RoomListHandler(PacketSession session, ByteBuffer buffer)
    {
    }
}