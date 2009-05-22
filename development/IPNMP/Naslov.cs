using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Naslov
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public String Ulica
        {
            set;
            get;
        }

        public String HišnaŠtevilka
        {
            set;
            get;
        }

        public int IDNaslova
        {
            get;
            set;
        }

        public string Mesto
        {
            get;
            set;
        }

        public int PoštnaŠtevilka
        {
            get;
            set;
        }

        /// <summary>
        /// Vrne naslov glede na podan id naslova
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public static Naslov VrniNaslov(int ID)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("naslov_vrni", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_naslova", SqlDbType.Int));
            ukaz.Parameters["@id_naslova"].Value = ID;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Naslov tmp = new Naslov();
            Bralec.Read();
            tmp.HišnaŠtevilka = (string)Bralec["HisnaStevilka"];
            tmp.Ulica = (string)Bralec["Ulica"];
            tmp.Mesto = (string)Bralec["Mesto"];
            tmp.PoštnaŠtevilka = (int)Bralec["PostnaStevilka"];

            povezava.Close();
            return tmp;
        }
    }
}
