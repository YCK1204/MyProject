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
        var posInfo = PosInfo.CreatePosInfo(builder, Dir.DOWN, -7, 3);
        var data = C_Spawn.CreateC_Spawn(builder, posInfo);
        var pkt = GameManager.Packet.CreatePacket(data, builder, PacketType.C_Spawn);

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
        var pkt = GameManager.Packet.CreatePacket(data, builder, PacketType.C_EnterRoom);

        session.Send(pkt);
    }
    public static void S_CreateRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;
        S_CreateRoom ret = S_CreateRoom.GetRootAsS_CreateRoom(buffer);

        switch (ret.Ok)
        {
            case CreateRoomError.SUCCESS:
                Debug.Log($"�� ���� ����!!");
                EnterRoom(serverSession);
                break;
            case CreateRoomError.OVERLAPPED:
                Debug.Log($"�� ���� ���� : ��ȣ �ߺ�");
                break;
            case CreateRoomError.INVALID_ID:
                Debug.Log($"�� ���� ���� : ���������� ���� ���ȣ�� ���� �õ�");
                break;
            case CreateRoomError.INVALID_GAME_LEVEL:
                Debug.Log($"�� ���� ���� : ���������� ���� ���� ���̵��� ���� �õ�");
                break;
            case CreateRoomError.UNKNOWN:
                Debug.Log($"�� ���� ���� : ���� �� �� ����");
                break;
        }
    }

    public static void S_RoomListHandler(PacketSession session, ByteBuffer buffer)
    {
    }
}