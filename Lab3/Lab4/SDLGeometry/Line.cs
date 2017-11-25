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

        public Line(Point p1, Point p2)
        {
            this.p1 = p1;
            this.p2 = p2;
        }

        /// <summary>
        /// Draw line on global line context
        /// </summary>
        public void Draw()
        {
            context.DrawLine(p1, p2);
        }
    }
}
