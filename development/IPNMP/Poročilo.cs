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

        public int ŠtevilkaPoročila
        {
            get;
            set;
        }

        ///// <summary>
        ///// Vrne vsa poročila glede na datum kreacije poročila
        ///// </summary>
        //public static Poročilo[] VrniPorocilaPoDatumu(DateTime datum)
        //{
        //    SqlConnection povezava = new SqlConnection(PotPovezave);

        //    SqlCommand ukaz = new SqlCommand("VrniPorocilaPoDatumu", povezava);
        //    ukaz.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime));
        //    ukaz.Parameters["@Datum"].Value = datum;
        //    ukaz.CommandType = CommandType.StoredProcedure;
        //    povezava.Open();
        //    SqlDataReader Bralec = ukaz.ExecuteReader();

        //    List<Poročilo> seznam = new List<Poročilo>();

        //    while (Bralec.Read())
        //    {
                
             
        //            Poročilo tmp = new Poročilo();
        //            tmp.AkcijeReševalcev = (string)Bralec["AkcijeReševalcev"];
        //            //tmp.Avtor = Zaposleni.VrniPoEmšo((string)Bralec["Avtor"]);
        //            tmp.Datum = datum;
        //            tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
        //            tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
        //            tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnišnico"];
        //            tmp.ŠtevilkaPoročila = (int)Bralec["ŠtevilkaPoročila"];
        //            seznam.Add(tmp);
                
        //    }

        //    Poročilo[] ds = seznam.ToArray();
        //    povezava.Close();
        //    return ds;
        //}
        /// <summary>
        /// posodobi poročilo v podatkovni bazi, glede na številko poročila objekta.
        /// </summary>
        public void PosodobiPorocilo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("porocilo_uredi", povezava);

            ukaz.Parameters.Add(new SqlParameter("@akcijeresevalcev_porocila", SqlDbType.NVarChar, 255));
       //     ukaz.Parameters.Add(new SqlParameter("@Avtor", SqlDbType.NVarChar, 255));
        //    ukaz.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@opisdogodka_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@stanjepacientaobprispetju_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@stanjepacientaobprispetjuvbolnisnico_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@id_porocila", SqlDbType.Int));


            ukaz.Parameters["@akcijeresevalcev_porocila"].Value = this.AkcijeReševalcev;
      //      ukaz.Parameters["@Avtor"].Value = this.Avtor.EMŠO;
            ukaz.Parameters["@id_porocila"].Value = this.ŠtevilkaPoročila;
      //      ukaz.Parameters["@Datum"].Value = this.Datum;
            ukaz.Parameters["@opisdogodka_porocila"].Value = this.OpisDogodka;
            ukaz.Parameters["@stanjepacientaobprispetju_porocila"].Value = this.StanjePacientaObPrispetju;
            ukaz.Parameters["@stanjepacientaobprispetjuvbolnisnico_porocila"].Value = this.StanjePacientaObPrispetjuVBolnišnico;

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

            SqlCommand ukaz = new SqlCommand("porocilo_vrniVse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                tmp.AkcijeReševalcev = (string)Bralec["AkcijeResevalcev"];
                //tmp.Avtor = Zaposleni.VrniPoEmšo((string)Bralec["Avtor"]);
            //    tmp.Datum = (DateTime)Bralec["Datum"];
                tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
                    
                tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnisnico"];
                tmp.ŠtevilkaPoročila = (int)Bralec["ID"];
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

            SqlCommand ukaz = new SqlCommand("porocilo_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@akcijeresevalcev_porocila", SqlDbType.NVarChar));
            //ukaz.Parameters.Add(new SqlParameter("@Avtor", SqlDbType.NVarChar, 255));
            //ukaz.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@opisdogodka_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@stanjepacientaobprispetju_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@stanjepacientaobprispetjuvbolnisnico_porocila", SqlDbType.NVarChar));

            ukaz.Parameters["@akcijeresevalcev_porocila"].Value = this.AkcijeReševalcev;
            //ukaz.Parameters["@Avtor"].Value = this.Avtor.EMŠO;
         //   ukaz.Parameters["@Datum"].Value = this.Datum;
            ukaz.Parameters["@opisdogodka_porocila"].Value = this.OpisDogodka;
            ukaz.Parameters["@stanjepacientaobprispetju_porocila"].Value = this.StanjePacientaObPrispetju;
            ukaz.Parameters["@stanjepacientaobprispetjuvbolnisnico_porocila"].Value = this.StanjePacientaObPrispetjuVBolnišnico;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }
        ///// <summary>
        ///// Metoda poišče poročila v podatkovni bazi glede na podanega avtorja(njegov emšo)
        ///// </summary>
        ///// <returns>Vrne polje poročil napolnjen z poročili, ki jih je avtor napisal</returns>
        //public static Poročilo[] VrniPorocilaPoAvtorju(Zaposleni Avtor)
        //{
        //    SqlConnection povezava = new SqlConnection(PotPovezave);

        //    SqlCommand ukaz = new SqlCommand("VrniPorocilaPoAvtorju", povezava);
        //    ukaz.CommandType = CommandType.StoredProcedure;
        //    povezava.Open();
        //    SqlDataReader Bralec = ukaz.ExecuteReader();

        //    List<Poročilo> seznam = new List<Poročilo>();

        //    while (Bralec.Read())
        //    {
        //        Poročilo tmp = new Poročilo();
        //        if (Zaposleni.VrniPoEmšo((string)Bralec["Avtor"]).EMŠO == Avtor.EMŠO)
        //        {
        //            tmp.AkcijeReševalcev = (string)Bralec["AkcijeReševalcev"];
        //            tmp.Avtor = Zaposleni.VrniPoEmšo((string)Bralec["Avtor"]);
        //            tmp.Datum = (DateTime)Bralec["Datum"];
        //            tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
        //            tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
        //            tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnišnico"];
        //            tmp.ŠtevilkaPoročila = (int)Bralec["ŠtevilkaPoročila"];
        //            seznam.Add(tmp);
        //        }
        //    }

        //    Poročilo[] ds = seznam.ToArray();
        //    povezava.Close();
        //    return ds;
        //}

      

        /// <summary>
        /// Vrne poročilo iz podatkovne baze glede na ID številko poročila
        /// </summary>
        /// <param name="StevilkaPorocila">ID številka poročila</param>
        public static IPNMP.Poročilo[] VrniPorociloPoID(int StevilkaPorocila)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("porocilo_vrni", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_porocila", SqlDbType.Int));
            ukaz.Parameters["@id_porocila"].Value = StevilkaPorocila;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                tmp.AkcijeReševalcev = (string)Bralec["AkcijeResevalcev"];
               // tmp.Avtor = Zaposleni.VrniPoEmšo((string)Bralec["Avtor"]);
                //tmp.Datum = (DateTime)Bralec["Datum"];
                tmp.OpisDogodka = (string)Bralec["OpisDogodka"];

                tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnisnico"];
                tmp.ŠtevilkaPoročila = (int)Bralec["ID"];
                seznam.Add(tmp);
            }

            Poročilo[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }
    }
}
