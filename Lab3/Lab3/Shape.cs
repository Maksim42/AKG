using System;

namespace Lab3
{
    abstract class Shape
    {
        public int positionX;
        public int posiitonY;
        public double angle;
        private WindowContext conext;

        /// <summary>
        /// Draw form on context
        /// </summary>
        public abstract void Draw();

        /// <summary>
        /// Check if the point is inside the shape 
        /// </summary>
        /// <param name="x">Point X position</param>
        /// <param name="y">Point Y position</param>
        /// <returns></returns>
        public abstract bool PointIn(int x, int y);

        /// <summary>
        /// Animation shape move to linkShape
        /// </summary>
        /// <param name="linkShape">Linked shape</param>
        /// <returns></returns>
        public abstract bool AnimateMove(Shape linkShape);
    }
}
