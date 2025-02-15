using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Game.Room
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
        public int sqrMagnitude { get { return x * x + y * y; } }
        public int cellDistFromZero { get { return Math.Abs(x) + Math.Abs(y); } }
    }
    public class Map
    {
        int xMax;
        int yMax;
        int xMin;
        int yMin;

        bool[,] _conllision;

        public void LoadMap(int id, string pathPrefix = "../../../../../Common/Map")
        {
            string mapName = $"Map_{id.ToString("000")}";
            if (File.Exists($"{pathPrefix}/{mapName}") == false)
                return;
            // 파일이 없는경우 예외처리 추가 필요 

            using (var sr = new StreamReader($"{pathPrefix}/{mapName}", Encoding.UTF8))
            {
                xMin = int.Parse(sr.ReadLine());
                xMax = int.Parse(sr.ReadLine());
                yMin = int.Parse(sr.ReadLine());
                yMax = int.Parse(sr.ReadLine());

                int xCount = xMax - xMin + 1;
                int yCount = yMax - yMin + 1;
                _conllision = new bool[yCount, xCount];
                for (int y = 0; y < yCount; y++)
                {
                    string line = sr.ReadLine();
                    for (int x = 0; x < xCount; x++)
                    {
                        if (line[x] == '0')
                            _conllision[y, x] = false;
                        else
                            _conllision[y, x] = true;
                    }
                }
            }
        }
        public Vector2 CellToPos(Vector2 cellPos)
        {
            return new Vector2(cellPos.x + xMin, yMax - cellPos.y);
        }
        public Vector2 PosToCell(Vector2 pos)
        {
            return new Vector2(pos.x - xMin, yMax - pos.y);
        }
    }
}
