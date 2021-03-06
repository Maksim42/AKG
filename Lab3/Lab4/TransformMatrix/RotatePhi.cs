﻿using System;

namespace SDLGeometry.TransformMatrix
{
    class RotatePhi
        : Matrix
    {
        public double angle;

        public RotatePhi(double angle)
        {
            this.angle = angle;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            double lx = x;
            double ly = y;
            double lz = z;
            double angleT = angle - Math.PI;

            //// Z
            //x = lx * Math.Cos(angleT) + ly * (-Math.Sin(angleT));
            //y = lx * Math.Sin(angleT) + ly * Math.Cos(angleT);
            //// Y
            //x = lx * Math.Cos(angleT) + lz * Math.Sin(angleT);
            //z = lx * (-Math.Sin(angleT)) + lz * Math.Cos(angleT);
            // X
            y = ly * Math.Cos(angleT) + lz * (-Math.Sin(angleT));
            z = ly * Math.Sin(angleT) + lz * Math.Cos(angleT);
        }
    }
}
