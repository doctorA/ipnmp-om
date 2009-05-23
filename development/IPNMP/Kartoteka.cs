﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{
    public class Kartoteka
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public IPNMP.Terapija[] terapije
        {

            get;
            set;
        }

        public IPNMP.Diagnoza[] Diagnoze
        {
            get;
            set;
        }

        public IPNMP.Preiskava[] Preiskave
        {
            get;
            set;
        }

        public IPNMP.Medicinski_pripomočki[] Medicinski_pripomocki
        {
            get;
            set;
        }

        public int ŠtevilkaKartoteke
        {
            get;
            set;
        }

        

        /// <summary>
        /// Vrne kartoteko pacienta iz podatkovne baze
        /// </summary>
        /// <param name="ŠtevilkaKartoteke">Vrne kartoteko iz podatkovne baze glede na številko kartoteke</param>
        public static Kartoteka VrniKartoteko(int ŠtevilkaKartoteke)
        {
            Kartoteka tmp = new Kartoteka();
            tmp.Diagnoze = Diagnoza.VrniDiagnozePoID(ŠtevilkaKartoteke);
            tmp.terapije = Terapija.VrniVseTerapijePoIdKartoteke(ŠtevilkaKartoteke);
            tmp.Preiskave = Preiskava.VrniVsePreiskavePoID(ŠtevilkaKartoteke);
            tmp.Medicinski_pripomocki = Medicinski_pripomočki.VrniVseMedPripPoID(ŠtevilkaKartoteke);
            return tmp;
        }

    }
}
