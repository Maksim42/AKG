using System;
using System.Collections.Generic;
using SDLGeometry;
using SDLWindow;
using SDLGeometry.TransformMatrix;

namespace Shape3
{
    class Shape3C
    {
        protected List<Line> lines;
        protected List<Point> points;
        protected List<Point> transformPoints;
        // local transform matrix
        protected RotateX rotateX;
        protected RotateY rotateY;
        protected RotateZ rotateZ;
        protected Move move;
        protected Scale scale;

        protected Shape3C()
        {
            lines = new List<Line>();
            points = new List<Point>();
            transformPoints = new List<Point>();

            // transform matrix initialization
            rotateX = new RotateX(0);
            rotateY = new RotateY(0);
            rotateZ = new RotateZ(0);
            move = new Move(0, 0, 0);
            scale = new Scale(1);
            //partScale = new PartScale(1, 1, 1);
        }

        #region Position propertys
        public double X
        {
            get
            {
                return move.dx;
            }

            set
            {
                move.dx = value;
            }
        }

        public double Y
        {
            get
            {
                return move.dy;
            }

            set
            {
                move.dy = value;
            }
        }

        public double Z
        {
            get
            {
                return move.dz;
            }

            set
            {
                move.dz = value;
            }
        }
        #endregion Position propertys

        #region Rotate propertys
        public double xAngle
        {
            get
            {
                return rotateX.angle;
            }

            set
            {
                rotateX.angle = CheckAngle(value);
            }
        }

        public double yAngle
        {
            get
            {
                return rotateY.angle;
            }

            set
            {
                rotateY.angle = CheckAngle(value);
            }
        }

        public double zAngle
        {
            get
            {
                return rotateZ.angle;
            }

            set
            {
                rotateZ.angle = CheckAngle(value);
            }
        }

        /// <summary>
        /// Check angle value
        /// </summary>
        /// <param name="angle">Raw angle value</param>
        /// <returns>Right angle value</returns>
        private double CheckAngle(double angle)
        {
            double absAngle = Math.Abs(angle);

            if (absAngle > Math.PI * 2)
            {
                int s = Math.Sign(angle);
                angle = absAngle - Math.PI * 2;
            }

            return angle;
        }
        #endregion Rotate propertys

        /// <summary>
        /// Draw shape in global line context
        /// </summary>
        public void Draw()
        {
            TransformPoint();

            foreach (var line in lines)
            {
                line.Draw();
            }
        }

        /// <summary>
        /// Transform init points to actual shape state
        /// </summary>
        protected void TransformPoint()
        {
            for (int i = 0; i < points.Count; i++)
            {
                transformPoints[i].Copy(points[i]);

                // transformation
                transformPoints[i].T(rotateX).T(rotateY).T(rotateZ)
                                    .T(move);
            }
        }

        /// <summary>
        /// Initialize transform point list
        /// </summary>
        protected void InitTransformPointList()
        {
            foreach (var p in points)
            {
                transformPoints.Add(new Point());
            }
        }

        #region Transform
        /// <summary>
        /// Move shape
        /// </summary>
        /// <param name="x">X shift</param>
        /// <param name="y">Y shift</param>
        /// <param name="z">Z shift</param>
        public void Move(double x, double y, double z)
        {
            move.dx = x;
            move.dy = y;
            move.dz = z;

            foreach (Point p in points)
            {
                p.T(move);
            }
        }

        /// <summary>
        /// Scale shape
        /// </summary>
        /// <param name="scale">Scale</param>
        public void Scale(double scale)
        {
            this.scale.scale = 1 / scale;

            foreach (Point p in points)
            {
                p.T(this.scale);
            }
        }

        public void RotateX(double angle)
        {
            rotateX.angle = angle;

            foreach (Point p in points)
            {
                p.T(rotateX);
            }
        }

        public void RotateY(double angle)
        {
            rotateY.angle = angle;

            foreach (Point p in points)
            {
                p.T(rotateY);
            }
        }

        public void RotateZ(double angle)
        {
            rotateZ.angle = angle;

            foreach (Point p in points)
            {
                p.T(rotateZ);
            }
        }
        #endregion Transform
    }
}
