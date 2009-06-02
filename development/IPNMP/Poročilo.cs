using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
    public class Poročilo
    {
        protected static string PotPovezave = Properties.Settings.Default.ConnectionString;
        public String StanjePacientaObPrispetju { set; get; }
        public String StanjePacientaObPrispetjuVBolnišnico { set; get; }
        public String OpisDogodka { set;get;}
        public String AkcijeReševalcev { set; get; }

        //public DateTime ČasDogodka { set; get; }

        public Poročilo()
        {
        }
        /// <summary>
        /// V podatkovni bazi je to id pacienta
        /// </summary>
        public IPNMP.Pacient Pacient
        {
            get;
            set;
        }

        public int ŠtevilkaPoročila
        {
            get;
            set;
        }

        ///// <summary>
        ///// Vrne vsa poročila glede na datum kreacije poročila
        ///// </summary>
        //public static Poročilo[] VrniPorocilaPoDatumu(DateTime datum)
        //{
        //    SqlConnection povezava = new SqlConnection(PotPovezave);

        //    SqlCommand ukaz = new SqlCommand("VrniPorocilaPoDatumu", povezava);
        //    ukaz.Parameters.Add(new SqlParameter("@DatumObiska", SqlDbType.DateTime));
        //    ukaz.Parameters["@DatumObiska"].Value = datum;
        //    ukaz.CommandType = CommandType.StoredProcedure;
        //    povezava.Open();
        //    SqlDataReader Bralec = ukaz.ExecuteReader();

        //    List<Poročilo> seznam = new List<Poročilo>();

        //    while (Bralec.Read())
        //    {
                
             
        //            Poročilo tmp = new Poročilo();
        //            tmp.AkcijeReševalcev = (string)Bralec["AkcijeReševalcev"];
        //            //tmp.Pacient = Zaposleni.VrniPoEmšo((string)Bralec["Pacient"]);
        //            tmp.DatumObiska = datum;
        //            tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
        //            tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
        //            tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnišnico"];
        //            tmp.ŠtevilkaPoročila = (int)Bralec["ŠtevilkaPoročila"];
        //            seznam.Add(tmp);
                
        //    }

        //    Poročilo[] ds = seznam.ToArray();
        //    povezava.Close();
        //    return ds;
        //}
        /// <summary>
        /// posodobi poročilo v podatkovni bazi, glede na številko poročila objekta.
        /// </summary>
        public void PosodobiPorocilo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("porocilo_uredi", povezava);

            ukaz.Parameters.Add(new SqlParameter("@akcijeresevalcev_porocila", SqlDbType.NVarChar, 255));
          //  ukaz.Parameters.Add(new SqlParameter("@IdPacient_porocila", SqlDbType.Int));
        //    ukaz.Parameters.Add(new SqlParameter("@DatumObiska", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@opisdogodka_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@stanjepacientaobprispetju_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@stanjepacientaobprispetjuvbolnisnico_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@id_porocila", SqlDbType.Int));


            ukaz.Parameters["@akcijeresevalcev_porocila"].Value = this.AkcijeReševalcev;
           // ukaz.Parameters["@IdPacient_porocila"].Value = this.Pacient.IdPacienta;
            ukaz.Parameters["@id_porocila"].Value = this.ŠtevilkaPoročila;
      //      ukaz.Parameters["@DatumObiska"].Value = this.DatumObiska;
            ukaz.Parameters["@opisdogodka_porocila"].Value = this.OpisDogodka;
            ukaz.Parameters["@stanjepacientaobprispetju_porocila"].Value = this.StanjePacientaObPrispetju;
            ukaz.Parameters["@stanjepacientaobprispetjuvbolnisnico_porocila"].Value = this.StanjePacientaObPrispetjuVBolnišnico;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne vsa poročila iz podatkovne baze
        /// </summary>
        public static Poročilo[] VrniVsaPorocila()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("porocilo_vrniVse", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();

            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                tmp.AkcijeReševalcev = (string)Bralec["AkcijeResevalcev"];
                //tmp.Pacient = Zaposleni.VrniPoEmšo((string)Bralec["Pacient"]);
            //    tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
                    
                tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnisnico"];
                tmp.ŠtevilkaPoročila = (int)Bralec["ID"];
                tmp.Pacient = Pacient.VrniPoIdPacient((int)Bralec["idPacient"]);
                tmp.Ekipa = Zaposleni.VrniZaposlenePoIdPoročila((int)Bralec["ID"]);
                tmp.Naslov = Poročilo.VrniVoznjoPoIdPorocila((int)Bralec["ID"]).Naslov;
                tmp.ČasDogodka = Poročilo.VrniVoznjoPoIdPorocila((int)Bralec["ID"]).ČasDogodka;
                seznam.Add(tmp);
            }

            Poročilo[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        /// <summary>
        /// Ustvari poročilo v podatkovni bazi
        /// </summary>
        public void UstvariPorocilo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("porocilo_dodaj", povezava);

            ukaz.Parameters.Add(new SqlParameter("@akcijeresevalcev_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@IdPacient_porocila", SqlDbType.Int));
            //ukaz.Parameters.Add(new SqlParameter("@DatumObiska", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@opisdogodka_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@stanjepacientaobprispetju_porocila", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@stanjepacientaobprispetjuvbolnisnico_porocila", SqlDbType.NVarChar));

            ukaz.Parameters["@akcijeresevalcev_porocila"].Value = this.AkcijeReševalcev;
            ukaz.Parameters["@IdPacient_porocila"].Value = this.Pacient.IdPacienta;
         //   ukaz.Parameters["@DatumObiska"].Value = this.DatumObiska;
            ukaz.Parameters["@opisdogodka_porocila"].Value = this.OpisDogodka;
            ukaz.Parameters["@stanjepacientaobprispetju_porocila"].Value = this.StanjePacientaObPrispetju;
            ukaz.Parameters["@stanjepacientaobprispetjuvbolnisnico_porocila"].Value = this.StanjePacientaObPrispetjuVBolnišnico;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
         
            Bralec.Read();
            this.ŠtevilkaPoročila = Convert.ToInt32(Bralec[0]);

            Vožnja tst = new Vožnja();
            tst.ČasDogodka = this.ČasDogodka;
            tst.ČasKlicanjaReševalcev = this.ČasKlicanjaReševalcev;
            tst.ČasPrispetjaReševalcev = this.ČasPrispetjaReševalcev;
            tst.ČasPrispetjaVBolnišnico = this.ČasPrispetjaVBolnišnico;
            tst.Naslov = this.Naslov;
            int id_voznje=tst.Ustvari();
            Vožnja.VP(this.ŠtevilkaPoročila, id_voznje);
           foreach(Zaposleni z in this.Ekipa){
               Vožnja.VZ(z.IdZaposleni, id_voznje);
           }

            
        }
        ///// <summary>
        ///// Metoda poišče poročila v podatkovni bazi glede na podanega avtorja(njegov emšo)
        ///// </summary>
        ///// <returns>Vrne polje poročil napolnjen z poročili, ki jih je avtor napisal</returns>
        //public static Poročilo[] VrniPorocilaPoAvtorju(Zaposleni Pacient)
        //{
        //    SqlConnection povezava = new SqlConnection(PotPovezave);

        //    SqlCommand ukaz = new SqlCommand("VrniPorocilaPoAvtorju", povezava);
        //    ukaz.CommandType = CommandType.StoredProcedure;
        //    povezava.Open();
        //    SqlDataReader Bralec = ukaz.ExecuteReader();

        //    List<Poročilo> seznam = new List<Poročilo>();

        //    while (Bralec.Read())
        //    {
        //        Poročilo tmp = new Poročilo();
        //        if (Zaposleni.VrniPoEmšo((string)Bralec["Pacient"]).EMŠO == Pacient.EMŠO)
        //        {
        //            tmp.AkcijeReševalcev = (string)Bralec["AkcijeReševalcev"];
        //            tmp.Pacient = Zaposleni.VrniPoEmšo((string)Bralec["Pacient"]);
        //            tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
        //            tmp.OpisDogodka = (string)Bralec["OpisDogodka"];
        //            tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
        //            tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnišnico"];
        //            tmp.ŠtevilkaPoročila = (int)Bralec["ŠtevilkaPoročila"];
        //            seznam.Add(tmp);
        //        }
        //    }

        //    Poročilo[] ds = seznam.ToArray();
        //    povezava.Close();
        //    return ds;
        //}

      

        /// <summary>
        /// Vrne poročilo iz podatkovne baze glede na ID številko poročila
        /// </summary>
        /// <param name="StevilkaPorocila">ID številka poročila</param>
        public static IPNMP.Poročilo VrniPorociloPoID(int StevilkaPorocila)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("porocilo_vrni", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_porocila", SqlDbType.Int));
            ukaz.Parameters["@id_porocila"].Value = StevilkaPorocila;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Bralec.Read();
           
                Poročilo tmp = new Poročilo();
                tmp.AkcijeReševalcev = (string)Bralec["AkcijeResevalcev"];
               // tmp.Pacient = Zaposleni.VrniPoEmšo((string)Bralec["Pacient"]);
                //tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.OpisDogodka = (string)Bralec["OpisDogodka"];

                tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnisnico"];
                tmp.ŠtevilkaPoročila = (int)Bralec["ID"];
                tmp.Pacient = Pacient.VrniPoIdPacient((int)Bralec["idPacient"]);
                tmp.Ekipa = Zaposleni.VrniZaposlenePoIdPoročila((int)Bralec["ID"]);
                tmp.Naslov = Poročilo.VrniVoznjoPoIdPorocila((int)Bralec["ID"]).Naslov;
                tmp.ČasDogodka = Poročilo.VrniVoznjoPoIdPorocila((int)Bralec["ID"]).ČasDogodka;
          
            return tmp;
        }

        /// <summary>
        /// Vrne vsa poročila iz baze glede na pacienta
        /// </summary>
        public static IPNMP.Poročilo[] VrniPorocilaPoPacientu(Pacient pacient)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("porocilo_vrnipoidpacient", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_pacient", SqlDbType.Int));
            ukaz.Parameters["@id_pacient"].Value = pacient.IdPacienta;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
         

            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                tmp.AkcijeReševalcev = (string)Bralec["AkcijeResevalcev"];
                //tmp.Pacient = Zaposleni.VrniPoEmšo((string)Bralec["Pacient"]);
                //    tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.OpisDogodka = (string)Bralec["OpisDogodka"];

                tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnisnico"];
                tmp.ŠtevilkaPoročila = (int)Bralec["ID"];
                tmp.Pacient = Pacient.VrniPoIdPacient((int)Bralec["idPacient"]);
                tmp.Ekipa = Zaposleni.VrniZaposlenePoIdPoročila((int)Bralec["ID"]);
                tmp.Naslov = Poročilo.VrniVoznjoPoIdPorocila((int)Bralec["ID"]).Naslov;
                tmp.ČasDogodka = Poročilo.VrniVoznjoPoIdPorocila((int)Bralec["ID"]).ČasDogodka;
                seznam.Add(tmp);
            }

            Poročilo[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        public static IPNMP.Poročilo[] VrniPorocilaPoZaposlenem(Zaposleni zaposleni)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("porocilo_vrnipoidzaposleni", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_zaposleni", SqlDbType.Int));
            ukaz.Parameters["@id_zaposleni"].Value = zaposleni.IdZaposleni;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();


            List<Poročilo> seznam = new List<Poročilo>();

            while (Bralec.Read())
            {
                Poročilo tmp = new Poročilo();
                tmp.AkcijeReševalcev = (string)Bralec["AkcijeResevalcev"];
                //tmp.Pacient = Zaposleni.VrniPoEmšo((string)Bralec["Pacient"]);
                //    tmp.DatumObiska = (DateTime)Bralec["DatumObiska"];
                tmp.OpisDogodka = (string)Bralec["OpisDogodka"];

                tmp.StanjePacientaObPrispetju = (string)Bralec["StanjePacientaObPrispetju"];
                tmp.StanjePacientaObPrispetjuVBolnišnico = (string)Bralec["StanjePacientaObPrispetjuVBolnisnico"];
                tmp.ŠtevilkaPoročila = (int)Bralec["ID"];
                tmp.Pacient = Pacient.VrniPoIdPacient((int)Bralec["idPacient"]);
                tmp.Ekipa = Zaposleni.VrniZaposlenePoIdPoročila((int)Bralec["ID"]);
                tmp.Naslov = Poročilo.VrniVoznjoPoIdPorocila((int)Bralec["ID"]).Naslov;
                tmp.ČasDogodka = Poročilo.VrniVoznjoPoIdPorocila((int)Bralec["ID"]).ČasDogodka;
                seznam.Add(tmp);
            }

            Poročilo[] ds = seznam.ToArray();
            povezava.Close();
            return ds;
        }

        public DateTime ČasDogodka
        {
            get;
            set;
        }

        public IPNMP.Zaposleni[] Ekipa
        {
            get;
            set;
        }

        public static Vožnja VrniVoznjoPoIdPorocila(int idporocila)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("voznja_vrniPoIDPorocilo", povezava);
            ukaz.Parameters.Add(new SqlParameter("@id_porocilo", SqlDbType.Int));
            ukaz.Parameters["@id_porocilo"].Value = idporocila;
            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            SqlDataReader Bralec = ukaz.ExecuteReader();
            Bralec.Read();

            Vožnja v = new Vožnja();
           v.ČasDogodka = (DateTime)Bralec["casdogodka"];
           v.Naslov = Naslov.VrniNaslov((int)Bralec["Idnaslov"]);

           return v;
        }

        public Naslov Naslov
        {
            get;
            set;
        }

        public DateTime ČasKlicanjaReševalcev
        {
            get;
            set;
        }

        public DateTime ČasPrispetjaReševalcev
        {
            get;
            set;
        }

        public DateTime ČasPrispetjaVBolnišnico
        {
            get;
            set;
        }

    }
}
