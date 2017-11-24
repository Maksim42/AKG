using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLGeometry;

namespace Shape3
{
    class Arrows
        : Shape3C
    {
        private double length;
        private double shift;

        public Arrows(double side)
            : base()
        {
            length = side;
            shift = side * 0.1;

            PointInit();
            LineInit();
        }

        private void PointInit()
        {
            points.Add(new Point(0, 0, 0));
            // X
            points.Add(new Point(length, 0, 0));
            points.Add(new Point(length - shift * 2, -shift, -shift));
            points.Add(new Point(length - shift * 2, -shift, shift));
            points.Add(new Point(length - shift * 2, shift, shift));
            points.Add(new Point(length - shift * 2, shift, -shift));
            // Y
            points.Add(new Point(0, length, 0));
            points.Add(new Point(-shift, length - shift * 2, -shift));
            points.Add(new Point(-shift, length - shift * 2, shift));
            points.Add(new Point(shift, length - shift * 2, shift));
            points.Add(new Point(shift, length - shift * 2, -shift));
            // Z
            points.Add(new Point(0, 0, length));
            points.Add(new Point(-shift, -shift, length - shift * 2));
            points.Add(new Point(-shift, shift, length - shift * 2));
            points.Add(new Point(shift, shift, length - shift * 2));
            points.Add(new Point(shift, -shift, length - shift * 2));

            InitTransformPointList();

            rotator = new Rotator(points[4], points[2]);
        }

        private void LineInit()
        {
            // X
            lines.Add(new Line(transformPoints[0], transformPoints[1]));
            lines.Add(new Line(transformPoints[1], transformPoints[2]));
            lines.Add(new Line(transformPoints[1], transformPoints[3]));
            lines.Add(new Line(transformPoints[1], transformPoints[4]));
            lines.Add(new Line(transformPoints[1], transformPoints[5]));
            lines.Add(new Line(transformPoints[2], transformPoints[4]));
            lines.Add(new Line(transformPoints[3], transformPoints[5]));
            // Y
            lines.Add(new Line(transformPoints[0], transformPoints[6]));
            lines.Add(new Line(transformPoints[6], transformPoints[7]));
            lines.Add(new Line(transformPoints[6], transformPoints[8]));
            lines.Add(new Line(transformPoints[6], transformPoints[9]));
            lines.Add(new Line(transformPoints[6], transformPoints[10]));
            lines.Add(new Line(transformPoints[7], transformPoints[9]));
            lines.Add(new Line(transformPoints[8], transformPoints[10]));
            // Z
            lines.Add(new Line(transformPoints[0], transformPoints[11]));
            lines.Add(new Line(transformPoints[11], transformPoints[12]));
            lines.Add(new Line(transformPoints[11], transformPoints[13]));
            lines.Add(new Line(transformPoints[11], transformPoints[14]));
            lines.Add(new Line(transformPoints[11], transformPoints[15]));
            lines.Add(new Line(transformPoints[12], transformPoints[14]));
            lines.Add(new Line(transformPoints[13], transformPoints[15]));

        }
    }
}
