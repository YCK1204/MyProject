using Google.FlatBuffers;
using ServerCore;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacketHandler
{
    public static void S_SpawnHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;
    }
    public static void S_LeaveRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;
    }
    public static void S_EnterRoomHandler(PacketSession session, ByteBuffer buffer)
    {
        ServerSession serverSession = session as ServerSession;
    }
    static void EnterRoom(ServerSession session)
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(1024);

        var data = C_EnterRoom.CreateC_EnterRoom(builder, 100);
        builder.Finish(data.Value);
        var bytes = builder.SizedByteArray();
        var pkt = Network.NetworkManager.packet.CreatePacket(bytes, PacketType.C_EnterRoom);
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
            case CreateRoomError.UNKNOWN:
                Debug.Log($"�� ���� ���� : ���� �� �� ����");
                break;
        }
    }

    internal static void S_RoomListHandler(PacketSession session, ByteBuffer buffer)
    {
        throw new NotImplementedException();
    }
}