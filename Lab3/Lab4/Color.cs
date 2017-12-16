namespace SDLColor
{
    class Color
    {
        private byte r, g, b;
        private bool invisible;

        public byte R => r;
        public byte G => g;
        public byte B => b;

        public Color(byte r,byte g,byte b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
            invisible = false;
        }

        public static Color Black = new Color(0, 0, 0);
        public static Color White = new Color(255, 255, 255);
        public static Color Red = new Color(255, 0, 0);
        public static Color Green = new Color(0, 255, 0);
        public static Color Blue = new Color(0, 0, 255);
    }
}
