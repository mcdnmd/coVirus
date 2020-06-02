using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame
{
    public class Level_2 : ILevel
    {
        public int[] IntMap { get; set; }
        public Dictionary<char, int> ByteEntityRelation { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int W = (int)Tail.Wall;
        public int P = (int)Tail.Player;
        public int Z = (int)Tail.Zombie;
        public int O = (int)Tail.Empty;
        public int S = (int)Tail.Shotgun;

        public Level_2()
        {
            int[] levelGrid =
            {
                W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,
                W,O,O,O,O,O,O,O,W,O,O,O,O,O,O,O,O,O,O,O,O,O,O,W,
                W,O,O,O,W,O,Z,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,O,W,
                W,W,O,W,W,W,W,W,W,O,O,O,O,O,O,O,O,O,O,O,O,O,O,W,
                W,O,O,O,W,O,O,O,W,O,O,W,W,W,W,W,W,W,O,O,O,O,O,W,
                W,O,W,O,O,O,W,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,O,Z,W,O,O,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,W,O,O,O,W,O,W,O,O,W,O,O,O,O,O,O,O,O,O,O,O,W,
                W,O,O,O,W,O,O,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,W,O,W,W,W,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,O,O,O,O,O,O,O,O,O,W,W,W,W,W,W,W,O,O,O,O,O,W,
                W,O,O,O,O,O,O,O,O,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,O,O,O,O,O,O,O,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,O,O,O,O,O,O,O,Z,O,W,O,O,O,O,O,O,O,O,O,O,O,W,
                W,O,O,O,O,O,O,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,O,W,O,W,O,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,O,W,O,W,W,O,W,O,O,W,W,W,W,W,W,W,O,O,O,O,O,W,
                W,O,O,W,O,O,W,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,O,O,W,O,W,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,Z,O,O,W,O,W,O,W,O,O,W,O,S,O,O,O,O,O,O,O,O,O,W,
                W,O,O,O,W,W,W,O,W,O,O,W,O,O,O,O,O,W,O,O,O,O,O,W,
                W,O,O,O,O,O,O,O,O,O,O,W,O,O,O,O,O,W,O,O,P,O,O,W,
                W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W,W
            };
            IntMap = levelGrid;
            Width = 24;
            Height = IntMap.Length / 24;
        }
    }
}
