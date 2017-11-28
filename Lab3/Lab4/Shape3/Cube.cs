using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLGeometry;

namespace Shape3
{
    class Cube
        : Shape3C
    {
        private double halfSide;

        public Cube(double side)
            : base()
        {
            halfSide = side / 2;
            
            PointInit();
            LineInit();
            SurfaceInit();
        }

        private void PointInit()
        {
            // back
            points.Add(new Point(-halfSide, -halfSide, -halfSide));
            points.Add(new Point(-halfSide, halfSide, -halfSide));
            points.Add(new Point(halfSide, halfSide, -halfSide));
            points.Add(new Point(halfSide, -halfSide, -halfSide));
            // front
            points.Add(new Point(-halfSide, -halfSide, halfSide));
            points.Add(new Point(-halfSide, halfSide, halfSide));
            points.Add(new Point(halfSide, halfSide, halfSide));
            points.Add(new Point(halfSide, -halfSide, halfSide));

            InitTransformPointList();

            rotator = new Rotator(points[4], points[2]);
        }

        private void LineInit()
        {
            // back
            lines.Add(new Line(transformPoints[0], transformPoints[1])); // 0
            lines.Add(new Line(transformPoints[1], transformPoints[2])); // 1
            lines.Add(new Line(transformPoints[2], transformPoints[3])); // 2
            lines.Add(new Line(transformPoints[3], transformPoints[0])); // 3
            // front
            lines.Add(new Line(transformPoints[4], transformPoints[5])); // 4
            lines.Add(new Line(transformPoints[5], transformPoints[6])); // 5
            lines.Add(new Line(transformPoints[6], transformPoints[7])); // 6
            lines.Add(new Line(transformPoints[7], transformPoints[4])); // 7

            // back
            lines.Add(new Line(transformPoints[0], transformPoints[4]));
            lines.Add(new Line(transformPoints[1], transformPoints[5]));
            lines.Add(new Line(transformPoints[2], transformPoints[6]));
            lines.Add(new Line(transformPoints[3], transformPoints[7]));
        }

        private void SurfaceInit()
        {
            Surface s = new Surface();
            surfaces.Add(s);
            s.AddBorder(lines[0]);
            s.AddBorder(lines[1]);
            s.AddBorder(lines[2]);
            s.AddBorder(lines[3]);

        }
    }
}
