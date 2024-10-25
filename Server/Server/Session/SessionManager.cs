using Server.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Session
{
    public class SessionManager : IManager
    {
        int _id = 0;
        Dictionary<int, ClientSession> _sessions = new Dictionary<int, ClientSession>();
        object _lock = new object();
        public void Clear()
        {
        }

        public void Init()
        {
        }
        public ClientSession Generate()
        {
            ClientSession session;
            lock (_lock)
            {
                session = new ClientSession();
                session.ID = ++_id;
                _sessions.Add(_id, session);
            }
            return session;
        }
        public ClientSession Find(int id)
        {
            ClientSession session = null;
            lock (_lock)
            {
                if (_sessions.ContainsKey(id))
                    session = _sessions[id];
            }
            return session;
        }
        public bool Remove(int id)
        {
            lock (_lock)
            {
                return _sessions.Remove(id);
            }
        }
    }
}
