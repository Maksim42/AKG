using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Shapes
{
    class Trapeze : Shape
    {
        /// <summary>
        /// Trapeze constructor
        /// </summary>
        /// <param name="context">Shape context</param>
        /// <param name="width">Shape width</param>
        /// <param name="height">Shape height</param>
        /// <param name="topProportion">Proportion top side to botom side</param>
        /// <param name="x">Shape center X position</param>
        /// <param name="y">Shape center Y position</param>
        public Trapeze(WindowContext context, int width, int height, double topProportion, int x, int y)
        {
            this.context = context;
            this.height = height;
            this.width = width;
            positionX = x;
            positionY = y;

            double halfWidth = width / 2.0;
            double halfHeight = height / 2.0;
            double topSide = width * topProportion;

            points = new Point[] {
                new Point(halfWidth, halfHeight),
                new Point(halfWidth, -halfHeight),
                new Point(-halfWidth, -halfHeight),
                new Point(halfWidth - topSide, halfHeight)
            };
        }

        public override void DrawLineInShape(Point p1, Point p2)
        {

        }


        public override bool PointIn(Point p)
        {
            p = GlobalToLocalTransform(p);

            if (p.x <= points[0].x && p.x >= points[2].x &&
                p.y <= points[0].y && p.y >= points[2].y)
            {
                return true;
            }

            return false;
        }
    }
}
