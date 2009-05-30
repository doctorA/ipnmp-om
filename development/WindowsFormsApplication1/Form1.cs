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
            /*
            Oseba janez = new Oseba();
            //janez=Oseba.VrniPoEmšo("1234");
            janez=Oseba.VrniPoIDOsebe(1);
            Oseba[] osebe =Oseba.VrniVse();
            Oseba test = new Oseba();
            test.Ime = "Chuck";
            test.Priimek = "Norris";
            test.Spol = "God";
            test.DatumRojstva = Convert.ToDateTime("01.01.1900");
            test.EMŠO = "1234455god33";
            Naslov naslov_testa = new Naslov();
            naslov_testa.HišnaŠtevilka = "5";
            naslov_testa.Mesto = "Palace";
            naslov_testa.PoštnaŠtevilka = 1;
            naslov_testa.Ulica = "Norris street";

            naslov_testa.IDNaslova = naslov_testa.Ustvari();
            test.Naslov = naslov_testa;
            test.Ustvari();
            */
            Oseba achmed = new Oseba();
            achmed = Oseba.VrniPoEmšo("1234");
            achmed = Oseba.VrniPoIDOsebe(40);
            Oseba[] achmedi = Oseba.VrniVse();
            achmed.Priimek = "kosica";
            achmed.Posodobi();
            //achmed.Izbrisi();

                       
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
           
        }



        /// <summary>
        /// Vrne osebo(po emšu)iz podatkovne baze in jo izpiše v text boxih
        /// </summary>
       
        private void button5_Click(object sender, EventArgs e)
        {

           // TestPacientVrniPoEmšo();
            //TestPacientVrniPoImenu();
            //TestPacientLokalneMetode();
           // TestPacientVrniVse();
            TestPacientPosodobi();
        }

        public void TestPacientUstvari()
        {
            Pacient marija = new Pacient();
            marija.Ime = "Marija";
            marija.Priimek = "Klobasa";
            marija.Spol = "ženski";
            marija.EMŠO = "123456789";
            marija.DatumRojstva = Convert.ToDateTime("01.01.1950");
            marija.KrvnaSkupina = "AB-";
            marija.Naslov = Naslov.VrniNaslov(122);
            marija.Teža = 9999;
            marija.Višina = 220;
            marija.ZZZS = "99494";
            marija.Ustvari();
                    }

        public void TestPacientVrniPoEmšo()
        {
            Pacient emsek = new Pacient();
            emsek = Pacient.VrniPoEmšo("2807976505444");
            

        }

        public void TestPacientVrniPoImenu()
        {
            Pacient[] rezultat = Pacient.VrniVsePoImenu("julija", "kosica");

        }

        /// <summary>
        /// test metod pacienta, ki ne potrebujejo povezave s bazo, samo napolnjen objekt 
        /// </summary>
        public void TestPacientLokalneMetode()
        {
            Pacient emsek = new Pacient();
            emsek = Pacient.VrniPoEmšo("2807976505444");
            Diagnoza[] d=emsek.VrniAlergije();
            Terapija[] t = emsek.VrniOperacije();
            int starost = emsek.VrniStarost();

        }

        public void TestPacientVrniVse()
        {
            Pacient[] vsi = Pacient.VrniVse();
        }

        public void TestPacientIzbrisi()
        {
            //še ni testirano, saj ne dela dodajanje, v bazi pa so samo trije pacienti
        }

        public void TestPacientPosodobi()
        {
            Pacient emsek = new Pacient();
            emsek = Pacient.VrniPoEmšo("2807976505444");
            emsek.Teža = 345;
            emsek.Posodobi();

        }



        /// <summary>
        /// Posodobi osebo v podat. bazi
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

             //TestZaposleniUstvari();  //ne dela
             //TestZaposleniPosodobi();  //ne dela
            //TestZaposleniBrisi();
            //TestZaposleniVrniPoEmso();
            //TestZaposleniVrniVse();
            //TestZaposleniVrniPoTipu();
            TestZaposleniVrniPoImenu();


        }


        public void TestZaposleniUstvari()
        {
            Zaposleni joze = new Zaposleni();
            joze.Ime = "Joze";
            joze.Priimek = "Čevap";
            joze.Spol = "moški";
            joze.DatumRojstva = Convert.ToDateTime("01.10.1960");
            joze.Naslov = Naslov.VrniNaslov(122);
            joze.EMŠO = "3546782";
            joze.DatumZaposlitve = Convert.ToDateTime("05.11.2000");
            joze.TipZaposlenega = "Brisalec";
            joze.Specializacija = "oken";
            joze.Ustvari();


        }
        public void TestZaposleniPosodobi()
        {
            Zaposleni lol = Zaposleni.VrniPoEmšo("1605969500555");
            lol.Specializacija = "dr.ek";
            lol.Posodobi();
        }
        public void TestZaposleniBrisi()
        {
            Zaposleni tst = Zaposleni.VrniPoEmšo("0101990500000");
            tst.Izbrisi();

        }
        public void TestZaposleniVrniPoEmso()
        {
            Zaposleni test = Zaposleni.VrniPoEmšo("0101990500000");

        }

        public void TestZaposleniVrniVse()
        {
            Zaposleni[] vsi = Zaposleni.VrniVse();

        }

        public void TestZaposleniVrniPoTipu()
        {
            Zaposleni[] test = Zaposleni.VrniVsePoTipu("tehnik");
        }

        public void TestZaposleniVrniPoImenu()
        {
            Zaposleni[] test = Zaposleni.VrniVsePoImenu("david", "racman");

        }

    }
}

