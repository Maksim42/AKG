using System.Collections.Generic;
using System;
using SDLColor;

namespace SDLGeometry
{
    class Zbufer
    {
        private SDLWindow.WindowContext context;
        private double[,] bufer;
        private Color[,] colorBufer;
        // border limits
        private int buferHeight, buferWidth;
        // real border limits
        private int up, down, left, right;
        // unvalidate area
        private int maxX, maxY, minX, minY;

        private const double backgroundLevel = 500;

        public Zbufer(SDLWindow.WindowContext c, int height, int width)
        {
            context = c;

            buferHeight = height;
            buferWidth = width;
            up = height / 2;
            down = - height / 2;
            left = - width / 2;
            right = width / 2;
            maxX = width - 1;
            maxY = height - 1;
            minX = 0;
            minY = 0;

            bufer = new double[height, width];
            colorBufer = new Color[height, width];
        }

        /// <summary>
        /// Add point to z-bufer
        /// </summary>
        /// <param name="p">Point position</param>
        /// <param name="depth">Point depth</param>
        public void Add(Point p, double depth, Color fillColor = null)
        {
            if (!CheckBorder(p))
            {
                return;
            }

            TransformPosition(p, out int x, out int y);

            if (x > maxX) maxX = x;
            if (x < minX) minX = x;
            if (y > maxY) maxY = y;
            if (y < minY) minY = y;

            if (bufer[y, x] > depth)
            {
                bufer[y, x] = depth;

                // FILL
                //if (fillColor != null)
                //{
                //    colorBufer[y, x] = fillColor;
                //}
            }
        }

        /// <summary>
        /// Check visible for this position
        /// </summary>
        /// <param name="position">Cheking posithion</param>
        /// <returns>Visible</returns>
        public bool Visible(Point p, double depth)
        {
            if (!CheckBorder(p))
            {
                return false;
            }

            TransformPosition(p, out int x, out int y);

            double eps = double.Epsilon;

            if (bufer[y, x] >= depth - eps /*|| bufer[y, x] >= depth + eps*/)
            {
                return true;
            }

            return false;
        }

        public void ColorFill()
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    Color color = colorBufer[y, x];

                    if (color != null)
                    {
                        context.SetColor(color);
                        var p = ReverseTransformPosition(x, y);
                        context.DrawPoint(p);
                    }
                }
            }
        }

        /// <summary>
        /// Unvalidate positions matrix
        /// </summary>
        public void Unvalidate()
        {
            for (int y = minY; y <= maxY; y++)
            {
                for (int x = minX; x <= maxX; x++)
                {
                    bufer[y, x] = backgroundLevel;
                    // FILL
                    //colorBufer[y, x] = null;
                }
            }

            minX = buferWidth - 1;
            minY = buferHeight - 1;
            maxX = 0;
            maxY = 0;
        }

        /// <summary>
        /// Transform screen position to z-bufer position
        /// </summary>
        /// <param name="positon">Sreen position</param>
        /// <param name="x">Z-bufer X position</param>
        /// <param name="y">Z-bufer Y position</param>
        private void TransformPosition(Point positon, out int x, out int y)
        {
            x = right + (int)Math.Round(positon.rX);
            y = up + (int)Math.Round(positon.rY);
        }

        /// <summary>
        /// Transform z-bufer position to screen position
        /// </summary>
        /// <param name="x">z-bufer X</param>
        /// <param name="y">z-bufer Y</param>
        /// <returns>Points contain screen position</returns>
        private Point ReverseTransformPosition(int x, int y)
        {
            return new Point(x - right, y - up);
        }

        /// <summary>
        /// Return true if point in bufer borders
        /// </summary>
        /// <param name="position">Checking point</param>
        /// <returns>Checking result</returns>
        private bool CheckBorder(Point position)
        {
            return (position.rX < left || position.rY < down ||
                    position.rX >= right || position.rY >= up)
                    ? false : true;
        }
    }
}
