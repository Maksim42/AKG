using System;
using System.Collections.Generic;

namespace SDLGeometry
{
    class BresenhamApproximation
    {
        private int x, y;
        private int dx, dy;
        private int s1, s2;
        private bool swap;

        public BresenhamApproximation(int x1, int y1, int x2, int y2) {
            x = x1;
            y = y1;
            dx = Math.Abs(x2 - x1);
            dy = Math.Abs(y2 - y1);
            s1 = Sign(x2 - x1);
            s2 = Sign(y2 - y1);

            if (dy > dx)
            {
                int temp = dx;
                dx = dy;
                dy = temp;
                swap = true;
            }
        }

        /// <summary>
        /// Enumerator return approximate line (x1, y1, x2, y2 in constructor) points
        /// </summary>
        /// <returns>Approximate point on line</returns>
        public IEnumerator<Point> GetEnumerator()
        { 
            int e = 2 * dy - dx;

            for (int i = 1; i <= dx; i++)
            {
                yield return new Point(x, y);

                while (e >= 0)
                {
                    if (swap)
                    {
                        x = x + s1;
                    }
                    else
                    {
                        y = y + s2;
                    }
                    e = e - 2 * dx;
                }

                if (swap)
                {
                    y = y + s2;
                }
                else
                {
                    x = x + s1;
                }
                e = e + 2 * dy;
            }

            yield break;
        }


        /// <summary>
        /// Get sign of number
        /// </summary>
        /// <remarks>System function for DrawDotedLine</remarks>
        /// <param name="number">Value</param>
        /// <returns>Sign value</returns>
        protected static int Sign(int number)
        {
            if (number < 0)
            {
                return -1;
            }

            if (number > 0)
            {
                return 1;
            }

            return 0;
        }
    }
}
