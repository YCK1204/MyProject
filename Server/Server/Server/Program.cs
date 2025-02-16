using Server.Managers;
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
            Manager.Pool.CreatePool<ClientSession>(500, 500);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 8080);

            // 만약 500명의 Client가 동접이라면 레전드 뻑남
            // 식은땀내면서 고칠준비해야함
            _listener.Init(endPoint, () => { return Manager.Pool.Pop<ClientSession>(); }); 
            while (true) ;
        }
    }
}