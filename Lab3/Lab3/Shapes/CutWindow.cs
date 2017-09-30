using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDL2;

namespace Lab3.Shapes
{
    class CutWindow : Shape
    {
        public CutWindow(WindowContext context, int width, int height, int x, int y)
        {
            this.context = context;
            this.width = width;
            this.height = height;
            positionX = x;
            positionY = y;

            Redraw();
        }

        #region Propertys
        public int Height
        {
            get => height;

            set
            {
                height = CheckSize(height, value);

                Redraw();
            }
        }

        public int Width
        {
            get => width;

            set
            {
                width = CheckSize(width, value);

                Redraw();
            }
        }
        #endregion Propertys

        public override bool AnimateMove(Shape linkShape)
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            context.DrawLine(TransformPoint(points[0]),
                             TransformPoint(points[1]));
            context.DrawLine(TransformPoint(points[1]),
                             TransformPoint(points[2]));
            context.DrawLine(TransformPoint(points[2]),
                             TransformPoint(points[3]));
            context.DrawLine(TransformPoint(points[3]),
                             TransformPoint(points[0]));
        }

        public override bool PointIn(int x, int y)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Check new size parametr and return corect value
        /// </summary>
        /// <param name="curent">Curent parametr value</param>
        /// <param name="newValue">New parametr value</param>
        /// <returns></returns>
        private int CheckSize(int curent, int newValue)
        {
            if ((newValue < 0 ) || (newValue >= 10000))
            {
                return curent;
            }

            return newValue;
        }

        /// <summary>
        /// Redraw point in shape cordinate after change size parametrs
        /// </summary>
        private void Redraw()
        {
            double halfWidth = width / 2.0;
            double halfHeight = height / 2.0;

            points = new Point[] {
                new Point(halfWidth, halfHeight),
                new Point(halfWidth, -halfHeight),
                new Point(-halfWidth, -halfHeight),
                new Point(-halfWidth, halfHeight)
            };
        }
    }
}
