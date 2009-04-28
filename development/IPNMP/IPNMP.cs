﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace IPNMP
{
   
       
        

    public class Oseba
    {
        protected string PotPovezave = "";
        /// <summary>
        /// Privzeti konstruktor, ob klicanju pobere parametre, za vzpostavitev povezave z bazo
        /// </summary>
        public Oseba()
        {
            PotPovezave = Properties.Settings.Default.ConnectionString;
        }
        /// <summary>
        /// Nastavi parametre osebe glede na vpisani emšo
        /// </summary>
        /// <param name="EMŠO"></param>
        public Oseba(int EMŠO)
        {
            PotPovezave = Properties.Settings.Default.ConnectionString;

            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("OsebaKonstruktor", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Oseba");
            povezava.Close();

            DataRow vrstica;
            vrstica = ds.Tables[0].Rows[0];
            this.Ime = vrstica["Ime"].ToString();
            this.Priimek = vrstica["Priimek"].ToString();
            this.EMŠO = EMŠO;
            this.DatumRojstva = (DateTime)vrstica["DatumRojstva"];
            Naslov naslov_osebe = new Naslov();
            String Naslovtext = vrstica["Naslov"].ToString();
            int i;
            for( i=0;i<=Naslovtext.Length;i++){
                if(Char.IsNumber(Naslovtext[i])){
                    break;
                }

            }
            naslov_osebe.Ulica = Naslovtext.Substring(0, i);
            naslov_osebe.HišnaŠtevilka = Naslovtext.Substring(i, Naslovtext.Length-i);
            this.Naslov = naslov_osebe;
            this.Spol = vrstica["Spol"].ToString();

        }
        public String Ime { set; get; }
        public String Priimek { set; get; }
        public int EMŠO { set; get; }
        public DateTime DatumRojstva { set; get; }

        public String Spol { set; get; }

        public Naslov Naslov
        {
            get;
            set;
        }




        /// <summary>
        /// Ustvari novo osebo v podatkovni bazi
        /// </summary>
        public void UstvariOsebo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Naslov", SqlDbType.NVarChar,255));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = this.Ime;
            ukaz.Parameters["@Priimek"].Value = this.Priimek;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@Naslov"].Value = this.Naslov;
            ukaz.Parameters["@DatumRojstva"].Value = this.DatumRojstva;
            ukaz.Parameters["@Spol"].Value = this.Spol;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();

        }

        /// <summary>
        /// Izbriše osebo iz podatkovne baze, glede na EMŠO
        /// </summary>
        /// <param name="EMŠO">Unikatna številka Osebe ,tipa int</param>
        public void IzbrisiOsebo(int EMŠO)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiOsebo", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();

        }

        /// <summary>
        /// Vrne vse osebe iz podatkovne baze
        /// </summary>
        /// <returns>Vrne napolnjen dataset</returns>
        public DataSet VrniVseOsebe()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseOsebe", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Osebe");
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Posodobi podatke za osebo v podatkovni bazi
        /// </summary>
        public void PosodobiOsebo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Naslov", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = this.Ime;
            ukaz.Parameters["@Priimek"].Value = this.Priimek;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@Naslov"].Value = this.Naslov;
            ukaz.Parameters["@DatumRojstva"].Value = this.DatumRojstva;
            ukaz.Parameters["@Spol"].Value = this.Spol;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();



        }

        /// <summary>
        /// Vrne osebo iz podatkovne baze glede na emšo
        /// </summary>
        public Oseba VrniOseboEmšo()
        {
            throw new System.NotImplementedException();
        }

       
        
    }
    /// <summary>
    /// Podatki o pacientu
    /// </summary>
    public class Pacient : Oseba
    {
        public Pacient()
        {
            throw new System.NotImplementedException();
        }

        /// <param name="ZZZS">Napolni objekt iz baze s pomočjo zzzs številke</param>
        public Pacient(int ZZZS)
        {
            throw new System.NotImplementedException();
        }
     
        /// <summary>
        /// Višina v centimetrih
        /// </summary>
        public int Višina { set; get; }
        /// <summary>
        /// Teža v gramih
        /// </summary>
        public int Teža { set; get; }
        public String KrvnaSkupina { set; get; }

        public int ZZZS
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IPNMP.Kartoteka Kartoteka
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Poročilo Poročilo
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
        /// Vrne paciente iz podatkovne baze glede na številko ZZZS
        /// </summary>
        public Pacient VrniPacientZZZS()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Ustvari nov vnos v podatkovno bazo
        /// </summary>
        public void UstvariPacient()
        {
       
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@KrvnaSkupina", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Teža", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Višina", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));

            ukaz.Parameters["@KrvnaSkupina"].Value = this.KrvnaSkupina;
            ukaz.Parameters["@Teža"].Value = this.Teža;
            ukaz.Parameters["@Višina"].Value = this.Višina;
            ukaz.Parameters["@ZZZS"].Value = this.ZZZS;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Izbriše pacienta iz podatkovne baze glede na njegovo številko ZZZS
        /// </summary>
        /// <param name="ZZZS">številka ZZZS</param>
        public void IzbrisiPacient(int ZZZS)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiPacient", povezava);
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));
            ukaz.Parameters["@ZZZS"].Value = ZZZS;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne vse paciente iz podatkovne baze
        /// </summary>
        public DataSet VrniVsePacient()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePacient", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Pacienti");
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Posodobi podatke pacienta v podatkovni bazi
        /// </summary>
        public void PosodobiPacient()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@KrvnaSkupina", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Teža", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Višina", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@ZZZS", SqlDbType.Int));

            ukaz.Parameters["@KrvnaSkupina"].Value = this.KrvnaSkupina;
            ukaz.Parameters["@Teža"].Value = this.Teža;
            ukaz.Parameters["@Višina"].Value = this.Višina;
            ukaz.Parameters["@ZZZS"].Value = this.ZZZS;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne starost v letih, izračunano s pomočjo datuma rojstva
        /// </summary>
        public String VrniStarost()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Vrne vse alergije ki jih pacient ima (spada pod Diagnoze)
        /// </summary>
        public DataSet VrniAlergije()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Vrne vse operacije, ki jih je pacient imel(spada pod zdravljenje)
        /// </summary>
        public DataSet VrniOperacije()
        {
            throw new System.NotImplementedException();
        }


    }
    /// <summary>
    /// Karoteka obiskov pri zdravniku
    /// </summary>
    public class Kartoteka
    {

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
        public void VrniKartoteko(Kartoteka ŠtevilkaKartoteke)
        {
            throw new System.NotImplementedException();
        }

        }
    /// <summary>
    /// Poročila ob prispetju reševalnega vozila
    /// </summary>
    public class Poročilo
    {
        protected string PotPovezave = "";

        public String OpisDogodka { set; get; }
        public String StanjePacientaObPrispetju { set; get; }
        public String AkcijeReševalcev { set; get; }
        public String StanjePacientaObPrispetjuVBolnišnico { set; get; }

        public DateTime Datum{            get;            set;        }

        public Poročilo()
        {
            PotPovezave = Properties.Settings.Default.ConnectionString;
        }
        public Zaposleni Avtor
        {
            get;
            set;
        }

        /// <summary>
        /// Vrne vsa poročila glede na datum kreacije poročila
        /// </summary>
        public DataSet VrniPorocilaPoDatumu()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaPoročilaPoDatumu", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            ukaz.Parameters.Add(new SqlParameter("@Datum", SqlDbType.Date));
            ukaz.Parameters["@Datum"].Value = this.Datum;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "PoročilaPoDatumu");
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Vrne vsa poročila iz podatkovne baze
        /// </summary>
        public DataSet VrniVsaPorocila()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaPoročila", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Poročila");
            povezava.Close();

            return ds;
        }

        /// <summary>
        /// Ustvari poročilo v podatkovni bazi
        /// </summary>
        public void UstvariPorocilo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariPorocilo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@AkcijeResevalcev", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Avtor", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@Datum", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@OpisDogotka", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@StanjePacientaObPrispetju", SqlDbType.NVarChar));
            ukaz.Parameters.Add(new SqlParameter("@StanjePacientaObPrispetjuVBolnisnico", SqlDbType.NVarChar));

            ukaz.Parameters["@AkcijeResevalcev"].Value = this.AkcijeReševalcev;
            ukaz.Parameters["@Avtor"].Value = this.Avtor.EMŠO;
            ukaz.Parameters["@Datum"].Value = this.Datum;
            ukaz.Parameters["@OpisDogotka"].Value = this.OpisDogodka;
            ukaz.Parameters["@StanjePacientaObPrispetju"].Value = this.StanjePacientaObPrispetju;
            ukaz.Parameters["@StanjePacientaObPrispetjuVBolnisnico"].Value = this.StanjePacientaObPrispetjuVBolnišnico;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }
        /// <summary>
        /// Metoda poišče poročila v podatkovni bazi glede na podanega avtorja(njegov emšo)
        /// </summary>
        /// <returns>Vrne Dataset napolnjen z poročili, ki jih je avtor napisal</returns>
        public DataSet VrniPorocilaPoAvtorju(Zaposleni Avtor)
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsaPoročilaPoAvtorju", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            ukaz.Parameters.Add(new SqlParameter("@Avtor", SqlDbType.Int));
            ukaz.Parameters["@Avtor"].Value = this.Avtor.EMŠO;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "PoročilaPoAvtorju");
            povezava.Close();

            return ds;
        }

        public int ŠtevilkaPoročila
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
    public class Zaposleni : Oseba
    {
        public Zaposleni()
        {
            throw new System.NotImplementedException();
        }

        /// <param name="EMŠO">Napolni objekt zaposleni iz baze, glede na podan emšo</param>
        public Zaposleni(string EMŠO)
        {
            throw new System.NotImplementedException();
        }
    
        public DateTime DatumZaposlitve { set; get; }
        public String TipZaposlenega { set; get; }

        public int Specializacija
        {
            get;
            set;
        }

        public Ekipa Ekipa
        {
            get;
            set;
        }

        public IPNMP.Poročilo[] Poročila
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
        /// Vrne vse zaposlene iz podatkovne baze
        /// </summary>
        public DataSet VrniVseZaposlene()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseZaposlene", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Zaposleni");
            povezava.Close();

            return ds;

        }

        /// <summary>
        /// Ustvari nov vnos v podatkovni bzi
        /// </summary>
        public void UstvariZaposlenega()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariZaposlenega", povezava);

            ukaz.Parameters.Add(new SqlParameter("@DatumZaposlitve", SqlDbType.DateTime));    
            ukaz.Parameters.Add(new SqlParameter("@Specializacija", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@TipZaposlenega", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Specializacija"].Value = this.Naslov;
            ukaz.Parameters["@DatumZaposlitve"].Value = this.DatumZaposlitve;
            ukaz.Parameters["@TipZaposlenega"].Value = this.TipZaposlenega;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Izbriše zaposlenega iz podatkovne baze
        /// </summary>
        public void IzbrisiZaposlenega()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);
            SqlCommand ukaz = new SqlCommand("IzbrisiZaposlenega", povezava);
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters["@EMŠO"].Value = EMŠO;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Posodobi podatke zaposlenega
        /// </summary>
        public void PosodobiZaposlenega()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiZaposlenega", povezava);

            ukaz.Parameters.Add(new SqlParameter("@DatumZaposlitve", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Specializacija", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@TipZaposlenega", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Specializacija"].Value = this.Naslov;
            ukaz.Parameters["@DatumZaposlitve"].Value = this.DatumZaposlitve;
            ukaz.Parameters["@TipZaposlenega"].Value = this.TipZaposlenega;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();
        }

        /// <summary>
        /// Vrne vse zaposlene glede na tip
        /// </summary>
        public DataSet VrniVsePoTipu(string TipZaposlenega)
        {
            PotPovezave = Properties.Settings.Default.ConnectionString;

            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVsePoTipuZaposlenega", povezava);
            ukaz.Parameters.Add(new SqlParameter("@TipZaposlenega", SqlDbType.NVarChar));
            ukaz.Parameters["@TipZaposlenega"].Value = TipZaposlenega;
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Zaposleni");
            povezava.Close();
            return ds;
        }

    }
    //Morebitni dodatki:
    //Šifranti naselij(poštnih številk, krajev)
    //Šifranti bolezni in simptomov http://www.who.int/classifications/icd/en/
}

