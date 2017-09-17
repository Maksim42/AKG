using System;
using System.Windows.Forms;

namespace Lab1
{
    public partial class MainForm : Form
    {
        MainWindow window;

        public MainForm()
        {
            InitializeComponent();

            window = new MainWindow(640, 480);
        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            Hide();
            Close();

            window.Shown();
        }
    }
}
