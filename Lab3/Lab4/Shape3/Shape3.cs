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
        protected PartScale partScale;
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
            partScale = new PartScale(1, 1, 1);
        }

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
                rotateZ.angle = 0;
                transformPoints[i].T(rotateZ);
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
        /// Scale shape on 3 dimension
        /// </summary>
        /// <param name="sx">X scale</param>
        /// <param name="sy">Y scale</param>
        /// <param name="sz">Z scale</param>
        public void PartScale(double sx, double sy, double sz)
        {
            partScale.sx = sx;
            partScale.sy = sy;
            partScale.sz = sz;

            foreach (Point p in points)
            {
                p.T(partScale);
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
