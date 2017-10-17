using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Lab3.Shapes
{
    class CutWindow : Shape
    {
        public CutWindow(WindowContext context, int width, int height, int x, int y)
        {
            this.context = context;
            this.width = width;
            this.height = height;
            positionX = x;
            positionY = y;

            Redraw();
        }

        #region Properties
        public int Height
        {
            get => height;

            set
            {
                height = CheckSize(height, value);

                Redraw();
            }
        }

        public int Width
        {
            get => width;

            set
            {
                width = CheckSize(width, value);

                Redraw();
            }
        }
        #endregion Properties

        public override void DrawLineInShape(Point p1, Point p2)
        {
            if (! LineTrivialVisible(p1, p2))
            {
                context.DrawDotedLine(p1, p2);
                return;
            }

            p1 = GlobalToLocalTransform(p1);
            p2 = GlobalToLocalTransform(p2);

            Point crossingPoint = null;
            for (int i = 0; i < points.Length - 1; i++)
            {
                crossingPoint = LineCrossing(p1, p2, points[i], points[i + 1]);
            }

            if (crossingPoint != null)
            {
                context.DrawLine(TransformPoint(p1), TransformPoint(crossingPoint));
            }
            else
            {
                context.DrawLine(TransformPoint(p1), TransformPoint(p2));
            }



            //crossingPoint = LineCrossing(p1, p2,
            //                             points[points.Length - 1], points[0]);
        }

        public override bool PointIn(Point p)
        {
            p = GlobalToLocalTransform(p);

            if (p.x < points[0].x && p.x > points[2].x &&
                p.y < points[0].y && p.y > points[2].y)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Check new size parametr and return corect value
        /// </summary>
        /// <param name="curent">Curent parametr value</param>
        /// <param name="newValue">New parametr value</param>
        /// <returns></returns>
        private int CheckSize(int curent, int newValue)
        {
            if ((newValue < 0 ) || (newValue >= 10000))
            {
                return curent;
            }

            return newValue;
        }

        /// <summary>
        /// Redraw point in shape cordinate after change size parametrs
        /// </summary>
        private void Redraw()
        {
            double halfWidth = width / 2.0;
            double halfHeight = height / 2.0;

            points = new Point[] {
                new Point(halfWidth, halfHeight),
                new Point(halfWidth, -halfHeight),
                new Point(-halfWidth, -halfHeight),
                new Point(-halfWidth, halfHeight)
            };
        }
    }
}
