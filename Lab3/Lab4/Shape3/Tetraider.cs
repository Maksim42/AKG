using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLGeometry;

namespace Shape3
{
    class Tetraider : Shape3C
    {
        private double halfSide;
        private double hHeight;
        private double h;

        public Tetraider(double side)
            : base()
        {
            halfSide = side / 2;
            hHeight = (Math.Sqrt(2 / 3.0) * side) / 3.0;
            double hP = (3 * side) / 2.0;
            h = (2 * Math.Sqrt(hP * Math.Pow(hP - side, 3))) / side / 3;

            Console.WriteLine(hHeight);

            PointInit();
            LineInit();
        }

        private void PointInit()
        {
            points.Add(new Point(0, 2 * hHeight, 0)); // top
            points.Add(new Point(-halfSide, -hHeight, -h));
            points.Add(new Point(halfSide, -hHeight, -h));
            points.Add(new Point(0, -hHeight, 2 * h));

            InitTransformPointList();

            rotator = new Rotator(points[0], points[3]);
            //rotator = new Rotator(new Point(3, 2, 2), new Point(2, 1, 1));
        }

        private void LineInit()
        {
            lines.Add(new Line(transformPoints[0], transformPoints[1]));
            lines.Add(new Line(transformPoints[0], transformPoints[2]));
            lines.Add(new Line(transformPoints[0], transformPoints[3]));
            lines.Add(new Line(transformPoints[1], transformPoints[2]));
            lines.Add(new Line(transformPoints[2], transformPoints[3]));
            lines.Add(new Line(transformPoints[3], transformPoints[1]));
        }
    }
}
