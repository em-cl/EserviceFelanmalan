using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HV2020.Eservice.Klient.MVC.Models.Emil.DataModels
{
    public partial class DataTransferModels
    {
        public partial class DataReciver
        {

            public HashSet<PersonData> personer { get; set; }

            public HashSet<TelefonData> telefoner { get; set; }

            public HashSet<PersonTypData> personTyper { get; set; }

            public HashSet<AdressData> adresser { get; set; }

            public HashSet<OrtData> orter { get; set; }

            public HashSet<VerksamhetData> verksamheter { get; set; }

            public HashSet<GranskadData> granskade { get; set; }

            public HashSet<SynpunktData> synpunkter { get; set; }

        }
    }
}