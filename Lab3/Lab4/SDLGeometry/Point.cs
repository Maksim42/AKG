using System;

namespace SDLGeometry
{
    class Point
    {
        private double x;
        private double y;
        private double z;
        private double h;

        public int X => (int)Math.Round(x / h);
        public int Y => (int)Math.Round(y / h);
        /// <summary>
        /// Unnormal Z position
        /// </summary>
        public int Z => (int)z;
        /// <summary>
        /// Raw X coordinate
        /// </summary>
        public double rX => x;
        /// <summary>
        /// Raw Y coordinate
        /// </summary>
        public double rY => y;
        /// <summary>
        /// Raw Z coordinate
        /// </summary>
        public double rZ => z;

        public Point()
        {
            x = 0;
            y = 0;
            z = 0;
            h = 1;
        }

        public Point(double x, double y)
        {
            this.x = x;
            this.y = y;
            z = 0;
            h = 1;
        }

        public Point(double x, double y, double z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
            h = 1;
        }

        public Point(Point p)
        {
            Copy(p);
        }

        public void Copy(Point p)
        {
            x = p.x;
            y = p.y;
            z = p.z;
            h = p.h;
        }

        public override string ToString()
        {
            return $"[{x} {y} {z} {h}]";
        }

        public override bool Equals(object obj)
        {
            var p = obj as Point;

            if (p == null)
            {
                return false;
            }

            return (x == p.rX && y == p.rY && z == p.rZ);
        }

        #region Operations
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.x + p2.x, p1.y + p2.y, p1.z + p2.z);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y - p2.y, p1.z - p2.z);
        }

        public static Point operator *(Point p1, double k)
        {
            return new Point(p1.x * k, p1.y * k, p1.z * k);
        }

        public static Point operator /(Point p1, double k)
        {
            return new Point(p1.x / k, p1.y / k, p1.z / k);
        }

        public static double ScalarMultiplication(Point p1, Point p2)
        {
            return p1.X * p2.X + p1.Y * p2.Y;
        }
        #endregion Operations

        #region Transformation
        /// <summary>
        /// Transform point
        /// </summary>
        /// <param name="m">Trasnform matrix</param>
        /// <returns>Transfomed point (for chain realization)</returns>
        public Point T(TransformMatrix.Matrix m)
        {
            m.Transform(ref x, ref y, ref z, ref h);

            return this;
        }
        #endregion Transformation
    }

}
