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
        private List<Surface> attachedSurfaces;

        const int visibleLength = 3;

        public Line(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
            valid = false;
            draw = false;

            raster = new List<Point>();
            attachedSurfaces = new List<Surface>();
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

        public void AttachSurface(Surface surface)
        {
            attachedSurfaces.Add(surface);
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
            //context.DrawLine(p1, p2);

            int visibleCount = 0;
            bool visible = false;


            foreach (var p in raster)
            {
                var dep = Depth(p);

                if (context.Zbufer.Visible(p, dep))
                {
                    context.DrawPoint(p);
                }
                else
                {
                    visibleCount += 1;
                    if (visibleCount >= visibleLength)
                    {
                        visible = !visible;
                        visibleCount = 0;
                    }

                    if (visible)
                    {
                        context.DrawPoint(p);
                    }
                }
            }
        }

        private double Depth(Point p)
        {
            double result = attachedSurfaces[0].CalculateDepth(p);

            foreach (var surface in attachedSurfaces)
            {
                double z = surface.CalculateDepth(p);

                if (z < result)
                {
                    result = z;
                }
            }

            return result;
        }
    }
}
