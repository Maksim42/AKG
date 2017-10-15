using System;
using System.Collections.Generic;

namespace Lab3
{
    class Animation
    {
        /// <summary>
        /// Animate shapes
        /// </summary>
        private Shape shape1, shape2;
        /// <summary>
        /// Shape path
        /// </summary>
        private IEnumerator<Point> path1, path2;
        /// <summary>
        /// Animation delay time
        /// </summary>
        private int delay;
        /// <summary>
        /// Curent animation time after last delay
        /// </summary>
        private int time;
        /// <summary>
        /// Save animation parametrs for previous step
        /// </summary>
        private bool resume;

        public Animation(Shape s1, Shape s2, int delay = 3)
        {
            shape1 = s1;
            shape2 = s2;
            resume = false;
            this.delay = delay;
            time = 0;
        }

        /// <summary>
        /// Animate step
        /// </summary>
        public void Play()
        {
            time++;
            if (time < delay)
            {
                return;
            }
            time = 0;

            if (!resume)
            {
                InitPath();
                resume = true;
            }

            shape1.Angle += 0.05;
            shape2.Angle -= 0.05;

            path1.MoveNext();
            path2.MoveNext();

            shape1.Move(path1.Current);
            shape2.Move(path2.Current);
        }

        /// <summary>
        /// Cansel animation
        /// </summary>
        public void Reset()
        {
            resume = false;
        }

        /// <summary>
        /// Initialize bresenhame path structure for shape moving
        /// </summary>
        private void InitPath()
        {
            int middleX = Math.Abs(shape1.X + shape2.X) / 2;
            int middleY = Math.Abs(shape1.Y + shape2.Y) / 2;
            Point middle = new Point(middleX, middleY);

            path1 = new BresenhamApproximation(shape1.X, shape1.Y, middle.x, middle.y).GetEnumerator();
            path2 = new BresenhamApproximation(shape2.X, shape2.Y, middle.x, middle.y).GetEnumerator();
        }
    }
}
