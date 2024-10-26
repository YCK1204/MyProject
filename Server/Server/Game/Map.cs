using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game
{
    public struct Pos
    {
        public Pos(int x, int y) { X = x; Y = y; }
        public int Y;
        public int X;
    }
    public struct Vector2
    {
        public int x;
        public int y;
        public Vector2(int x, int y) { this.x = x; this.y = y; }
        public static Vector2 up { get { return new Vector2(0, 1); } }
        public static Vector2 down { get { return new Vector2(0, -1); } }
        public static Vector2 left { get { return new Vector2(-1, 0); } }
        public static Vector2 right { get { return new Vector2(1, 0); } }

        public static Vector2 operator +(Vector2 a, Vector2 b) { return new Vector2(a.x + b.x, a.y + b.y); }
        public static Vector2 operator -(Vector2 a, Vector2 b) { return new Vector2(a.x - b.x, a.y - b.y); }
        public float magnitude { get { return (float)Math.Sqrt(sqrMagnitude); } }
        public int sqrMagnitude { get { return (x * x + y * y); } }
        public int cellDistFromZero { get { return Math.Abs(x) + Math.Abs(y); } }
    }
    public class Map
    {
        int xMax;
        int yMax;
        int xMin;
        int yMin;

        public void LoadMap(int id)
        {
            using (var sr = new StreamReader($"../../../../Common/Map/Map_{id.ToString("000")}", Encoding.UTF8))
            {
                xMin = int.Parse(sr.ReadLine());
                xMax = int.Parse(sr.ReadLine());
                yMin = int.Parse(sr.ReadLine());
                yMax = int.Parse(sr.ReadLine());
                for (int y = )
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine();
                    if (line == null || line.Length == 0)
                        break;

                }
            }
        }
    }
}
