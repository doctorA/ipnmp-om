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
            IPNMP.IPNMPdb1 vmesnik=new IPNMPdb1();
            vmesnik.UstvariOsebo(oseba);
        }

        private void button2_Click(object sender, EventArgs e)
        {
           int EMŠO = Convert.ToInt32(textBox3.Text);
            IPNMP.IPNMPdb1 vmesnik=new IPNMPdb1();
           vmesnik.IzbrisiOsebo(EMŠO);
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            IPNMP.IPNMPdb1 vmesnik = new IPNMPdb1();
            vmesnik.VrniVseOsebe();
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            IPNMP.Oseba oseba = new Oseba();
            oseba.Ime = textBox1.Text;
            oseba.Priimek = textBox2.Text;
            oseba.EMŠO = Convert.ToInt32(textBox3.Text);
            oseba.DatumRojstva = Convert.ToDateTime(textBox4.Text);
            oseba.Spol = textBox5.Text;
            IPNMP.IPNMPdb1 vmesnik = new IPNMPdb1();
            vmesnik.PosodobiOsebo(oseba);
        }
    }
}
