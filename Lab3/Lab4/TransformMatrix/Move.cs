using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDLGeometry.TransformMatrix
{
    class Move : Matrix
    {
        public double dx, dy, dz;

        public Move(double dx, double dy, double dz)
        {
            this.dx = dx;
            this.dy = dy;
            this.dz = dz;
        }

        public override void Transform(ref double x, ref double y, ref double z, ref double h)
        {
            double lx = x;
            double ly = y;
            double lz = z;
            double lh = h;

            x = x + dx;
            y = y + dy;
            z = z + dz;
        }
    }
}
