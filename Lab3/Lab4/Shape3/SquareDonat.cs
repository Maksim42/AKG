using SDLGeometry;
using SDLColor;

namespace Shape3
{
    class SquareDonat
        : Shape3C
    {
        private double h;
        private double a;
        private double b;

        public SquareDonat(double h)
            : base()
        {
            this.h = h;
            b = 1.5 * h;
            a = 3 * b;

            PointInit();
            LineInit();
            SurfaceInit();
        }

        private void PointInit()
        {
            double halfA = a / 2;
            double halfB = b / 2;
            double halfH = h / 2;

            // front
            points.Add(new Point(-halfA, -halfH, -halfA)); // 0
            points.Add(new Point(-halfA, halfH, -halfA)); // 1
            points.Add(new Point(halfA, halfH, -halfA)); // 2
            points.Add(new Point(halfA, -halfH, -halfA)); // 3
            // back
            points.Add(new Point(-halfA, -halfH, halfA)); // 4
            points.Add(new Point(-halfA, halfH, halfA)); // 5
            points.Add(new Point(halfA, halfH, halfA)); // 6
            points.Add(new Point(halfA, -halfH, halfA)); // 7

            // inner
            // front
            points.Add(new Point(-halfB, -halfH, -halfB)); // 8
            points.Add(new Point(-halfB, halfH, -halfB)); // 9
            points.Add(new Point(halfB, halfH, -halfB)); // 10
            points.Add(new Point(halfB, -halfH, -halfB)); // 11
            // back
            points.Add(new Point(-halfB, -halfH, halfB)); // 12
            points.Add(new Point(-halfB, halfH, halfB)); // 13
            points.Add(new Point(halfB, halfH, halfB)); // 14
            points.Add(new Point(halfB, -halfH, halfB)); // 15

            //// invisble borders
            //// front
            //points.Add(new Point(-halfB, -halfH, -halfA)); // 16
            //points.Add(new Point(-halfB, halfH, -halfA)); // 17
            //points.Add(new Point(halfB, halfH, -halfA)); // 18
            //points.Add(new Point(halfB, -halfH, -halfA)); // 19
            //// back
            //points.Add(new Point(-halfB, -halfH, halfA)); // 20
            //points.Add(new Point(-halfB, halfH, halfA)); // 21
            //points.Add(new Point(halfB, halfH, halfA)); // 22
            //points.Add(new Point(halfB, -halfH, halfA)); // 23

            rotator = new Rotator(points[4], points[2]);

            InitTransformPointList();
        }

        private void LineInit()
        {
            // front
            lines.Add(new Line(transformPoints[0], transformPoints[1])); // 0
            lines.Add(new Line(transformPoints[1], transformPoints[2])); // 1
            lines.Add(new Line(transformPoints[2], transformPoints[3])); // 2
            lines.Add(new Line(transformPoints[3], transformPoints[0])); // 3
            // back
            lines.Add(new Line(transformPoints[4], transformPoints[5])); // 4
            lines.Add(new Line(transformPoints[5], transformPoints[6])); // 5
            lines.Add(new Line(transformPoints[6], transformPoints[7])); // 6
            lines.Add(new Line(transformPoints[7], transformPoints[4])); // 7
            // midle
            lines.Add(new Line(transformPoints[0], transformPoints[4])); // 8
            lines.Add(new Line(transformPoints[1], transformPoints[5])); // 9
            lines.Add(new Line(transformPoints[2], transformPoints[6])); // 10
            lines.Add(new Line(transformPoints[3], transformPoints[7])); // 11

            // inner 
            // front
            lines.Add(new Line(transformPoints[8], transformPoints[9])); // 12
            lines.Add(new Line(transformPoints[9], transformPoints[10])); // 13
            lines.Add(new Line(transformPoints[10], transformPoints[11])); // 14
            lines.Add(new Line(transformPoints[11], transformPoints[8])); // 15
            // back
            lines.Add(new Line(transformPoints[12], transformPoints[13])); // 16
            lines.Add(new Line(transformPoints[13], transformPoints[14])); // 17
            lines.Add(new Line(transformPoints[14], transformPoints[15])); // 18
            lines.Add(new Line(transformPoints[15], transformPoints[12])); // 19
            // midle
            lines.Add(new Line(transformPoints[8], transformPoints[12])); // 20
            lines.Add(new Line(transformPoints[9], transformPoints[13])); //21
            lines.Add(new Line(transformPoints[10], transformPoints[14])); // 22
            lines.Add(new Line(transformPoints[11], transformPoints[15])); // 23

            // inisible
            // up
            invisibleLines.Add(new Line(transformPoints[5], transformPoints[13])); //0
            invisibleLines.Add(new Line(transformPoints[1], transformPoints[9])); //1
            invisibleLines.Add(new Line(transformPoints[10], transformPoints[2])); //2
            invisibleLines.Add(new Line(transformPoints[14], transformPoints[6])); //3
            // down
            invisibleLines.Add(new Line(transformPoints[4], transformPoints[12])); //4
            invisibleLines.Add(new Line(transformPoints[0], transformPoints[8])); //5
            invisibleLines.Add(new Line(transformPoints[11], transformPoints[3])); //6
            invisibleLines.Add(new Line(transformPoints[15], transformPoints[7])); //7

        }

        private void SurfaceInit()
        {
            // front
            var s = new Surface(Color.Red);
            surfaces.Add(s);
            s.AddBorder(lines[0]);
            s.AddBorder(lines[1]);
            s.AddBorder(lines[2]);
            s.AddBorder(lines[3]);
            s.AddLine(lines[0]);
            s.AddLine(lines[1]);
            s.AddLine(lines[2]);
            s.AddLine(lines[3]);
            // back
            s = new Surface(Color.Blue);
            surfaces.Add(s);
            s.AddBorder(lines[4]);
            s.AddBorder(lines[5]);
            s.AddBorder(lines[6]);
            s.AddBorder(lines[7]);
            s.AddLine(lines[4]);
            s.AddLine(lines[5]);
            s.AddLine(lines[6]);
            s.AddLine(lines[7]);
            // midle left
            s = new Surface(Color.Green);
            surfaces.Add(s);
            s.AddBorder(lines[8]);
            s.AddBorder(lines[9]);
            s.AddBorder(lines[0]);
            s.AddBorder(lines[4]);
            s.AddLine(lines[8]);
            s.AddLine(lines[9]);
            s.AddLine(lines[0]);
            s.AddLine(lines[4]);
            //midle right
            s = new Surface(Color.Yellow);
            surfaces.Add(s);
            s.AddBorder(lines[10]);
            s.AddBorder(lines[11]);
            s.AddBorder(lines[2]);
            s.AddBorder(lines[6]);
            s.AddLine(lines[10]);
            s.AddLine(lines[11]);
            s.AddLine(lines[2]);
            s.AddLine(lines[6]);

            // inner front
            s = new Surface(Color.Red);
            surfaces.Add(s);
            s.AddBorder(lines[12]);
            s.AddBorder(lines[13]);
            s.AddBorder(lines[14]);
            s.AddBorder(lines[15]);
            s.AddLine(lines[12]);
            s.AddLine(lines[13]);
            s.AddLine(lines[14]);
            s.AddLine(lines[15]);
            // inner back
            s = new Surface(Color.Blue);
            surfaces.Add(s);
            s.AddBorder(lines[16]);
            s.AddBorder(lines[17]);
            s.AddBorder(lines[18]);
            s.AddBorder(lines[19]);
            s.AddLine(lines[16]);
            s.AddLine(lines[17]);
            s.AddLine(lines[18]);
            s.AddLine(lines[19]);
            // inner midle left
            s = new Surface(Color.Green);
            surfaces.Add(s);
            s.AddBorder(lines[20]);
            s.AddBorder(lines[21]);
            s.AddBorder(lines[12]);
            s.AddBorder(lines[16]);
            s.AddLine(lines[20]);
            s.AddLine(lines[21]);
            s.AddLine(lines[12]);
            s.AddLine(lines[16]);
            // inner midle right
            s = new Surface(Color.Yellow);
            surfaces.Add(s);
            s.AddBorder(lines[22]);
            s.AddBorder(lines[23]);
            s.AddBorder(lines[14]);
            s.AddBorder(lines[18]);
            s.AddLine(lines[22]);
            s.AddLine(lines[23]);
            s.AddLine(lines[14]);
            s.AddLine(lines[18]);

            // up left
            s = new Surface(Color.Green);
            surfaces.Add(s);
            s.AddBorder(lines[9]);
            s.AddBorder(invisibleLines[0]);
            s.AddBorder(invisibleLines[1]);
            s.AddBorder(lines[21]);
            s.AddLine(lines[9]);
            s.AddLine(lines[21]);
            // up front
            s = new Surface(Color.Red);
            surfaces.Add(s);
            s.AddBorder(lines[1]);
            s.AddBorder(invisibleLines[1]);
            s.AddBorder(invisibleLines[2]);
            s.AddBorder(lines[13]);
            s.AddLine(lines[1]);
            s.AddLine(lines[13]);
            // up right
            s = new Surface(Color.Yellow);
            surfaces.Add(s);
            s.AddBorder(lines[22]);
            s.AddBorder(invisibleLines[2]);
            s.AddBorder(invisibleLines[3]);
            s.AddBorder(lines[10]);
            s.AddLine(lines[22]);
            s.AddLine(lines[10]);
            // up back
            s = new Surface(Color.Blue);
            surfaces.Add(s);
            s.AddBorder(lines[17]);
            s.AddBorder(invisibleLines[0]);
            s.AddBorder(invisibleLines[3]);
            s.AddBorder(lines[5]);
            s.AddLine(lines[5]);
            s.AddLine(lines[17]);

            // down left
            s = new Surface(Color.Green);
            surfaces.Add(s);
            s.AddBorder(lines[8]);
            s.AddBorder(invisibleLines[4]);
            s.AddBorder(invisibleLines[5]);
            s.AddBorder(lines[20]);
            s.AddLine(lines[8]);
            s.AddLine(lines[20]);
            // down front
            s = new Surface(Color.Red);
            surfaces.Add(s);
            s.AddBorder(lines[15]);
            s.AddBorder(invisibleLines[6]);
            s.AddBorder(invisibleLines[5]);
            s.AddBorder(lines[3]);
            s.AddLine(lines[15]);
            s.AddLine(lines[3]);
            // down right
            s = new Surface(Color.Yellow);
            surfaces.Add(s);
            s.AddBorder(lines[23]);
            s.AddBorder(invisibleLines[6]);
            s.AddBorder(invisibleLines[7]);
            s.AddBorder(lines[11]);
            s.AddLine(lines[11]);
            s.AddLine(lines[23]);
            // down back
            s = new Surface(Color.Blue);
            surfaces.Add(s);
            s.AddBorder(lines[7]);
            s.AddBorder(invisibleLines[4]);
            s.AddBorder(invisibleLines[7]);
            s.AddBorder(lines[19]);
            s.AddLine(lines[7]);
            s.AddLine(lines[19]);
        }
    }
}
