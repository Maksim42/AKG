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

        PartScale tRight = new PartScale(1, 1, -1);

        public Rotator(Point rp1, Point rp2)
        {
            rotateX = new RotateX(0);
            rotateY = new RotateY(0);
            arotateX = new RotateX(0);
            aroteteY = new RotateY(0);
            rotateZ = new RotateZ(0);

            Point p1;
            Point p2;
            p1 = new Point(rp1);
            p2 = new Point(rp2);
            //if (rp1.rZ > rp2.rZ)
            //{
            //    p1 = new Point(rp1);
            //    p2 = new Point(rp2);
            //}
            //else
            //{
            //    p1 = new Point(rp2);
            //    p2 = new Point(rp1);
            //}

            //p1.T(tRight);
            //p2.T(tRight);

            Console.WriteLine("P1:{0}\nP2:{1}", p1, p2);

            move = new Move(-p1.rX, -p1.rY, -p1.rZ);
            amove = new Move(p1.rX, p1.rY, p1.rZ);


            // calculate rotate matrix
            Point c;
            if (p1.rZ > p2.rZ)
            {
                c = (p1 - p2);
            }
            else
            {
                c = (p2 - p1);
            }
            double k = Math.Sqrt(Math.Pow(c.rX, 2) + Math.Pow(c.rY, 2) + Math.Pow(c.rZ, 2));
            c = c / k;
            Console.WriteLine("c:{0}", c);
            double d = Math.Sqrt(Math.Pow(c.rY, 2) + Math.Pow(c.rZ, 2));
            rotateX.angle = Math.Asin(c.rY / d);
            rotateY.angle = -Math.Asin(c.rX);
            arotateX.angle = -rotateX.angle;
            aroteteY.angle = -rotateY.angle;

            Func<double, double> RtG = (double r) => r * 180 / Math.PI;
            Console.WriteLine("x:{0} y:{1}", RtG(rotateX.angle), RtG(rotateY.angle));
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
