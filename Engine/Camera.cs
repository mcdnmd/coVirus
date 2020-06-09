using System;
using System.Drawing;

namespace _3DGame
{
    public static class Camera
    {
        public static PointF PlaneVector;

        public static void CreatePlaneVector()
        {
            var tan = Math.Tan(ScreenRender.FieldOfView / 2);
            var x = 0;
            var y = (float)Math.Round(tan, 2);
            PlaneVector = new PointF(0, 0.66f);
        }

    }
}
