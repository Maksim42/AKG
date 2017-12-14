using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLWindow;

namespace SDLGeometry
{
    class Surface
    {
        private static WindowContext context;

        private List<Line> borders;
        private List<Line> lines;
        private bool valid; // ???
        // Surface index
        private double a, b, c, d;

        public Surface()
        {
            valid = false;
            borders = new List<Line>();
            lines = new List<Line>();
        }

        public static void SetContext(WindowContext context)
        {
            Surface.context = context;
        }

        public void AddBorder(Line border)
        {
            borders.Add(border);
        }

        public void AddLine(Line line)
        {
            line.AttachSurface(this);
            //lines.Add(line);
        }

        public void Unvalidate()
        {
            valid = false;
        }

        /// <summary>
        /// Fill z-bufer of defualt context
        /// </summary>
        public void Rasterization()
        {
            var borderPoints = new List<Point>();
            Point[] borderPointsCopy;

            foreach (var border in borders)
            {
                borderPoints.AddRange(border.Rasterization());
            }

            borderPointsCopy = new Point[borderPoints.Count];
            borderPoints.CopyTo(borderPointsCopy);

            CalculateIndex();

            Fill(borderPoints);
        }

        /// <summary>
        /// Return z position in surface
        /// </summary>
        /// <param name="p">Position</param>
        /// <returns>Depth on surface</returns>
        public double CalculateDepth(Point p)
        {
            return (p.rX * a + p.rY * b + d) / -c;
        }

        // ?????
        public void Draw()
        {
            var linePoints = new List<Point>();
            Point[] linePointsCopy;

            foreach (var line in lines)
            {
                if (!line.draw)
                {
                    linePoints.AddRange(line.Rasterization());
                    line.draw = true;
                }
            }

            linePointsCopy = new Point[linePoints.Count];
            linePoints.CopyTo(linePointsCopy);

            DrawLines(linePointsCopy);
        }

        /// <summary>
        /// Fill z-bufer
        /// </summary>
        /// <param name="borderPoints">Border points</param>
        private void Fill(List<Point> borderPoints)
        {
            while (borderPoints.Count > 0)
            {
                Point curent = borderPoints[0];

                var inlinePoints = borderPoints.FindAll((p) => p.rY == curent.rY);

                // if point one in line
                if (inlinePoints.Count == 1)
                {
                    var p = inlinePoints.First();
                    var dep = CalculateDepth(p);

                    context.Zbufer.Add(p, dep);
                    return;
                }

                inlinePoints.Sort((p1, p2) =>
                {
                    if (p1.rX < p2.rX)
                        return -1;
                    if (p1.rX > p2.rX)
                        return 1;
                    return 0;
                });

                var brezenhem = new BresenhamApproximation(inlinePoints.First(), inlinePoints.Last());

                foreach (var p in brezenhem)
                {
                    var dep = CalculateDepth(p);

                    context.Zbufer.Add(p, dep);
                    // --TEMP--
                    context.DrawPoint(p);
                }

                borderPoints.RemoveAll((p) => inlinePoints.Contains(p));
            }
        }

        /// <summary>
        /// Calculate surface expression index
        /// </summary>
        private void CalculateIndex()
        {
            Point p1 = borders[0].P1;
            Point p2 = borders[0].P2;

            Point p3 = null;

            for (int i = 1; i < borders.Count; i++)
            {
                if (borders[i].P1 != p1 && borders[i].P1 != p2)
                {
                    p3 = borders[i].P1;
                    break;
                }

                if (borders[i].P2 != p1 && borders[i].P2 != p2)
                {
                    p3 = borders[i].P2;
                    break;
                }
            }

            if (p3 == null)
            {
                throw new Exception("Не найдено 3 точек в плоскости");
            }

            a = p1.rY * (p2.rZ - p3.rZ) + p2.rY * (p3.rZ - p1.rZ) +
                p3.rY * (p1.rZ - p2.rZ);
            b = p1.rZ * (p2.rX - p3.rX) + p2.rZ * (p3.rX - p1.rX) +
                p3.rZ * (p1.rX - p2.rX);
            c = p1.rX * (p2.rY - p3.rY) + p2.rX * (p3.rY - p1.rY) +
                p3.rX * (p1.rY - p2.rY);
            d = -(p1.rX * (p2.rY * p3.rZ - p3.rY * p2.rZ) +
                p2.rX * (p3.rY * p1.rZ - p1.rY * p3.rZ) +
                p3.rX * (p1.rY * p2.rZ - p2.rY * p1.rZ));
        }

        // ?????
        private void DrawLines(Point[] borderPoint)
        {
            int visibleCount = 0;
            bool visible = false;
            
            
            //foreach (var p in borderPoint)
            //{
            //    var dep = CalculateDepth(p);

            //    if (context.Zbufer.Visible(p, dep))
            //    {
            //        context.DrawPoint(p);
            //    }
            //    else
            //    {
            //        visibleCount += 1;
            //        if (visibleCount >= visibleLength)
            //        {
            //            visible = !visible;
            //            visibleCount = 0;
            //        }

            //        if (visible)
            //        {
            //            context.DrawPoint(p);
            //        }
            //    }
            //}
        }
    }
}
