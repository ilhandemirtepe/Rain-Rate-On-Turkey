using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MuhendislikYagmur.Class;

namespace MuhendislikYagmur
{
    public partial class Form1 : Form
    {
        YagmurEntities yagmurum = new YagmurEntities();
        Ozellikler ozel = new Ozellikler();
        Random r = new Random();
        public Color RandomColor()//burda rastgele renkler üretiyoruz
        {
            return Color.FromArgb(80,r.Next(0, 255), r.Next(0, 255), r.Next(0, 255));
        }

        public void Getir()
        {
            int ilId = int.Parse((comboBox1.SelectedItem as ComboboxItem).Value); //comboboxtan1 1. il ıd al
            var xEksen = from x in yagmurum.Ozellikler where x.İlNo == ilId select x; //databaseden 1. ilin verileri çek
            int il2id = int.Parse((comboBox2.SelectedItem as ComboboxItem2).Value); //comboboxtan 2.il ıd al
            var x2Eksen = from x2 in yagmurum.Ozellikler where x2.İlNo == il2id select x2; //databaseden 2.ilin verileri çek
            int boyut = xEksen.ToList().Count;//Seçtiğin 1. ilde kaç defa  yağmur yağdı saymaya yarar
            int boyut2 = x2Eksen.ToList().Count; //seçtiğin 2.ilde kaç defa yağmur yağdır saymaya yarar
            int[,] matris = new int[boyut, boyut];// 1.il için 2 boyutlu oldu satırlar yılları,sütünlar miktarı tutar 
            int[,] matris2 = new int[boyut2, boyut2];
            int i = 0;

            foreach (var item in xEksen)
            {
                matris[i, 0] = item.Yillar.Value;      //satırları yıl yap
                matris[i, 1] = item.YagmurMiktar.Value;  //sütünları miktar  yap
                i++;
            }
            int i2 = 0;
            foreach (var item2 in x2Eksen)
            {
                matris2[i2, 0] = item2.Yillar.Value;
                matris2[i2, 1] = item2.YagmurMiktar.Value;
                i2++;
            }
            Graphics gr = this.CreateGraphics();
            Pen pencil = new Pen(RandomColor(), 10);
            Pen penciil1 = new Pen(Color.Aqua, 1);

            Pen pencil2 = new Pen(RandomColor(), 10);
            Pen penciil12 = new Pen(Color.Azure, 1);
            int posX = 100;
            int posX2 =80;
            int posY = 400;
            int posY2 = 400;
            int incrementX =60;
            int incrementY = -50;
            int incrementX2 =50;
            int incrementY2 = -30;
            int bound0 = matris.GetUpperBound(0);//yıllar==yıl sayısı  satir sayısı kadar yıl tut
            int bound1 = matris.GetUpperBound(1);// miktarlar==her bir yıl ne kadar yağmur yazğdığını tut
            int bound02 = matris2.GetUpperBound(0);
            int bound12 = matris2.GetUpperBound(1);
            List<int> yazdirilanlarYillar = new List<int>();
            List<int> yazdirilanlarMiktarlar = new List<int>();
            List<int> yazdirilanlarYillar2 = new List<int>();
            List<int> yazdirilanlarMiktarlar2 = new List<int>();

            for (int a = 0; a <= bound0; a++)//yıl = X EKSENİ sen burada bound1 de yazabilirsin dostum
            {
                incrementY = (-1 * matris[a, 1]);
                if (!yazdirilanlarYillar.Contains(matris[a, 0]))//iki defa 2000 yazısı yazmayı engeller
                {
                    // x eksenindeki yılları  çizdirdik
                    gr.DrawString(matris[a, 0].ToString(), new Font("Arial", 10), new SolidBrush(Color.Brown), posX + incrementX-15, posY);
                }
                if (!yazdirilanlarMiktarlar.Contains(matris[a, 1]))//iki defa 10 yazısı engellemek için yazdık
                {
                    // y eksenindeki miktarları  çizdirdik
                    gr.DrawString(matris[a, 1].ToString(), new Font("Arial", 10), new SolidBrush(Color.Brown), posX, posY + incrementY);
                }

                if (!yazdirilanlarYillar.Contains(matris[a, 0]))//yıllar da mesela  2000 yılında 2 defa yazğmur yazğdı onu düzeltir
                {
                    // Miktarlar boyunca X ekseni uzrenideki line ları çizdirdik  //yanlama
                    gr.DrawLine(penciil1, posX, posY + incrementY, posX + incrementX, posY + incrementY);
                    // Yıllar boyunca y leri çizdirecez   //dikey
                    gr.DrawLine(pencil, posX + incrementX, posY, posX + incrementX, posY + incrementY);
                    yazdirilanlarYillar.Add(matris[a, 0]);
                }
                else //BURAYI DÜZELTTİK
                {
                    incrementX -= 40;
                    // Miktarlar boyunca X ekseni uzrenideki line ları çizdirdik //yanlama
                     gr.DrawLine(penciil1, posX, posY + incrementY, posX + incrementX, posY + incrementY);
                    // Yıllar boyunca y leri çizdirecez  //dikey
                     gr.DrawLine(pencil, posX + incrementX, posY, posX + incrementX, posY + incrementY);

                }
                incrementX += 40; 
            }
            for (int a2 = 0; a2 <= bound02; a2++)//yıl = X EKSENİ sen burada bound1 de yazabilirsin dostum
            {
                incrementY2 = (-1 * matris2[a2, 1]);
              
                if (!yazdirilanlarMiktarlar2.Contains(matris2[a2, 1]))//iki defa 10 yazısı engellemek için yazdık
                {
                    // y eksenindeki miktarları  çizdirdik
                     gr.DrawString(matris2[a2, 1].ToString(), new Font("Arial", 10), new SolidBrush(Color.Brown), posX2, posY2 + incrementY2);
                }


                if (!yazdirilanlarYillar2.Contains(matris2[a2, 0]))//yıllar da mesela  2000 yılında 2 defa yazğmur yazğdı onu düzeltir
                {
                    // Miktarlar boyunca X ekseni uzrenideki line ları çizdirdik  //yanlama
                    gr.DrawLine(penciil12, posX2, posY2 + incrementY2, posX2 + incrementX2, posY2 + incrementY2);
                    // Yıllar boyunca y leri çizdirecez   //dikey
                    gr.DrawLine(pencil2, posX2 + incrementX2, posY2, posX2 + incrementX2, posY2 + incrementY2);
                    yazdirilanlarYillar2.Add(matris2[a2, 0]);
                }
                else //BURAYI DÜZELTTİK
                {
                     incrementX2 -=50;
                    // Miktarlar boyunca X ekseni uzrenideki line ları çizdirdik //yanlama
                     gr.DrawLine(penciil12, posX2, posY2 + incrementY2, posX2 + incrementX2, posY2 + incrementY2);
                    // Yıllar boyunca y leri çizdirecez  //dikey
                    gr.DrawLine(pencil2, posX2 + incrementX2, posY2, posX2 + incrementX2, posY2 + incrementY2);

                }
                incrementX2 +=55; 
              
            }

        }

        public void Doldur()//illerin hepsini combobox'a at
        {
            var illerimiz = from p in yagmurum.iller select p;
            foreach (var item in illerimiz)
            {
                ComboboxItem newItem = new ComboboxItem();
                newItem.Text = item.iladi;
                newItem.Value = item.ID.ToString();
                comboBox1.Items.Add(newItem);
            }
            var illerimiz2 = from p2 in yagmurum.iller select p2;
            foreach (var item in illerimiz2)
            {
                ComboboxItem2 newItem2 = new ComboboxItem2();
                newItem2.Text = item.iladi;
                newItem2.Value = item.ID.ToString();
                comboBox2.Items.Add(newItem2);
            }

        }

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            draw();//eksenleri çizmeyi çağırdık
        }

        private void draw()//Eksenleri çizmeye yarar
        {
            Graphics g = this.CreateGraphics();
            Pen pencil = new Pen(Color.Red, 4);
            g.DrawLine(pencil, 100, 400, 600, 400);//x ekseni
            g.DrawLine(pencil, 100, 400, 100, 100);//y ekseni
        }

        private void button1_Click(object sender, EventArgs e)//bu buton çizmeye yarar
        {
            label1.Visible = true;
            label2.Visible = true;
            draw();
            Getir();
        }

        private void button2_Click(object sender, EventArgs e)//Bu buton temizlenmeye yarar
        {
            this.Invalidate();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Doldur();
        }
    }
}


