using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
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

        public string Simptomi
        {
            get;
            set;
        }

        public DateTime DatumObiska
        {
            get;
            set;
        }

        

        /// <summary>
        /// Vrne kartoteko pacienta iz podatkovne baze
        /// </summary>
        /// <param name="ŠtevilkaKartoteke">Vrne kartoteko iz podatkovne baze glede na številko kartoteke</param>
        public static Kartoteka[] VrniKartotekePoIdPacienta(int idPacienta)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            int ŠtevilkaKartoteke = 0;
            SqlCommand ukaz = new SqlCommand("kartoteka_vrnivse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            List<Kartoteka> seznam = new List<Kartoteka>();



            while (Bralec.Read())
            {

                if ((int)Bralec["idPacient"] == idPacienta)
                {
                    
                    Kartoteka tmp = new Kartoteka();
                    tmp.ŠtevilkaKartoteke = (int)Bralec["id"];
                    tmp.Diagnoze = Diagnoza.VrniDiagnozePoID(tmp.ŠtevilkaKartoteke);
                    tmp.terapije = Terapija.VrniVseTerapijePoIdKartoteke(tmp.ŠtevilkaKartoteke);
                    tmp.Preiskave = Preiskava.VrniVsePreiskavePoID(tmp.ŠtevilkaKartoteke);
                    tmp.Medicinski_pripomocki = Medicinski_pripomočki.VrniVseMedPripPoID(tmp.ŠtevilkaKartoteke);
                    tmp.DatumObiska = (DateTime)Bralec["datumObiska"];
                    tmp.Simptomi = (string)Bralec["simptomi"];
                    seznam.Add(tmp);

                }
                
            }
            Kartoteka[] ds = seznam.ToArray();
            return ds;

            
        }
        public static int VrniStevilkoKartotekePoIdPacienta(int idPacienta)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            int stevilkaKartoteke = 0;
            SqlCommand ukaz = new SqlCommand("kartoteka_vrnivse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

           

            while (Bralec.Read())
            {
             
                if ((int)Bralec["idPacient"] == idPacienta)
                {
                    stevilkaKartoteke=(int)Bralec["id"];


                }
              
            }

            return stevilkaKartoteke;

        }

        /// <summary>
        /// Ustvari nov primerek tipa Kartoteka v podatkovni bazi
        /// </summary>
        public void UstvariKartoteko(Pacient pacient, Zaposleni zaposlen)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("kartoteka_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@datum_obiska", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@simptomi", SqlDbType.Text));
            ukaz.Parameters.Add(new SqlParameter("@id_pacient", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@id_zaposleni", SqlDbType.Int));

            ukaz.Parameters["@datum_obiska"].Value = this.DatumObiska;
            ukaz.Parameters["@simptomi"].Value = this.Simptomi;
            ukaz.Parameters["@id_pacient"].Value = pacient.IdPacienta;
            ukaz.Parameters["@id_zaposleni"].Value = zaposlen.IdZaposleni;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
           
            Bralec.Read();
            int IDKartoteke = Convert.ToInt32(Bralec[0]);

            foreach (Terapija t in this.terapije)
            {
                int IDTerapije = t.UstvariTerapijo();
                Terapija.KT(IDKartoteke,IDTerapije);
                
            }

            foreach (Preiskava p in this.Preiskave)
            {
                int IDPreiskave = p.UstvariPreiskavo();
                Preiskava.KP(IDKartoteke, IDPreiskave);

            }

            foreach (Diagnoza d in this.Diagnoze)
            {
                int IDDiagnoza = d.UstvariDiagnozo();
                Diagnoza.KD(IDKartoteke, IDDiagnoza);

            }

            foreach (Medicinski_pripomočki m in this.Medicinski_pripomocki)
            {
                int IDMedPrip = m.UstvariMedPrip();
                Terapija.KT(IDKartoteke, IDMedPrip);

            }

           
        }

    }
}
