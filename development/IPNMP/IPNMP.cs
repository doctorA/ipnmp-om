using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Oseba
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;

        /// <summary>
        /// Privzeti konstruktor, ob klicanju pobere parametre, za vzpostavitev povezave z bazo
        /// </summary>

        /// <summary>
        /// Nastavi parametre osebe glede na vpisani emšo
        /// </summary>
        /// <param name="EMŠO"></param>
        public Oseba(int EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("OsebaKonstruktor", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Oseba");
            povezava.Close();

            DataRow vrstica;
            vrstica = ds.Tables[0].Rows[0];
            this.Ime = vrstica["Ime"].ToString();
            this.Priimek = vrstica["Priimek"].ToString();
            this.EMŠO = EMŠO;
            this.DatumRojstva = (DateTime)vrstica["DatumRojstva"];
            Naslov naslov_osebe = new Naslov();
            String Naslovtext = vrstica["Naslov"].ToString();
            int i;
            for (i = 0; i <= Naslovtext.Length; i++)
            {
                if (Char.IsNumber(Naslovtext[i]))
                {
                    break;
                }

            }
            naslov_osebe.Ulica = Naslovtext.Substring(0, i);
            naslov_osebe.HišnaŠtevilka = Naslovtext.Substring(i, Naslovtext.Length - i);
            this.Naslov = naslov_osebe;
            this.Spol = vrstica["Spol"].ToString();

        }
        public Oseba() { }
        public String Ime { set; get; }
        public String Priimek { set; get; }
        public int EMŠO { set; get; }
        public DateTime DatumRojstva { set; get; }

        public String Spol { set; get; }

        public Naslov Naslov
        {
            get;
            set;
        }




        /// <summary>
        /// Ustvari novo osebo v podatkovni bazi
        /// </summary>
        public void UstvariOsebo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@IDNaslova", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = this.Ime;
            ukaz.Parameters["@Priimek"].Value = this.Priimek;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@IDNaslov"].Value = this.Naslov;
            ukaz.Parameters["@DatumRojstva"].Value = this.DatumRojstva;
            ukaz.Parameters["@Spol"].Value = this.Spol;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();

        }

        /// <summary>
        /// Izbriše osebo iz podatkovne baze, glede na EMŠO objekta
        /// </summary>

        public void IzbrisiOsebo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiOsebo", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();

        }

        /// <summary>
        /// Vrne vse osebe iz podatkovne baze
        /// </summary>
        /// <returns>Vrne napolnjen dataset</returns>
        public static Oseba[] VrniVseOsebe()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseOsebe", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Oseba> seznam = new List<Oseba>();

            while (Bralec.Read())
            {
                Oseba tmp = new Oseba();
                tmp.Ime = (string)Bralec["Ime"];
                tmp.Priimek = (string)Bralec["Priimek"];
                tmp.Spol = (string)Bralec["Spol"];
                tmp.EMŠO = (int)Bralec["EMŠO"];
                tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
                seznam.Add(tmp);


            }

            Oseba[] ds = seznam.ToArray();
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Posodobi podatke za osebo v podatkovni bazi
        /// </summary>
        public void PosodobiOsebo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Naslov", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = this.Ime;
            ukaz.Parameters["@Priimek"].Value = this.Priimek;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@Naslov"].Value = this.Naslov;
            ukaz.Parameters["@DatumRojstva"].Value = this.DatumRojstva;
            ukaz.Parameters["@Spol"].Value = this.Spol;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();



        }

        /// <summary>
        /// Vrne osebo iz podatkovne baze glede na emšo
        /// </summary>
        public static Oseba VrniOseboEmšo()
        {
            throw new System.NotImplementedException();
        }



    }
    /// <summary>
    /// Podatki o pacientu
    /// </summary>
    public class Pacient : Oseba
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;

        /// <param name="ZZZS">Napolni objekt iz baze s pomočjo zzzs številke</param>
        public Pacient()
        {

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
        /// Vrne paciente iz podatkovne baze glede na številko ZZZS
        /// </summary>
        public static Pacient VrniPacientZZZS()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Ustvari nov vnos v podatkovno bazo
        /// </summary>
        public void UstvariPacient()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@KrvnaSkupina", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Teža", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Višina", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaKartoteke", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));

            ukaz.Parameters["@KrvnaSkupina"].Value = this.KrvnaSkupina;
            ukaz.Parameters["@Teža"].Value = this.Teža;
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
        public void IzbrisiPacient(int EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiPacient", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne vse paciente iz podatkovne baze
        /// </summary>
        public static Pacient[] VrniVsePaciente()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePaciente", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Pacient> seznam = new List<Pacient>();

            while (Bralec.Read())
            {
                Pacient tmp = new Pacient();
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
        /// Posodobi podatke pacienta v podatkovni bazi
        /// </summary>
        public void PosodobiPacient()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@KrvnaSkupina", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Teža", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Višina", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaKartoteke", SqlDbType.Int));



            ukaz.Parameters["@ŠtevilkaKartoteke"].Value = this.Kartoteka.ŠtevilkaKartoteke;
            ukaz.Parameters["@KrvnaSkupina"].Value = this.KrvnaSkupina;
            ukaz.Parameters["@Teža"].Value = this.Teža;
            ukaz.Parameters["@Višina"].Value = this.Višina;
            ukaz.Parameters["@ZZZS"].Value = this.ZZZS;

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


    }

    public class Zaposleni : Oseba
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public Zaposleni()
        {
            throw new System.NotImplementedException();
        }

        /// <param name="EMŠO">Napolni objekt zaposleni iz baze, glede na podan emšo</param>
        public Zaposleni(string EMŠO)
        {
            throw new System.NotImplementedException();
        }

        public DateTime DatumZaposlitve { set; get; }
        public String TipZaposlenega { set; get; }

        public int Specializacija
        {
            get;
            set;
        }

        public int ŠtevilkaEkipe
        {
            get;
            set;
        }

        public IPNMP.Poročilo[] Poročila
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// Vrne vse zaposlene iz podatkovne baze
        /// </summary>
        public static Zaposleni[] VrniVseZaposlene()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseZaposlene", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zaposleni> seznam = new List<Zaposleni>();

            while (Bralec.Read())
            {
                Zaposleni tmp = new Zaposleni();
                tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                tmp.ŠtevilkaEkipe = (int)Bralec["ŠtevilkaEkipe"];
                tmp.Specializacija = (int)Bralec["Specializacija"];
                tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                seznam.Add(tmp);
            }

            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;

        }

        /// <summary>
        /// Ustvari nov vnos v podatkovni bzi
        /// </summary>
        public void UstvariZaposlenega()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariZaposlenega", povezava);

            ukaz.Parameters.Add(new SqlParameter("@DatumZaposlitve", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Specializacija", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@TipZaposlenega", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Specializacija"].Value = this.Naslov;
            ukaz.Parameters["@DatumZaposlitve"].Value = this.DatumZaposlitve;
            ukaz.Parameters["@TipZaposlenega"].Value = this.TipZaposlenega;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Izbriše zaposlenega iz podatkovne baze
        /// </summary>
        public void IzbrisiZaposlenega()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiZaposlenega", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Posodobi podatke zaposlenega
        /// </summary>
        public void PosodobiZaposlenega()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiZaposlenega", povezava);

            ukaz.Parameters.Add(new SqlParameter("@DatumZaposlitve", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Specializacija", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@TipZaposlenega", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Specializacija"].Value = this.Naslov;
            ukaz.Parameters["@DatumZaposlitve"].Value = this.DatumZaposlitve;
            ukaz.Parameters["@TipZaposlenega"].Value = this.TipZaposlenega;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne vse zaposlene glede na tip
        /// </summary>
        public static Zaposleni[] VrniVsePoTipu(string TipZaposlenega)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePoTipu", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zaposleni> seznam = new List<Zaposleni>();

            while (Bralec.Read())
            {
                Zaposleni tmp = new Zaposleni();
                if ((string)Bralec["TipZaposlenega"] == TipZaposlenega)
                {
                    tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                    tmp.ŠtevilkaEkipe = (int)Bralec["ŠtevilkaEkipe"];
                    tmp.Specializacija = (int)Bralec["Specializacija"];
                    tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                    seznam.Add(tmp);
                }
            }

            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Vrne zaposlenega iz baze glede na podano zzzs številko
        /// </summary>
        public static Zaposleni VrniZaposlenegaZZZS(int ZZZS)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniZaposlenegaZZZS", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));
            ukaz.Parameters["@ZZZS"].Value = ZZZS;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Zaposleni tmp = new Zaposleni();
            Bralec.Read();
            tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
            tmp.ŠtevilkaEkipe = (int)Bralec["ŠtevilkaEkipe"];
            tmp.Specializacija = (int)Bralec["Specializacija"];
            tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];

            povezava.Close();
            return tmp;
        }

        public static IPNMP.Zaposleni[] VrniZaposlenePoEkipi(int ŠtevilkaEkipe)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniZaposlenePoEkipi", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaEkipe", SqlDbType.Int));
            ukaz.Parameters["@ŠtevilkaEkipe"].Value = ŠtevilkaEkipe;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            List<Zaposleni> seznam = new List<Zaposleni>();

            while (Bralec.Read())
            {
                Zaposleni tmp = new Zaposleni();
                if ((int)Bralec["ŠtevilkaEkipe"] == ŠtevilkaEkipe)
                {
                    tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                    tmp.ŠtevilkaEkipe = ŠtevilkaEkipe;
                    tmp.Specializacija = (int)Bralec["Specializacija"];
                    tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                    seznam.Add(tmp);
                }
            }
            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;
        }

    }
    //Morebitni dodatki:
    //Šifranti naselij(poštnih številk, krajev)
    //Šifranti bolezni in simptomov http://www.who.int/classifications/icd/en/
}

