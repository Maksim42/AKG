using System;

namespace SDLGeometry.TransformMatrix
{
    class RotateZ
        : Matrix
    {
        public double angle;

        public RotateZ(double angle)
        {
            this.angle = angle;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            double lx = x;
            double ly = y;

            x = lx * Math.Cos(angle) + ly * (-Math.Sin(angle));
            y = lx * Math.Sin(angle) + ly * Math.Cos(angle);
        }
    }
}
