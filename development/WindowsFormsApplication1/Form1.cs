using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IPNMP;
using csUnit;

namespace WindowsFormsApplication1
{
    [TestFixture]
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
            achmed.Priimek = "kosica2";
            achmed.Posodobi();
            //achmed.Izbrisi();

                       
          
        }

        private void button3_Click(object sender, EventArgs e)
        {
            TestKartotekaVrniID();
            //TestKartotekaVrniIDpoID(); //pri testnih parametrih (int = 12) vrne vrednost 120 => metoda dela
           
        }

        /// <summary>
        /// Testiranje metod za kartoteko
        /// </summary>

        public void TestKartotekaVrniID()
        {
            //Subquery returned more than 1 value. This is not permitted when the subquery follows =, !=, <, <= , >, >= or when the subquery is used as an expression.

            int id_test = 12; 
            Kartoteka[] rezultat = Kartoteka.VrniKartotekePoIdPacienta(id_test);
        }

        public void TestKartotekaVrniIDpoID()
        {
            int id_test = 12;
            int rezultat = Kartoteka.VrniStevilkoKartotekePoIdPacienta(id_test);
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
           // TestPacientPosodobi();
           // TestPacientIzbrisi();  //ne dela
            TestPacientUstvari();
        }

        [Test]
        public void TestPacientUstvari()
        {
            Pacient marija = new Pacient();
            marija.Ime = "Marija2";
            marija.Priimek = "Klobasa3";
            marija.Spol = "ženski";
            marija.EMŠO = "12345678932";
            marija.DatumRojstva = Convert.ToDateTime("01.01.1950");
            marija.KrvnaSkupina = "AB-";
            marija.Naslov = Naslov.VrniNaslov(122);
            marija.Teža = 9999;
            marija.Višina = 220;
            marija.ZZZS = "99494";
            marija.Ustvari();
                    }
        [Test]
        public void TestPacientVrniPoEmšo()
        {
            Pacient emsek = new Pacient();
            emsek = Pacient.VrniPoEmšo("2807976505444");


            Assert.NotNull(emsek);
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
            Pacient tst = new Pacient();
            tst = Pacient.VrniPoIdPacient(17);
            tst.Izbrisi();
        }

        [Test]
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

             TestZaposleniUstvari();  //ne dela
             //TestZaposleniPosodobi();  
            //TestZaposleniBrisi();
            //TestZaposleniVrniPoEmso();
            //TestZaposleniVrniVse();
            //TestZaposleniVrniPoTipu();
            //TestZaposleniVrniPoImenu();


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
            lol.Specializacija = "dr.ek2";
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

        /// <summary>
        /// Testiranje metode Poročilo
        /// </summary>
        private void button6_Click(object sender, EventArgs e)
        {
            //TestPorociloPosodobi(); //storage procedure so napačno spisane
            //TestPorociloUstvari(); //dela
            //TestPorociloVrniVse(); //dela
           // TestPorociloPoPacientu();
           // TestPorociloUstvari();  //Dela
           // TestPorociloPosodobi();  //dela

        }

        

        public void TestPorociloPosodobi()
        {
            int id = 5334;

            List<Zaposleni> seznam = new List<Zaposleni>();

            seznam.Add(Zaposleni.VrniPoEmšo("0110960500456"));
            seznam.Add(Zaposleni.VrniPoEmšo("1605969500555"));
            

            Poročilo test = Poročilo.VrniPorociloPoID(id);
            test.OpisDogodka = "možganska kap";
            test.Ekipa = seznam.ToArray();
            test.PosodobiPorocilo();
            
        }

        public void TestPorociloUstvari()
        {
            Poročilo porocilo = new Poročilo();
           

            porocilo.OpisDogodka = "Kolaterarna škoda, ki je nastala zaradi Chuck Norrisovega roundhouse kicka";
            porocilo.StanjePacientaObPrispetju = "Pacient mrtev, pulz 0";
            porocilo.StanjePacientaObPrispetjuVBolnišnico = "Pacient mrtev, pulz 0";
            porocilo.AkcijeReševalcev = "Razgrnitev mrtvaške vrečke, pobiranje človeških udov v obsegu 150 m od nesreče";
            porocilo.Pacient = Pacient.VrniPoEmšo("1305281500333");
            porocilo.Naslov= Naslov.VrniNaslov(122);
            List<Zaposleni> lista = new List<Zaposleni>();
            lista.Add(Zaposleni.VrniPoEmšo("1605969500555"));
            lista.Add(Zaposleni.VrniPoEmšo("0904987505222"));
            lista.Add(Zaposleni.VrniPoEmšo("0110960500456"));

            porocilo.Ekipa = lista.ToArray();
            porocilo.ČasDogodka = Convert.ToDateTime("01.01.2008");
            porocilo.ČasKlicanjaReševalcev = Convert.ToDateTime("01.01.2011");
            porocilo.ČasPrispetjaReševalcev = Convert.ToDateTime("01.01.2010");
            porocilo.ČasPrispetjaVBolnišnico = Convert.ToDateTime("01.01.2009");
            porocilo.UstvariPorocilo();
        }

        public void TestPorociloVrniVse()
        {
            Poročilo[] vsi = Poročilo.VrniVsaPorocila();
        }

        public void TestPorociloPoPacientu()
        {
            Pacient joze = Pacient.VrniPoEmšo("2903965500999");
            Poročilo[] pacienti = Poročilo.VrniPorocilaPoPacientu(joze);
        }

        /// <summary>
        /// Testiranje preiskave
        /// </summary>
        private void button7_Click(object sender, EventArgs e)
        {
            //TestPreiskavaVrniVse(); //deluje
            TestPreiskavaVrniPoID(); //deluje
        }

        public void TestPreiskavaVrniVse()
        {
            Preiskava[] vsi = Preiskava.VrniVsePreiskave();
        }

        public void TestPreiskavaVrniPoID()
        {
            int id = 130;

            Preiskava[] vsi = Preiskava.VrniVsePreiskavePoID(id);
        }
        /// <summary>
        /// Testiranje medicinskih pripomočkov
        /// </summary>

        private void button8_Click(object sender, EventArgs e)
        {
            //TestMedPrimVrniVse(); //dela
            TestMedPrimVrniID(); //deluje
        }

        public void TestMedPrimVrniVse()
        {
            Medicinski_pripomočki[] vsi = Medicinski_pripomočki.VrniVseMedPrip();
        }
        public void TestMedPrimVrniID()
        {
            int id = 120;

            Medicinski_pripomočki[] vsi = Medicinski_pripomočki.VrniVseMedPripPoID(id);
        }
        /// <summary>
        /// Testiranje razreda Diagnoza
        /// </summary>
        private void button9_Click(object sender, EventArgs e)
        {
            //TestDiagnozaVrniVse(); //deluje
            TestDiagnozaVrniPoID(); //deluje
        }

        public void TestDiagnozaVrniVse()
        {
            Diagnoza[] vse = Diagnoza.VrniVseDiagnoze();
        }

        public void TestDiagnozaVrniPoID()
        {
            int id = 120;

            Diagnoza[] vsi = Diagnoza.VrniDiagnozePoID(id);
        }
    }
}

