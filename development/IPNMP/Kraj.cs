using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Kraj : Naslov
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public int Mesto
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public int PoštnaŠtevilka
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
