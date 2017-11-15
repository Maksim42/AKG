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

        protected Shape3C()
        {
            lines = new List<Line>();
            points = new List<Point>();
        }

        /// <summary>
        /// Draw shape in global line context
        /// </summary>
        public void Draw()
        {
            foreach (var line in lines)
            {
                line.Draw();
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
            foreach (Point p in points)
            {
                p.T(new Move(x, y, z));
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
            foreach (Point p in points)
            {
                p.T(new PartScale(sx, sy, sz));
            }
        }

        /// <summary>
        /// Scale shape
        /// </summary>
        /// <param name="scale">Scale</param>
        public void Scale(double scale)
        {
            foreach (Point p in points)
            {
                p.T(new Scale(scale));
            }
        }

        public void RotateX(double angle)
        {
            foreach (Point p in points)
            {
                p.T(new RotateX(angle));
            }
        }

        public void RotateY(double angle)
        {
            foreach (Point p in points)
            {
                p.T(new RotateY(angle));
            }
        }

        public void RotateZ(double angle)
        {
            foreach (Point p in points)
            {
                p.T(new RotateZ(angle));
            }
        }
        #endregion Transform
    }
}
