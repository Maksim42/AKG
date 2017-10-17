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
            double tLower = 0;
            double tUpper = 1;
            Point d = p2 - p1;
            double t;

            for (int i = 0; i < points.Length; i++)
            {
                Point sidePoint0 = points[i];
                Point sidePoint1 = (i < points.Length - 1) ? (points[i + 1])
                                   : (points[0]);
                Point n = new Point(sidePoint1.y - sidePoint0.y,
                                    sidePoint0.x - sidePoint1.x);
                Point f = sidePoint0;
                Point w = p1 - f;
                double dScalar = Point.ScalarMultiplication(d, n);
                double wScalar = Point.ScalarMultiplication(w, n);

                if (Math.Abs(dScalar) < double.Epsilon)
                {
                    if (wScalar < 0)
                    {
                        tLower = 2;
                        break;
                    }
                }
                else
                {
                    t = -wScalar / dScalar;
                    if (dScalar > 0)
                    {
                        if (t > 1)
                        {
                            tLower = 2;
                            break;
                        }

                        tLower = Math.Max(t, tLower);
                        continue;
                    }
                    if (t < 0)
                    {
                        tLower = 2;
                        break;
                    }

                    tUpper = Math.Min(t, tUpper);
                }
            }
            if (tLower < tUpper)
            {
                Point lowerPoint = p1 + (p2 - p1) * tLower;
                Point upperPoint = p1 + (p2 - p1) * tUpper;
                context.DrawDotedLine(TransformPoint(p1),
                                      TransformPoint(lowerPoint));
                context.DrawLine(TransformPoint(lowerPoint),
                                 TransformPoint(upperPoint));
                context.DrawDotedLine(TransformPoint(upperPoint),
                                      TransformPoint(p2));
                return;
            }

            context.DrawDotedLine(TransformPoint(p1),
                                  TransformPoint(p2));

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
