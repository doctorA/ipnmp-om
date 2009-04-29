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

        public Ekipa Ekipa
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
        public static DataSet VrniVoznjePoEkipi()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Vrne vse vožnje v podatkovni bazi
        /// </summary>
        public static DataSet VrniVseVoznje()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Vrne vožnje glede na čas dogodka
        /// </summary>
        public DataSet VrniVoznjePoCasuDogodka()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Vrne vožnje glede na čas dogodka
        /// </summary>
        /// <param name="cas_dogodka">Čas dogodka YYYY-MM-DD HH:MM:SS</param>
        public IPNMP.Vožnja[] VrniVoznjePoCasuDogodka(DateTime cas_dogodka)
        {
            throw new System.NotImplementedException();
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
