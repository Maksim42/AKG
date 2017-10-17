namespace Lab3
{
    class Point
    {
        public int x;
        public int y;

        public Point(double x, double y)
        {
            this.x = (int)x;
            this.y = (int)y;
        }

        #region Operations
        public static Point operator +(Point p1, Point p2)
        {
            return new Point(p1.x + p2.x, p1.y + p2.y);
        }

        public static Point operator -(Point p1, Point p2)
        {
            return new Point(p1.x - p2.x, p1.y - p2.y);
        }

        public static Point operator *(Point p1, double k)
        {
            return new Point(p1.x * k, p1.y * k);
        }

        public static double ScalarMultiplication(Point p1, Point p2)
        {
            return p1.x * p2.x + p1.y * p2.y;
        }
        #endregion Operations
    }

}
