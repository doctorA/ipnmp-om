using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Poročilo
    {
        protected static string PotPovezave = "";
        public String StanjePacientaObPrispetju { set; get; }
        public String StanjePacientaObPrispetjuVBolnišnico { set; get; }
        public String OpisDogodka { set;get;}
        public String AkcijeReševalcev { set; get; }

        public DateTime Datum { set; get; }

        public Poročilo()
        {
            PotPovezave = Properties.Settings.Default.ConnectionString;
        }
        public Zaposleni Avtor
        {
            get;
            set;
        }

        /// <summary>
        /// Vrne vsa poročila glede na datum kreacije poročila
        /// </summary>
        public static DataSet VrniPorocilaPoDatumu(DateTime datum)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaPoročilaPoDatumu", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            ukaz.Parameters.Add(new SqlParameter("@Datum", SqlDbType.Date));
            ukaz.Parameters["@Datum"].Value = datum;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "PoročilaPoDatumu");
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Vrne vsa poročila iz podatkovne baze
        /// </summary>
        public static DataSet VrniVsaPorocila()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaPoročila", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Poročila");
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Ustvari poročilo v podatkovni bazi
        /// </summary>
        public void UstvariPorocilo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariPorocilo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@AkcijeResevalcev", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Avtor", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@OpisDogotka", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@StanjePacientaObPrispetju", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@StanjePacientaObPrispetjuVBolnisnico", SqlDbType.NVarChar));

            ukaz.Parameters["@AkcijeResevalcev"].Value = this.AkcijeReševalcev;
            ukaz.Parameters["@Avtor"].Value = this.Avtor.EMŠO;
            ukaz.Parameters["@Datum"].Value = this.Datum;
            ukaz.Parameters["@OpisDogotka"].Value = this.OpisDogodka;
            ukaz.Parameters["@StanjePacientaObPrispetju"].Value = this.StanjePacientaObPrispetju;
            ukaz.Parameters["@StanjePacientaObPrispetjuVBolnisnico"].Value = this.StanjePacientaObPrispetjuVBolnišnico;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }
        /// <summary>
        /// Metoda poišče poročila v podatkovni bazi glede na podanega avtorja(njegov emšo)
        /// </summary>
        /// <returns>Vrne Dataset napolnjen z poročili, ki jih je avtor napisal</returns>
        public static DataSet VrniPorocilaPoAvtorju(Zaposleni Avtor)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaPoročilaPoAvtorju", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            ukaz.Parameters.Add(new SqlParameter("@Avtor", SqlDbType.Int));
            ukaz.Parameters["@Avtor"].Value = Avtor.EMŠO;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "PoročilaPoAvtorju");
            povezava.Close();

            return ds;
        }

        public int ŠtevilkaPoročila
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }
    }
}
