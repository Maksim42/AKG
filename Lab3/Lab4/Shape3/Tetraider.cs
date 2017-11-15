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
        public Tetraider()
            : base()
        {
            PointInit();
            LineInit();
        }

        private void PointInit()
        {
            points.Add(new Point(5, 10, 5)); // top
            points.Add(new Point(5, 0, 0));
            points.Add(new Point(0, 0, 10));
            points.Add(new Point(10, 0, 10));
        }

        private void LineInit()
        {
            lines.Add(new Line(points[0], points[1]));
            lines.Add(new Line(points[0], points[2]));
            lines.Add(new Line(points[0], points[3]));
            lines.Add(new Line(points[1], points[2]));
            lines.Add(new Line(points[2], points[3]));
            lines.Add(new Line(points[3], points[1]));
        }
    }
}
