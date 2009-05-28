using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    [Obsolete("Razred ni potreben", true)]
    public class Ekipa
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public Zaposleni Vodja
        {
            get;set;
        }

        public Zaposleni[] _Zaposleni
        {
            get;
            set;

        }

        public int ŠtevilkaEkipe
        {
            get;
            set;
        }

        /// <summary>
        /// Vrne določeno ekipo zaposlenih iz podatkovne baze
        /// </summary>
        /// 
        /*
        public static Ekipa VrniEkipoŠt(int ŠtevilkaEkipe)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniEkipoŠt", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaEkipe", SqlDbType.Int));
            ukaz.Parameters["@ŠtevilkaEkipe"].Value = ŠtevilkaEkipe;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Ekipa tmp = new Ekipa();
            Bralec.Read();
            tmp.Vodja = (Zaposleni)Bralec["Vodja"];
            povezava.Close();
            Zaposleni[] množica = IPNMP.Zaposleni.VrniVse();
            List<Zaposleni> seznam = new List<Zaposleni>();

            foreach (Zaposleni z in množica)
            {
               if(z.ŠtevilkaEkipe==ŠtevilkaEkipe){

                    seznam.Add(z);
               }
            }

            tmp._Zaposleni = seznam.ToArray();

            
            return tmp;
        }
        */
        /// <summary>
        /// Ustvari novo ekipo zaposlenih v podatkovni bazi
        /// </summary>
        /// 
        /*
        public void UstvariEkipo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariEkipo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Vodja", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaEkipe", SqlDbType.Int));


            ukaz.Parameters["@Vodja"].Value = this.Vodja;
            ukaz.Parameters["@ŠtevilkaEkipe"].Value = this.ŠtevilkaEkipe;
            foreach (Zaposleni z in this._Zaposleni)
            {
                z.ŠtevilkaEkipe = ŠtevilkaEkipe;
                z.Posodobi();

            }
       
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Izbriše ekipo zaposlenih iz podatkovne baze glede na ŠtevilkoEkipe v objektu
        /// </summary>
        public void IzbrišiEkipo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiEkipo", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaEkipe", SqlDbType.Int));
            ukaz.Parameters["@ŠtevilkaEkipe"].Value = this.ŠtevilkaEkipe;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }
         * */
    }
}
