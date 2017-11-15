using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLGeometry;

namespace Shape3
{
    class SquareDonat
        : Shape3C
    {
        public SquareDonat()
            : base()
        {
            PointInit();
            LineInit();
        }

        private void PointInit()
        {
            points.Add(new Point(0, 0, 0));
            points.Add(new Point(0, 7, 0));
            points.Add(new Point(10, 7, 0));
            points.Add(new Point(10, 0, 0));

            points.Add(new Point(0, 0, 10));
            points.Add(new Point(0, 7, 10));
            points.Add(new Point(10, 7, 10));
            points.Add(new Point(10, 0, 10));

            points.Add(new Point(2.5, 0, 2.5));
            points.Add(new Point(2.5, 7, 2.5));
            points.Add(new Point(7.5, 7, 2.5));
            points.Add(new Point(7.5, 0, 2.5));

            points.Add(new Point(2.5, 0, 7.5));
            points.Add(new Point(2.5, 7, 7.5));
            points.Add(new Point(7, 7, 7.5));
            points.Add(new Point(7, 0, 7.5));
        }

        private void LineInit()
        {
            lines.Add(new Line(points[0], points[1]));
            lines.Add(new Line(points[1], points[2]));
            lines.Add(new Line(points[2], points[3]));
            lines.Add(new Line(points[3], points[0]));

            lines.Add(new Line(points[4], points[5]));
            lines.Add(new Line(points[5], points[6]));
            lines.Add(new Line(points[6], points[7]));
            lines.Add(new Line(points[7], points[4]));

            lines.Add(new Line(points[0], points[4]));
            lines.Add(new Line(points[1], points[5]));
            lines.Add(new Line(points[2], points[6]));
            lines.Add(new Line(points[3], points[7]));

            // inner 
            lines.Add(new Line(points[8], points[9]));
            lines.Add(new Line(points[9], points[10]));
            lines.Add(new Line(points[10], points[11]));
            lines.Add(new Line(points[11], points[8]));

            lines.Add(new Line(points[12], points[13]));
            lines.Add(new Line(points[13], points[14]));
            lines.Add(new Line(points[14], points[15]));
            lines.Add(new Line(points[15], points[12]));

            lines.Add(new Line(points[8], points[12]));
            lines.Add(new Line(points[9], points[13]));
            lines.Add(new Line(points[10], points[14]));
            lines.Add(new Line(points[11], points[15]));
        }
    }
}
