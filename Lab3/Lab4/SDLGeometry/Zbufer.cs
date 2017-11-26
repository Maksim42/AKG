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

        public void Add(Point position)
        {
            var exist = bufer.Find((i) => i.ComparePosition(position));

            if (exist == null)
            {
                bufer.Add(new ZbuferItem(position));
            }
            else
            {
                exist.SetDepth(position.rX);
            }
        }

        public bool Visible(Point position)
        {
            var exist = bufer.Find((i) => i.ComparePosition(position));

            if (exist != null)
            {
                return exist.Position.rX <= position.rX;
            }
            else
            {
                return true;
            }
        }

        public void Unvalidating()
        {
            foreach(var item in bufer)
            {
                item.Unvalidate();
            }
        }
    }
}
