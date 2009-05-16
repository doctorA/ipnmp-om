using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IPNMP
{
    public class Oseba
    {
        private static string PotPovezave = Properties.Settings.Default.ConnectionString;

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
        public void Ustvari()
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
            ukaz.Parameters["@IDNaslova"].Value = this.Naslov.IDNaslova;
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

        public void Izbrisi()
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
        public static Oseba[] VrniVse()
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
                tmp.Naslov = Naslov.VrniNaslov((int)Bralec["IDNaslova"]);
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
        public void Posodobi()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@IDNaslova", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = this.Ime;
            ukaz.Parameters["@Priimek"].Value = this.Priimek;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@Naslov"].Value = this.Naslov.IDNaslova;
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
        public static Oseba VrniPoEmšo(int EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniPoEmšoOsebo", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Oseba tmp = new Oseba();
            Bralec.Read();
            tmp.Ime = (string)Bralec["Ime"];
            tmp.Priimek = (string)Bralec["Priimek"];
            tmp.Spol = (string)Bralec["Spol"];
            tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
            tmp.Naslov = Naslov.VrniNaslov((int)Bralec["IDNaslova"]);

            povezava.Close();
            return tmp;
        }



    }
}
