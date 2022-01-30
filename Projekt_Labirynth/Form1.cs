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
using System.IO;
namespace Projekt_Labirynth
{
    public partial class Form1 : Form
    {
        private Size desired_image_size;
        Image<Bgr, byte> viewport_Buffer, path_Buffer, walls_Buffer, dot_Buffer,cacheglobal_Buffer, cacheinfnc_Buffer;
        Mat image_Buffer = new Mat();

        public Form1()
        {
            InitializeComponent();
            desired_image_size = new Size(320, 240);
            viewport_Buffer = new Image<Bgr, byte>(desired_image_size);
            path_Buffer = new Image<Bgr, byte>(desired_image_size);
            walls_Buffer = new Image<Bgr, byte>(desired_image_size);
            dot_Buffer = new Image<Bgr, byte>(desired_image_size);
            cacheglobal_Buffer = new Image<Bgr, byte>(desired_image_size);
            cacheinfnc_Buffer = new Image<Bgr, byte>(desired_image_size);
        }

        private void Load_IMG_button_Click(object sender, EventArgs e)
        {
            image_Buffer = CvInvoke.Imread(@"C:\Users\miste\Desktop\labizprez.png");
            CvInvoke.Resize(image_Buffer, image_Buffer, desired_image_size);
            viewport_Buffer = image_Buffer.ToImage<Bgr, byte>();
            path_Buffer = image_Buffer.ToImage<Bgr, byte>();
            Viewport.Image = viewport_Buffer.Bitmap;
        }

        private void Viewport_Click(object sender, EventArgs e)
        {
                CvInvoke.Resize(image_Buffer, image_Buffer, desired_image_size);
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



            /*ThresholdColor(true, 10, 60, 0, 50, 0, 255, 255);
            path_Buffer = cacheglobal_Buffer;
            Quant(true, 255);
            dot_Buffer = cacheglobal_Buffer ;
            walls_Buffer.Data= OROPERATION(path_Buffer, dot_Buffer);
            viewport_Walls.Image = dot_Buffer.Bitmap;
            viewport_Dot.Image = path_Buffer.Bitmap;
            viewport_Path.Image = walls_Buffer.Bitmap; */
            dot_Buffer.Data = ThresholdColor(viewport_Buffer, 0, 190, 0, 255, 0, 255, 255);
            
            path_Buffer.Data = Quant(viewport_Buffer, 255);
            path_Buffer.Data = OROPERATION(dot_Buffer, path_Buffer);
            path_Buffer.Data = Dilate(path_Buffer);
            path_Buffer.Data = Dilate(path_Buffer);
            path_Buffer.Data = Erode(path_Buffer);
            path_Buffer.Data = Erode(path_Buffer);

            


            walls_Buffer.Data = filterHIGH(viewport_Buffer);
            walls_Buffer.Data = filterHIGH(walls_Buffer);
            walls_Buffer.Data = ThresholdColor(walls_Buffer, 0, 230, 0, 230, 0, 230, 255);
          








            viewport_Path.Image = path_Buffer.Bitmap;
            viewport_Dot.Image = dot_Buffer.Bitmap;
            viewport_Walls.Image = walls_Buffer.Bitmap;
        }







        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Dylatacja
        private byte[,,] Dilate(Image<Bgr,byte> img)
        {
            double R, G, B;
            byte[,,] temp1 = img.Data;
            byte[,,] temp2 = cacheglobal_Buffer.Data;

            for (int x = 1; x < viewport_Buffer.Width - 1; x++)
            {
                for (int y = 1; y < viewport_Buffer.Height - 1; y++)
                {
                    R = G = B = 0;

                    B = temp1[y - 1, x - 1, 0];
                    B = Math.Max(temp1[y - 1, x, 0], B);
                    B = Math.Max(temp1[y - 1, x + 1, 0], B);

                    B = Math.Max(temp1[y, x - 1, 0], B);
                    B = Math.Max(temp1[y, x, 0], B);
                    B = Math.Max(temp1[y, x + 1, 0], B);

                    B = Math.Max(temp1[y + 1, x - 1, 0], B);
                    B = Math.Max(temp1[y + 1, x, 0], B);
                    B = Math.Max(temp1[y + 1, x + 1, 0], B);

                    G = temp1[y - 1, x - 1, 1];
                    G = Math.Max(temp1[y - 1, x, 1], G);
                    G = Math.Max(temp1[y - 1, x + 1, 1], G);

                    G = Math.Max(temp1[y, x - 1, 1], G);
                    G = Math.Max(temp1[y, x, 1], G);
                    G = Math.Max(temp1[y, x + 1, 1], G);

                    G = Math.Max(temp1[y + 1, x - 1, 1], G);
                    G = Math.Max(temp1[y + 1, x, 1], G);
                    G = Math.Max(temp1[y + 1, x + 1, 1], G);

                    R = temp1[y - 1, x - 1, 2];
                    R = Math.Max(temp1[y - 1, x, 2], R);
                    R = Math.Max(temp1[y - 1, x + 1, 2], R);

                    R = Math.Max(temp1[y, x - 1, 2], R);
                    R = Math.Max(temp1[y, x, 2], R);
                    R = Math.Max(temp1[y, x + 1, 2], R);

                    R = Math.Max(temp1[y + 1, x - 1, 2], R);
                    R = Math.Max(temp1[y + 1, x, 2], R);
                    R = Math.Max(temp1[y + 1, x + 1, 2], R);

                    temp2[y, x, 0] = (byte)B;
                    temp2[y, x, 1] = (byte)G;
                    temp2[y, x, 2] = (byte)R;
                }
            }
            return temp2;
        }

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Erozja

        private byte[,,] Erode(Image<Bgr, byte> img)
        {
            double R, G, B;
            byte[,,] temp1 = img.Data;
            byte[,,] temp2 = cacheglobal_Buffer.Data;
         
            for (int x = 1; x < viewport_Buffer.Width - 1; x++)
            {
                for (int y = 1; y < viewport_Buffer.Height - 1; y++)
                {
                    R = G = B = 0;

                    B = temp1[y - 1, x - 1, 0];
                    B = Math.Min(temp1[y - 1, x, 0], B);
                    B = Math.Min(temp1[y - 1, x + 1, 0], B);

                    B = Math.Min(temp1[y, x - 1, 0], B);
                    B = Math.Min(temp1[y, x, 0], B);
                    B = Math.Min(temp1[y, x + 1, 0], B);

                    B = Math.Min(temp1[y + 1, x - 1, 0], B);
                    B = Math.Min(temp1[y + 1, x, 0], B);
                    B = Math.Min(temp1[y + 1, x + 1, 0], B);

                    G = temp1[y - 1, x - 1, 1];
                    G = Math.Min(temp1[y - 1, x, 1], G);
                    G = Math.Min(temp1[y - 1, x + 1, 1], G);

                    G = Math.Min(temp1[y, x - 1, 1], G);
                    G = Math.Min(temp1[y, x, 1], G);
                    G = Math.Min(temp1[y, x + 1, 1], G);

                    G = Math.Min(temp1[y + 1, x - 1, 1], G);
                    G = Math.Min(temp1[y + 1, x, 1], G);
                    G = Math.Min(temp1[y + 1, x + 1, 1], G);

                    R = temp1[y - 1, x - 1, 2];
                    R = Math.Min(temp1[y - 1, x, 2], R);
                    R = Math.Min(temp1[y - 1, x + 1, 2], R);

                    R = Math.Min(temp1[y, x - 1, 2], R);
                    R = Math.Min(temp1[y, x, 2], R);
                    R = Math.Min(temp1[y, x + 1, 2], R);

                    R = Math.Min(temp1[y + 1, x - 1, 2], R);
                    R = Math.Min(temp1[y + 1, x, 2], R);
                    R = Math.Min(temp1[y + 1, x + 1, 2], R);

                    temp2[y, x, 0] = (byte)B;
                    temp2[y, x, 1] = (byte)G;
                    temp2[y, x, 2] = (byte)R;
                }
            }
            return temp2;
        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //FILTER
        private byte[,,] filterHIGH(Image<Bgr, byte> img)
        {
            double[] wsp = new double[9];
            double suma_wsp = 0;
            wsp[0] = -1;
            wsp[1] = -1;
            wsp[2] = -1;
            wsp[3] = -1;
            wsp[4] = 9;
            wsp[5] = -1;
            wsp[6] = -1;
            wsp[7] = -1;
            wsp[8] = -1;

            for (int i = 0; i < 9; i++)
                suma_wsp += wsp[i];

            byte[,,] temp1 = img.Data;
            byte[,,] temp2 = cacheglobal_Buffer.Data;

            for (int x = 1; x < desired_image_size.Width - 1; x++)
            {
                for (int y = 1; y < desired_image_size.Height - 1; y++)
                {
                    double R = 0, G = 0, B = 0;
                    B += wsp[0] * temp1[y - 1, x - 1, 0];
                    B += wsp[1] * temp1[y - 1, x, 0];
                    B += wsp[2] * temp1[y - 1, x + 1, 0];

                    B += wsp[3] * temp1[y, x - 1, 0];
                    B += wsp[4] * temp1[y, x, 0];
                    B += wsp[5] * temp1[y, x + 1, 0];

                    B += wsp[6] * temp1[y + 1, x - 1, 0];
                    B += wsp[7] * temp1[y + 1, x, 0];
                    B += wsp[8] * temp1[y + 1, x + 1, 0];


                    G += wsp[0] * temp1[y - 1, x - 1, 1];
                    G += wsp[1] * temp1[y - 1, x, 1];
                    G += wsp[2] * temp1[y - 1, x + 1, 1];

                    G += wsp[3] * temp1[y, x - 1, 1];
                    G += wsp[4] * temp1[y, x, 1];
                    G += wsp[5] * temp1[y, x + 1, 1];

                    G += wsp[6] * temp1[y + 1, x - 1, 1];
                    G += wsp[7] * temp1[y + 1, x, 1];
                    G += wsp[8] * temp1[y + 1, x + 1, 1];


                    R += wsp[0] * temp1[y - 1, x - 1, 2];
                    R += wsp[1] * temp1[y - 1, x, 2];
                    R += wsp[2] * temp1[y - 1, x + 1, 2];

                    R += wsp[3] * temp1[y, x - 1, 2];
                    R += wsp[4] * temp1[y, x, 2];
                    R += wsp[5] * temp1[y, x + 1, 2];

                    R += wsp[6] * temp1[y + 1, x - 1, 2];
                    R += wsp[7] * temp1[y + 1, x, 2];
                    R += wsp[8] * temp1[y + 1, x + 1, 2];

                    if ((int)suma_wsp != 0)
                    {
                        B /= suma_wsp;
                        G /= suma_wsp;
                        R /= suma_wsp;
                    }


                        B /= 2;
                        G /= 2;
                        R /= 2;
                        B += 128;
                        G += 128;
                        R += 128;

                    if (B < 0) B = 0;
                    if (G < 0) G = 0;
                    if (R < 0) R = 0;
                    if (B > 255) B = 255;
                    if (G > 255) G = 255;
                    if (R > 255) R = 255;

                    temp2[y, x, 0] = (byte)B;
                    temp2[y, x, 1] = (byte)G;
                    temp2[y, x, 2] = (byte)R;
                }
            }
            return temp2;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////// 

        //Kwantowanie
        private byte[,,] Quant(Image<Bgr, byte> img, int Thresh)
        {
            int bins = Thresh;
            byte[,,] temp1 = img.Data;
            byte[,,] temp2 = cacheglobal_Buffer.Data;
        

            for (int x = 0; x < desired_image_size.Width; x++)
            {
                for (int y = 0; y < desired_image_size.Height; y++)
                {
                    int val = temp1[y, x, 0] / bins;
                    val *= bins;
                    temp2[y, x, 0] = (byte)val;

                    val = temp1[y, x, 1] / bins;
                    val *= bins;
                    temp2[y, x, 1] = (byte)val;

                    val = temp1[y, x, 2] / bins;
                    val *= bins;
                    temp2[y, x, 2] = (byte)val;
                }
            }
            return temp2;

        }


        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Progowanie

        private byte[,,] ThresholdColor(Image<Bgr, byte> img, double BL, double BH, double GL, double GH, double RL, double RH,int Thresh)
        {
            int thresh = Thresh;
            byte[,,] temp1 = img.Data;
            byte[,,] temp2 = cacheglobal_Buffer.Data;

            for (int x = 0; x < desired_image_size.Width; x++)
            {
                for (int y = 0; y < desired_image_size.Height; y++)
                {
                    if (temp1[y, x, 0] >= BL && temp1[y, x, 0] <= BH &&
                        temp1[y, x, 1] >= GL && temp1[y, x, 1] <= GH &&
                        temp1[y, x, 2] >= RL && temp1[y, x, 2] <= RH)
                    {
                        temp2[y, x, 0] = 255;
                        temp2[y, x, 1] = 255;
                        temp2[y, x, 2] = 255;
                    }
                    else
                    {
                        temp2[y, x, 0] = 0;
                        temp2[y, x, 1] = 0;
                        temp2[y, x, 2] = 0;
                    }
                }
            }
            return temp2;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //OR
        private byte[,,] OROPERATION(Image<Bgr, byte> b1, Image<Bgr, byte> b2)
        {
            byte[,,] temp1, temp2, temp3;
            temp1 = b1.Data;
            temp2 = b2.Data;
            temp3 = cacheglobal_Buffer.Data;
            for (int x = 0; x < desired_image_size.Width; x++)
            {
                for (int y = 0; y < desired_image_size.Height; y++)
                {
                    temp3[y, x, 0] = (byte)(temp1[y, x, 0] | temp2[y, x, 0]);
                    temp3[y, x, 1] = (byte)(temp1[y, x, 1] | temp2[y, x, 1]);
                    temp3[y, x, 2] = (byte)(temp1[y, x, 2] | temp2[y, x, 2]);
                }
            }
            return temp3;
        }
        }

    }


