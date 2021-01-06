using System;

namespace HV2020.Eservice.Klient.MVC.Models.Emil.DataModels
{
    public partial class DataTransferModels
    {
        public class GranskadData
        {
            public GranskadData(int dokumentNr, DateTime? datumGranskad)
            {
                DokumentNr = dokumentNr;
                DatumGranskad = datumGranskad;
            }


            public int DokumentNr { get; set; }

            public DateTime? DatumGranskad { get; set; }

            public int SynpunktID { get; set; }

            public int? LoginID { get; set; }

            public SynpunktData SynpunktData { get; set; }
        }

    }

}
