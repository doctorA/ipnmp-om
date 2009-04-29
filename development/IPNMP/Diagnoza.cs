using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Diagnoza
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public int Tip
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

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
        /// Vrne vse diagnoze pacienta iz podatkovne baze
        /// </summary>
        public static Diagnoza[] VrniVseDiagnoze()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseDiagnoze", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Diagnoza> seznam = new List<Diagnoza>();

            while (Bralec.Read())
            {
                Diagnoza tmp = new Diagnoza();
                tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Opis = (string)Bralec["Opis"];
                tmp.Tip = (int)Bralec["Tip"];
                seznam.Add(tmp);
            }

            Diagnoza[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }
    }
}
