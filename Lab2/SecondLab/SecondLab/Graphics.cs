using System;
using System.Windows.Forms;

namespace SecondLab
{
    public partial class Graphics : Form
    {
        GraphicsWindow window;

        public Graphics()
        {
            InitializeComponent();

            window = new GraphicsWindow(640, 640);
        }

        private void Graphics_Shown(object sender, EventArgs e)
        {
            Hide();
            Close();

            window.Shown();
        }
    }
}
