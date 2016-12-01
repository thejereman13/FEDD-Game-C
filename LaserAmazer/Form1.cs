using System;
using System.Windows.Forms;

namespace LaserAmazer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

		private void glControl1_Load(object sender, EventArgs e) {
			
		}

		private void glControl1_KeyPress(object sender, KeyPressEventArgs e) {

		}

		private void Form1_MouseMove(object sender, MouseEventArgs e) {
			Console.WriteLine(e.X + ", " + e.Y);
		}
	}
}
