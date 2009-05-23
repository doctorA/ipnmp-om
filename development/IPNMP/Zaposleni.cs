﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace IPNMP
{
    public class Zaposleni : Oseba
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public Zaposleni()
        {

        }

        public Zaposleni(Oseba o)
        {

            this.Ime = o.Ime;
            this.Priimek = o.Priimek;
            this.Naslov = o.Naslov;
            this.Spol = o.Spol;
            this.EMŠO = o.EMŠO;
            this.DatumRojstva = o.DatumRojstva;

        }


        public DateTime DatumZaposlitve { set; get; }
        public String TipZaposlenega { set; get; }

        public string Specializacija
        {
            get;
            set;
        }

     

        /// <summary>
        /// Vrne vse zaposlene iz podatkovne baze
        /// </summary>
        public static Zaposleni[] VrniVse()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("zaposleni_vrniVse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zaposleni> seznam = new List<Zaposleni>();

            while (Bralec.Read())
            {
                Zaposleni tmp = new Zaposleni();
                tmp.Ime = (string)Bralec["Ime"];
                tmp.Priimek = (string)Bralec["Priimek"];
                tmp.Spol = (string)Bralec["Spol"];
                tmp.EMŠO = (string)Bralec["EMSO"];
                tmp.Naslov = Naslov.VrniNaslov((int)Bralec["Idnaslov"]);
                tmp.DatumRojstva = (DateTime)Bralec["DatumRojstva"];
                tmp.IDOseba = (int)Bralec["id"];
                tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                tmp.Specializacija = (string)Bralec["Specializacija"];
                tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                seznam.Add(tmp);
            }

            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;

        }

        /// <summary>
        /// Ustvari nov vnos v podatkovni bazi za zaposlenega glede na njegov emšo /najprej posodobi/ustvari osebne podatke
        /// </summary>
        public void Ustvari()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            Oseba o = new Oseba();
            o.Ime = this.Ime;
            o.Priimek = this.Priimek;
            o.Naslov = this.Naslov;
            o.Spol = this.Spol;
            o.EMŠO = this.EMŠO;
            o.DatumRojstva = this.DatumRojstva;

            o.Posodobi();

            SqlCommand ukaz = new SqlCommand("UstvariZaposlenega", povezava);

            ukaz.Parameters.Add(new SqlParameter("@DatumZaposlitve", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Specializacija", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@TipZaposlenega", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@Specializacija"].Value = this.Specializacija;
            ukaz.Parameters["@DatumZaposlitve"].Value = this.DatumZaposlitve;
            ukaz.Parameters["@TipZaposlenega"].Value = this.TipZaposlenega;
            


            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Izbriše zaposlenega iz podatkovne baze
        /// </summary>
        public void Izbrisi()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("zaposleni_brisi", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_zapos", SqlDbType.Int));
            ukaz.Parameters["@id_zapos"].Value = this.IDOseba;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Posodobi podatke zaposlenega glede na njegov emšo //najprej posodobi/ustvari osebne podatke
        /// </summary>
        public void Posodobi()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            Oseba o = new Oseba();
            o.Ime = this.Ime;
            o.Priimek = this.Priimek;
            o.Naslov = this.Naslov;
            o.Spol = this.Spol;
            o.EMŠO = this.EMŠO;
            o.DatumRojstva = this.DatumRojstva;

            o.Posodobi();
            SqlCommand ukaz = new SqlCommand("PosodobiZaposlenega", povezava);

            ukaz.Parameters.Add(new SqlParameter("@DatumZaposlitve", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Specializacija", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@TipZaposlenega", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@Specializacija"].Value = this.Naslov;
            ukaz.Parameters["@DatumZaposlitve"].Value = this.DatumZaposlitve;
            ukaz.Parameters["@TipZaposlenega"].Value = this.TipZaposlenega;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne vse zaposlene glede na tip
        /// </summary>
        public static Zaposleni[] VrniVsePoTipu(string TipZaposlenega)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePoTipuZaposlenega", povezava);
            ukaz.Parameters.Add(new SqlParameter("@TipZaposlenega", SqlDbType.NVarChar, 255));
            ukaz.Parameters["@TipZaposlenega"].Value = TipZaposlenega;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zaposleni> seznam = new List<Zaposleni>();

            while (Bralec.Read())
            {
                
                
                    Zaposleni tmp = new Zaposleni(Oseba.VrniPoEmšo((string)Bralec["EMŠO"]));
                    tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                    tmp.Specializacija = (string)Bralec["Specializacija"];
                    tmp.TipZaposlenega = TipZaposlenega;
                    seznam.Add(tmp);
              
            }

            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Vrne zaposlenega iz baze glede na podano emšo številko
        /// </summary>
        public static Zaposleni VrniPoEmšo(string EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniPoEmšoZaposleni", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.NVarChar, 255));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Zaposleni tmp = new Zaposleni(Oseba.VrniPoEmšo((string)Bralec["EMŠO"]));
            Bralec.Read();
            tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
            tmp.Specializacija = (string)Bralec["Specializacija"];
            tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];

            povezava.Close();
            return tmp;
        }

        /* 
         public static IPNMP.Zaposleni[] VrniZaposlenePoEkipi(int ŠtevilkaEkipe)
         {
             SqlConnection povezava = new SqlConnection(PotPovezave);

             SqlCommand ukaz = new SqlCommand("VrniZaposlenePoEkipi", povezava);
             ukaz.Parameters.Add(new SqlParameter("@ŠtevilkaEkipe", SqlDbType.Int));
             ukaz.Parameters["@ŠtevilkaEkipe"].Value = ŠtevilkaEkipe;
             ukaz.CommandType = CommandType.StoredProcedure;
             povezava.Open();
             SqlDataReader Bralec = ukaz.ExecuteReader();
             List<Zaposleni> seznam = new List<Zaposleni>();

             while (Bralec.Read())
             {
                 Zaposleni tmp = new Zaposleni();
                 if ((int)Bralec["ŠtevilkaEkipe"] == ŠtevilkaEkipe)
                 {
                     tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                     tmp.ŠtevilkaEkipe = ŠtevilkaEkipe;
                     tmp.Specializacija = (int)Bralec["Specializacija"];
                     tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                     seznam.Add(tmp);
                 }
             }
             Zaposleni[] ds = seznam.ToArray();
             povezava.Close();

             return ds;
         }
         * */




        public static IPNMP.Zaposleni[] VrniVsePoImenu(string Ime, string Priimek)
        {

            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseZaposlenePoImenu", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));


            ukaz.Parameters["@Ime"].Value = Ime;
            ukaz.Parameters["@Priimek"].Value = Ime;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Zaposleni> seznam = new List<Zaposleni>();



            while (Bralec.Read())
            {
                Zaposleni tmp = new Zaposleni(Oseba.VrniPoEmšo((string)Bralec["EMŠO"]));

                tmp.DatumZaposlitve = (DateTime)Bralec["DatumZaposlitve"];
                tmp.Specializacija = (string)Bralec["Specializacija"];
                tmp.TipZaposlenega = (string)Bralec["TipZaposlenega"];
                seznam.Add(tmp);
            }

            Zaposleni[] ds = seznam.ToArray();
            povezava.Close();

            return ds;

        }
    }
}
