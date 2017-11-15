using System;

namespace SDLGeometry.TransformMatrix
{
    class RotateX
        : Matrix
    {
        public double angle;

        public RotateX(double angle)
        {
            this.angle = angle;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            double lx = x;
            double ly = y;
            double lz = z;

            y = ly * Math.Cos(angle) + lz * (-Math.Sin(angle));
            z = ly * Math.Sin(angle) + lz * Math.Cos(angle);
        }
    }
}
