using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLGeometry.TransformMatrix
{
    class Scale
        : Matrix
    {
        public double scale;

        public Scale(double scale)
        {
            this.scale = scale;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            h = h * scale;
        }
    }
}
