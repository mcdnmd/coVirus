using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3DGame
{
    public class Player : IEntity
    {
        public PointF Location { get; set;  }
        public int Health { get; set; }
        public bool Alive { get; set; }
        public PointF DirectionVector;
        public Queue<Command> Commands;
        public double MoveSpeed;
        public double RotationSpeed;
        public IWeapon Weapon;

        public Player(int x, int y)
        {
            Location = new PointF(x, y);
            DirectionVector = new PointF(-1, 0);
            Camera.CreatePlaneVector();
            Commands = new Queue<Command>();
            Health = 100;
            Alive = true;
            MoveSpeed = 0.6d;
            RotationSpeed = 0.4d;
            Weapon = null;
        }

        public void Act()
        {
            HandleCommands();
            Game.AliveActors.Enqueue(this);
        }

        public void CollectItem()
        {
            if (Map.TileMap[(int)Location.X, (int)Location.Y] == (int)Tail.Shotgun)
                Weapon = (IWeapon) Map.EntityMap[new PointF(Location.X, Location.Y)];
        }

        public void HandleCommands()
        {
            while (Commands.Count != 0)
            {
                var command = Commands.Dequeue();
                float X = Location.X;
                float Y = Location.Y;
                switch (command)
                {
                    case Command.KeyUp:
                        if (Map.TileMap[(int)(Location.X + DirectionVector.X * MoveSpeed), (int)Location.Y] != 1)
                            X = Location.X + (float)(DirectionVector.X * MoveSpeed);
                        if (Map.TileMap[(int)Location.X, (int)(Location.Y + DirectionVector.Y * MoveSpeed)] != 1)
                            Y = Location.Y + (float)(DirectionVector.Y * MoveSpeed);
                        break;
                    case Command.KeyDown:
                        if (Map.TileMap[(int)(Location.X - DirectionVector.X * MoveSpeed), (int)Location.Y] != 1)
                            X = Location.X - (float)(DirectionVector.X * MoveSpeed);
                        if (Map.TileMap[(int)Location.X, (int)(Location.Y - DirectionVector.Y * MoveSpeed)] != 1)
                            Y = Location.Y - (float)(DirectionVector.Y * MoveSpeed);
                        break;
                    case Command.KeyRight:
                        RotateVectorSystem(-RotationSpeed);
                        break;
                    case Command.KeyLeft:
                        RotateVectorSystem(RotationSpeed);
                        break;
                }
                Location = new PointF(X, Y);
            }
        }

        public void RotateVectorSystem(double angle)
        {
            var dirX = (float)(DirectionVector.X * Math.Cos(angle) - DirectionVector.Y * Math.Sin(angle));
            var dirY = (float)(DirectionVector.X * Math.Sin(angle) + DirectionVector.Y * Math.Cos(angle));
            DirectionVector = new PointF(dirX, dirY);
            var planeX = (float)(Camera.PlaneVector.X * Math.Cos(angle) - Camera.PlaneVector.Y * Math.Sin(angle));
            var planeY = (float)(Camera.PlaneVector.X * Math.Sin(angle) + Camera.PlaneVector.Y * Math.Cos(angle));
            Camera.PlaneVector = new PointF(planeX, planeY);
        }
    }
}
