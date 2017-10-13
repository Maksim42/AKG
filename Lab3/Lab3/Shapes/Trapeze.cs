﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Shapes
{
    class Trapeze : Shape
    {
        /// <summary>
        /// Trapeze constructor
        /// </summary>
        /// <param name="context">Shape context</param>
        /// <param name="width">Shape width</param>
        /// <param name="height">Shape height</param>
        /// <param name="topProportion">Proportion top side to botom side</param>
        /// <param name="x">Shape center X position</param>
        /// <param name="y">Shape center Y position</param>
        public Trapeze(WindowContext context, int width, int height, double topProportion, int x, int y)
        {
            this.context = context;
            this.height = height;
            this.width = width;
            positionX = x;
            positionY = y;

            double halfWidth = width / 2.0;
            double halfHeight = height / 2.0;
            double topSide = width * topProportion;

            points = new Point[] {
                new Point(halfWidth, halfHeight),
                new Point(halfWidth, -halfHeight),
                new Point(-halfWidth, -halfHeight),
                new Point(halfWidth - topSide, halfHeight)
            };
        }

        public override bool AnimateMove(Shape linkShape)
        {
            throw new NotImplementedException();
        }

        public override void Draw()
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                context.DrawLine(TransformPoint(points[i]),
                                 TransformPoint(points[i + 1]));
            }
            context.DrawLine(TransformPoint(points[points.Length - 1]),
                             TransformPoint(points[0]));
        }

        public override bool PointIn(Point p)
        {
            throw new NotImplementedException();
        }
    }
}
