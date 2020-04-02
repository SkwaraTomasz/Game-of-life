using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gameOfLife
{
    public partial class Form1 : Form
    {
        int x, y, liczkakrokow;
        public Graphics rysunek1 { get; set; }
        private Bitmap bitmapa1;
        String stanPoczatkowy;
        SolidBrush zywa = new SolidBrush(Color.Black);
        SolidBrush martwa = new SolidBrush(Color.White);
        Pen pen = new Pen(Color.Black, 2);
        Komorka[,] tabKomorka;


        public Form1()
        {
            InitializeComponent();
            this.WindowState = FormWindowState.Maximized;
            
            

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int.TryParse(textBox1.Text, out x);
            int.TryParse(textBox2.Text, out y);
            int.TryParse(textBox3.Text, out liczkakrokow);
            stanPoczatkowy = comboBox1.SelectedItem.ToString();

            bitmapa1 = new Bitmap((x*10)+2, (y * 10)+2);
            pictureBox1.Image = bitmapa1;
            rysunek1 = Graphics.FromImage(pictureBox1.Image);
           
            pictureBox1.Size = new System.Drawing.Size((x * 10)+2, (y * 10)+2);

            rysujSiatke();
            tabKomorka = new Komorka[x, y];
            for (int i = 0; i < x;  i++)
            {
                for (int j = 0; j < y; j++)
                {
                    tabKomorka[i, j] = new Komorka(false, i , j );
                }
            }
            switch (stanPoczatkowy)
            {
                case "Glider":
                    {
                        ozyw(tabKomorka[x / 2, y / 2]);
                        ozyw(tabKomorka[(x / 2)+1, y / 2]);
                        ozyw(tabKomorka[(x / 2)-1, (y / 2)+1]);
                        ozyw(tabKomorka[x / 2, (y / 2)+1]);
                        ozyw(tabKomorka[(x / 2)+1, (y / 2) + 2]);

                        break;
                    }
                case "Oscylator":
                    {
                        ozyw(tabKomorka[x / 2, y / 2]);
                        ozyw(tabKomorka[x / 2, (y / 2)+1]);
                        ozyw(tabKomorka[x / 2, (y / 2) + 2]);
                        break;
                    }
                case "Kryształ":
                    {
                        ozyw(tabKomorka[x / 2, y / 2]);
                        ozyw(tabKomorka[(x / 2) + 1, y / 2]);
                        ozyw(tabKomorka[(x / 2) -1, (y / 2)+1]);
                        ozyw(tabKomorka[(x / 2) + 2, (y / 2) + 1]);
                        ozyw(tabKomorka[x / 2, (y / 2)+2]);
                        ozyw(tabKomorka[(x / 2) + 1, (y / 2) + 2]);
                        break;
                    }
                case "Losowy":
                    {
                        Random rand = new Random();
                        for (int i = 0; i < x; i++)
                        
                            for (int j = 0; j < y; j++)
                            { 
                                if (rand.Next(0, 2) == 0)
                                    usmierc(tabKomorka[i, j]);
                                else
                                    ozyw(tabKomorka[i, j]);
                            }
                        break;
                    }
                case "Własny":
                    {

                        break;
                    }




            }

            pictureBox1.Refresh();
        }
        void zmienStan(Komorka komorka) {
            if (komorka.getStan()) {
                komorka.setStan(false);
                rysunek1.FillRectangle(martwa, komorka.getX()*10,komorka.getY()*10,10,10);
                rysunek1.DrawRectangle(pen, komorka.getX() * 10, komorka.getY() * 10, 10, 10);
            }
            else
            {
                komorka.setStan(true);
                rysunek1.FillRectangle(zywa, komorka.getX() * 10, komorka.getY() * 10, 10, 10);
                rysunek1.DrawRectangle(pen, komorka.getX() * 10, komorka.getY() * 10, 10, 10);
            }
        }
        void skopiujStan(Komorka pierwsza, Komorka druga) {
            if (druga.getStan())
            {
                pierwsza.setStan(true);
                rysunek1.FillRectangle(zywa, pierwsza.getX() * 10, pierwsza.getY() * 10, 10, 10);
                rysunek1.DrawRectangle(pen, pierwsza.getX() * 10, pierwsza.getY() * 10, 10, 10);
            }
            else
            {
                pierwsza.setStan(false);
                rysunek1.FillRectangle(martwa, pierwsza.getX() * 10, pierwsza.getY() * 10, 10, 10);
                rysunek1.DrawRectangle(pen, pierwsza.getX() * 10, pierwsza.getY() * 10, 10, 10);
            }
        }
        void ozyw(Komorka komorka) {
            komorka.setStan(true);
            rysunek1.FillRectangle(zywa, komorka.getX() * 10, komorka.getY() * 10, 10, 10);
            rysunek1.DrawRectangle(pen, komorka.getX() * 10, komorka.getY() * 10, 10, 10);
        }
        void usmierc(Komorka komorka) {
            komorka.setStan(false);
            rysunek1.FillRectangle(martwa, komorka.getX() * 10, komorka.getY() * 10, 10, 10);
            rysunek1.DrawRectangle(pen, komorka.getX() * 10, komorka.getY() * 10, 10, 10);
        }
        private void pictureBox1_MouseClick(object sender, MouseEventArgs e)
        {
            zmienStan(tabKomorka[e.X / 10, e.Y / 10]);
            pictureBox1.Refresh();
        }

        private void symulacja() {
            Komorka [,] tabZastepcza = new Komorka[x, y];

            for (int i = 0; i < x; i++)
            {
                for (int j = 0; j < y; j++)
                {
                    tabZastepcza[i, j] = new Komorka(tabKomorka[i, j]);
                }
            }

            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    int licznik = 0;
                    
                    for (int k = i-1; k <= i+1; k++)
                    {
                        for (int l = j-1; l <= j+1; l++) {
                            
                            int tmpY = warunekBrzegowy(l, y);
                            int tmpX = warunekBrzegowy(k, x);
                            if (tmpY == j && tmpX == i) continue;
                            else if  (tabKomorka[tmpX, tmpY].getStan() == true) licznik++;

                        }
                    }
                    if (tabZastepcza[i, j].getStan() == true) {
                        if (licznik ==2) { continue; }
                        else if(licznik ==3) { continue; }
                        else { usmierc(tabZastepcza[i, j]); }
                    }
                    if (tabZastepcza[i, j].getStan() == false && licznik == 3) { ozyw(tabZastepcza[i, j]); }
                    
                
                }
            }
            for (int i = 0; i < x; i++)
                for (int j = 0; j < y; j++)
                    skopiujStan(tabKomorka[i, j], tabZastepcza[i, j]);
        }
        private int warunekBrzegowy(int a, int brzeg)
        {
            
            return a < 0 ? brzeg - 1 : a % brzeg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < liczkakrokow; i++)
            {
                symulacja();
                pictureBox1.Refresh();
                System.Threading.Thread.Sleep(500);
            }
        }

        void rysujSiatke()
        {
             for (int j = 1; j <= (y * 10)+2; j = j + 10)
             {
                    rysunek1.DrawLine(pen, 0,j,x*10,j );
                    rysunek1.DrawLine(pen, j, 0, j, x*10);
             }

         
        }

    }
}
