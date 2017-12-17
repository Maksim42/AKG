using System;

namespace SDLGeometry.TransformMatrix
{
    class PerspectiveMove
        : Matrix
    {
        public double distance;
        public double phi;
        public double thet;

        public PerspectiveMove(double distance, double phi, double thet)
        {
            this.distance = distance;
            this.phi = phi;
            this.thet = thet;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            //x -= distance * Math.Sin(phi) * Math.Sin(thet);
            //y -= distance * Math.Cos(phi);
            //z -= distance * Math.Sin(phi) * Math.Cos(thet);

            y -= distance * Math.Sin(phi) * Math.Sin(thet);
            z -= distance * Math.Cos(phi);
            x -= distance * Math.Sin(phi) * Math.Cos(thet);
        }
    }
}
