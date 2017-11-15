namespace SDLGeometry.TransformMatrix
{
    class PartScale : Matrix
    {
        public double sx;
        public double sy;
        public double sz;

        public PartScale(double sx, double sy, double sz)
        {
            this.sx = sx;
            this.sy = sy;
            this.sz = sz;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            double lx = x;
            double ly = y;
            double lz = z;
            double lh = h;

            x = x * sx;
            y = y * sy;
            z = z * sz;
            h = h * 1;
        }
    }
}
