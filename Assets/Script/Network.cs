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
        Debug.Log($"OnConnect");
        string sendStr = "Send!!";
        Debug.Log(sendStr);
        FlatBufferBuilder builder = new FlatBufferBuilder(20);
        var str = builder.CreateString(sendStr);
        var s_test =  S_Test.CreateS_Test(builder, str);
        builder.Finish(s_test.Value);
        var data = builder.SizedByteArray();
        var d = network.packet.CreatePacket(data, PacketType.S_Test);
        Send(d);
    }

    public override void OnDisconnect(EndPoint endPoint)
    {
        Debug.Log($"OnDisconnect");
    }

    public override void OnRecvPacket(ArraySegment<byte> data)
    {
        GameObject go = GameObject.Find("Test");
        Network net = go.GetComponent<Network>();
        net.Push(data);
    }

    public override void OnSend(int numOfBytes)
    {
        Debug.Log($"OnSend");
    }
}
public class Network : MonoBehaviour
{
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
