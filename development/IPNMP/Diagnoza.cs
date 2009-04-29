using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{
    public class Diagnoza
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public String Tip
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

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
        /// Vrne vse diagnoze pacienta iz podatkovne baze
        /// </summary>
        public static void VrniVseDiagnoze()
        {
            throw new System.NotImplementedException();
        }
    }
}
