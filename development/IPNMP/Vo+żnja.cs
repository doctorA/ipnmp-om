using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    
    public class Vožnja
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public DateTime ČasKlicanjaReševalcev { set; get; }

        public DateTime ČasPrispetjaReševalcev { set; get; }

        public DateTime ČasPrispetjaVBolnišnico { set; get; }

        public DateTime ČasDogodka { set; get; }

        public Naslov Naslov
        {
            get;
            set;
        }

        public int Ustvari()
        {

            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("voznja_dodaj", povezava);


            ukaz.Parameters.Add(new SqlParameter("@casDogodka_voznje", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@casklicanjaresevalcev_voznje", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@casprispetjaresevalcev_voznje", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@casprispetjavbolnisnico_voznje", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@idnaslov_voznje", SqlDbType.Int));


            ukaz.Parameters["@casDogodka_voznje"].Value = this.ČasDogodka;
            ukaz.Parameters["@casklicanjaresevalcev_voznje"].Value = this.ČasKlicanjaReševalcev;
            ukaz.Parameters["@casprispetjaresevalcev_voznje"].Value = this.ČasPrispetjaReševalcev;
            ukaz.Parameters["@casprispetjavbolnisnico_voznje"].Value = this.ČasPrispetjaVBolnišnico;
            ukaz.Parameters["@idnaslov_voznje"].Value = this.Naslov.IDNaslova;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
          
            Bralec.Read();
            int ID = Convert.ToInt32(Bralec[0]);


            povezava.Close();




            return ID;
        }

        public static void VZ(int id_zap,int id_voz)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("V_Z_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@id_zap", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@id_voz", SqlDbType.Int));
          

            ukaz.Parameters["@id_zap"].Value = id_zap;
            ukaz.Parameters["@id_voz"].Value = id_voz;
           

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        public static void VP(int id_porocilo,int id_voznja)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("V_P_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@id_porocilo", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@id_voznja", SqlDbType.Int));


            ukaz.Parameters["@id_porocilo"].Value = id_porocilo;
            ukaz.Parameters["@id_voznja"].Value = id_voznja;


            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();

        }
        
    }
}
