using System.Collections.Generic;
using System.Drawing;

namespace _3DGame
{
    public static class Map
    {
        public static int Width;
        public static int Height;
        public static int[,] TileMap;
        public static Dictionary<PointF, IEntity> EntityMap;
        public static Queue<IEntity> Actors;
        public static List<IEntity> Enemies;


        public static void CreateMap(ILevel level)
        {
            Width = level.Width;
            Height = level.Height;
            TileMap = new int[Width, Height];
            EntityMap = new Dictionary<PointF, IEntity>();
            Actors = new Queue<IEntity>();
            Enemies = new List<IEntity>();
            CreateMaps(level.IntMap);
        }

        private static void CreateMaps(int[] map)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    TileMap[x, y] = map[y * Width + x];
                    var location = new PointF(x, y);
                    switch (map[y * Width + x])
                    {
                        case (int)Tail.Player:
                            EntityMap[location] = new Player(x, y);
                            Game._Player = (Player)EntityMap[location];
                            Actors.Enqueue(EntityMap[location]);
                            break;
                        case (int)Tail.Zombie:
                            EntityMap[location] = new Zombi(x, y);
                            Enemies.Add(EntityMap[location]);
                            Actors.Enqueue(EntityMap[location]);
                            break;
                        case (int)Tail.Shotgun:
                            EntityMap[location] = new Shotgun(x, y);
                            Enemies.Add(EntityMap[location]);
                            break;
                    }
                }
            }
        }
    }
}
