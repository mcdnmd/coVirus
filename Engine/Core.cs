using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _3DGame
{
    public static class Core
    {
        public static int CPUNumber;
        public static int LocalBufferSize;
        public static Raycast[] RaycastAreas;
        public static int ScreenWidth;
        public static int ScreenHeight;

        public static void InitCore(int width, int height)
        {
            CPUNumber = Environment.ProcessorCount;
            LocalBufferSize = width / CPUNumber;
            ScreenWidth = width;
            ScreenHeight = height;
            ScreenRender.InitScreenRender();
            CreatRaycastingAreas();
        }

        private static void CreatRaycastingAreas()
        {
            RaycastAreas = new Raycast[CPUNumber];
            // overflow maybe
            for (int i = 0; i < ScreenWidth; i += LocalBufferSize) 
            {
                ScreenRender.Buffer.Add(new Bitmap(LocalBufferSize, ScreenHeight));
                RaycastAreas[i / LocalBufferSize] = new Raycast(i,
                i + LocalBufferSize,
                ScreenRender.Buffer[i/LocalBufferSize],
                ScreenWidth,
                ScreenHeight);
            }
        }

        public static void ParallelScreenRendering(PaintEventArgs e)
        {
            Parallel.For(0, CPUNumber, index => {
                var location = new PointF(Game._Player.Location.X, Game._Player.Location.Y);
                var dirVector = new PointF(Game._Player.DirectionVector.X, Game._Player.DirectionVector.Y);
                var planeVector = new PointF(Camera.PlaneVector.X, Camera.PlaneVector.Y);
                RaycastAreas[index].Cast(location, dirVector, planeVector);
            });
        }

    }
}