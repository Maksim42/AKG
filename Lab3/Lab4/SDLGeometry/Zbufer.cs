using System.Collections.Generic;
using System;

namespace SDLGeometry
{
    class Zbufer
    {
        private double[,] bufer;
        // border limits
        private int buferHeight;
        private int buferWidth;
        // real border limits
        private int up;
        private int down;
        private int left;
        private int right;

        private const double backgroundLevel = 500;

        public Zbufer(int height, int width)
        {
            buferHeight = height;
            buferWidth = width;
            up = height / 2;
            down = - height / 2;
            left = - width / 2;
            right = width / 2;

            bufer = new double[height, width];
        }

        /// <summary>
        /// Add point to z-bufer
        /// </summary>
        /// <param name="p">Point position</param>
        /// <param name="depth">Point depth</param>
        public void Add(Point p, double depth)
        {
            if (!CheckBorder(p))
            {
                return;
            }

            TransformPosition(p, out int x, out int y);

            if (bufer[y, x] > depth)
            {
                bufer[y, x] = depth;
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

            if (bufer[y, x] >= depth - eps || bufer[y, x] >= depth + eps)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Unvalidate positions matrix
        /// </summary>
        public void Unvalidate()
        {
            for (int y = 0; y < buferHeight; y++)
            {
                for (int x = 0; x < buferWidth; x++)
                {
                    bufer[y, x] = backgroundLevel;
                }
            }
        }

        private void TransformPosition(Point positon, out int x, out int y)
        {
            x = right + (int)Math.Round(positon.rX);
            y = up + (int)Math.Round(positon.rY);
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
