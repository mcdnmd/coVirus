﻿using System;
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
                Move();
                Game.AliveActors.Enqueue(this);
                return;
            }
        }

        private void Move()
        {
            double xDes, yDes;
            var rush = random.Next(1, 10);
            if (rush > 5)
            {
                var minusX = random.NextDouble();
                var minusY = random.NextDouble();
                xDes = random.NextDouble();
                yDes = Math.Sqrt(1 - xDes * xDes);
                if (minusX < 0.5d)
                    xDes *= (-1);
                if (minusY < 0.5d)
                    yDes *= (-1);
            }
            else
            {
                xDes = Game._Player.Location.X - Location.X;
                yDes = Game._Player.Location.Y - Location.Y;
                var desLength = Math.Sqrt(xDes * xDes + yDes * yDes);
                xDes = (float)(xDes / desLength);
                yDes = (float)(yDes / desLength);
            }
            DirectionVector = new PointF((float)xDes, (float)yDes);
            float X = Location.X;
            float Y = Location.Y;
            if (Map.TileMap[(int)(Location.X + DirectionVector.X * MoveSpeed), (int)Location.Y] != 1)
                X = Location.X + (float)(DirectionVector.X * MoveSpeed);
            if (Map.TileMap[(int)Location.X, (int)(Location.Y + DirectionVector.Y * MoveSpeed)] != 1)
                Y = Location.Y + (float)(DirectionVector.Y * MoveSpeed);
            Location = new PointF(X, Y);
        }

        private void GiveDamage(IEntity player)
        {
            player.Health -= 1;
        }
    }
}
