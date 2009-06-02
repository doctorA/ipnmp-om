using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    //[Obsolete("Razred ni potreben", true)]
    public class Vožnja
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;

        public DateTime ČasDogodka { set; get; }

        public Naslov Naslov
        {
            get;
            set;
        }


        
    }
}
