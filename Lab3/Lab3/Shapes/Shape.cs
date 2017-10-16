using System;

namespace Lab3
{
    /// <summary>
    /// Abstract class for all shapes
    /// </summary>
    abstract class Shape
    {
        /// <summary>
        /// Shape center X position
        /// </summary>
        protected int positionX;
        /// <summary>
        /// Shape center Y position
        /// </summary>
        protected int positionY;
        /// <summary>
        /// Angel on which the shape is rotated
        /// </summary>
        protected double angle;
        /// <summary>
        /// Shape width
        /// </summary>
        protected int width;
        /// <summary>
        /// Shape height
        /// </summary>
        protected int height;
        /// <summary>
        /// Context where shape is painting
        /// </summary>
        protected WindowContext context;
        /// <summary>
        /// Shape points in shape coordinate
        /// </summary>
        protected Point[] points;

        #region Properties
        public int X => positionX;

        public int Y => positionY;

        public double Angle
        {
            get
            {
                return angle;
            }
            set
            {
                // TODO: max value = 360
                angle = value;
            }
        }
        #endregion Properties

        /// <summary>
        /// Draw form on context
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Check if the point is inside the shape
        /// </summary>
        /// <param name="p">Checking point</param>
        /// <returns>True if point in shape</returns>
        public abstract bool PointIn(Point p);

        /// <summary>
        /// Move shape to position
        /// </summary>
        /// <param name="movePoint">Move position point</param>
        public void Move(Point movePoint)
        {
            positionX = movePoint.x;
            positionY = movePoint.y;
        }

        /// <summary>
        /// Transform point from shape coordinates to word coordinate 
        /// </summary>
        /// <param name="point">Point in shape coordinate</param>
        protected Point TransformPoint(Point point)
        {
            // Rotate and shift
            point = new Point(point.x * Math.Cos(angle) - point.y * Math.Sin(angle) + positionX,
                              point.x * Math.Sin(angle) + point.y * Math.Cos(angle) + positionY);

            //point = new Point();
            return point;
        }

        /// <summary>
        /// Transform global coordinates to local shape coordinates
        /// </summary>
        /// <param name="point">Point in global coordinates</param>
        /// <returns>Point in local shape coordinates</returns>
        protected Point GlobalToLocalTransform(Point point)
        {
            double localX = point.x - positionX;
            double localY = point.y - positionY;
            point = new Point(localX * Math.Cos(-angle) - localY * Math.Sin(-angle),
                              localX * Math.Sin(-angle) + localY * Math.Cos(-angle));

            //context.DrawPoint(point);
            return point;
        }
    }
}
