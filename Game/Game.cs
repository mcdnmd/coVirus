using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace _3DGame
{
    public static class Game
    {
        public static Player _Player;
		public static int Width;
		public static int Height;
        public static Queue<IEntity> AliveActors;
        public static List<IEntity> Enemies;

        public static void InitGame()
        {
            Width = 24;
            Height = 24;
            AliveActors = Map.Actors;
            Enemies = Map.Enemies;
        }

        public static void PlayRound()
        {
            var entities = GetEntities();
            foreach (var entity in entities)
                entity.Act();
        }

        private static List<IEntity> GetEntities()
        {
            var entities = new List<IEntity>();
            while (AliveActors.Count != 0)
                entities.Add(AliveActors.Dequeue());
            return entities;
        }

        // BFS!!!

    }
}
