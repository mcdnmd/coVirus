using System.Collections.Generic;
using System.Drawing;
using System.IO;

namespace _3DGame
{
    public class Level_1 : ILevel
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

        public Level_1()
        {
            int[] levelGrid =
            {
                W,W,W,W,W,W,W,W,W,W,W,W,
                W,S,O,O,O,O,O,O,O,O,O,W,
                W,O,O,O,O,O,Z,O,O,O,O,W,
                W,O,O,O,O,O,O,O,W,O,O,W,
                W,O,O,W,O,W,O,O,W,O,O,W,
                W,O,O,W,O,W,W,O,W,O,O,W,
                W,O,O,W,O,O,W,O,W,O,O,W,
                W,O,Z,O,W,O,W,O,W,O,O,W,
                W,O,O,O,W,O,W,O,W,O,O,W,
                W,O,O,O,W,W,W,O,W,O,O,W,
                W,O,O,O,O,O,O,O,O,O,O,W,
                W,O,O,O,W,W,W,W,O,O,O,W,
                W,O,O,O,O,O,O,O,O,O,O,W,
                W,O,O,O,P,O,O,O,O,Z,O,W,
                W,O,O,O,O,O,O,O,W,O,O,W,
                W,O,O,W,O,W,O,O,W,O,O,W,
                W,O,O,W,O,W,W,O,W,O,O,W,
                W,O,O,W,O,O,W,O,W,O,O,W,
                W,O,O,O,W,O,W,O,W,O,O,W,
                W,O,O,O,W,O,W,O,W,O,O,W,
                W,O,O,O,W,W,W,O,W,O,O,W,
                W,O,O,O,O,O,O,O,O,O,O,W,
                W,W,W,W,W,W,W,W,W,W,W,W
            };
            IntMap = levelGrid;
            Width = 12;
            Height = 23;
        }
    }
}