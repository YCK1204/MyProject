using Server.Game.Room;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Managers
{
    public class MapManager : IManager
    {
        Dictionary<int, Map> _maps = new Dictionary<int, Map>();
        public void Clear()
        {
        }

        public void Init()
        {
        }
    }
}
