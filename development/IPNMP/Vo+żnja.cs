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
    }
}
