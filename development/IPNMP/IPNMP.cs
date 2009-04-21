using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IPNMP
{

    public class Oseba
    {
        public String Ime { set; get; }
        public String Priimek { set; get; }
        public int EMŠO { set; get; }
        public DateTime DatumRojstva { set; get; }

        public String Spol { set; get; }


       
        
    }
    /// <summary>
    /// Podatki o pacientu
    /// </summary>
    public class Pacient : Oseba
    {
     
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

        public Kartoteka Kartoteka
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


    }
    /// <summary>
    /// Karoteka obiskov pri zdravniku
    /// </summary>
    public class Kartoteka
    {

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

        public Kartoteka Kartoteka
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

    }
    //Morebitni dodatki:
    //Šifranti naselij(poštnih številk, krajev)
    //Šifranti bolezni in simptomov http://www.who.int/classifications/icd/en/
}

