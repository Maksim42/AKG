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
        public int positionX;
        /// <summary>
        /// Shape center Y position
        /// </summary>
        public int positionY;
        /// <summary>
        /// Angel on which the shape is rotated
        /// </summary>
        public double angle;
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
