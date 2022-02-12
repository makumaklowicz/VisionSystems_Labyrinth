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
using System.Threading;
namespace Projekt_Labirynth
{
    public partial class Form1 : Form
    {
        private Size desired_image_size;
        Image<Bgr, byte> viewport_Buffer, path_Buffer, walls_Buffer, dot_Buffer,cacheglobal_Buffer, cacheinfnc_Buffer;
        Mat image_Buffer = new Mat();
        Point Pc;
        Point starto;
        Point end;
        private Random rnd = new Random();
        private int liczba_promieni, kat_poczatkowy;
        private double[][] tabela_promieni;
        private double[][] tabela_promieni_mem;
        bool runMovie = false;
        VideoCapture camera;


        public Form1()
        {
            InitializeComponent();
            desired_image_size = new Size(700, 700);
            viewport_Buffer = new Image<Bgr, byte>(desired_image_size);
            path_Buffer = new Image<Bgr, byte>(desired_image_size);
            walls_Buffer = new Image<Bgr, byte>(desired_image_size);
            dot_Buffer = new Image<Bgr, byte>(desired_image_size);
            cacheglobal_Buffer = new Image<Bgr, byte>(desired_image_size);
            cacheinfnc_Buffer = new Image<Bgr, byte>(desired_image_size);
            try
            {
                camera = new VideoCapture();
                camera.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameWidth, desired_image_size.Width);
                camera.SetCaptureProperty(Emgu.CV.CvEnum.CapProp.FrameHeight, desired_image_size.Height);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void Load_IMG_button_Click(object sender, EventArgs e)
        {
            Viewport.Image = get_image_bitmap_from_file(textbox_path.Text, ref viewport_Buffer);
        }

        private void Segmentation_Click(object sender, EventArgs e)
        {
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
            command_List.Items.Add("Dokonano dylatacji!");
            return temp2;
        }

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
            command_List.Items.Add("Dokonano erozji!");
            return temp2;
        }

        private void DrawVectors_Click(object sender, EventArgs e)
        {
            double dlugi = 0;
            double dlugosc = 0;
            int indeks = 0;
            double kat = 0;
            liczsrodek();
            tabela_promieni_mem = sygnatura_radialna(Pc);
            tabela_promieni = tabela_promieni_mem;
            kat = indeks * 3 * (Math.PI / 180);

            for (int i = 0; i < liczba_promieni; i++)
            {
                dlugosc = Math.Sqrt(Math.Pow(tabela_promieni[0][i], 2) + Math.Pow(tabela_promieni[1][i], 2));

                if (dlugi < dlugosc)
                {
                    dlugi = dlugosc;
                    indeks = i;
                }
            }
            CvInvoke.ArrowedLine(viewport_Buffer, Pc, new Point((int)(Pc.X + tabela_promieni[0][indeks]), (int)(Pc.Y + tabela_promieni[1][indeks])), new MCvScalar(255, 0, 0), 4);
            dlugi = 0;
            if (indeks >= 45 && indeks <= 315)
            {
                for (int r = (indeks - 45); r <= (indeks + 45); r++)
                {
                    tabela_promieni[0][r] = 0;
                    tabela_promieni[1][r] = 0;
                }
            }
            else if (indeks < 45)
            {
                for (int r = 0; r <= (indeks + 45); r++)
                {
                    tabela_promieni[0][r] = 0;
                    tabela_promieni[1][r] = 0;
                }
                for (int r = 315 + indeks; r <= 360; r++)
                {
                    tabela_promieni[0][r] = 0;
                    tabela_promieni[1][r] = 0;
                }
            }
            else if (indeks > 315)
            {
                for (int r = 360; r >= (indeks - 45); r--)
                {
                    tabela_promieni[0][r] = 0;
                    tabela_promieni[1][r] = 0;
                }
                for (int r = 0; r <= (45 - (360 - indeks)); r++)
                {
                    tabela_promieni[0][r] = 0;
                    tabela_promieni[1][r] = 0;
                }
            }

            for (int i = 0; i < liczba_promieni; i++)
            {
                dlugosc = Math.Sqrt(Math.Pow(tabela_promieni[0][i], 2) + Math.Pow(tabela_promieni[1][i], 2));

                if (dlugi < dlugosc)
                {
                    dlugi = dlugosc;
                    indeks = i;
                }
            }
            CvInvoke.ArrowedLine(viewport_Buffer, Pc, new Point((int)(Pc.X + tabela_promieni[0][indeks]), (int)(Pc.Y + tabela_promieni[1][indeks])), new MCvScalar(0, 255, 0), 6);
            Viewport.Image = viewport_Buffer.Bitmap;
            command_List.Items.Add("Narysowano możliwe posunięcia!");
        }

        private void DrawSolve_Click(object sender, EventArgs e)
        {
            double dlugi = 0;
            double dlugosc = 0;
            int indeks = 0;
            bool notEnded = true;
            int cnt = 0;
            liczsrodek();
            starto = Pc;
            end = Pc;
            tabela_promieni_mem = sygnatura_radialna(starto);
            tabela_promieni = tabela_promieni_mem;

            do
            {
                dlugi = 0;
                for (int i = 0; i < liczba_promieni; i++)
                {
                    dlugosc = Math.Sqrt(Math.Pow(tabela_promieni[0][i], 2) + Math.Pow(tabela_promieni[1][i], 2));

                    if (dlugi < dlugosc)
                    {
                        dlugi = dlugosc;
                        indeks = i;
                    }
                }
                end = new Point((int)(starto.X + tabela_promieni[0][indeks]), (int)(starto.Y + tabela_promieni[1][indeks]));
                CvInvoke.ArrowedLine(viewport_Buffer, starto, end , new MCvScalar(0, 0, 255), 4);

                starto.X = starto.X + (int)tabela_promieni[0][indeks];
                starto.Y = starto.Y + (int)tabela_promieni[1][indeks];
                tabela_promieni_mem = sygnatura_radialna(starto);
                tabela_promieni = tabela_promieni_mem;
                indeks = indeks + 180;
                if (indeks > 360)
                {
                    indeks = indeks - 360;
                }
                if (indeks >= 45 && indeks <= 315)
                {
                    for (int r = (indeks - 45); r <= (indeks + 45); r++)
                    {
                        tabela_promieni[0][r] = 0;
                        tabela_promieni[1][r] = 0;
                    }
                }
                else if (indeks < 45)
                {
                    for (int r = 0; r <= (indeks + 45); r++)
                    {
                        tabela_promieni[0][r] = 0;
                        tabela_promieni[1][r] = 0;
                    }
                    for (int r = 315 + indeks; r <= 360; r++)
                    {
                        tabela_promieni[0][r] = 0;
                        tabela_promieni[1][r] = 0;
                    }
                }
                else if (indeks > 315)
                {
                    for (int r = 360; r >= (indeks - 45); r--)
                    {
                        tabela_promieni[0][r] = 0;
                        tabela_promieni[1][r] = 0;
                    }
                    for (int r = 0; r <= (45 - (360 - indeks)); r++)
                    {
                        tabela_promieni[0][r] = 0;
                        tabela_promieni[1][r] = 0;
                    }
                }
                cnt++;

           } while (cnt<36);
            Viewport.Image = viewport_Buffer.Bitmap;
            command_List.Items.Add("Narysowano rozwiązanie!");
        }

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
            command_List.Items.Add("Wykonano filtrowanie górnoprzepustowe!");
            return temp2;
        }

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
            command_List.Items.Add("Skwantyzowano!");
            return temp2;
        }

        private void browse_Click(object sender, EventArgs e)
        {
            textbox_path.Text= get_image_path();
        }

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
            command_List.Items.Add("Wykonano progowanie!");
            return temp2;
        }

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
            command_List.Items.Add("Wykonano sumę logiczną!");
            return temp3;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dot_Buffer.Data = ThresholdColor(viewport_Buffer, 0, 170, 0, 255, 0, 255, 255);
            dot_Buffer.Data = Dilate(dot_Buffer);

            path_Buffer.Data = Quant(viewport_Buffer, 220);
            path_Buffer.Data = OROPERATION(dot_Buffer, path_Buffer);
            path_Buffer.Data = Dilate(path_Buffer);
            path_Buffer.Data = Dilate(path_Buffer);
            path_Buffer.Data = Erode(path_Buffer);
            path_Buffer.Data = Erode(path_Buffer);
            path_Buffer.Data = ThresholdColor(path_Buffer, 0, 170, 0, 255, 0, 255, 220);
            path_Buffer.Data = ThresholdColor(path_Buffer, 0, 170, 0, 255, 0, 255, 220);


            walls_Buffer.Data = filterHIGH(viewport_Buffer);
            walls_Buffer.Data = filterHIGH(walls_Buffer);
            walls_Buffer.Data = ThresholdColor(walls_Buffer, 0, 230, 0, 230, 0, 230, 255);
            walls_Buffer.Data = ThresholdColor(walls_Buffer, 0, 230, 0, 230, 0, 230, 255);
            walls_Buffer.Data = Quant(walls_Buffer, 160);
            walls_Buffer.Data = ThresholdColor(walls_Buffer, 0, 100, 0, 255, 0, 255, 160);

            viewport_Path.Image = path_Buffer.Bitmap;
            viewport_Dot.Image = dot_Buffer.Bitmap;
            viewport_Walls.Image = walls_Buffer.Bitmap;
        }

        private void cam_photo_Click(object sender, EventArgs e)
        {
            Viewport.Image = get_image_bitmap_from_camera(ref viewport_Buffer);
        }

        private Bitmap get_image_bitmap_from_camera(ref Image<Bgr, byte> Data)
        {
            Mat temp = new Mat();
            camera.Read(temp);
            CvInvoke.Resize(temp, temp, desired_image_size);
            Data = temp.ToImage<Bgr, byte>();
            return Data.Bitmap;
        }

        private void cam_film_Click(object sender, EventArgs e)
        {
            runMovie = !runMovie;

            timer1.Enabled = runMovie;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Mat temp = camera.QueryFrame();
            CvInvoke.Resize(temp, temp, Viewport.Size);
            viewport_Buffer = temp.ToImage<Bgr, byte>();
            Viewport.Image = viewport_Buffer.Bitmap;

            button1.PerformClick();
            DrawVectors.PerformClick();
            DrawSolve.PerformClick();

        }

        private void liczsrodek()
        {
            double[][] tablica = new double[2][];
            tablica[0] = new double[liczba_promieni + 1];
            tablica[1] = new double[liczba_promieni + 1];
            double F, Sx, Sy, x0, y0;
            double Jx0, Jy0, Jx0y0, Jx, Jy, Jxy, Je_0, Jt_0;
            double alfa_e, alfa_t, alfa_e_deg, alfa_t_deg;
            F = Sx = Sy = Jx0 = Jy0 = Jx0y0 = Jx = Jy = Jxy = Je_0 = Jt_0 = alfa_e = alfa_t = alfa_e_deg = alfa_t_deg = x0 = y0 = 0;
            CvInvoke.Rectangle(dot_Buffer, new Rectangle(0, 0, desired_image_size.Width, desired_image_size.Height), new MCvScalar(0, 0, 0), 2);
            viewport_Dot.Image = dot_Buffer.Bitmap;
            Application.DoEvents();
            byte[,,] temp = dot_Buffer.Data;
            for (int X = 0; X < desired_image_size.Width; X++)
            {
                for (int Y = 0; Y < desired_image_size.Height; Y++)
                {
                    if (temp[Y, X, 0] == 0xFF && temp[Y, X, 1] == 0xFF && temp[Y, X, 2] == 0xFF)
                    {
                        F = F + 1;
                        Sx = Sx + Y;
                        Sy = Sy + X;
                        Jx = Jx + Math.Pow(Y, 2);
                        Jy = Jy + Math.Pow(X, 2);
                        Jxy = Jxy + X * Y;
                    }
                }
            }
            if (F > 0)
            {
                x0 = Sy / F;
                y0 = Sx / F;
            }
     

            Pc = new Point((int)x0, (int)y0);
            command_List.Items.Add("Wyliczono środek kropki!");
        }

        private double[][] sygnatura_radialna(Point start)
        {
            liczba_promieni =360;
            kat_poczatkowy = 90;
            MCvScalar kolor_promienia = new MCvScalar();
            double[,] katy_kolejnych_promieni = new double[liczba_promieni, 2];
            double[][] promienie = new double[2][];
            promienie[0] = new double[liczba_promieni+1];
            promienie[1] = new double[liczba_promieni+1];
            double krok_katowy, aktualny_kat;

            generuj_losowy_kolor(ref kolor_promienia);
            aktualny_kat = kat_poczatkowy * (Math.PI / 180);

                krok_katowy = (2 * Math.PI / liczba_promieni);

            for (int i = 0; i < liczba_promieni; i++)
            {
                katy_kolejnych_promieni[i, 0] = Math.Cos(aktualny_kat);
                katy_kolejnych_promieni[i, 1] = Math.Sin(aktualny_kat);
                aktualny_kat += krok_katowy;
            }

            byte[,,] temp1 = path_Buffer.Data;
            int zakres = (int)Math.Sqrt(Math.Pow(desired_image_size.Width, 2) + Math.Pow(desired_image_size.Height, 2));
            for (int p = 0; p < liczba_promieni; p++)
            {
                for (int d = 0; d < zakres; d++)
                {
                    Point cp = new Point();
                    double dx, dy;
                    dx = (d * katy_kolejnych_promieni[p, 0]);
                    dy = (d * katy_kolejnych_promieni[p, 1]);
                    if (Math.Abs(dx) < zakres && Math.Abs(dy) < zakres)
                    {
                        cp.X = (int)(start.X + dx);
                        cp.Y = (int)(start.Y + dy);
                        if (temp1[cp.Y, cp.X, 0] == 0x00)
                        {
                            CvInvoke.Line(path_Buffer, start, cp, kolor_promienia, 1);
                            promienie[0][p] = dx;
                            promienie[1][p] = dy;
                            break;
                        }
                    }
                }
            }

            viewport_Path.Image = path_Buffer.Bitmap;
            return promienie;
        }

        private int modulo(int a, int b)
        {
            return (Math.Abs(a * b) + a) % b;
        }

        private void generuj_losowy_kolor(ref MCvScalar kolor)
        {
            kolor.V0 = rnd.Next(0, 255);
            kolor.V1 = rnd.Next(0, 255);
            kolor.V2 = rnd.Next(0, 255);
        }

        private string get_image_path()
        {
            string ret = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Obrazy|*.jpg;*.jpeg;*.png;*.bmp";
            openFileDialog1.Title = "Wybierz obrazek.";
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                ret = openFileDialog1.FileName;
            }
            return ret;
        }

        private Bitmap get_image_bitmap_from_file(string path, ref Image<Bgr, byte> Data)
        {
            Mat temp = CvInvoke.Imread(path);
            CvInvoke.Resize(temp, temp, desired_image_size);
            Data = temp.ToImage<Bgr, byte>();
            return Data.Bitmap;
        }


    }

}


