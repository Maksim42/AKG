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
        /// <summary>
        /// Next layer shape
        /// </summary>
        protected Shape crossingShape;

        /// <summary>
        /// For minimalize code magic in Draw 
        /// </summary>
        private delegate void LineDrawer(Point p1, Point p2);

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
                angle = (Math.Abs(value) > 360) ? value - 360 : value; 
            }
        }

        /// <summary>
        /// Seter for crosingShape
        /// </summary>
        public Shape CrossingShape
        {
            set
            {
                crossingShape = value;
            }
        }
        #endregion Properties

        /// <summary>
        /// Draw form on context
        /// </summary>
        public void Draw()
        {
            LineDrawer drawLine;
            if (crossingShape == null)
            {
                drawLine = context.DrawLine;
            }
            else
            {
                drawLine = crossingShape.DrawLineInShape;
            }
            
            for (int i = 0; i < points.Length - 1; i++)
            {
                drawLine(TransformPoint(points[i]),
                         TransformPoint(points[i + 1]));
            }
            drawLine(TransformPoint(points[points.Length - 1]),
                     TransformPoint(points[0]));
        }

        #region Crossing
        /// <summary>
        /// Draw line crossing(or no) this shape
        /// </summary>
        /// <param name="p1">First line point</param>
        /// <param name="p2">Second line point</param>
        public abstract void DrawLineInShape(Point p1, Point p2);

        /// <summary>
        /// Trivial check for visible line in shape
        /// </summary>
        /// <param name="p1">First line point</param>
        /// <param name="p2">Second line point</param>
        /// <returns>False if not fisible in shape</returns>
        protected bool LineTrivialVisible(Point p1, Point p2)
        {
            int code1 = CrosingCode(p1);
            int code2 = CrosingCode(p2);

            return !((code1 & code2) != 0);
        }

        //?????????????????????
        /// <summary>
        /// Find crossing point
        /// </summary>
        /// <param name="p1">First point first line</param>
        /// <param name="p2">Second point first line</param>
        /// <param name="p3">First point second line</param>
        /// <param name="p4">Second point second line</param>
        /// <returns>Crossing point or null if line dont crossing</returns>
        protected Point LineCrossing(Point p1, Point p2, Point p3, Point p4)
        {
            if (p1.x > p2.x)
            {
                Point temp = p1;
                p1 = p2;
                p2 = temp;
            }

            if (p3.x > p4.x)
            {
                Point temp = p3;
                p3 = p4;
                p4 = temp;
            }

            

            return null;
        }


        /// <summary>
        /// Part of LineTrivialVisible algoritm
        /// </summary>
        /// <param name="p">Point for check</param>
        /// <returns>Specific code for checking visible</returns>
        private int CrosingCode(Point p)
        {
            int result = 0;
            p = GlobalToLocalTransform(p);

            if (p.x < -(width / 2))
            {
                result = result | 1;
            }

            if (p.x > width / 2)
            {
                result = result | 2;
            }

            if (p.y < -(height / 2))
            {
                result = result | 4;
            }

            if (p.y > (height / 2))
            {
                result = result | 8;
            }

            return result;
        }
        #endregion Crossing

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

        /// <summary>
        /// Check new size parametr and return corect value
        /// </summary>
        /// <param name="curent">Curent parametr value</param>
        /// <param name="newValue">New parametr value</param>
        /// <returns></returns>
        protected int CheckSize(int curent, int newValue)
        {
            if ((newValue < 0) || (newValue >= 10000))
            {
                return curent;
            }

            return newValue;
        }
    }
}
