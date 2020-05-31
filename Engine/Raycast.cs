using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3DGame
{
    public static class Raycast
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

        public static void Cast()
        {
            FloorHitting(); 
            for (int x = 0; x < ScreenRender.ScreenWidth; x++)
            {
                //x-coordinate in camera space
                cameraPlaneX = 2 * x / (double) ScreenRender.ScreenWidth - 1;

                Ray.DirectionX = Game._Player.DirectionVector.X + Camera.PlaneVector.X * cameraPlaneX;
                Ray.DirectionY = Game._Player.DirectionVector.Y + Camera.PlaneVector.Y * cameraPlaneX;

                //which box of the map we're in
                MapX = (int)Game._Player.Location.X;
                MapY = (int)Game._Player.Location.Y;

                //length of ray from one x or y-side to next x or y-side
                DeltaDistanceX = (Ray.DirectionY == 0) ? 0 : ((Ray.DirectionX == 0) ? 1 : Math.Abs(1 / Ray.DirectionX));
                DeltaDistanceY = (Ray.DirectionX == 0) ? 0 : ((Ray.DirectionY == 0) ? 1 : Math.Abs(1 / Ray.DirectionY));

                CalculateStepAndSideDistance();
                WallHitting();
                if (HittedSide == 0)
                    WallDistance = (MapX - Game._Player.Location.X + (1 - StepX) / 2) / Ray.DirectionX;
                else
                    WallDistance = (MapY - Game._Player.Location.Y + (1 - StepY) / 2) / Ray.DirectionY;
                ScreenRender.FillPixeledStrip(x);
            }
        }

        private static void FloorHitting()
        {
            for (int y = 0; y < ScreenRender.ScreenHeight; y++)
            {
                Ray.DirX0 = (float)(Game._Player.Location.X - Camera.PlaneVector.X);
                Ray.DirY0 = (float)(Game._Player.Location.Y - Camera.PlaneVector.Y);
                Ray.DirY0 = (float)(Game._Player.Location.X + Camera.PlaneVector.X);
                Ray.DirY0 = (float)(Game._Player.Location.Y + Camera.PlaneVector.Y);

                var position = y - 0.5 * ScreenRender.ScreenHeight;
                var posZ = 0.5 * ScreenRender.ScreenHeight;
                var rowDistance = posZ / position;
                var floorStepX = rowDistance * (Ray.DirX1 - Ray.DirX0) / ScreenRender.ScreenWidth;
                var floorStepY = rowDistance * (Ray.DirY1 - Ray.DirY0) / ScreenRender.ScreenWidth;
                var floorX = Game._Player.Location.X + rowDistance * Ray.DirX0;
                var floorY = Game._Player.Location.Y + rowDistance * Ray.DirY0;

                for (int x = 0; x < ScreenRender.ScreenWidth; x++)
                {
                    var cellX = (int)floorX;
                    var cellY = (int)floorY;
                    int tx = (int)(ScreenRender.TextureSize * (floorX - cellX)) & (ScreenRender.TextureSize - 1);
                    int ty = (int)(ScreenRender.TextureSize * (floorY - cellY)) & (ScreenRender.TextureSize - 1);
                    floorX += floorStepX;
                    floorY += floorStepY;
                    var color = ScreenRender.Textures["Floor"].GetPixel(tx, ty);
                    ScreenRender.Buffer.SetPixel(x, y, color);
                }
            }
           

        }

        private static void WallHitting()
        {
            var hit = 0;
            while (hit == 0)
            {
                if(SideDistanceX < SideDistanceY)
                {
                    SideDistanceX += DeltaDistanceX;
                    MapX += StepX;
                    HittedSide = 0;
                }
                else
                {
                    SideDistanceY += DeltaDistanceY;
                    MapY += StepY;
                    HittedSide = 1;
                }
                // Probably should be changed [MapY, MapX]
                if (Map.TileMap[MapX, MapY] == 1)
                    hit = 1;
            }
        }

        private static void CalculateStepAndSideDistance()
        {
            if (Ray.DirectionX < 0)
            {
                StepX = -1;
                SideDistanceX = (Game._Player.Location.X - MapX) * DeltaDistanceX;
            }
            else
            {
                StepX = 1;
                SideDistanceX = (MapX + 1.0 - Game._Player.Location.X) * DeltaDistanceX;
            }
            if (Ray.DirectionY < 0)
            {
                StepY = -1;
                SideDistanceY = (Game._Player.Location.Y - MapY) * DeltaDistanceY;
            }
            else
            {
                StepY = 1;
                SideDistanceY = (MapY + 1.0 - Game._Player.Location.Y) * DeltaDistanceY;
            }
        }
    }
}
