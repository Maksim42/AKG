using System;

namespace SDLGeometry.TransformMatrix
{
    abstract class Matrix
    {
        public abstract void Transform(ref double x, ref double y, ref double z, ref double h);
    }
}
