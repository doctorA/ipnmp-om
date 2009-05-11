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

        public  Vožnja()
        {
            
        }

        public Naslov Naslov
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IPNMP.Poročilo[] Poročila
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int ŠtevilkaEkipe
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        /// <summary>
        /// Vrne vse vožnje iz podatkovne baze glede na ekipo
        /// </summary>
        /// <param name="ImeEkipe">Ime ekipe, za katero želite dobiti vsa poročila</param>
        public static IPNMP.Vožnja[] VrniVoznjePoEkipi(string ImeEkipe)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVoznjePoEkipi", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Vožnja> seznam = new List<Vožnja>();

            while (Bralec.Read())
            {
                Vožnja tmp = new Vožnja();
                if ((string)Bralec["ŠtevilkaEkipe"] == ImeEkipe)
                {
                    tmp.ČasDogodka = (DateTime)Bralec["ČasDogodka"];
                    tmp.ČasKlicanjaReševalcev = (DateTime)Bralec["ČasKlicanjaReševalcev"];
                    tmp.ČasPrispetjaReševalcev = (DateTime)Bralec["ČasPrispetjaReševalcev"];
                    tmp.ČasPrispetjaVBolnišnico = (DateTime)Bralec["ČasPrispetjaVBolnišnico"];
                    tmp.ŠtevilkaEkipe = (int)Bralec["ŠtevilkaEkipe"];
                    tmp.Kilometrina = (int)Bralec["Kilometrina"];
                    seznam.Add(tmp);
                }
            }

            Vožnja[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }
                

        /// <summary>
        /// Vrne vse vožnje v podatkovni bazi
        /// </summary>
        public static IPNMP.Vožnja[] VrniVseVoznje()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseVoznje", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Vožnja> seznam = new List<Vožnja>();

            while (Bralec.Read())
            {
                Vožnja tmp = new Vožnja();
                tmp.ČasDogodka = (DateTime)Bralec["ČasDogodka"];
                tmp.ČasKlicanjaReševalcev = (DateTime)Bralec["ČasKlicanjaReševalcev"];
                tmp.ČasPrispetjaReševalcev = (DateTime)Bralec["ČasPrispetjaReševalcev"];
                tmp.ČasPrispetjaVBolnišnico = (DateTime)Bralec["ČasPrispetjaVBolnišnico"];
                tmp.ŠtevilkaEkipe = (int)Bralec["ŠtevilkaEkipe"];
                tmp.Kilometrina = (int)Bralec["Kilometrina"];
                seznam.Add(tmp);
            }
            
            Vožnja[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Vrne vožnje glede na čas dogodka
        /// </summary>
        /// <param name="cas_dogodka">Čas dogodka YYYY-MM-DD HH:MM:SS</param>
        public IPNMP.Vožnja[] VrniVoznjePoCasuDogodka(DateTime cas_dogodka)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVoznjePoCasuDogodka", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Vožnja> seznam = new List<Vožnja>();

            while (Bralec.Read())
            {
                Vožnja tmp = new Vožnja();
                if ((DateTime)Bralec["ČasDogodka"] == cas_dogodka)
                {
                    tmp.ČasDogodka = (DateTime)Bralec["ČasDogodka"];
                    tmp.ČasKlicanjaReševalcev = (DateTime)Bralec["ČasKlicanjaReševalcev"];
                    tmp.ČasPrispetjaReševalcev = (DateTime)Bralec["ČasPrispetjaReševalcev"];
                    tmp.ČasPrispetjaVBolnišnico = (DateTime)Bralec["ČasPrispetjaVBolnišnico"];
                    tmp.ŠtevilkaEkipe = (int)Bralec["ŠtevilkaEkipe"];
                    tmp.Kilometrina = (int)Bralec["Kilometrina"];
                    seznam.Add(tmp);
                }
            }

            Vožnja[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Izračuna časovno razliko, med prispetjem reševalcev na kraj nesreče in prispetjem v bolnišnico, ter vrne kot datetime
        /// </summary>
        public DateTime IzračunajČas()
        {
            throw new System.NotImplementedException();
        }

        public int Kilometrina
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
