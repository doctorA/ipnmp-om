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

        /// <summary>
        /// Pošlje kartoteko na bazo.
        /// </summary>
        /// <param name="seznam">Kartoteka ime_kartoteke</param>
        public void UstvariKartoteko(Kartoteka seznam)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariKartoteko", povezava);

            ukaz.Parameters.Add(new SqlParameter("@DatumObiska", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Simptomi", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@DatumObiska"].Value = seznam.DatumObiska;
            ukaz.Parameters["@Simptomi"].Value = seznam.Simptomi;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }
        /// <summary>
        /// Izbris kartoteke na podlagi ID-ja
        /// </summary>
        /// <param name="seznam">int ID</param>
        public void IzbrisiKartoteko(int id)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiKartoteko", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
            ukaz.Parameters["@ID"].Value = id;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }
        /// <summary>
        /// Vrne vse kartoteke iz podatkovne baze
        /// </summary>
        /// <returns></returns>
        public DataSet VrniVseKartoteke()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseKartoteke", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;

            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Kartoteke");
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Posodobi kartoteko v podatkovni bazi
        /// </summary>
        /// <param name="seznam">Kartoteka kartoteka1</param>
        public void PosodobiKartoteko(Kartoteka seznam)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiKartoteko", povezava);

            ukaz.Parameters.Add(new SqlParameter("@DatumObiska", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Simptomi", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@DatumObiska"].Value = seznam.DatumObiska;
            ukaz.Parameters["@Simptomi"].Value = seznam.Simptomi;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

    }
}
