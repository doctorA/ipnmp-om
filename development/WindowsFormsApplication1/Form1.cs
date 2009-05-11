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
            IPNMP.Oseba oseba = new Oseba();
            oseba.Ime=textBox1.Text;
            oseba.Priimek=textBox2.Text;
            oseba.EMŠO=Convert.ToInt32(textBox3.Text);
            oseba.DatumRojstva=Convert.ToDateTime(textBox4.Text);
            oseba.Spol=textBox5.Text;
           
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           int EMŠO = Convert.ToInt32(textBox3.Text);
           Oseba tmp = new Oseba();
           tmp.EMŠO = EMŠO;
                tmp.IzbrisiOsebo();
                Ekipa nekaj = new Ekipa();
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Oseba[] lastnosti =Oseba.VrniVseOsebe();
            
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IPNMP.Oseba oseba = new Oseba();
            oseba.Ime = textBox1.Text;
            oseba.Priimek = textBox2.Text;
            oseba.EMŠO = Convert.ToInt32(textBox3.Text);
            oseba.DatumRojstva = Convert.ToDateTime(textBox4.Text);
            oseba.Spol = textBox5.Text;
            oseba.PosodobiOsebo();
            

            
            
            

            

            

               

        }

        private void button5_Click(object sender, EventArgs e)
        {
            IPNMP.Oseba oseba = new Oseba(Convert.ToInt32(textBox3.Text));
            textBox1.Text = oseba.Ime;
            textBox2.Text = oseba.Priimek;
            textBox4.Text = oseba.DatumRojstva.ToString();
            textBox5.Text = oseba.Spol;
            textBox6.Text = oseba.Naslov.ToString();
        }
    }
}
