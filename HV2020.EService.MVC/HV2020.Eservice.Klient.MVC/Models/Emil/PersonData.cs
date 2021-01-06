using System.Collections.Generic;

namespace HV2020.Eservice.Klient.MVC.Models.Emil
{
    public partial class DataTransferModels
    {
        public class PersonData
        {   
            //Objekt med fjärran nycklar
            private HashSet<AdressData> adresser = new HashSet<AdressData>();
            private HashSet<SynpunktData> synpunkter = new HashSet<SynpunktData>();
            private HashSet<TelefonData> telefoner = new HashSet<TelefonData>();

            public PersonData(int personID, string epost)
            {
                PersonID = personID;
                Epost = epost;
            }


            public int PersonID { get; set; }

            public string Epost { get; set; }

            public PersonTypData PersonTyp { get; set; }

            public HashSet<AdressData> Adresser
            {
                get { return adresser; }
                set { this.adresser = value; }
            }

            public HashSet<SynpunktData> Synpunkter
            {
                get { return synpunkter; }
                set { this.synpunkter = value; }
            }

            public HashSet<TelefonData> Telefoner
            {
                get { return telefoner; }
                set { this.telefoner = value; }
            }
        }

    }
}
