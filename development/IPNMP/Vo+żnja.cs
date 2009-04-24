using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{
    public class Vožnja
    {
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

        public IPNMP.Zaposleni[] Ekipa
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
        public IPNMP.Vožnja[] VrniVoznjePoEkipi()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Vrne vse vožnje v podatkovni bazi
        /// </summary>
        public Vožnja VrniVseVoznje()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Vrne vožnje glede na čas dogodka
        /// </summary>
        public IPNMP.Vožnja[] VrniVoznjePoCasuDogodka()
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

        
    }
}
