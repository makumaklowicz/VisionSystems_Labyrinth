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
        private Size desired_image_size;
        Image<Bgr, byte> viewport_Buffer, path_Buffer, walls_Buffer, dot_Buffer;
        Mat image_Buffer = new Mat();

        public Form1()
        {
            InitializeComponent();
            desired_image_size = new Size(320, 240);
            viewport_Buffer = new Image<Bgr, byte>(desired_image_size);
            path_Buffer = new Image<Bgr, byte>(desired_image_size);

        }

        private void Load_IMG_button_Click(object sender, EventArgs e)
        {
            image_Buffer = CvInvoke.Imread(@"C:\Users\miste\Desktop\Projekt_Wizyjne_Labirynth\Labirynth.png");
            CvInvoke.Resize(image_Buffer, image_Buffer, Viewport.Size);
            viewport_Buffer = image_Buffer.ToImage<Bgr, byte>();
            path_Buffer = image_Buffer.ToImage<Bgr, byte>();
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

        private void Segmentation_Click(object sender, EventArgs e)
        {
            byte[,,] temp1 = viewport_Buffer.Data;
            byte[,,] temp2 = path_Buffer.Data;
            for (int x = 0; x < Viewport.Width; x++)
            {
                for (int y = 0; y < Viewport.Height; y++)
                {
                    if (temp1[y, x, 0] == 0 && temp1[y, x, 1] == 0 && temp1[y, x, 2] == 0 )
                    {
                        temp2[y, x, 0] = 255;
                        temp2[y, x, 1] = 255;
                        temp2[y, x, 2] = 255;
                    }
                    else if (temp1[y, x, 0] == 255 && temp1[y, x, 1] == 255 && temp1[y, x, 2] == 255)
                    {
                        temp2[y, x, 0] = 0;
                        temp2[y, x, 1] = 0;
                        temp2[y, x, 2] = 0;
                    }

                }
            }

            
            viewport_Path.Image = path_Buffer.Bitmap;
        
        }
    }
}
