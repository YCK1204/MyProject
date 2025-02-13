using Server.Session;
using ServerCore;
using System.Net;

namespace Server
{
    public class Program
    {
        static Listener _listener = new Listener();
        static void Main(string[] args)
        {
            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 8080);
            _listener.Init(endPoint, () => { return new ClientSession(); });
            while (true) ;
        }
    }
}