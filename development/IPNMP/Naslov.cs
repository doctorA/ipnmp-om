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
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string Mesto
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int PoštnaŠtevilka
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public static Naslov VrniNaslov(int ID)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniNaslov", povezava);
            ukaz.Parameters.Add(new SqlParameter("@IDNaslova", SqlDbType.Int));
            ukaz.Parameters["@IDNaslova"].Value = ID;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Naslov tmp = new Naslov();
            Bralec.Read();
            tmp.HišnaŠtevilka = (string)Bralec["HišnaŠtevilka"];
            tmp.Ulica = (string)Bralec["Ulica"];
            tmp.Mesto = (string)Bralec["Mesto"];
            tmp.PoštnaŠtevilka = (int)Bralec["PoštnaŠtevilka"];

            povezava.Close();
            return tmp;
        }
    }
}
