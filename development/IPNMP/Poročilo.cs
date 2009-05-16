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
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public String StanjePacientaObPrispetju { set; get; }
        public String StanjePacientaObPrispetjuVBolnišnico { set; get; }
        public String OpisDogodka { set;get;}
        public String AkcijeReševalcev { set; get; }

        public DateTime Datum { set; get; }

        public Poročilo()
        {
        }
        /// <summary>
        /// V podatkovni bazi je to emšo osebe
        /// </summary>
        public Zaposleni Avtor
        {
            get;
            set;
        }

        /// <summary>
        /// Vrne vsa poročila glede na datum kreacije poročila
        /// </summary>
        public static Poročilo[] VrniPorocilaPoDatumu(DateTime datum)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniPorocilaPoDatumu", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                if ((DateTime)Bralec["Datum"] == datum)
                {
                    tmp.AkcijeReševalcev = (string)Bralec["AkcijeReševalcev"];
                    tmp.Avtor = Zaposleni.VrniZaposlenegaZZZS((int)Bralec["Avtor"]);
                    tmp.Datum = (DateTime)Bralec["Datum"];
                    tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
                    tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                    tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnišnico"];
                    tmp.ŠtevilkaPoročila = (int)Bralec["ŠtevilkaPoročila"];
                    seznam.Add(tmp);
                }
            }

            Poročilo[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }
        /// <summary>
        /// posodobi poročilo v podatkovni bazi, glede na številko poročila objekta.
        /// </summary>
        public void PosodobiPorocilo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiPorocilo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@AkcijeResevalcev", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Avtor", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@OpisDogotka", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@StanjePacientaObPrispetju", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@StanjePacientaObPrispetjuVBolnisnico", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaPoročila", SqlDbType.Int));


            ukaz.Parameters["@AkcijeResevalcev"].Value = this.AkcijeReševalcev;
            ukaz.Parameters["@Avtor"].Value = this.Avtor.EMŠO;
            ukaz.Parameters["@ŠtevilkaPoročila"].Value = this.ŠtevilkaPoročila;
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
        /// Vrne vsa poročila iz podatkovne baze
        /// </summary>
        public static Poročilo[] VrniVsaPorocila()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaPorocila", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                tmp.AkcijeReševalcev = (string)Bralec["AkcijeReševalcev"];
                tmp.Avtor = Zaposleni.VrniZaposlenegaZZZS((int)Bralec["Avtor"]);
                tmp.Datum = (DateTime)Bralec["Datum"];
                tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
                    
                tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnišnico"];
                tmp.ŠtevilkaPoročila = (int)Bralec["ŠtevilkaPoročila"];
                seznam.Add(tmp);
            }

            Poročilo[] ds = seznam.ToArray();
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
        /// <returns>Vrne polje poročil napolnjen z poročili, ki jih je avtor napisal</returns>
        public static Poročilo[] VrniPorocilaPoAvtorju(Zaposleni Avtor)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniPorocilaPoAvtorju", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                if (Zaposleni.VrniZaposlenegaZZZS((int)Bralec["Avtor"]).EMŠO == Avtor.EMŠO)
                {
                    tmp.AkcijeReševalcev = (string)Bralec["AkcijeReševalcev"];
                    tmp.Avtor = Zaposleni.VrniZaposlenegaZZZS((int)Bralec["Avtor"]);
                    tmp.Datum = (DateTime)Bralec["Datum"];
                    tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
                    tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                    tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnišnico"];
                    tmp.ŠtevilkaPoročila = (int)Bralec["ŠtevilkaPoročila"];
                    seznam.Add(tmp);
                }
            }

            Poročilo[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        public int ŠtevilkaPoročila
        {
            get;
            set;
        }

        /// <summary>
        /// Vrne poročilo iiz podatkovne baze glede na ID številko poročila
        /// </summary>
        /// <param name="StevilkaPorocila">ID številka poročila</param>
        public static IPNMP.Poročilo[] VrniPorociloPoID(int StevilkaPorocila)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniPorociloPoID", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaPoročila", SqlDbType.Int));
            ukaz.Parameters["@ŠtevilkaPoročila"].Value = StevilkaPorocila;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                tmp.AkcijeReševalcev = (string)Bralec["AkcijeReševalcev"];
                tmp.Avtor = Zaposleni.VrniZaposlenegaZZZS((int)Bralec["Avtor"]);
                tmp.Datum = (DateTime)Bralec["Datum"];
                tmp.OpisDogodka = (string)Bralec["OpisDogodka"];

                tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnišnico"];
                tmp.ŠtevilkaPoročila = (int)Bralec["ŠtevilkaPoročila"];
                seznam.Add(tmp);
            }

            Poročilo[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }
    }
}
