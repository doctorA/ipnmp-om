using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Terapija
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public string Opis
        {
            get;
            set;
        }

        public string Tip
        {
            get;
            set;
        }

        public string Kaj
        {
            get;
            set;
        }
/*
        public DateTime DatumObiska
        {
            get;
            set;
        }
*/
        /// <summary>
        /// Vrne vsa zdravljenja iz podatkovne baze
        /// </summary>
        public static Terapija[] VrniVseTerapije()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("terapija_vrniVse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Terapija> seznam = new List<Terapija>();

            while (Bralec.Read())
            {
                Terapija tmp = new Terapija();
        //        tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Tip = (string)Bralec["Tip"];
                tmp.Opis = (string)Bralec["Opis"];
                tmp.Kaj = (string)Bralec["Kaj"];
                seznam.Add(tmp);
            }

            Terapija[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Vrne vsa zdravljenja iz podatkovne baze glede na ID kartoteke
        /// </summary>
        /// <param name="StevilkaKartoteke">ID številka kartoteke</param>
        public static IPNMP.Terapija[] VrniVseTerapijePoIdKartoteke(int StevilkaKartoteke)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("terapija_vrnipoidkartoteka", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_kartoteka", SqlDbType.Int));
            ukaz.Parameters["@id_kartoteka"].Value = StevilkaKartoteke;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Terapija> seznam = new List<Terapija>();

            while (Bralec.Read())
            {
                Terapija tmp = new Terapija();
            //    tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Tip = (string)Bralec["Tip"];
                tmp.Opis = (string)Bralec["Opis"];
                tmp.Kaj = (string)Bralec["Kaj"];
                seznam.Add(tmp);
            }

            Terapija[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Ustvari nov primerek terapije v podatkovni bazi
        /// </summary>
        public int UstvariTerapijo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("terapija_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@tip", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@opis", SqlDbType.Text));
            ukaz.Parameters.Add(new SqlParameter("@kaj", SqlDbType.Text));

            ukaz.Parameters["@tip"].Value = this.Tip;
            ukaz.Parameters["@opis"].Value = this.Opis;
            ukaz.Parameters["@kaj"].Value = this.Kaj;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Bralec.Read();
            int ID = Convert.ToInt32(Bralec[0]);
            return ID;
        }

        public static void KT(int id_kartoteka, int id_terapija)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("KT_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@id_kartoteka", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@id_terapija", SqlDbType.Int));


            ukaz.Parameters["@id_kartoteka"].Value = id_kartoteka;
            ukaz.Parameters["@id_terapija"].Value = id_terapija;


            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();

        }
    }
}
