using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Shapes
{
    class Ellipse : Shape
    {
        /// <summary>
        /// Focus distance for curent ellipse
        /// </summary>
        private double focusDistance;
        /// <summary>
        /// Focus points for curent ellipse
        /// </summary>
        private Point focus1, focus2;

        /// <summary>
        /// Create Ellipse shape
        /// </summary>
        /// <param name="context">Window context</param>
        /// <param name="width">Shape width</param>
        /// <param name="height">Shape height</param>
        /// <param name="x">X position on window context</param>
        /// <param name="y">Y position on window context</param>
        /// <param name="aproximateStep">Frequency finding ellipse points</param>
        public Ellipse(WindowContext context, int width, int height, int x, int y, int aproximateStep = 3)
        {
            this.context = context;
            this.height = height;
            this.width = width;
            positionX = x;
            positionY = y;
            focusDistance = Math.Max(width, height);

            int halfWidth = (int)Math.Floor(width / 2.0);
            int halfHeight = (int)Math.Floor(height / 2.0);

            // Find focus offset
            int c = (int)Math.Sqrt(Math.Pow(halfWidth, 2) - Math.Pow(halfHeight, 2));
            // Find focus points
            focus1 = new Point(c, 0);
            focus2 = new Point(-c, 0);

            List<Point> tempPoint = new List<Point>();

            for (int i = -halfWidth; i <= halfWidth; i += aproximateStep)
            {
                tempPoint.Add(new Point(i,
                                        Math.Sqrt((1 - Math.Pow(i,2) /
                                        Math.Pow(halfWidth, 2)) * Math.Pow(halfHeight, 2))));
            }

            for (int i = halfWidth; i >= -halfWidth; i -= aproximateStep)
            {
                tempPoint.Add(new Point(i,
                                        -Math.Sqrt((1 - Math.Pow(i, 2) /
                                        Math.Pow(halfWidth, 2)) * Math.Pow(halfHeight, 2))));
            }

            points = tempPoint.ToArray();
        }

        public override void Draw()
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                context.DrawLine(TransformPoint(points[i]),
                                 TransformPoint(points[i + 1]));
            }
            context.DrawLine(TransformPoint(points[points.Length - 1]),
                             TransformPoint(points[0]));
        }

        public override bool PointIn(Point p)
        {
            p = GlobalToLocalTransform(p);

            double d1 = FindDistance(focus1, p);
            double d2 = FindDistance(focus2, p);

            if (d1 + d2 <= focusDistance)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Find distacnce between two points
        /// </summary>
        /// <param name="p1">First point</param>
        /// <param name="p2">Second point</param>
        /// <returns>Distance</returns>
        private double FindDistance(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.x - p2.x, 2) + Math.Pow(p1.y - p2.y, 2));
        }
    }
}
