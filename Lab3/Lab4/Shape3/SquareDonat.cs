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
        private double h;
        private double a;
        private double b;

        public SquareDonat(double h)
            : base()
        {
            this.h = h;
            b = 1.5 * h;
            a = 3 * b;

            PointInit();
            LineInit();
        }

        private void PointInit()
        {
            double halfA = a / 2;
            double halfB = b / 2;
            double halfH = h / 2;

            points.Add(new Point(-halfA, -halfH, -halfA));
            points.Add(new Point(-halfA, halfH, -halfA));
            points.Add(new Point(halfA, halfH, -halfA));
            points.Add(new Point(halfA, -halfH, -halfA));

            points.Add(new Point(-halfA, -halfH, halfA));
            points.Add(new Point(-halfA, halfH, halfA));
            points.Add(new Point(halfA, halfH, halfA));
            points.Add(new Point(halfA, -halfH, halfA));

            points.Add(new Point(-halfB, -halfH, -halfB));
            points.Add(new Point(-halfB, halfH, -halfB));
            points.Add(new Point(halfB, halfH, -halfB));
            points.Add(new Point(halfB, -halfH, -halfB));

            points.Add(new Point(-halfB, -halfH, halfB));
            points.Add(new Point(-halfB, halfH, halfB));
            points.Add(new Point(halfB, halfH, halfB));
            points.Add(new Point(halfB, -halfH, halfB));

            InitTransformPointList();
        }

        private void LineInit()
        {
            lines.Add(new Line(transformPoints[0], transformPoints[1]));
            lines.Add(new Line(transformPoints[1], transformPoints[2]));
            lines.Add(new Line(transformPoints[2], transformPoints[3]));
            lines.Add(new Line(transformPoints[3], transformPoints[0]));

            lines.Add(new Line(transformPoints[4], transformPoints[5]));
            lines.Add(new Line(transformPoints[5], transformPoints[6]));
            lines.Add(new Line(transformPoints[6], transformPoints[7]));
            lines.Add(new Line(transformPoints[7], transformPoints[4]));

            lines.Add(new Line(transformPoints[0], transformPoints[4]));
            lines.Add(new Line(transformPoints[1], transformPoints[5]));
            lines.Add(new Line(transformPoints[2], transformPoints[6]));
            lines.Add(new Line(transformPoints[3], transformPoints[7]));

            // inner 
            lines.Add(new Line(transformPoints[8], transformPoints[9]));
            lines.Add(new Line(transformPoints[9], transformPoints[10]));
            lines.Add(new Line(transformPoints[10], transformPoints[11]));
            lines.Add(new Line(transformPoints[11], transformPoints[8]));

            lines.Add(new Line(transformPoints[12], transformPoints[13]));
            lines.Add(new Line(transformPoints[13], transformPoints[14]));
            lines.Add(new Line(transformPoints[14], transformPoints[15]));
            lines.Add(new Line(transformPoints[15], transformPoints[12]));

            lines.Add(new Line(transformPoints[8], transformPoints[12]));
            lines.Add(new Line(transformPoints[9], transformPoints[13]));
            lines.Add(new Line(transformPoints[10], transformPoints[14]));
            lines.Add(new Line(transformPoints[11], transformPoints[15]));
        }
    }
}
