namespace SDLGeometry.TransformMatrix
{
    class Perspective
        : Matrix
    {
        public double distance;

        public Perspective(double screenDistance)
        {
            distance = screenDistance;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            //x = distance * x / y;
            //y = distance * z / y;
            //z = y;

            x = distance * x / z;
            y = distance * y / z;
        }
    }
}
