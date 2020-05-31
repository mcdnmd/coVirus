using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DGame
{
    public static class ScreenRender
    {
        private static Graphics ScreenGraphics;
        public static int FieldOfView = 60;
        public static int ScreenWidth { get; private set; }
        public static int ScreenHeight { get; private set; }
        public static Bitmap Buffer;
        public static Dictionary<string, Bitmap> Textures;
        public static int TextureSize = 64;



        private static SolidBrush brush;

        public static void InitScreenRender(int width, int height)
        {
            ScreenWidth = width;
            ScreenHeight = height;
            Buffer = new Bitmap(ScreenWidth, ScreenHeight);
            Textures = new Dictionary<string, Bitmap>();
            LoadTextures();
        }

        public static void Raycasting(PaintEventArgs e)
        {
            ScreenGraphics = e.Graphics;
            Raycast.Cast();
            //Raycast.ParallelCast();
        }

        public static void Clear(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
        }

        public static void FillPixeledStrip(int x)
        {
            var lineHeight = (int)(ScreenHeight / Raycast.WallDistance);
            var drawStart = -lineHeight / 2 + ScreenHeight / 2;
            if (drawStart < 0)
                drawStart = 0;
            int drawEnd = lineHeight / 2 + ScreenHeight / 2;
            if (drawEnd >= ScreenHeight)
                drawEnd = ScreenHeight - 1;
            double wallX;
            if (Raycast.HittedSide == 0) 
                wallX = Game._Player.Location.Y + Raycast.WallDistance * Ray.DirectionY;
            else 
                wallX = Game._Player.Location.X + Raycast.WallDistance * Ray.DirectionX;
            wallX -= Math.Floor(wallX);
            int texX = (int)(wallX * TextureSize);
            if ((Raycast.HittedSide == 0 && Ray.DirectionX > 0) || (Raycast.HittedSide == 1 && Ray.DirectionY < 0)) 
                texX = TextureSize - texX - 1;
            double step = 1.0 * TextureSize / lineHeight;
            double texPos = (drawStart - ScreenHeight / 2 + lineHeight / 2) * step;
            for (int y = drawStart; y < drawEnd; y++)
            {
                int texY = (int)texPos & (TextureSize - 1);
                texPos += step;
                var color = Textures["Wall"].GetPixel(texX,texY);
                //if (Raycast.HittedSide == 1) 
                //    color = Color.FromArgb(color.R / 2, color.G / 2, color.B / 2);
                Buffer.SetPixel(x, y, color);
            }
        }

        public static void DrawBuffer()
        {
            ScreenGraphics.DrawImageUnscaled(Buffer, 0, 0);
            for (int i = 0; i < ScreenHeight; i++)
                for (int j = 0; j < ScreenWidth; j++)
                    Buffer.SetPixel(j, i, Color.Empty);
        }

        private static void LoadTextures()
        {
            Textures["Wall"] = new Bitmap(Properties.Resources.brick2);
            Textures["Floor"] = new Bitmap(Properties.Resources.floor);
        }


    }
}
