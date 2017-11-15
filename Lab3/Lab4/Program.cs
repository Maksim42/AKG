using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SDLWindow;

namespace Lab4
{
    class Program
    {
        static void Main(string[] args)
        {
            var window = new MainWindow(700, 500);

            Console.ReadLine();

            window.Close();
        }
    }
}
