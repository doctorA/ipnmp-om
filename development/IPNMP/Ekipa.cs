using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{
    public class Ekipa
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public Zaposleni Vodja
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Zaposleni
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
        /// Vrne določeno ekipo zaposlenih iz podatkovne baze
        /// </summary>
        public Ekipa VrniEkipo()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Ustvari novo ekipo zaposlenih v podatkovni bazi
        /// </summary>
        public void UstvariEkipo()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Izbriše ekipo zaposlenih iz podatkovne baze
        /// </summary>
        public void IzbrišiEkipo()
        {
            throw new System.NotImplementedException();
        }
    }
}
