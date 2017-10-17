using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3
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

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            //window.Close();
        }
    }
}
