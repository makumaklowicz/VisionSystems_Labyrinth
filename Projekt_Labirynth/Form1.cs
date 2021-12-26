using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;

namespace Projekt_Labirynth
{
    public partial class Form1 : Form
    {

        Image<Bgr, byte> viewport_Buffer;
        Mat image_Buffer = new Mat();

        public Form1()
        {
            InitializeComponent();
        }

        private void Load_IMG_button_Click(object sender, EventArgs e)
        {
            image_Buffer = CvInvoke.Imread(@"C:\Users\miste\Desktop\Projekt_Wizyjne_Labirynth\Labirynth.png");
            CvInvoke.Resize(image_Buffer, image_Buffer, Viewport.Size);
            viewport_Buffer = image_Buffer.ToImage<Bgr, byte>();
            Viewport.Image = viewport_Buffer.Bitmap;
        }

        private void Viewport_Click(object sender, EventArgs e)
        {
                CvInvoke.Resize(image_Buffer, image_Buffer, Viewport.Size);
                viewport_Buffer = image_Buffer.ToImage<Bgr, byte>();
                int X, Y;
                MouseEventArgs me = e as MouseEventArgs;
                X = me.X;
                Y = me.Y;
                CvInvoke.Circle(viewport_Buffer, new Point(X, Y), 10, new MCvScalar(0, 0, 255), -1);
            Viewport.Image = viewport_Buffer.Bitmap;
        }
    }
}
