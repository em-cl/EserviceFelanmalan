namespace HV2020.Eservice.Klient.MVC.Models.Emil
{
    public partial class DataTransferModels
    {
        public class OrtData
        {
            public OrtData(int postnummer, string ort)
            {
                this.Postnummer = postnummer;
                this.Ort = ort;
            }

            public int Postnummer { get; set; }

            public string Ort { get; set; }

            public AdressData AdressData { get; set; }
        }

    }

}
