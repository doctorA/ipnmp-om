using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Zdravljenje
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

        public DateTime DatumObiska
        {
            get;
            set;
        }

        /// <summary>
        /// Vrne vsa zdravljenja iz podatkovne baze
        /// </summary>
        public static Zdravljenje[] VrniVsaZdravljenja()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaZdravljenja", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zdravljenje> seznam = new List<Zdravljenje>();

            while (Bralec.Read())
            {
                Zdravljenje tmp = new Zdravljenje();
                tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Tip = (string)Bralec["Tip"];
                tmp.Opis = (string)Bralec["Opis"];
                seznam.Add(tmp);
            }

            Zdravljenje[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Vrne vsa zdravljenja iz podatkovne baze glede na ID kartoteke
        /// </summary>
        /// <param name="StevilkaKartoteke">ID številka kartoteke</param>
        public static IPNMP.Zdravljenje[] VrniVsaZdravljenjaPoID(int StevilkaKartoteke)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaZdravljenjaPoID", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaKartoteke", SqlDbType.Int));
            ukaz.Parameters["@ŠtevilkaKartoteke"].Value = StevilkaKartoteke;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zdravljenje> seznam = new List<Zdravljenje>();

            while (Bralec.Read())
            {
                Zdravljenje tmp = new Zdravljenje();
                tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Tip = (string)Bralec["Tip"];
                tmp.Opis = (string)Bralec["Opis"];
                seznam.Add(tmp);
            }

            Zdravljenje[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }
    }
}
