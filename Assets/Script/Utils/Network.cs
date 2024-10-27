using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Google.FlatBuffers;
using ServerCore;
using System.Net;
using System;
public class ServerSession : PacketSession
{
    public Network network;
    public override void OnConnect(EndPoint endPoint)
    {
        FlatBufferBuilder builder = new FlatBufferBuilder(1); // 후에 오브젝트 풀링으로 관리

        int roomId = 100;
        int memberCnt = 5;

        var room = C_CreateRoom.CreateC_CreateRoom(builder, room_id: roomId, member_count:memberCnt);
        builder.Finish(room.Value);
        var bytes = builder.SizedByteArray();
        var b = builder.DataBuffer.ToArray(0, builder.DataBuffer.Length);
        var pkt = network.packet.CreatePacket(bytes, PacketType.C_CreateRoom);
        Send(pkt);
    }

    public override void OnDisconnect(EndPoint endPoint)
    {
        Debug.Log($"OnDisconnect");
    }

    public override void OnRecvPacket(ArraySegment<byte> data)
    {
        network.Push(data);
    }

    public override void OnSend(int numOfBytes)
    {
        Debug.Log($"OnSend");
    }
}
public class Network : MonoBehaviour
{
    static Network _instance = null;
    public static Network NetworkManager
    {
        get { return _instance; }
        set { _instance = value; }
    }
    public PacketManager packet = new PacketManager();
    Connector _connector = new Connector();
    ServerSession session = new ServerSession();
    object _lock = new object();
    Queue<ArraySegment<byte>> _packetQueue = new Queue<ArraySegment<byte>>();
    public void Push(ArraySegment<byte> data)
    {
        lock (_lock)
        {
            _packetQueue.Enqueue(data);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        _instance = this;
        session.network = this;
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 8080);
        _connector.Init(endPoint, () => { return session; });
    }
    void Update()
    {
        lock (_lock)
        {
            while (_packetQueue.Count > 0)
            {
                var data = _packetQueue.Dequeue();
                packet.OnRecvPacket(session, data);
            }
        }
    }
}