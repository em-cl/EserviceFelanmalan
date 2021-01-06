namespace HV2020.Eservice.Klient.MVC.Models.Emil
{
    public partial class DataTransferModels
    {
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

            public string Enhet { get; set; }

            public string Verksamhet { get; set; }
        }

    }

}
