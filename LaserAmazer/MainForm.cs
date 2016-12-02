using System;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using System.Drawing;

namespace LaserAmazer
{
    public partial class MainForm : Form
    {
		bool loaded = false;
        public MainForm()
        {
            InitializeComponent();
        }

		private void glControl1_Load(object sender, EventArgs e) {
			loaded = true;
			GL.ClearColor(Color.Bisque);
			new GameInstance();
		}

		private void glControl1_KeyPress(object sender, KeyPressEventArgs e) {

		}

		private void Form1_MouseMove(object sender, MouseEventArgs e) {
			//Console.WriteLine(e.X + ", " + e.Y);
		}

		private void MainForm_Load(object sender, EventArgs e) {

		}

		private void glControl1_Paint(object sender, PaintEventArgs e) {
			if(!loaded)
				return;
			GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
			glControl1.SwapBuffers();
		}

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
			Window.closeWindow = true;

		}
	}
}
