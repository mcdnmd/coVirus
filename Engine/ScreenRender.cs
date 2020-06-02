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
        public static List<Bitmap> Buffer;
        public static double[] ZBuffer;
        public static Dictionary<string, Bitmap> Textures;
        private static Rectangle skyRectangle;
        private static Rectangle floorRectangle;
        private static TextureBrush textureSky;
        public static int TextureSize = 64;

        public static void InitScreenRender()
        {
            ZBuffer = new double[Core.ScreenWidth];
            Buffer = new List<Bitmap>();
            Textures = new Dictionary<string, Bitmap>();
            skyRectangle = new Rectangle(0, 0, Core.ScreenWidth, Core.ScreenHeight / 2);
            floorRectangle = new Rectangle(0, Core.ScreenHeight / 2, Core.ScreenWidth, Core.ScreenHeight / 2);
            LoadTextures();
            textureSky = new TextureBrush(Textures["Sky"]);
        }

        public static void Clear(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
        }

        public static void DrawBuffer(PaintEventArgs e)
        {
            ScreenGraphics = e.Graphics;
            ScreenGraphics.Clear(Color.Black);
            var start = 0;
            for (int i = 0; i < Core.CPUNumber; i++)
            {
                ScreenGraphics.DrawImageUnscaled(Buffer[i], start, 0);
                start += Core.LocalBufferSize;
            }
            for (int k = 0; k < Core.CPUNumber; k++)
            {
                var g = Graphics.FromImage(Buffer[k]);
                g.Clear(Color.White);
            }
        }

        public static void DrawBackground(PaintEventArgs e)
        {
            ScreenGraphics = e.Graphics;
            ScreenGraphics.FillRectangle(textureSky, skyRectangle);
            ScreenGraphics.FillRectangle(Brushes.Gray, floorRectangle);
        }

        public static void DrawWeapon()
        {
            //var weaponHeight = (int)(Core.ScreenHeight / 64);
            //var weaponWidth = (int)(Core.ScreenHeight / 64);
            //var drawStartY = Core.ScreenHeight - weaponHeight;
            //var drawEndY = Core.ScreenHeight;
            //var drawStartX = -weaponWidth / 2 + Core.ScreenWidth;
            //var drawEndX = weaponWidth / 2 + Core.ScreenWidth;
            //for (int stripe = drawStartX; stripe < drawEndX; stripe++)
            //{
            //    int texX = (256 * (stripe - (-weaponWidth / 2 + Core.ScreenWidth)) * 64 / weaponWidth) / 256;  
            //    for (int y = drawStartY; y < drawEndY; y++) //for every pixel of the current stripe
            //    {
            //        var texY = 64 / weaponHeight ;
            //        Color color;
            //        if (Game._Player.Weapon is Shotgun)
            //            color = Textures["Shotgun"].GetPixel(texX, texY);
            //        else
            //            color = Textures["Arm"].GetPixel(texX, texY);
            //        var index = stripe / Core.LocalBufferSize;
            //        if (index >= Core.CPUNumber)
            //            index = Core.CPUNumber - 1;
            //        Buffer[index].SetPixel(stripe % Core.LocalBufferSize, y, color);
            //    }
            //}
            if (Game._Player.Weapon is Shotgun)
                ScreenGraphics.DrawImage(Textures["Shotgun"],new Point(Core.ScreenWidth/2, Core.ScreenHeight-100));
            else
                ScreenGraphics.DrawImage(Textures["Arm"], new Point(Core.ScreenWidth / 2, Core.ScreenHeight - 100));
        }


        private static void LoadTextures()
        {
            Textures["Wall"] = new Bitmap(Properties.Resources.brick2);
            Textures["Floor"] = new Bitmap(Properties.Resources.greystone);
            Textures["Floor2"] = new Bitmap(Properties.Resources.floor2);
            Textures["Sky"] = new Bitmap(Properties.Resources.sky1);
            Textures["Zombi"] = new Bitmap(Properties.Resources.zombie_cop);
            Textures["DeadZombi"] = new Bitmap(Properties.Resources.dead_zombie);
            Textures["Barrel"] = new Bitmap(Properties.Resources.barrel);
            Textures["Shotgun"] = new Bitmap(Properties.Resources.bombom1);
            Textures["Arm"] = new Bitmap(Properties.Resources.arm1);
        }
    }
}
