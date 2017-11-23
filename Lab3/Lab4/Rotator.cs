using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLGeometry.TransformMatrix;

namespace SDLGeometry
{
    class Rotator
    {
        public double angle = 0;
        private RotateX rotateX, arotateX;
        private RotateY rotateY, aroteteY;
        private RotateZ rotateZ;
        private Move move, amove;

        public Rotator(Point p1, Point p2)
        {
            rotateX = new RotateX(0);
            rotateY = new RotateY(0);
            arotateX = new RotateX(0);
            aroteteY = new RotateY(0);
            rotateZ = new RotateZ(0);
            move = new Move(-p1.rX, -p1.rY, 0);
            amove = new Move(p1.rX, p1.rY, 0);
            

            // calculate rotate matrix
            Point c = (p1 - p2);
            double k = Math.Sqrt(Math.Pow(c.rX, 2) + Math.Pow(c.rY, 2) + Math.Pow(c.rZ, 2));
            c = c / k;
            double d = Math.Sqrt(Math.Pow(c.rY, 2) + Math.Pow(c.rZ, 2));
            rotateX.angle = Math.Asin(c.rY / d);
            rotateY.angle = Math.Asin(c.rX);
            arotateX.angle = -rotateX.angle;
            aroteteY.angle = -rotateY.angle;
        }

        public Point Rotate(Point p)
        {
            rotateZ.angle = angle;

            p.T(move).T(rotateX).T(rotateY)
             .T(rotateZ)
             .T(aroteteY).T(arotateX).T(amove);

            return p;
        }
    }
}
