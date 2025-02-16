using Server.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Session
{
    public class SessionManager
    {
        UInt64 _id = 0;
        Dictionary<UInt64, ClientSession> _sessions = new Dictionary<UInt64, ClientSession>();
        object _lock = new object();
        public void Push(ClientSession session)
        {
            lock (_lock)
            {
                session.UserInfo = new Server.Session.UserInfo(); // 스팀 계정에 따라 초기화 필요
                session.UserInfo.ID = ++_id;
            }
        }
        public ClientSession Find(UInt64 id)
        {
            ClientSession session = null;
            lock (_lock)
            {
                if (_sessions.ContainsKey(id))
                    session = _sessions[id];
            }
            return session;
        }
        public bool Remove(UInt64 id)
        {
            lock (_lock)
            {
                ClientSession session = Find(id);
                if (session != null)
                {
                    session.UserInfo = null;
                    Manager.Pool.Push<ClientSession>(session);
                }
                return _sessions.Remove(id);
            }
        }
    }
}
