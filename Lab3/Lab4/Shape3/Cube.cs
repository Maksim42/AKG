using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLGeometry;
using SDLColor;

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
            // midle
            lines.Add(new Line(transformPoints[0], transformPoints[4])); // 8
            lines.Add(new Line(transformPoints[1], transformPoints[5])); // 9
            lines.Add(new Line(transformPoints[2], transformPoints[6])); // 10
            lines.Add(new Line(transformPoints[3], transformPoints[7])); // 11
        }

        private void SurfaceInit()
        {
            // front
            Surface s = new Surface();
            surfaces.Add(s);
            s.AddBorder(lines[0]);
            s.AddBorder(lines[1]);
            s.AddBorder(lines[2]);
            s.AddBorder(lines[3]);
            s.AddLine(lines[0]);
            s.AddLine(lines[1]);
            s.AddLine(lines[2]);
            s.AddLine(lines[3]);
            // back
            s = new Surface();
            surfaces.Add(s);
            s.AddBorder(lines[4]);
            s.AddBorder(lines[5]);
            s.AddBorder(lines[6]);
            s.AddBorder(lines[7]);
            s.AddLine(lines[4]);
            s.AddLine(lines[5]);
            s.AddLine(lines[6]);
            s.AddLine(lines[7]);
            // midle right
            s = new Surface();
            surfaces.Add(s);
            s.AddBorder(lines[0]);
            s.AddBorder(lines[4]);
            s.AddBorder(lines[8]);
            s.AddBorder(lines[9]);
            s.AddLine(lines[8]);
            s.AddLine(lines[9]);
            s.AddLine(lines[0]);
            s.AddLine(lines[4]);
            // midle left
            s = new Surface();
            surfaces.Add(s);
            s.AddBorder(lines[10]);
            s.AddBorder(lines[11]);
            s.AddBorder(lines[2]);
            s.AddBorder(lines[6]);
            s.AddLine(lines[10]);
            s.AddLine(lines[11]);
            s.AddLine(lines[2]);
            s.AddLine(lines[6]);
            // midle up
            s = new Surface();
            surfaces.Add(s);
            s.AddBorder(lines[8]);
            s.AddBorder(lines[11]);
            s.AddBorder(lines[3]);
            s.AddBorder(lines[7]);
            s.AddLine(lines[11]);
            s.AddLine(lines[8]);
            s.AddLine(lines[7]);
            s.AddLine(lines[3]);
            // midle down
            s = new Surface();
            surfaces.Add(s);
            s.AddBorder(lines[9]);
            s.AddBorder(lines[10]);
            s.AddBorder(lines[1]);
            s.AddBorder(lines[5]);
            s.AddLine(lines[10]);
            s.AddLine(lines[9]);
            s.AddLine(lines[5]);
            s.AddLine(lines[1]);
        }
    }
}
