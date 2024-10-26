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

    internal static void S_RoomListHandler(PacketSession session, ByteBuffer buffer)
    {
        throw new NotImplementedException();
    }
}