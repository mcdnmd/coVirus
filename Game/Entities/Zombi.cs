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
        public int DamageDistance;

        public float Speed;

        public Zombi(int x, int y)
        {
            Location = new Point(x, y);
            Alive = true;
            Speed = 0.1f;
            Health = 125;
            DamageDistance = 64;
        }


        public void Act()
        {
            if (Health < 0)
                Alive = false;
            if (Alive)
            {
                //var displacement = GetDisplacement();
                //MakeMovement(displacement[0], displacement[1]);
                //var distance = new Point(Game.player.Location.X - Location.X, Game.player.Location.Y - Location.Y);
                //var playerDistance = Math.Sqrt(distance.X * distance.X + distance.Y * distance.Y);
                //if (playerDistance <= DamageDistance)
                //    GiveDamage(Game.player);
                //Game.AliveActors.Enqueue(this);
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
