using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Utils
{
    interface IPool
    {
        public void Init(int count, int max);
    }
    class Pool<T> : IPool where T : class, new()
    {
        Stack<T> _items = new Stack<T>();
        int _max;
        public void Init(int count, int max)
        {
            _items.Clear();
            for (int i = 0; i < count; i++)
            {
                T item = new T();
                _items.Push(item);
            }
            _max = max;
        }
        public void Push(T item)
        {
            if (_items.Count >= _max || item == null)
                return;
            _items.Push(item);
        }
        public T Pop()
        {
            T item = null;
            if (_items.Count > 0)
                item = _items.Pop();
            return item;
        }
    }
    public class PoolManager
    {
        Dictionary<Type, IPool> _pools = new Dictionary<Type, IPool>();
        public void CreatePool<T>(int count = 5, int max = 10) where T : class, new()
        {
            Type type = typeof(T);
            if (_pools.ContainsKey(type))
            {
                _pools[type].Init(max, count);
                return;
            }
            Pool<T> pool = new Pool<T>();
            pool.Init(max, count);
            _pools.Add(type, pool);
        }
        public void Push<T>(T item) where T : class, new()
        {
            Type type = typeof(T);

            if (_pools.ContainsKey(type) == false)
                return;
            Pool<T> pool = (Pool<T>)_pools[type];
            pool.Push(item);
        }
        public T Pop<T>() where T : class, new()
        {
            Type type = typeof(T);

            if (_pools.ContainsKey(type) == false)
                CreatePool<T>();
            Pool<T> pool = (Pool<T>)_pools[type];

            return pool.Pop();
        }
    }
}
