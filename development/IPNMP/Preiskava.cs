using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Preiskava
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public string Opis
        {
            get;
            set;
        }

        public string Rezultati
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
        /// Vrne vse preiskave pacienta iz podatkovne baze
        /// </summary>
        public static Preiskava[] VrniVsePreiskave()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePreiskave", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Preiskava> seznam = new List<Preiskava>();

            while (Bralec.Read())
            {
                Preiskava tmp = new Preiskava();
                tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Opis = (string)Bralec["Opis"];
                tmp.Rezultati = (string)Bralec["Rezultati"];
                seznam.Add(tmp);
            }

            Preiskava[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Vrne vse preiskave pacienta iz podatkovne baze glede na ID kartoteke
        /// </summary>
        /// <param name="StevilkaKartoteke">ID številka kartoteke</param>
        public IPNMP.Preiskava[] VrniVsePreiskavePoID(int StevilkaKartoteke)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePreiskavePoID", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaKartoteke", SqlDbType.Int));
            ukaz.Parameters["@ŠtevilkaKartoteke"].Value = StevilkaKartoteke;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Preiskava> seznam = new List<Preiskava>();

            while (Bralec.Read())
            {
                Preiskava tmp = new Preiskava();
                tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Opis = (string)Bralec["Opis"];
                tmp.Rezultati = (string)Bralec["Rezultati"];
                seznam.Add(tmp);
            }

            Preiskava[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }
    }
}
