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
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string Tip
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DateTime DatumObiska
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
    }
}
