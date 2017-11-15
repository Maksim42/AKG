namespace SDLGeometry
{
    class Point
    {
        private double x;
        private double y;
        private double z;
        private double h;

        public int X => (int)(x / h);
        public int Y => (int)(y / h);

        public Point(double x, double y)
        {
            this.x = (int)x;
            this.x = (int)y;
            z = 0;
            h = 1;
        }

        public Point(double x, double y, double z)
        {
            this.x = (int)x;
            this.y = (int)y;
            this.z = (int)z;
            h = 1;
        }

        #region Operations
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.X + p2.X, p1.Y + p2.Y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.X - p2.X, p1.Y - p2.Y);
        }

        public static Point operator *(Point p1, double k)
        {
            return new Point(p1.X * k, p1.Y * k);
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
