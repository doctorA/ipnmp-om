using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Pacient : Oseba
    {

        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;


        public Pacient()
        {

        }
        /// <summary>
        /// Kreira pacienta, z osnovnimi podatki iz osebe
        /// </summary>
        /// <param name="o">Objekt oseba z osnovnimi podatki</param>
        public Pacient(Oseba o)
        {
            this.Ime = o.Ime;
            this.Priimek = o.Priimek;
            this.Spol = o.Spol;
            this.Naslov = o.Naslov;
            this.EMŠO = o.EMŠO;
            this.DatumRojstva = o.DatumRojstva;


        }

        /// <summary>
        /// Višina v centimetrih
        /// </summary>
        public int Višina { set; get; }
        /// <summary>
        /// Teža v gramih
        /// </summary>
        public int Teža { set; get; }
        /// <summary>
        /// Krvna skupina 
        /// </summary>
        public String KrvnaSkupina { set; get; }
        /// <summary>
        /// Številka zdravstvenega zavarovanja
        /// </summary>
        public int ZZZS
        {
            get;
            set;
        }

        /// <summary>
        /// Kartoteka, ki pripada vsakemu pacientu
        /// </summary>
        public IPNMP.Kartoteka Kartoteka
        {
            get;
            set;
        }



        /// <summary>
        /// Vrne paciente iz podatkovne baze glede na številko emšo
        /// </summary>
        public static Pacient VrniPoEmšo(int EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            Oseba tmp = new Oseba();
            tmp = Oseba.VrniPoEmšo(EMŠO);
            SqlCommand ukaz = new SqlCommand("VrniPacientEmšo", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Pacient tmp2 = new Pacient(tmp);
            Bralec.Read();

            tmp2.KrvnaSkupina = (string)Bralec["KrvnaSkupina"];
            tmp2.Teža = (int)Bralec["Teža"];
            tmp2.Višina = (int)Bralec["Višina"];
            tmp2.ZZZS = (int)Bralec["ZZZS"];
            tmp2.Kartoteka = Kartoteka.VrniKartoteko((int)Bralec["ŠtevilkaKartoteke"]);
           

            povezava.Close();
            return tmp2;

        }

        /// <summary>
        /// Ustvari nov vnos v podatkovno bazo za pacienta glede na emšo osebo (katero najprej ustvari/posodobi)
        /// </summary>
        public void Ustvari()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariPacienta", povezava);

            Oseba o = new Oseba();
            o.Ime = this.Ime;
            o.Priimek = this.Priimek;
            o.Naslov = this.Naslov;
            o.Spol = this.Spol;
            o.EMŠO = this.EMŠO;
            o.DatumRojstva = this.DatumRojstva;

            o.Posodobi();

            ukaz.Parameters.Add(new SqlParameter("@KrvnaSkupina", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Teža", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Višina", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaKartoteke", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));

            ukaz.Parameters["@KrvnaSkupina"].Value = this.KrvnaSkupina;
            ukaz.Parameters["@Teža"].Value = this.Teža;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@ŠtevilkaKartoteke"].Value = this.Kartoteka.ŠtevilkaKartoteke;
            ukaz.Parameters["@Višina"].Value = this.Višina;
            ukaz.Parameters["@ZZZS"].Value = this.ZZZS;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Izbriše pacienta iz podatkovne baze glede na njegovo številko EMŠO
        /// </summary>
        /// <param name="EMŠO">številka EMŠO</param>
        public void Izbrisi(int EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiPacienta", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne vse paciente iz podatkovne baze
        /// </summary>
        public static Pacient[] VrniVse()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePaciente", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Pacient> seznam = new List<Pacient>();
            


            while (Bralec.Read())
            {
                Pacient tmp = new Pacient(Oseba.VrniPoEmšo((int)Bralec["EMŠO"]));

                tmp.KrvnaSkupina = (string)Bralec["KrvnaSkupina"];
                tmp.Teža = (int)Bralec["Teža"];
                tmp.Višina = (int)Bralec["Višina"];
                tmp.ZZZS = (int)Bralec["ZZZS"];
                tmp.Kartoteka = Kartoteka.VrniKartoteko((int)Bralec["ŠtevilkaKartoteke"]);
                seznam.Add(tmp);
            }

            Pacient[] ds = seznam.ToArray();
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Posodobi podatke pacienta v podatkovni bazi glede na njegov emšo /najprej posodobi/ustvari osebne podatke
        /// </summary>
        public void Posodobi()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiPacienta", povezava);


            Oseba o = new Oseba();
            o.Ime = this.Ime;
            o.Priimek = this.Priimek;
            o.Naslov = this.Naslov;
            o.Spol = this.Spol;
            o.EMŠO = this.EMŠO;
            o.DatumRojstva = this.DatumRojstva;

            o.Posodobi();


            ukaz.Parameters.Add(new SqlParameter("@KrvnaSkupina", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Teža", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Višina", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaKartoteke", SqlDbType.Int));



            ukaz.Parameters["@ŠtevilkaKartoteke"].Value = this.Kartoteka.ŠtevilkaKartoteke;
            ukaz.Parameters["@KrvnaSkupina"].Value = this.KrvnaSkupina;
            ukaz.Parameters["@Teža"].Value = this.Teža;
            ukaz.Parameters["@Višina"].Value = this.Višina;
            ukaz.Parameters["@ZZZS"].Value = this.ZZZS;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne starost v letih, izračunano s pomočjo datuma rojstva
        /// </summary>
        public string VrniStarost()
        {
            DateTime datum_roj = this.DatumRojstva;
            DateTime trenutni_datum = DateTime.Today;

            TimeSpan izracun = trenutni_datum - datum_roj;
            int starost = (int)(izracun.TotalDays / 365.255);   //deljenje dni z 365.255 popravi problem prestopnega leta
            //0.25 za prestopna leta, 0.005 za ostale popravke (razna prestavljanja ur itd.)
            return starost.ToString();
        }

        /// <summary>
        /// Vrne vse alergije ki jih pacient ima (spada pod Diagnoze)
        /// </summary>
        public Diagnoza[] VrniAlergije()
        {
            List<Diagnoza> seznam = new List<Diagnoza>();
            foreach (Diagnoza d in this.Kartoteka.Diagnoze)
            {
                if (d.Tip == "Alergija")
                {

                    seznam.Add(d);
                }

            }
            return seznam.ToArray();
        }

        /// <summary>
        /// Vrne vse operacije, ki jih je pacient imel(spada pod zdravljenje)
        /// </summary>
        public Zdravljenje[] VrniOperacije()
        {
            List<Zdravljenje> seznam = new List<Zdravljenje>();
            foreach (Zdravljenje d in this.Kartoteka.Zdravljenja)
            {
                if (d.Tip == "Operacija")
                {

                    seznam.Add(d);
                }

            }
            return seznam.ToArray();
        }

        /// <summary>
        /// Vrne vse paciente, katerih ime se delno ali popolno ujema s vhodnim imenom in priimkom
        /// </summary>
        /// <param name="Ime">Ime pacienta, lahko je nepopolno</param>
        /// <param name="Priimek">Priimek pacienta, lahko je nepopolen</param>
        /// <returns>Array pacientov, ki ustrezajo imenu in priimkus</returns>
        public static IPNMP.Pacient[] VrniVsePoImenu(string Ime, string Priimek)
        {

            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePacientePoImenu", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));


            ukaz.Parameters["@Ime"].Value = Ime;
            ukaz.Parameters["@Priimek"].Value = Ime;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Pacient> seznam = new List<Pacient>();

            

            while (Bralec.Read())
            {
                Pacient tmp = new Pacient(Oseba.VrniPoEmšo((int)Bralec["EMŠO"]));

                tmp.KrvnaSkupina = (string)Bralec["KrvnaSkupina"];
                tmp.Teža = (int)Bralec["Teža"];
                tmp.Višina = (int)Bralec["Višina"];
                tmp.ZZZS = (int)Bralec["ZZZS"];
                tmp.Kartoteka = Kartoteka.VrniKartoteko((int)Bralec["ŠtevilkaKartoteke"]);
                seznam.Add(tmp);
            }

            Pacient[] ds = seznam.ToArray();
            povezava.Close();

            return ds;

        }


    }
}
