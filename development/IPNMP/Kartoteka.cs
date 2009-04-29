using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{
    public class Kartoteka
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public IPNMP.Zdravljenje Zdravljenja
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IPNMP.Diagnoza Diagnoze
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IPNMP.Preiskava Preiskave
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Medicinski_pripomočki Medicinski_pripomocki
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int ŠtevilkaKartoteke
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
        /// Vrne kartoteko pacienta iz podatkovne baze
        /// </summary>
        /// <param name="ŠtevilkaKartoteke">Vrne kartoteko iz podatkovne baze glede na številko kartoteke</param>
        public static void VrniKartoteko(Kartoteka ŠtevilkaKartoteke)
        {
            throw new System.NotImplementedException();
        }

    }
}
