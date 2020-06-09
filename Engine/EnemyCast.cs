using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace _3DGame
{
    public class EnemyCast
    {
        public static double cameraPlaneX;
        public static int MapX;
        public static int MapY;
        public static double DeltaDistanceX;
        public static double DeltaDistanceY;
        public static double SideDistanceX;
        public static double SideDistanceY;
        public static int StepX;
        public static int StepY;
        public static int HittedSide;
        public static double WallDistance;
        public static List<IEntity> CastedEnemies;

        public static Ray _Ray = new Ray();

        public static void Cast()
        {
            CastedEnemies = new List<IEntity>();
            var EnemiesDistance = new Dictionary<double, IEntity>();
            foreach (var enemy in Game.Enemies)
            {
                var distance = (Game._Player.Location.X - enemy.Location.X) * (Game._Player.Location.X - enemy.Location.X) + (Game._Player.Location.Y - enemy.Location.Y) * (Game._Player.Location.Y - enemy.Location.Y);
                EnemiesDistance[distance] = enemy;
            }
            var list = EnemiesDistance.Keys.ToList();
            list.Sort();

            for (int i = 0; i < EnemiesDistance.Count; i++)
            {
                var casted = false;
                var enemy = EnemiesDistance[list[i]];
                var spriteX = EnemiesDistance[list[i]].Location.X - Game._Player.Location.X;
                var spriteY = EnemiesDistance[list[i]].Location.Y - Game._Player.Location.Y;
                var inverseDeterminant = 1.0 / (Camera.PlaneVector.X * Game._Player.DirectionVector.Y - Game._Player.DirectionVector.X * Camera.PlaneVector.Y);
                var transformX = inverseDeterminant * (Game._Player.DirectionVector.Y * spriteX - Game._Player.DirectionVector.X * spriteY);
                var transformY = inverseDeterminant * (-Camera.PlaneVector.Y * spriteX + Camera.PlaneVector.X * spriteY);
                var spriteScreenX = (int)(Core.ScreenWidth / 2 * (1 + transformX / transformY));
                var spriteHeight = Math.Abs((int)(Core.ScreenHeight / transformY));
                var drawStartY = -spriteHeight / 2 + Core.ScreenHeight / 2;
                if (drawStartY < 0)
                    drawStartY = 0;
                var drawEndY = spriteHeight / 2 + Core.ScreenHeight / 2;
                if (drawEndY >= Core.ScreenHeight)
                    drawEndY = Core.ScreenHeight - 1;
                var spriteWidth = Math.Abs((int)(Core.ScreenWidth / transformY)); // int spriteWidth = abs( int (h / (transformY))) 
                var drawStartX = -spriteWidth / 2 + spriteScreenX;
                if (drawStartX < 0)
                    drawStartX = 0;
                var drawEndX = spriteWidth / 2 + spriteScreenX;
                if (drawEndX >= Core.ScreenWidth)
                    drawEndX = Core.ScreenWidth - 1;
                for (int stripe = drawStartX; stripe < drawEndX; stripe++)
                {
                    int texX = (256 * (stripe - (-spriteWidth / 2 + spriteScreenX)) * 64 / spriteWidth) / 256;
                    if (transformY > 0 && stripe > 0 && stripe < Core.ScreenWidth && transformY < Core.RaycastAreas[stripe / Core.LocalBufferSize].LocalZBuffer[stripe % Core.LocalBufferSize])
                    {
                        for (int y = drawStartY; y < drawEndY; y++) //for every pixel of the current stripe
                        {
                            var d = Math.Abs((y) * 256 - Core.ScreenHeight * 128 + spriteHeight * 128); //256 and 128 factors to avoid floats
                            var texY = ((d * 64) / spriteHeight) / 256;
                            Color color;
                            if (enemy.Alive && enemy is Zombi)
                                color = ScreenRender.Textures["Zombi"].GetPixel(texX, texY);
                            else if (enemy is Zombi)
                                color = ScreenRender.Textures["DeadZombi"].GetPixel(texX, texY);
                            else
                                color = ScreenRender.Textures["DoubleBarrel"].GetPixel(texX, texY);
                            if (color.R == 0 && color.G == 0 && color.B == 0)
                                continue;
                            ScreenRender.Buffer[stripe / Core.LocalBufferSize].SetPixel(stripe % Core.LocalBufferSize, y, color);
                        }
                        if (!casted)
                        {
                            CastedEnemies.Add(enemy);
                            casted = true;
                        }

                    }
                }

            }


        }
    }
}
