using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace _3DGame
{
    public class Raycast
    {
        public double cameraPlaneX;
        public int MapX;
        public int MapY;
        public double DeltaDistanceX;
        public double DeltaDistanceY;
        public double SideDistanceX;
        public double SideDistanceY;
        public int StepX;
        public int StepY;
        public int HittedSide;
        public double WallDistance;

        public int StartStrip;
        public int EndStrip;
        public Ray _Ray;
        public Bitmap Buffer;
        public int ScreenHeight;
        public int ScreenWidth;
        public PointF DirectionVector;
        public PointF PlaneVector;
        public PointF Location;

        public Raycast(int start, int end, Bitmap buffer, int width, int height)
        {
            StartStrip = start;
            EndStrip = end;
            _Ray = new Ray();
            Buffer = buffer;
            ScreenWidth = width;
            ScreenHeight = height;
        }   

        public void Cast(PointF location, PointF dirVector, PointF planeVector)
        {
            Location = location;
            DirectionVector = dirVector;
            PlaneVector = planeVector;
            FloorHitting();
            for (int x = StartStrip; x < EndStrip; x++)
            {
                //x-coordinate in camera space
                cameraPlaneX = 2 * x / (double) ScreenWidth - 1;

                _Ray.DirectionX = DirectionVector.X + PlaneVector.X * cameraPlaneX;
                _Ray.DirectionY = DirectionVector.Y + PlaneVector.Y * cameraPlaneX;

                //which box of the map we're in
                MapX = (int)Location.X;
                MapY = (int)Location.Y;

                //length of ray from one x or y-side to next x or y-side
                DeltaDistanceX = Math.Abs(1 / _Ray.DirectionX);
                DeltaDistanceY = Math.Abs(1 / _Ray.DirectionY);

                CalculateStepAndSideDistance();
                WallHitting();
                if (HittedSide == 0)
                    WallDistance = (MapX - Location.X + (1 - StepX) / 2) / _Ray.DirectionX;
                else
                    WallDistance = (MapY - Location.Y + (1 - StepY) / 2) / _Ray.DirectionY;
                FillPixeledStrip(x);
            }
        }

        private void FillPixeledStrip(int x)
        {
            var lineHeight = (int)(ScreenHeight / WallDistance);
            var drawStart = -lineHeight / 2 + ScreenHeight / 2;
            if (drawStart < 0)
                drawStart = 0;
            int drawEnd = lineHeight / 2 + ScreenHeight / 2;
            if (drawEnd >= ScreenHeight)
                drawEnd = ScreenHeight - 1;
            double wallX;
            if (HittedSide == 0)
                wallX = Location.Y + WallDistance * _Ray.DirectionY;
            else
                wallX = Location.X + WallDistance * _Ray.DirectionX;
            wallX -= Math.Floor(wallX);
            int texX = (int)(wallX * 64);
            if ((HittedSide == 0 && _Ray.DirectionX > 0) || (HittedSide == 1 && _Ray.DirectionY < 0))
                texX = 64 - texX - 1;
            double step = 1.0 * 64 / lineHeight;
            double texPos = (drawStart - ScreenHeight / 2 + lineHeight / 2) * step;
            for (int y = drawStart; y < drawEnd; y++)
            {
                int texY = (int)texPos & (64 - 1);
                texPos += step;
                Color color;
                lock (ScreenRender.Textures) 
                { 
                    color = ScreenRender.Textures["Wall"].GetPixel(texX, texY); 
                }
                if (HittedSide == 1) 
                    color = Color.FromArgb(color.R / 2, color.G / 2, color.B / 2);
                Buffer.SetPixel(x % (EndStrip - StartStrip), y, color);
                lock (ScreenRender.ZBuffer)
                {
                    ScreenRender.ZBuffer[x] = WallDistance;
                }
            }
        }

        private void FloorHitting()
        {
            _Ray.DirX0 = Location.X - PlaneVector.X;
            _Ray.DirY0 = Location.Y - PlaneVector.Y;
            _Ray.DirX1 = Location.X + PlaneVector.X;
            _Ray.DirY1 = Location.Y + PlaneVector.Y;
            for (int y = ScreenHeight / 2; y < ScreenHeight; y++)
            {
                var position = y - ScreenHeight / 2;
                var posZ = 0.5 * ScreenHeight;
                var rowDistance = posZ / position;
                var floorStepX = rowDistance * (_Ray.DirX1 - _Ray.DirX0) / ScreenWidth;
                var floorStepY = rowDistance * (_Ray.DirY1 - _Ray.DirY0) / ScreenWidth;
                var floorX = Location.X + rowDistance * _Ray.DirX0;
                var floorY = Location.Y + rowDistance * _Ray.DirY0;

                for (int x = StartStrip; x < EndStrip; x++)
                {
                    var cellX = (int)floorX;
                    var cellY = (int)floorY;
                    int tx = (int)(64 * (floorX - cellX)) & (64 - 1);
                    int ty = (int)(64 * (floorY - cellY)) & (64 - 1);
                    floorX += floorStepX;
                    floorY += floorStepY;
                    Color color;
                    lock (ScreenRender.Textures)
                    {
                        color = ScreenRender.Textures["Floor"].GetPixel(tx, ty);
                    }
                    Buffer.SetPixel(x % (EndStrip - StartStrip), y, color);
                }
            } 
        }

        private void WallHitting()
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
                lock (Map.TileMap) {
                    if (Map.TileMap[MapX, MapY] == 1)
                        hit = 1;
                }
            }
        }

        

        private void CalculateStepAndSideDistance()
        {
            if (_Ray.DirectionX < 0)
            {
                StepX = -1;
                SideDistanceX = (Location.X - MapX) * DeltaDistanceX;
            }
            else
            {
                StepX = 1;
                SideDistanceX = (MapX + 1.0 - Location.X) * DeltaDistanceX;
            }
            if (_Ray.DirectionY < 0)
            {
                StepY = -1;
                SideDistanceY = (Location.Y - MapY) * DeltaDistanceY;
            }
            else
            {
                StepY = 1;
                SideDistanceY = (MapY + 1.0 - Location.Y) * DeltaDistanceY;
            }
        }
    }
}
