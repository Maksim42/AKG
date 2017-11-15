using System;

namespace SDLGeometry.TransformMatrix
{
    class RotateY
        : Matrix
    {
        public double angle;

        public RotateY(double angle)
        {
            this.angle = angle;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            double lx = x;
            double ly = y;
            double lz = z;

            x = lx * Math.Cos(angle) + lz * Math.Sin(angle);
            z = lx * (-Math.Sin(angle)) + lz * Math.Cos(angle);
        }
    }
}
