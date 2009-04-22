using System;
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
            

        }
        public String Ime { set; get; }
        public String Priimek { set; get; }
        public int EMŠO { set; get; }
        public DateTime DatumRojstva { set; get; }

        public String Spol { set; get; }




        public void UstvariOsebo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("UstvariOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = this.Ime;
            ukaz.Parameters["@Priimek"].Value = this.Priimek;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
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

        public void PosodobiOsebo()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("PosodobiOsebo", povezava);

            ukaz.Parameters.Add(new SqlParameter("@Ime", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@Priimek", SqlDbType.NVarChar, 255));
            ukaz.Parameters.Add(new SqlParameter("@EMŠO", SqlDbType.Int));
            ukaz.Parameters.Add(new SqlParameter("@DatumRojstva", SqlDbType.DateTime));
            ukaz.Parameters.Add(new SqlParameter("@Spol", SqlDbType.NVarChar, 255));

            ukaz.Parameters["@Ime"].Value = this.Ime;
            ukaz.Parameters["@Priimek"].Value = this.Priimek;
            ukaz.Parameters["@EMŠO"].Value = this.EMŠO;
            ukaz.Parameters["@DatumRojstva"].Value = this.DatumRojstva;
            ukaz.Parameters["@Spol"].Value = this.Spol;

            ukaz.CommandType = CommandType.StoredProcedure;
            povezava.Open();
            ukaz.ExecuteNonQuery();
            povezava.Close();



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

        /// <param name="EMSO">številka EMŠO</param>
        public Pacient(int EMSO)
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
        public Kartoteka[] KartotekaPacienta { set; get; }
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
        public Pacient UstvariPacient()
        {
            throw new System.NotImplementedException();
        }

        /// <param name="ZZZS">številka ZZZS</param>
        public void IzbrisiPacient(int ZZZS)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Vrne vse paciente iz podatkovne baze
        /// </summary>
        public Pacient VrniVsePacient()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Posodobi podatke pacienta v podatkovni bazi
        /// </summary>
        public void PosodobiPacient()
        {
            throw new System.NotImplementedException();
        }


    }
    /// <summary>
    /// Karoteka obiskov pri zdravniku
    /// </summary>
    public class Kartoteka
    {

        public IPNMP.Terapija[] Terapije
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IPNMP.Diagnoza[] Diagnoze
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public IPNMP.Preiskava[] Preiskave
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
    /// <summary>
    /// Poročilo ob prispetju reševalnega vozila
    /// </summary>
    public class Poročilo : Vožnja
    {
        public String OpisDogodka { set; get; }
        public String StanjePacientaObPrispetju { set; get; }
        public String AkcijeReševalcev { set; get; }
        public String StanjePacientaObPrispetjuVBolnišnico { set; get; }

        public void VrniPorocilopoPacientu()
        {
            throw new System.NotImplementedException();
        }

        public void VrniPorocilopoZaposlenem()
        {
            throw new System.NotImplementedException();
        }
    }
    public class Zaposleni : Oseba
    {
        public DateTime DatumZaposlitve { set; get; }
        public String TipZaposlenega { set; get; }

        public int Specializacija
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Ekipa Ekipa
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Vožnja Vožnja
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public DataSet VrniVseZaposlene()
        {
            SqlConnection povezava = new SqlConnection(PotPovezave);

            SqlCommand ukaz = new SqlCommand("VrniVseZaposlene", povezava);
            ukaz.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(ukaz);
            DataSet ds = new DataSet();

            da.Fill(ds, "Osebe");
            povezava.Close();

            return ds;

        }

        public void UstvariZaposlenega()
        {
            throw new System.NotImplementedException();
        }

        public void IzbrisiZaposlenega()
        {
            throw new System.NotImplementedException();
        }

        public void PosodobiZaposlenega()
        {
            throw new System.NotImplementedException();
        }

        public Zaposleni VrniVsepoTipu()
        {
            throw new System.NotImplementedException();
        }

    }
    //Morebitni dodatki:
    //Šifranti naselij(poštnih številk, krajev)
    //Šifranti bolezni in simptomov http://www.who.int/classifications/icd/en/
}

