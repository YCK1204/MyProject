using Server.Game.Room;
using Server.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.Object
{
    public class Player : GameObject
    {
        public Player() { Type = ObjectType.PLAYER; }
        public ClientSession Session;
        public GameRoom Room = null;
    }
}
