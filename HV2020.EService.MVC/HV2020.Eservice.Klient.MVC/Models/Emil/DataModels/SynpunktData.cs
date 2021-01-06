using System;
using System.Collections.Generic;

namespace HV2020.Eservice.Klient.MVC.Models.Emil
{
    public partial class DataTransferModels
    {
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


            public int SynpunktID { get; set; }

            public string Enhet { get; set; }

            public VerksamhetData verksamhetData { get; set; }

            public DateTime DatumMottagit { get; set; }

            public string SynpunkText { get; set; }

            public short Avseende { get; set; }

            public int PersonID { get; set; }

            public PersonData PersonData { get; set; }

            public HashSet<GranskadData> Granskad
            {
                get { return granskade; }
                set { this.granskade = value; }
            }
        }

    }

}
