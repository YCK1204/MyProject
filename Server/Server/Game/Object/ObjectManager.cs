using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.Object
{
    public class ObjectManager
    {
        Dictionary<int, GameObject> _objects = new Dictionary<int, GameObject>();
        object _lock = new object();
        int _counter = 0;
        public T Generate<T>() where T : GameObject, new()
        {
            T gameObject = new T();
            lock (_lock)
            {
                gameObject.ID = GenerateId(gameObject.Type);
                _objects.Add(gameObject.ID, gameObject);
            }
            return gameObject;
        }
        int GenerateId(ObjectType type)
        {
            return ((int)type << 24) | (_counter++);
        }
        public ObjectType GetObjectTypeById(int id)
        {
            int type = (id >> 24) & 0x7F;
            return (ObjectType)type;
        }
        public T Find<T>(int id) where T : GameObject
        {
            GameObject obj = null;
            lock (_lock)
            {
                if (_objects.TryGetValue(id, out obj))
                    return obj as T;
            }
            return null;
        }
        public bool Remove(int id)
        {
            lock (_lock)
            {
                return _objects.Remove(id);
            }
        }
    }
}
