using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Varianta_64_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public System.Drawing.Graphics Desen;
        public System.Drawing.Pen Creion_albastru;
        public System.Drawing.Pen Creion_gri;
        public System.Drawing.SolidBrush Radiera;
        public System.Drawing.SolidBrush Pensula_gri;
        public System.Drawing.Font Font_nina;
        public Termometru termometru1;
        public System.Random nr;

        private void Form1_Load(object sender, EventArgs e)
        {
            Desen = this.CreateGraphics();
            Creion_albastru = new System.Drawing.Pen(System.Drawing.Color.Blue);
            Creion_gri = new System.Drawing.Pen(System.Drawing.Color.Gray);
            Radiera = new System.Drawing.SolidBrush(this.BackColor);
            Pensula_gri = new System.Drawing.SolidBrush(System.Drawing.Color.Gray);
            Font_nina = new System.Drawing.Font("Nina", 8);
            nr = new System.Random();
            termometru1 = new Termometru();
            termometru1.Initializare_termometru(100, 50, 20, 200, 200);
        }
        public class Termometru
        {
            float Coordonata_inceput_x;
            float Coordonata_inceput_y;
            float Latime_termometru;
            float Inaltime_termometru;
            float val_max;
            System.Drawing.SolidBrush Pensula_rosie = new System.Drawing.SolidBrush(System.Drawing.Color.Red);
            System.Drawing.SolidBrush Pensula_galbena = new System.Drawing.SolidBrush(System.Drawing.Color.Yellow);

            public void Desenez(System.Drawing.Graphics Zona_desenare, System.Drawing.Pen Creion_a, System.Drawing.Pen Creion_g, System.Drawing.SolidBrush Pensula_r, System.Drawing.Font Font_n)
            {
                Zona_desenare.DrawRectangle(Creion_a, Coordonata_inceput_x, Coordonata_inceput_y, Latime_termometru, Inaltime_termometru);//Desenam corpul termometrului
                for (int j = 0; j <= Inaltime_termometru; j += 5)// desenez gradatii
                {
                    if (j % 25 == 0)
                    {
                        Zona_desenare.DrawLine(Creion_g, Coordonata_inceput_x + Latime_termometru + 2, Coordonata_inceput_y + j, Coordonata_inceput_x + Latime_termometru + 12, Coordonata_inceput_y + j);
                        Zona_desenare.DrawString(System.Convert.ToString(val_max - j * val_max / Inaltime_termometru), Font_n, Pensula_r, Coordonata_inceput_x + Latime_termometru + 20, Coordonata_inceput_y + j - 7);
                    }
                    else
                    {
                        Zona_desenare.DrawLine(Creion_g, Coordonata_inceput_x + Latime_termometru + 2, Coordonata_inceput_y + j, Coordonata_inceput_x + Latime_termometru + 7, Coordonata_inceput_y + j);
                    }
                }
            }
            public void Sterg(System.Drawing.Graphics Zona_desenare, System.Drawing.SolidBrush Rad)
            {
                Zona_desenare.FillRectangle(Rad, Coordonata_inceput_x + 1, Coordonata_inceput_y + 1, Latime_termometru - 1, Inaltime_termometru -1);
            }
            public void Seteaza_val(float val, System.Drawing.Graphics Zona_desenare, System.Drawing.SolidBrush Pensula_r)
            {
                val = System.Convert.ToInt16(System.Convert.ToDouble(val) * (System.Convert.ToDouble(Inaltime_termometru) / System.Convert.ToDouble(val_max))); //scalare
                Zona_desenare.FillRectangle(Pensula_r, Coordonata_inceput_x + 1, Coordonata_inceput_y + Inaltime_termometru - val, Latime_termometru - 1, val);

            }
            public void Initializare_termometru(float pozx, float pozy, float latime, float inaltime, float valoarea_max)
            {
                Coordonata_inceput_x = pozx;
                Coordonata_inceput_y = pozy;
                Latime_termometru = latime;
                Inaltime_termometru = inaltime;
                val_max = valoarea_max;
                
            }
            public void Umple_termometru(System.Drawing.Graphics Zona_desenare, int valoare)
            {
                if (valoare < 150)
                {
                    Zona_desenare.FillRectangle(Pensula_galbena, Coordonata_inceput_x+1, Coordonata_inceput_y+200-valoare, Latime_termometru-1, valoare);
                }
                else
                {
                    Zona_desenare.FillRectangle(Pensula_rosie, Coordonata_inceput_x+1, Coordonata_inceput_y+200-valoare, Latime_termometru-1, valoare);
                }
            }
        }
        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            termometru1.Desenez(Desen, Creion_albastru, Creion_gri, Pensula_gri, Font_nina);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            termometru1.Sterg(Desen, Radiera);
            termometru1.Umple_termometru(Desen, trackBar1.Value);
        }
    }
}