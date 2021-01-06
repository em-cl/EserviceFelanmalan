namespace HV2020.Eservice.Klient.MVC.Models.Emil.DataModels
{
    public partial class DataTransferModels
    {
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


            public int Postnummer { get; set; }

            public string Gata { get; set; }

            public int PersonID { get; set; }

            public PersonData PersonData { get; set; }

            public OrtData OrtData { get; set; }
        }

    }

}
