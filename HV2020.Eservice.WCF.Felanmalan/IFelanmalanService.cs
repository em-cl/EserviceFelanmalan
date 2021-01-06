using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HV2020.Eservice.WCF.Felanmalan
{
    [ServiceContract]
    public interface IFelanmalanService
    {
        //Create-----------
        [OperationContract]
        void CreatePerson (string epost);
        [OperationContract]
        void CreateTelefon(int telefonnummer, int personID);
        [OperationContract]
        void CreateAdress(int postnummer, string gata, int personID);
        [OperationContract]
        void CreateOrt(int postnummer, string ort);
        [OperationContract]
        void CreatePersonTyp(int personID, bool personTyp);
        [OperationContract]
        void CreateSynpunkt(string enhet, DateTime datumMottagit, string synpunkText, short avseende, int personID );
        [OperationContract]
        void CreateVerksamhet(string enhet, string verksamhet);
        [OperationContract]
        void CreateGranskad(DateTime? datumGranskad, int synpunktID, int? LoginID);
        //ReadAll----------------------------------------------------------------------
        [OperationContract]
        HashSet<PersonData> ReadAllPerson();
        [OperationContract]
        HashSet<TelefonData> ReadAllTelefon();
        [OperationContract]
        HashSet<AdressData> ReadAllAdress();
        [OperationContract]
        HashSet<OrtData> ReadAllOrt();
        [OperationContract]
        HashSet<PersonTypData> ReadAllPersonTyp();
        [OperationContract]
        HashSet<SynpunktData> ReadAllSynpunkt();
        [OperationContract]
        HashSet<VerksamhetData> ReadAllVerksamhet();
        [OperationContract]
        HashSet<GranskadData> ReadAllGranskad();
        //Update--------------------------------------------------------------------
    }

    [DataContract]
    public class PersonData
    {   //Objekt med fjärran nycklar
        private HashSet<AdressData> adresser = new HashSet<AdressData>();
        private HashSet<SynpunktData> synpunkter = new HashSet<SynpunktData>();
        private HashSet<TelefonData> telefoner = new HashSet<TelefonData>();

        public PersonData(int personID, string epost)
        {
            PersonID = personID;
            Epost = epost;
        }

        [DataMember]
        public int PersonID { get; set; }
        [DataMember]
        public string Epost { get; set; }
        [DataMember]
        public PersonTyp PersonTyp { get; set; }
        [DataMember]
        public HashSet<AdressData> Adresserer
        {
            get { return adresser; }
            set { this.adresser = value; }
        }
        [DataMember]
        public HashSet<SynpunktData> Synpunkter
        {
            get { return synpunkter; }
            set { this.synpunkter = value; }
        }
        [DataMember]
        public HashSet<TelefonData> Telefoner 
        {
            get { return telefoner; }
            set { this.telefoner = value; }
        }
    }
    [DataContract]
    public class TelefonData
    {
        public TelefonData(int telefonnummer, int personID)
        {
            Telefonnummer = telefonnummer;
            PersonID = personID;
        }

        [DataMember]
        public int Telefonnummer { get; set; }
        [DataMember]
        public int PersonID { get; set; }
        [DataMember]//ägare
        public PersonData persondata { get; set; }
    }
    [DataContract]
    public class AdressData
    {
        public AdressData()
        {

        }
        public AdressData(int postnummer, string gata, int personID)
        {
            Postnummer = postnummer;
            Gata = gata;
            PersonID = personID;
        }

        [DataMember]
        public int Postnummer { get; set; }
        [DataMember]
        public string Gata { get; set; }
        [DataMember]
        public int PersonID { get; set; }
        [DataMember]//ägare
        public Person PersonData { get; set; }
        [DataMember]//uppdelad
        public Ort OrtData { get; set; }
    }
    [DataContract]
    public class OrtData
    {
        private Adress match;

        public OrtData(int postnummer, string ort)
        {
            this.Postnummer = postnummer;
            this.Ort = ort;
        }

        public OrtData(int postnummer, string ort, Adress match) : this(postnummer, ort)
        {
            this.match = match;
        }

        [DataMember]
        public int Postnummer { get; set; }
        [DataMember]
        public string Ort { get; set; }
        [DataMember]//uppdelad
        public AdressData AdressData { get; set; }
    }
    [DataContract]
    public class PersonTypData
    {
        public PersonTypData(int personID, bool personTyp1)
        {
            PersonID = personID;
            PersonTyp1 = personTyp1;
        }

        public PersonTypData(int telefonnummer, int personID)
        {
            Telefonnummer = telefonnummer;
            PersonID = personID;
        }

        [DataMember]
        public int PersonID { get; set; }
        [DataMember]
        public bool PersonTyp { get; set; }
        [DataMember]//ägare
        public PersonData PersonData { get; set; }
        public bool PersonTyp1 { get; }
        public int Telefonnummer { get; }
    }
    [DataContract]
    public class SynpunktData
    {
        private HashSet<GranskadData> granskade = new HashSet<GranskadData>();

        public SynpunktData(int synpunktID, string enhet, short avseende, DateTime datumMottagit, string synpunkText, int personID)
        {
            SynpunktID = synpunktID;
            Enhet = enhet;
            Avseende = avseende;
            DatumMottagit = datumMottagit;
            SynpunkText = synpunkText;
            PersonID = personID;
        }

        [DataMember]
        public int SynpunktID { get; set; }
        [DataMember]
        public string Enhet { get; set; }
        [DataMember]
        public VerksamhetData verksamhetData { get; set; }
        [DataMember]
        public DateTime DatumMottagit { get; set; }
        [DataMember]
        public string SynpunkText { get; set; }
        [DataMember]
        public short Avseende { get; set; }
        [DataMember]
        public int PersonID { get; set; }
        [DataMember]//ägare
        public PersonData PersonData { get; set; }
        [DataMember]
        public HashSet<GranskadData> Granskad
        {
            get { return granskade; }
            set { this.granskade = value; }
        }
    }
    [DataContract]
    public class VerksamhetData
    {
        public VerksamhetData()
        {

        }
        public VerksamhetData(string enhet, string verksamhet)
        {
            this.Enhet = enhet;
            this.Verksamhet = verksamhet;
        }
        [DataMember]
        public string Enhet { get; set; }
        [DataMember]
        public string Verksamhet { get; set; }
    }
    [DataContract]
    public class GranskadData
    {
        public GranskadData(int dokumentNr, DateTime? datumGranskad)
        {
            DokumentNr = dokumentNr;
            DatumGranskad = datumGranskad;
        }

        [DataMember]
        public int DokumentNr { get; set; }
        [DataMember]
        public DateTime? DatumGranskad { get; set; }
        [DataMember]
        public int SynpunktID { get; set; }
        [DataMember]
        public int? LoginID { get; set; }
        [DataMember]
        public SynpunktData SynpunktData { get; set; }
    }

}
