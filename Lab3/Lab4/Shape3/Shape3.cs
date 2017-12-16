using System;
using System.Collections.Generic;
using SDLGeometry;
using SDLWindow;
using SDLGeometry.TransformMatrix;

namespace Shape3
{
    class Shape3C
    {
        public static WindowContext context;

        protected List<Line> lines;
        protected List<Line> invisibleLines;
        protected List<Surface> surfaces;
        protected List<Point> points;
        protected List<Point> transformPoints;
        // local transform matrix
        protected RotateX rotateX;
        protected RotateY rotateY;
        protected RotateZ rotateZ;
        protected Move move;
        protected Scale scale;
        protected Rotator rotator;

        protected Shape3C()
        {
            lines = new List<Line>();
            invisibleLines = new List<Line>();
            surfaces = new List<Surface>();
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

        public double tAngle
        {
            get
            {
                return rotator.angle;
            }

            set
            {
                rotator.angle = CheckAngle(value);
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
                angle = s * (absAngle - Math.PI * 2);
            }

            return angle;
        }
        #endregion Rotate propertys

        #region Scale propertys
        public double Scale
        {
            get
            {
                return scale.scale;
            }

            set
            {
                scale.scale = value;
            }
        }
        #endregion Scale propertys

        /// <summary>
        /// Draw shape surfaces
        /// </summary>
        public void Draw()
        {
            TransformPoint();

            foreach (var surface in surfaces)
            {
                surface.Rasterization();
            }

            //context.Zbufer.ColorFill();

            foreach (var line in lines)
            {
                line.Draw();
            }
        }

        /// <summary>
        /// Unvalidate line and surface rasters of shape
        /// </summary>
        public void Unvalidate()
        {
            foreach (var line in lines)
            {
                line.Unvalidate();
            }

            foreach (var line in invisibleLines)
            {
                line.Unvalidate();
            }

            //foreach (var surface in surfaces)
            //{
            //    surface.Unvalidate();
            //}
        }

        /// <summary>
        /// Transform init points to actual shape state
        /// </summary>
        protected void TransformPoint()
        {
            for (int i = 0; i < points.Count; i++)
            {
                transformPoints[i].Copy(points[i]);

                // Degrees -> Radians
                //Func<double, double> GtR = (g) => g * Math.PI / 180;
                //rotateY.angle = GtR(60);
                //rotateX.angle = GtR(180);

                // transformation
                rotator.Rotate(transformPoints[i])
                    .T(rotateX).T(rotateY).T(rotateZ)
                    .T(move)
                    .T(scale);
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
    }
}
