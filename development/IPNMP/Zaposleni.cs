using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IPNMP
{
    public class Zaposleni : Oseba
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public Zaposleni()
        {

        }

        public Zaposleni(Oseba o)
        {

            this.Ime = o.Ime;
            this.Priimek = o.Priimek;
            this.Naslov = o.Naslov;
            this.Spol = o.Spol;
            this.EMŠO = o.EMŠO;
            this.DatumRojstva = o.DatumRojstva;

        }


        public DateTime DatumZaposlitve { set; get; }
        public String TipZaposlenega { set; get; }

        public string Specializacija
        {
            get;
            set;
        }

     

        /// <summary>
        /// Vrne vse zaposlene iz podatkovne baze
        /// </summary>
        public static Zaposleni[] VrniVse()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("zaposleni_vrniVse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zaposleni> seznam = new List<Zaposleni>();

            while (Bralec.Read())
            {
                Zaposleni tmp = new Zaposleni();
                tmp.Ime = (string)Bralec["Ime"];
                tmp.Priimek = (string)Bralec["Priimek"];
                tmp.Spol = (string)Bralec["Spol"];
                tmp.EMŠO = (string)Bralec["EMSO"];
                tmp.Naslov = Naslov.VrniNaslov((int)Bralec["Idnaslov"]);
                tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
                tmp.IDOseba = (int)Bralec["idosebe"];
                tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                tmp.Specializacija = (string)Bralec["Specializacija"];
                tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                seznam.Add(tmp);
            }

            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;

        }

        /// <summary>
        /// Ustvari nov vnos v podatkovni bazi za zaposlenega glede na njegov emšo /najprej posodobi/ustvari osebne podatke
        /// </summary>
        public void Ustvari()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            Oseba o = new Oseba();
            o.Ime = this.Ime;
            o.Priimek = this.Priimek;
            o.Naslov = this.Naslov;
            o.Spol = this.Spol;
            o.EMŠO = this.EMŠO;
            o.DatumRojstva = this.DatumRojstva;

            o.Posodobi();
            Oseba o2 = Oseba.VrniPoEmšo(this.EMŠO);
            this.IDOseba = o2.IDOseba;

            SqlCommand ukaz = new SqlCommand("zaposleni_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@datum_zapos", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@spec", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@tip_zapos", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@id_oseba", SqlDbType.Int));

            ukaz.Parameters["@id_oseba"].Value = this.IDOseba;
            ukaz.Parameters["@spec"].Value = this.Specializacija;
            ukaz.Parameters["@datum_zapos"].Value = this.DatumZaposlitve;
            ukaz.Parameters["@tip_zapos"].Value = this.TipZaposlenega;
            


            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Izbriše zaposlenega iz podatkovne baze
        /// </summary>
        public void Izbrisi()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("zaposleni_brisi", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_osebe", SqlDbType.Int));
            ukaz.Parameters["@id_osebe"].Value = this.IDOseba;
             ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Posodobi podatke zaposlenega glede na njegov emšo //najprej posodobi/ustvari osebne podatke
        /// </summary>
        public void Posodobi()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);


            Oseba o = new Oseba();
            o.Ime = this.Ime;
            o.Priimek = this.Priimek;
            o.Naslov = this.Naslov;
            o.Spol = this.Spol;
            o.EMŠO = this.EMŠO;
            o.DatumRojstva = this.DatumRojstva;

            o.Posodobi();
            Oseba o2 = Oseba.VrniPoEmšo(this.EMŠO);
            this.IDOseba = o2.IDOseba;
            SqlCommand ukaz = new SqlCommand("zaposleni_uredi", povezava);

            ukaz.Parameters.Add(new SqlParameter("@datum_zap", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@specializacija", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@tip", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@id", SqlDbType.Int));

            ukaz.Parameters["@id"].Value = this.IDOseba;
            ukaz.Parameters["@specializacija"].Value = this.Naslov;
            ukaz.Parameters["@datum_zap"].Value = this.DatumZaposlitve;
            ukaz.Parameters["@tip"].Value = this.TipZaposlenega;

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

            SqlCommand ukaz = new SqlCommand("zaposleni_vrnipotipu", povezava);
            ukaz.Parameters.Add(new SqlParameter("@tip", SqlDbType.NVarChar, 255));
            ukaz.Parameters["@tip"].Value = TipZaposlenega;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();    
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zaposleni> seznam = new List<Zaposleni>();

            while (Bralec.Read())
            {
                
                
                    Zaposleni tmp = new Zaposleni();
                    tmp.Ime = (string)Bralec["Ime"];
                    tmp.Priimek = (string)Bralec["Priimek"];
                    tmp.Spol = (string)Bralec["Spol"];
                    tmp.EMŠO = (string)Bralec["EMSO"];
                    tmp.Naslov = Naslov.VrniNaslov((int)Bralec["Idnaslov"]);
                    tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
                    tmp.IDOseba = (int)Bralec["idosebe"];
                    tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                    tmp.Specializacija = (string)Bralec["Specializacija"];
                    tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                    seznam.Add(tmp);
              
            }

            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Vrne zaposlenega iz baze glede na podano emšo številko
        /// </summary>
        public static Zaposleni VrniPoEmšo(string EMŠO)
        {
            

            SqlConnection povezava = new SqlConnection(PotPovezave);
            Oseba tmp = new Oseba();
            tmp = Oseba.VrniPoEmšo(EMŠO);
            SqlCommand ukaz = new SqlCommand("zaposleni_vrni", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_zapos", SqlDbType.Int));
            ukaz.Parameters["@id_zapos"].Value = tmp.IDOseba;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Zaposleni tmp2 = new Zaposleni(tmp);
            Bralec.Read();

            tmp2.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
            tmp2.Specializacija = (string)Bralec["Specializacija"];
            tmp2.TipZaposlenega = (string)Bralec["TipZaposlenega"];

            povezava.Close();
            return tmp2;
        }

        /* 
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
         * */




        public static IPNMP.Zaposleni[] VrniVsePoImenu(string Ime, string Priimek)
        {

            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("zaposleni_poimenu", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));


            ukaz.Parameters["@Ime"].Value = Ime;
            ukaz.Parameters["@Priimek"].Value = Ime;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zaposleni> seznam = new List<Zaposleni>();



            while (Bralec.Read())
            {
                Zaposleni tmp = new Zaposleni();

                tmp.Ime = (string)Bralec["Ime"];
                tmp.Priimek = (string)Bralec["Priimek"];
                tmp.Spol = (string)Bralec["Spol"];
                tmp.EMŠO = (string)Bralec["EMSO"];
                tmp.Naslov = Naslov.VrniNaslov((int)Bralec["Idnaslov"]);
                tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
                tmp.IDOseba = (int)Bralec["idosebe"];
                tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                tmp.Specializacija = (string)Bralec["Specializacija"];
                tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                seznam.Add(tmp);
            }

            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;

        }
    }
}
