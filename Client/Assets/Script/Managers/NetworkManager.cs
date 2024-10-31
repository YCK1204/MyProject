using System.Collections.Generic;
using ServerCore;
using System.Net;
using System;

public class NetworkManager : IManager
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
    public void Update()
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

    public void Init()
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 8080);
        _connector.Init(endPoint, () => { return session; });
    }

    public void Clear()
    {
    }
}