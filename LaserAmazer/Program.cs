using System;
using System.Windows.Forms;

namespace LaserAmazer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD:FEDDGame/Program.cs
            Application.Run(new Form1());
           // new FEDDGameLaunch();
=======
            Application.Run(new MainForm());
            Console.WriteLine("Yo");
            new LaserAmazerLaunch();
>>>>>>> origin/master:LaserAmazer/Program.cs
        }
    }
}
