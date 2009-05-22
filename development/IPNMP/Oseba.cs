using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using csUnit;

namespace IPNMP
{
    [TestFixture]
    public class Oseba
    {
        private static string PotPovezave = Properties.Settings.Default.ConnectionString;

       
        public Oseba() { }
        public String Ime { set; get; }
        public String Priimek { set; get; }
        public string EMŠO { set; get; }
        public DateTime DatumRojstva { set; get; }

        public String Spol { set; get; }

        public Naslov Naslov
        {
            get;
            set;
        }

        public int IDOseba
        {
            get;
            set;
        }



        [Test]
        /// <summary>
        /// Ustvari novo osebo v podatkovni bazi
        /// </summary>
        public void Ustvari()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("oseba_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@ime_osebe", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@priimek_osebe", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@emso", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@naslovID", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@roj_osebe", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@spol_osebe", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@ime_osebe"].Value = this.Ime;
            ukaz.Parameters["@priimek_osebe"].Value = this.Priimek;
            ukaz.Parameters["@emso"].Value = this.EMŠO;
            ukaz.Parameters["@naslovID"].Value = this.Naslov.IDNaslova;
            ukaz.Parameters["@roj_osebe"].Value = this.DatumRojstva;
            ukaz.Parameters["@spol_osebe"].Value = this.Spol;

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
            SqlCommand ukaz = new SqlCommand("oseba_brisi", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_osebe", SqlDbType.Int));
            ukaz.Parameters["@id_osebe"].Value = this.IDOseba;

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

            SqlCommand ukaz = new SqlCommand("oseba_vrniVse", povezava);
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
                tmp.EMŠO = (string)Bralec["EMSO"];
                tmp.Naslov = Naslov.VrniNaslov((int)Bralec["Idnaslov"]);
                tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
                tmp.IDOseba = (int)Bralec["id"];
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

            ukaz.Parameters.Add(new SqlParameter("@ime_osebe", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@priimek_osebe", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@emso", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@naslovID", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@roj_osebe", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@spol_osebe", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@ime_osebe"].Value = this.Ime;
            ukaz.Parameters["@priimek_osebe"].Value = this.Priimek;
            ukaz.Parameters["@emso"].Value = this.EMŠO;
            ukaz.Parameters["@id_osebe"].Value = this.IDOseba;
            ukaz.Parameters["@Naslov"].Value = this.Naslov.IDNaslova;
            ukaz.Parameters["@roj_osebe"].Value = this.DatumRojstva;
            ukaz.Parameters["@spol_osebe"].Value = this.Spol;


            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();



        }

        /// <summary>
        /// Vrne osebo iz podatkovne baze glede na emšo
        /// </summary>
        public static Oseba VrniPoEmšo(string EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniPoEmšoOseba", povezava);
            ukaz.Parameters.Add(new SqlParameter("@emso", SqlDbType.NVarChar, 255));
            ukaz.Parameters["@emso"].Value = EMŠO;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Oseba tmp = new Oseba();
            Bralec.Read();
            tmp.Ime = (string)Bralec["Ime"];
            tmp.Priimek = (string)Bralec["Priimek"];
            tmp.Spol = (string)Bralec["Spol"];
            tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
            tmp.Naslov = Naslov.VrniNaslov((int)Bralec["Idnaslov"]);
            tmp.IDOseba = (int)Bralec["id"];

            povezava.Close();
            return tmp;
        }

        public static Oseba VrniPoIDOsebe(int IDOseba)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("oseba_vrni", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_osebe", SqlDbType.Int));
            ukaz.Parameters["@id_osebe"].Value = IDOseba;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Oseba tmp = new Oseba();
            Bralec.Read();
            tmp.Ime = (string)Bralec["Ime"];
            tmp.Priimek = (string)Bralec["Priimek"];
            tmp.Spol = (string)Bralec["Spol"];
            tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
            tmp.Naslov = Naslov.VrniNaslov((int)Bralec["Idnaslov"]);
            tmp.IDOseba = (int)Bralec["id"];

            povezava.Close();
            return tmp;
        }



    }
}
