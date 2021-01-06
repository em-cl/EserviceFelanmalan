namespace HV2020.Eservice.Klient.MVC.Models.Emil
{
    public partial class DataTransferModels
    {
        public class TelefonData
        {
            public TelefonData(int telefonnummer, int personID)
            {
                Telefonnummer = telefonnummer;
                PersonID = personID;
            }


            public int Telefonnummer { get; set; }

            public int PersonID { get; set; }

            public PersonData persondata { get; set; }
        }

    }

}
