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

        public Form1()
        {
            InitializeComponent();
        }

        private void Load_IMG_button_Click(object sender, EventArgs e)
        {
            Mat temp = new Mat();
            temp = CvInvoke.Imread(@"C:\Users\miste\Desktop\Projekt_Wizyjne_Labirynth\Labirynth.png");
            CvInvoke.Resize(temp, temp, Viewport.Size);
            viewport_Buffer = temp.ToImage<Bgr, byte>();
            Viewport.Image = viewport_Buffer.Bitmap;
        }
    }
}
