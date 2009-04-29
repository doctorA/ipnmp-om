using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{
    public class Preiskava
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public int Opis
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int Rezultati
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int DatumObiska
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
        /// Vrne vse preiskave pacienta iz podatkovne baze
        /// </summary>
        public static void VrniVsePreiskave()
        {
            throw new System.NotImplementedException();
        }
    }
}
