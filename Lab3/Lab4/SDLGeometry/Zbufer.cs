using System.Collections.Generic;

namespace SDLGeometry
{
    class Zbufer
    {
        private List<ZbuferItem> bufer;

        public Zbufer()
        {
            bufer = new List<ZbuferItem>();
        }

        public void Add(Point position, double depth)
        {
            var exist = bufer.Find((i) => i.ComparePosition(position));

            if (exist == null)
            {
                bufer.Add(new ZbuferItem(position, depth));
            }
            else
            {
                exist.SetDepth(depth);
            }
        }

        /// <summary>
        /// Check visible for this position
        /// </summary>
        /// <param name="position">Cheking posithion</param>
        /// <returns>Visible</returns>
        public bool Visible(Point position, double depth)
        {
            var exist = bufer.Find((i) => i.ComparePosition(position));

            if (exist != null)
            {
                return exist.Depth <= depth;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Unvalidate positions matrix
        /// </summary>
        public void Unvalidate()
        {
            foreach(var item in bufer)
            {
                item.Unvalidate();
            }
        }
    }
}
