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
        private bool valid;

        public Surface()
        {
            valid = false;
            borders = new List<Line>();
        }

        public static void SetContext(WindowContext context)
        {
            Surface.context = context;
        }

        public void AddBorder(Line border)
        {
            borders.Add(border);
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

            Fill(borderPoints);

            DrawBorder(borderPointsCopy);
        }

        private void Fill(List<Point> borderPoints)
        {
            while (borderPoints.Count > 0)
            {
                Point curent = borderPoints[0];

                var inlinePoints = borderPoints.FindAll((p) => p.rY == curent.rY);

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
                    context.Zbufer.Add(p, 0);
                    // --TEMP--
                    //context.DrawPoint(p);
                }

                borderPoints.RemoveAll((p) => inlinePoints.Contains(p));
            }
        }

        private void DrawBorder(Point[] borderPoint)
        {
            int visibleCount = 0;
            bool visible = false;
            const int visibleLength = 3;
            
            foreach (var p in borderPoint)
            {
                if (context.Zbufer.Visible(p, 0))
                {
                    context.DrawPoint(p);
                }
                else
                {
                    visibleCount += 1;
                    if (visibleCount >= visibleLength)
                    {
                        visible = !visible;
                    }

                    if (visible)
                    {
                        context.DrawPoint(p);
                    }
                }
            }
        }
    }
}
