﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Medicinski_pripomočki
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public string Naziv
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public string Kategorija
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
        /// Vrne vse medicinske pripomočke iz podatkovne baze
        /// </summary>
        public static Medicinski_pripomočki[] VrniVseMedPrip()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseMedPrip", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Medicinski_pripomočki> seznam = new List<Medicinski_pripomočki>();

            while (Bralec.Read())
            {
                Medicinski_pripomočki tmp = new Medicinski_pripomočki();
                tmp.Kategorija = (string)Bralec["Kategorija"];
                tmp.Naziv = (string)Bralec["Naziv"];
                seznam.Add(tmp);
            }

            Medicinski_pripomočki[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }
    }
}
