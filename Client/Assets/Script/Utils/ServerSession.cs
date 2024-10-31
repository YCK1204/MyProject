using Google.FlatBuffers;
using ServerCore;
using System.Net;
using System;
using UnityEngine;

public class ServerSession : PacketSession
{
    public override void OnConnect(EndPoint endPoint)
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(1); // 후에 오브젝트 풀링으로 관리

        int roomId = 100;
        int memberCnt = 5;

        var room = C_CreateRoom.CreateC_CreateRoom(builder, room_id: roomId, member_count: memberCnt, game_level: 1);
        var pkt = GameManager.Packet.CreatePacket(room, builder, PacketType.C_CreateRoom);
        Send(pkt);
    }

    public override void OnDisconnect(EndPoint endPoint)
    {
        Debug.Log($"OnDisconnect");
    }

    public override void OnRecvPacket(ArraySegment<byte> data)
    {
        GameManager.Network.Push(data);
    }

    public override void OnSend(int numOfBytes)
    {
        Debug.Log($"OnSend");
    }
}
