using System.Collections.Generic;
using System.Drawing;
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
        public static int TextureSize = 64;

        public static void InitScreenRender()
        {
            ZBuffer = new double[Core.ScreenWidth];
            Buffer = new List<Bitmap>();
            Textures = new Dictionary<string, Bitmap>();
            LoadTextures(Textures);
        }

        public static void Clear(PaintEventArgs e)
        {
            e.Graphics.Clear(Color.Black);
        }

        public static void DrawBuffer(PaintEventArgs e)
        {
            ScreenGraphics = e.Graphics;
            ScreenGraphics.Clear(Color.Empty);
            var start = 0;
            for (int i = 0; i < Core.CPUNumber; i++)
            {
                ScreenGraphics.DrawImageUnscaled(Buffer[i], start, 0);
                start += Core.LocalBufferSize;
            }
            for (int k = 0; k < Core.CPUNumber; k++)
            {
                var g = Graphics.FromImage(Buffer[k]);
                g.Clear(Color.SkyBlue);
            }
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
                ScreenGraphics.DrawImage(Textures["Shotgun"], new Point(Core.ScreenWidth / 2, Core.ScreenHeight - 100));
            else
                ScreenGraphics.DrawImage(Textures["Arm"], new Point(Core.ScreenWidth / 2, Core.ScreenHeight - 100));
        }


        public static void LoadTextures(Dictionary<string, Bitmap> textures)
        {
            textures["Wall"] = new Bitmap(Properties.Resources.brick2);
            textures["Floor"] = new Bitmap(Properties.Resources.greystone);
            textures["Floor2"] = new Bitmap(Properties.Resources.floor2);
            textures["Sky"] = new Bitmap(Properties.Resources.sky1);
            textures["Zombi"] = new Bitmap(Properties.Resources.zombie_cop);
            textures["DeadZombi"] = new Bitmap(Properties.Resources.dead_zombie);
            textures["Barrel"] = new Bitmap(Properties.Resources.barrel);
            textures["Shotgun"] = new Bitmap(Properties.Resources.bombom1);
            textures["Arm"] = new Bitmap(Properties.Resources.arm1);
            textures["DoubleBarrel"] = new Bitmap(Properties.Resources.DoubleBarel);
        }
    }
}
