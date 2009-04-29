using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{
    public class Zdravljenje
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public string Opis
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string Tip
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DateTime DatumObiska
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
        /// Vrne vsa zdravljenja iz podatkovne baze
        /// </summary>
        public static Zdravljenje VrniVsaZdravljenja()
        {
            throw new System.NotImplementedException();
        }
    }
}
