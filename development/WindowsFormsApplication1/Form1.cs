using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPNMP;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Zaposleni marija = Zaposleni.VrniPoEmšo("1231454646");
            Poročilo[] zgodovina_del = Poročilo.VrniPorocilaPoAvtorju(marija);
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Oseba janez = new Oseba();
            janez.Ime = "janez";
            janez.Priimek = "Novak";
            janez.Spol = "Ženski";
            janez.DatumRojstva = Convert.ToDateTime("01.01.1944");
            janez.EMŠO = "1234567";
            janez.Naslov = Naslov.VrniNaslov(1);

            Pacient bolan_janez = new Pacient(janez);
            bolan_janez.KrvnaSkupina = "B";
            bolan_janez.Teža = 200000;
            bolan_janez.Višina = 19999;
            bolan_janez.ZZZS = 987654321;
            bolan_janez.Kartoteka = Kartoteka.VrniKartoteko(1);
            bolan_janez.Ustvari();
            bolan_janez.VrniStarost();
              
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Oseba[] spisek_oseb =Oseba.VrniVse();
            
           
        }


        /// <summary>
        /// Posodobi osebo v podat. bazi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            IPNMP.Oseba oseba = new Oseba();
            oseba.Ime = textBox1.Text;
            oseba.Priimek = textBox2.Text;
            oseba.EMŠO = textBox3.Text;
            oseba.DatumRojstva = Convert.ToDateTime(textBox4.Text);
            oseba.Spol = textBox5.Text;
            oseba.Posodobi();

   

        }

        /// <summary>
        /// Vrne osebo(po emšu)iz podatkovne baze in jo izpiše v text boxih
        /// </summary>
       
        private void button5_Click(object sender, EventArgs e)
        {
            Oseba oseba = IPNMP.Oseba.VrniPoEmšo(textBox3.Text);
            textBox1.Text = oseba.Ime;
            textBox2.Text = oseba.Priimek;
            textBox4.Text = oseba.DatumRojstva.ToString();
            textBox5.Text = oseba.Spol;
            textBox6.Text = oseba.Naslov.ToString();
        }
    }
}
