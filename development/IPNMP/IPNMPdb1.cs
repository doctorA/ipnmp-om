using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
   public class IPNMPdb1
    {
        private string PotPovezave = "";
       
        public IPNMPdb1()
        {
            PotPovezave = Properties.Settings.Default.ConnectionString;

        }



       /// <summary>
       /// Pošlje vhodno osebo na bazo
       /// </summary>
       /// <param name="clovek">Vhodni objekt tipa Ipnmp.Oseba, ki bo poslan</param>
        public void UstvariOsebo(Oseba clovek){
            SqlConnection povezava = new SqlConnection(PotPovezave);
            
            SqlCommand ukaz = new SqlCommand("UstvariOsebo", povezava);
           
            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva",  SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = clovek.Ime;
            ukaz.Parameters["@Priimek"].Value =clovek.Priimek;
            ukaz.Parameters["@EMŠO"].Value = clovek.EMŠO;
            ukaz.Parameters["@DatumRojstva"].Value = clovek.DatumRojstva;
            ukaz.Parameters["@Spol"].Value = clovek.Spol;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();

        }

       /// <summary>
       /// Izbriše osebo iz podatkovne baze, glede na EMŠO
       /// </summary>
       /// <param name="EMŠO">Unikatna številka Osebe ,tipa int</param>
        public void IzbrisiOsebo(int EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiOsebo", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

       /// <summary>
       /// Vrne vse osebe iz podatkovne baze
       /// </summary>
       /// <returns>Vrne napolnjen dataset</returns>
        public DataSet VrniVseOsebe()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseOsebe", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Osebe");
            povezava.Close();

            return ds;
        }

        public void PosodobiOsebo(Oseba clovek)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = clovek.Ime;
            ukaz.Parameters["@Priimek"].Value = clovek.Priimek;
            ukaz.Parameters["@EMŠO"].Value = clovek.EMŠO;
            ukaz.Parameters["@DatumRojstva"].Value = clovek.DatumRojstva;
            ukaz.Parameters["@Spol"].Value = clovek.Spol;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();



        }


    }
}
