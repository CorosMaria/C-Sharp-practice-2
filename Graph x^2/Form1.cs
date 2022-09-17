using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Varianta_65_2_Grafic_functie_x_patrat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        System.Drawing.Graphics Desen;
        int poz_x = 50;
        int poz_y = 50;
        int Valoare_maxima_x = 300;
        int Inaltime_chenar = 200; //Inaltime chenar/50 de diviziuni ==> patru patrate
        int Valoare_maxima_y = 200;
        static int[] valori = new int[0];
        osciloscop Osciloscop;
        public class osciloscop
        {
            int x0;
            int y0;
            int w;
            int h;
            int val_max, val_max_af, val, val_v;
            int nr_max;
            System.Drawing.Graphics zona_des;
            System.Drawing.Pen creion_r = new System.Drawing.Pen(System.Drawing.Color.Red);
            System.Drawing.Font font_ni = new System.Drawing.Font("Nina", 8);
            System.Drawing.SolidBrush pens_blu = new System.Drawing.SolidBrush(System.Drawing.Color.Blue);
            System.Drawing.SolidBrush radiera = new System.Drawing.SolidBrush(System.Drawing.Color.White);

            System.Drawing.Bitmap img;
            System.Drawing.Bitmap ims;

            public void setval(int[] vals, int Numar_valori)
            {
                img = new Bitmap(nr_max, val_max, zona_des);
                int i, j;

                // afisare grafic sub forma de puncte

                val_v = val_max - 1 - System.Convert.ToInt16(System.Convert.ToDouble(vals[0]) * (System.Convert.ToDouble(h) / System.Convert.ToDouble(val_max))); //scalare
                if (val_v >= val_max)
                    val_v = val_max - 1;
                if (val_v <= 0)
                    val_v = 1;
                for (i = 0; i < w; i++)
                {
                    val = val_max - 1 - System.Convert.ToInt16(System.Convert.ToDouble(vals[i]) * (System.Convert.ToDouble(h) / System.Convert.ToDouble(val_max))); //scalare
                    if (val >= val_max)
                        val = val_max - 1;
                    if (val <= 0)
                        val = 1;
                    if (val_v < val)
                    {
                        for (j = val_v; j <= val; j++)
                            img.SetPixel(i, j, System.Drawing.Color.Red);
                    }
                    else
                    {
                        for (j = val; j <= val_v; j++)
                            img.SetPixel(i, j, System.Drawing.Color.Red);

                    }
                    val_v = val;
                }
                zona_des.DrawImage(ims, x0, y0);
                zona_des.DrawImage(img, x0, y0);

                //valori axa x
                //zona_des.FillRectangle(radiera, x0, y0 + h, w + 20, 20); // pentru afisare dinamica valori axa x
                int d = 0, deplasare = 1;
                for (i = -15; i <= 15; i += 5)
                {
                    deplasare = 50 * d;
                    val = System.Convert.ToInt16(System.Convert.ToDouble(i) * (System.Convert.ToDouble(nr_max) / System.Convert.ToDouble(w))); //scalare
                    zona_des.DrawString(val.ToString(), font_ni, pens_blu, x0 + deplasare, y0 + h);
                    d = d + 1;
                }

                //valori axa y
                //zona_des.FillRectangle(radiera, x0 - 20, y0 - 10, 20, h + 20);// pentru afisare dinamica valori axa y

                for (i = 0; i <= h; i += 50)
                {
                    val = System.Convert.ToInt16(System.Convert.ToDouble(i) * (System.Convert.ToDouble(val_max_af) / System.Convert.ToDouble(h))); //scalare
                    zona_des.DrawString(val.ToString(), font_ni, pens_blu, x0 - 20, y0 + h - i - 10);
                }

            }
            public osciloscop(System.Drawing.Graphics desen, int pozx, int pozy, int n_maxx, int n_maxy, int vmaxa)
            {
                x0 = pozx;
                y0 = pozy;
                w = n_maxx;
                h = n_maxy;
                nr_max = n_maxx;
                val_max = n_maxy;
                val_max_af = vmaxa;
                zona_des = desen;
                int i, j;
                img = new Bitmap(nr_max, n_maxy, zona_des);
                ims = new Bitmap(nr_max, n_maxy, zona_des);
                // sterg imaginea

                for (j = 0; j < val_max; j++)
                {
                    for (i = 0; i < nr_max; i++)
                    {
                        ims.SetPixel(i, j, System.Drawing.Color.WhiteSmoke);
                    }
                }
                // grid
                for (j = 0; j < val_max; j++)
                {

                    // grid orizontal


                    if ((n_maxy - j - 1) % 10 == 0)
                    {
                        for (i = 0; i < nr_max; i++)
                        {
                            if ((n_maxy - j - 1) % 50 == 0)
                                ims.SetPixel(i, j, System.Drawing.Color.Gray);
                            else
                                ims.SetPixel(i, j, System.Drawing.Color.LightGray);
                        }
                    }
                    else
                    {

                        // grid orizontal vertical

                        for (i = 0; i < nr_max; i++)
                        {
                            if (i % 10 == 0)
                            {
                                if (i % 50 == 0)
                                    ims.SetPixel(i, j, System.Drawing.Color.Gray);
                                else
                                    ims.SetPixel(i, j, System.Drawing.Color.LightGray);
                            }
                        }
                    }
                }

                //chenar

                for (i = 0; i < n_maxx; i++)
                {
                    ims.SetPixel(i, 0, System.Drawing.Color.Blue);
                    ims.SetPixel(i, val_max - 1, System.Drawing.Color.Blue);
                }
                for (j = 0; j < val_max; j++)
                {
                    ims.SetPixel(0, j, System.Drawing.Color.Blue);
                    ims.SetPixel(nr_max - 1, j, System.Drawing.Color.Blue);
                }

            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Desen = this.CreateGraphics();
            Array.Resize(ref valori, Valoare_maxima_x + 1);
            Osciloscop = new osciloscop(Desen, poz_x, poz_y, Valoare_maxima_x, Inaltime_chenar, Valoare_maxima_y);
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            int Amplificarea = 20;
            double x = -3; //Valoare inceput
            double d_max = 6; //La valori mai mari da eroare 
            double pas = d_max / Valoare_maxima_x;
            for (int i = 0; i <= Valoare_maxima_x; i++)
            {
                int f = System.Convert.ToInt32(Amplificarea * (x*x));
                x += pas;
                valori[i] = f;
            }
            Osciloscop.setval(valori, Valoare_maxima_x);
        }
    }
}
