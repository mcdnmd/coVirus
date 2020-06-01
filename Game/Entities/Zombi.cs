using System;
using System.Collections.Generic;
using System.Drawing;

namespace _3DGame
{
    public class Zombi : IEntity
    {
        public PointF Location { get; set; }
        public int Health { get; set; }
        public bool Alive { get; set; }
        public PointF DirectionVector;
        public double MoveSpeed;
        public int DamageDistance;
        public Random random = new Random();

        public Zombi(int x, int y)
        {
            Location = new Point(x, y);
            Alive = true;
            MoveSpeed = 0.2d;
            Health = 125;
            DirectionVector = new PointF(0.8f, 0.6f);
            DamageDistance = 64;
        }


        public void Act()
        {
            if (Health < 0)
                Alive = false;
            if (Alive)
            {
                var xDirection = random.NextDouble();
                var yDirection = Math.Sqrt(1 - xDirection * xDirection);
                DirectionVector = new PointF((float)xDirection, (float)yDirection);
                float X = Location.X;
                float Y = Location.Y;
                if (Map.TileMap[(int)(Location.X + DirectionVector.X * MoveSpeed), (int)Location.Y] != 1)
                    X = Location.X + (float)(DirectionVector.X * MoveSpeed);
                if (Map.TileMap[(int)Location.X, (int)(Location.Y + DirectionVector.Y * MoveSpeed)] != 1)
                    Y = Location.Y + (float)(DirectionVector.Y * MoveSpeed);
                //var displacement = GetDisplacement();
                //MakeMovement(displacement[0], displacement[1]);
                //var distance = new Point(Game.player.Location.X - Location.X, Game.player.Location.Y - Location.Y);
                //var playerDistance = Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                //if (playerDistance <= DamageDistance)
                //    GiveDamage(Game.player);
                //Game.AliveActors.Enqueue(this);
                Location = new PointF(X, Y);
                Game.AliveActors.Enqueue(this);
                return;
            }
        }

        private void GiveDamage(IEntity player)
        {
            player.Health -= 1;
        }
    }
}
