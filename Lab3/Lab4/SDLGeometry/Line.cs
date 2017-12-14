using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLWindow;

namespace SDLGeometry
{
    class Line
    {
        /// <summary>
        /// Global context for lines
        /// </summary>
        public static WindowContext context;

        private Point p1;
        private Point p2;
        private bool valid;
        public bool draw;
        private List<Point> raster;

        public Line(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            valid = false;
            draw = false;

            raster = new List<Point>();
        }

        public Point P1 => p1;
        public Point P2 => p2;

        /// <summary>
        /// Return list rasterization poin of line
        /// </summary>
        /// <returns>Point list</returns>
        public List<Point> Rasterization()
        {
            if (!valid)
            {
                var brezenhem = new BresenhamApproximation(p1, p2);

                raster.Clear();

                foreach (var p in brezenhem)
                {
                    raster.Add(p);
                }

                valid = true;
            }

            return raster;
        }

        /// <summary>
        /// Unvalidate line raster
        /// </summary>
        public void Unvalidate()
        {
            valid = false;
            draw = false;
        }

        /// <summary>
        /// Draw line on global line context
        /// </summary>
        public void Draw()
        {
            context.DrawLine(p1, p2);

            //var raster = Rasterization();
            //bool visibleCount = true;

            //foreach (var p in raster)
            //{
            //    if (context.Zbufer.Visible(p, 0))
            //    {
            //        context.DrawPoint(p);
            //    }
            //    else
            //    {
            //        if (visibleCount)
            //        {
            //            context.DrawPoint(p);
            //        }

            //        visibleCount = !visibleCount;
            //    }
            //}
        }
    }
}
