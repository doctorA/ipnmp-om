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
 
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Oseba janez = new Oseba();
            //janez=Oseba.VrniPoEmšo("1234");
            janez=Oseba.VrniPoIDOsebe(1);
            Oseba[] osebe =Oseba.VrniVse();
            Oseba test = new Oseba();
            test.Ime = "Chuck";
            test.Priimek = "Norris";
            test.Spol = "God";
            test.DatumRojstva = Convert.ToDateTime("01.01.1000");
            test.EMŠO = "1234455god33";
            Naslov naslov_testa = new Naslov();
            naslov_testa.HišnaŠtevilka = "5";
            naslov_testa.Mesto = "Palace";
            naslov_testa.PoštnaŠtevilka = 1;
            naslov_testa.Ulica = "Norris street";
                       
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
           
        }


        /// <summary>
        /// Posodobi osebo v podat. bazi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
           

   

        }

        /// <summary>
        /// Vrne osebo(po emšu)iz podatkovne baze in jo izpiše v text boxih
        /// </summary>
       
        private void button5_Click(object sender, EventArgs e)
        {
          
        }
    }
}
