﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Shapes
{
    class Ellipse : Shape
    {
        public Ellipse(WindowContext context, int width, int height, int x, int y)
        {
            this.context = context;
            this.height = height;
            this.width = width;
            positionX = x;
            positionY = y;

            int halfWidth = (int)Math.Floor(width / 2.0);
            int halfHeight = (int)Math.Floor(height / 2.0);

            List<Point> tempPoint = new List<Point>();

            for (int i = -halfWidth; i <= halfWidth; i++)
            {
                tempPoint.Add(new Point(i,
                                        Math.Sqrt((1 - Math.Pow(i,2) / Math.Pow(halfWidth, 2)) * Math.Pow(halfHeight, 2))));
            }

            for (int i = halfWidth; i >= -halfWidth; i--)
            {
                tempPoint.Add(new Point(i,
                                        -Math.Sqrt((1 - Math.Pow(i, 2) / Math.Pow(halfWidth, 2)) * Math.Pow(halfHeight, 2))));
            }

            points = tempPoint.ToArray();
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

        public override bool PointIn(int x, int y)
        {
            throw new NotImplementedException();
        }
    }
}