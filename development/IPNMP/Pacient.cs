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
            this.IDOseba = o.IDOseba;


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
        public string ZZZS
        {
            get;
            set;
        }

        /// <summary>
        /// Kartoteka, ki pripada vsakemu pacientu
        /// </summary>
        public IPNMP.Kartoteka[] Kartoteke
        {
            get;
            set;
        }

        public int IdPacienta
        {
            get;
            set;
        }



        /// <summary>
        /// Vrne paciente iz podatkovne baze glede na številko emšo
        /// </summary>
        public static Pacient VrniPoEmšo(string EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            Oseba tmp = new Oseba();
            tmp = Oseba.VrniPoEmšo(EMŠO);
            SqlCommand ukaz = new SqlCommand("pacient_vrnipoidosebe", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_oseba", SqlDbType.Int));
            ukaz.Parameters["@id_oseba"].Value =tmp.IDOseba;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Pacient tmp2 = new Pacient(tmp);
            Bralec.Read();


            tmp2.IdPacienta = (int)Bralec["id"];
            tmp2.IDOseba = (int)Bralec["IDOseba"];
            tmp2.KrvnaSkupina = (string)Bralec["KrvnaSkupina"];
            tmp2.Teža = (int)Bralec["teza"];
            tmp2.Višina = (int)Bralec["visina"];
            tmp2.ZZZS = (string)Bralec["ZZZS"];
            tmp2.Kartoteke = Kartoteka.VrniKartotekePoIdPacienta((int)Bralec["id"]);
           

            povezava.Close();
            return tmp2;

        }

        /// <summary>
        /// Ustvari nov vnos v podatkovno bazo za pacienta glede na emšo osebo (katero najprej ustvari/posodobi)
        /// </summary>
        public void Ustvari()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("pacient_dodaj", povezava);

            Oseba o = new Oseba();
            o.Ime = this.Ime;
            o.Priimek = this.Priimek;
            o.Naslov = this.Naslov;
            o.Spol = this.Spol;
            o.EMŠO = this.EMŠO;
            o.DatumRojstva = this.DatumRojstva;

            o.Ustvari();
            Oseba o2= Oseba.VrniPoEmšo(this.EMŠO);
            this.IDOseba = o2.IDOseba;
            ukaz.Parameters.Add(new SqlParameter("@krvnaS", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@teza_p", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@visina_p", SqlDbType.Int));
            
            
            ukaz.Parameters.Add(new SqlParameter("@st_zzzs", SqlDbType.NVarChar,255));
            ukaz.Parameters.Add(new SqlParameter("@id_oseba", SqlDbType.Int));

            ukaz.Parameters["@krvnaS"].Value = this.KrvnaSkupina;
            
            ukaz.Parameters["@teza_p"].Value = this.Teža;
            ukaz.Parameters["@id_oseba"].Value = this.IDOseba;
            ukaz.Parameters["@visina_p"].Value = this.Višina;
            ukaz.Parameters["@st_zzzs"].Value = this.ZZZS;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Izbriše pacienta iz podatkovne baze glede na njegovo številko EMŠO
        /// </summary>
        /// <param name="EMŠO">številka EMŠO</param>
        public void Izbrisi()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("pacient_brisipoidosebe", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_osebe", SqlDbType.Int));
            ukaz.Parameters["@id_osebe"].Value = this.IDOseba;

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

            SqlCommand ukaz = new SqlCommand("pacient_vrnivse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Pacient> seznam = new List<Pacient>();


            
            


            while (Bralec.Read())
            {
                int IDOsebe = (int)Bralec["IDOseba"];
                Oseba tmp2 = Oseba.VrniPoIDOsebe(IDOsebe);


                Pacient tmp = new Pacient(tmp2);
                tmp.IDOseba = IDOsebe;

                tmp.IdPacienta = (int)Bralec["id"];
                tmp.KrvnaSkupina = (string)Bralec["KrvnaSkupina"];
                tmp.Teža = (int)Bralec["teza"];
                tmp.Višina = (int)Bralec["visina"];
                tmp.ZZZS = (string)Bralec["ZZZS"];
                tmp.Kartoteke = Kartoteka.VrniKartotekePoIdPacienta((int)Bralec["id"]);
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

            SqlCommand ukaz = new SqlCommand("pacient_uredi", povezava);


            Oseba o = new Oseba();
            o.Ime = this.Ime;
            o.Priimek = this.Priimek;
            o.Naslov = this.Naslov;
            o.Spol = this.Spol;
            o.EMŠO = this.EMŠO;
            o.DatumRojstva = this.DatumRojstva;

            o.Ustvari();
            Oseba o2 = Oseba.VrniPoEmšo(this.EMŠO);
            this.IDOseba = o2.IDOseba;

            ukaz.Parameters.Add(new SqlParameter("@krvnaS", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@teza_p", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@visina_p", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@st_zzzs", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@id_oseba", SqlDbType.Int));
           
            ukaz.Parameters["@krvnaS"].Value = this.KrvnaSkupina;
            ukaz.Parameters["@teza_p"].Value = this.Teža;
            ukaz.Parameters["@visina_p"].Value = this.Višina;
            ukaz.Parameters["@st_zzzs"].Value = this.ZZZS;
            ukaz.Parameters["@id_oseba"].Value = this.IDOseba;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne starost v letih, izračunano s pomočjo datuma rojstva
        /// </summary>
        public int VrniStarost()
        {
            DateTime datum_roj = this.DatumRojstva;
            DateTime trenutni_datum = DateTime.Today;

            TimeSpan izracun = trenutni_datum - datum_roj;
            int starost = (int)(izracun.TotalDays / 365.255);   //deljenje dni z 365.255 popravi problem prestopnega leta
            //0.25 za prestopna leta, 0.005 za ostale popravke (razna prestavljanja ur itd.)
            return starost;
        }

        /// <summary>
        /// Vrne vse alergije ki jih pacient ima (spada pod Diagnoze)
        /// </summary>
        public Diagnoza[] VrniAlergije()
        {
            List<Diagnoza> seznam = new List<Diagnoza>();
            foreach (Kartoteka k in this.Kartoteke)
            {


                foreach (Diagnoza d in k.Diagnoze)
                {
                    if (d.Tip == "Alergija")
                    {

                        seznam.Add(d);
                    }

                }
            }
            return seznam.ToArray();
        }

        /// <summary>
        /// Vrne vse operacije, ki jih je pacient imel(spada pod zdravljenje)
        /// </summary>
        public Terapija[] VrniOperacije()
        {
            List<Terapija> seznam = new List<Terapija>();
            foreach (Kartoteka k in this.Kartoteke)
            {

                foreach (Terapija d in k.terapije)
                {
                    if (d.Tip == "Operacija")
                    {

                        seznam.Add(d);
                    }

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

            SqlCommand ukaz = new SqlCommand("pacient_vrnipoimenu", povezava);
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
                int IDOsebe = (int)Bralec["IDOseba"];
                Oseba tmp2 = Oseba.VrniPoIDOsebe(IDOsebe);


                Pacient tmp = new Pacient(tmp2);
                tmp.IDOseba = IDOsebe;
                tmp.IdPacienta = (int)Bralec["id"];
                tmp.KrvnaSkupina = (string)Bralec["KrvnaSkupina"];
                tmp.Teža = (int)Bralec["teza"];
                tmp.Višina = (int)Bralec["visina"];
                tmp.ZZZS = (string)Bralec["ZZZS"];
                tmp.Kartoteke = Kartoteka.VrniKartotekePoIdPacienta((int)Bralec["id"]);
                seznam.Add(tmp);
            }

            Pacient[] ds = seznam.ToArray();
            povezava.Close();

            return ds;

        }

        public static Pacient VrniPoIdPacient(int idP)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
           
            SqlCommand ukaz = new SqlCommand("pacient_vrni", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_p", SqlDbType.Int));
            ukaz.Parameters["@id_p"].Value = idP;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            Bralec.Read();
            Oseba tmp = new Oseba();
            tmp = Oseba.VrniPoIDOsebe((int)Bralec["IDOseba"]);
            Pacient tmp2 = new Pacient(tmp);
         


            tmp2.IdPacienta = (int)Bralec["id"];
            tmp2.IDOseba = (int)Bralec["IDOseba"];
           
            tmp2.KrvnaSkupina = (string)Bralec["KrvnaSkupina"];
            tmp2.Teža = (int)Bralec["teza"];
            tmp2.Višina = (int)Bralec["visina"];
            tmp2.ZZZS = (string)Bralec["ZZZS"];
            tmp2.Kartoteke = Kartoteka.VrniKartotekePoIdPacienta((int)Bralec["id"]);


            povezava.Close();
            return tmp2;

        }


    }
}
