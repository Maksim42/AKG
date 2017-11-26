namespace SDLGeometry
{
    class ZbuferItem
    {
        private bool valid;
        private Point position;
        private double depth;

        public ZbuferItem(Point position)
        {
            this.position = new Point(position);
            depth = position.rZ;
            valid = true;
        }

        public Point Position => position;
        public double Depth => depth;

        /// <summary>
        /// Change depth value if posible
        /// </summary>
        /// <param name="newDepth">New depth value</param>
        public void SetDepth(double newDepth)
        {
            if (!valid)
            {
                depth = newDepth;
                valid = true;
                return;
            }

            if (newDepth > depth)
            {
                depth = newDepth;
            }
        }

        /// <summary>
        /// Check if position equivalent
        /// </summary>
        /// <param name="p">Another position</param>
        /// <returns>True if position equivalent</returns>
        public bool ComparePosition(Point p)
        {
            return (position.rX == p.rX && position.rY == p.rY);
        }

        public void Unvalidate()
        {
            valid = false;
        }
    }
}
