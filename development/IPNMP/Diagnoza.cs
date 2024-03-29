﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Diagnoza
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public string Tip
        {
            get;
            set;
        }

        public string Opis
        {
            get;
            set;
        }
/*
        public DateTime DatumObiska
        {
            get;
            set;
        }
        */
        /// <summary>
        /// Vrne vse diagnoze pacienta iz podatkovne baze
        /// </summary>
        public static Diagnoza[] VrniVseDiagnoze()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("diagnoza_vrniVse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Diagnoza> seznam = new List<Diagnoza>();

            while (Bralec.Read())
            {
                Diagnoza tmp = new Diagnoza();
               // tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Opis = (string)Bralec["Opis"];
                tmp.Tip = (string)Bralec["Tip"];
                seznam.Add(tmp);
            }

            Diagnoza[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Vrne vse diagnoze pacienta iz podatkovne baze glede na ID kartoteke
        /// </summary>
        /// <param name="StevilkaKartoteke">ID številka kartoteke</param>
        public static IPNMP.Diagnoza[] VrniDiagnozePoID(int StevilkaKartoteke)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("diagnoza_vrnipoIDkartoteka", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_kartoteka", SqlDbType.Int));
            ukaz.Parameters["@id_kartoteka"].Value = StevilkaKartoteke;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Diagnoza> seznam = new List<Diagnoza>();

            while (Bralec.Read())
            {
                Diagnoza tmp = new Diagnoza();
             //   tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.Opis = (string)Bralec["Opis"];
                tmp.Tip = (string)Bralec["Tip"];
                seznam.Add(tmp);
            }

            Diagnoza[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Ustvari nov vnos diagnoze v podatkovno bazo
        /// </summary>
        public int UstvariDiagnozo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("diagnoza_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@tip", SqlDbType.Text));
            ukaz.Parameters.Add(new SqlParameter("@opis", SqlDbType.Text));

            ukaz.Parameters["@tip"].Value = this.Tip;
            ukaz.Parameters["@opis"].Value = this.Opis;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Bralec.Read();
            int ID = Convert.ToInt32(Bralec[0]);
            return ID;
        }

        public static void KD(int id_kartoteka, int id_diagnoza)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("KD_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@id_kartoteka", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@id_diagnoza", SqlDbType.Int));


            ukaz.Parameters["@id_kartoteka"].Value = id_kartoteka;
            ukaz.Parameters["@id_diagnoza"].Value = id_diagnoza;


            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();

        }
    }
}
