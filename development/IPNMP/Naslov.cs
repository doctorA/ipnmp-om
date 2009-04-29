using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{
    public class Naslov
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public String Ulica
        {
            set;
            get;
        }

        public String HišnaŠtevilka
        {
            set;
            get;
        }
    }
}
