namespace HV2020.Eservice.Klient.MVC.Models.Emil.DataModels
{
    public partial class DataTransferModels
    {
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


            public int PersonID { get; set; }

            public bool PersonTyp { get; set; }

            public PersonData PersonData { get; set; }
            public bool PersonTyp1 { get; }
            public int Telefonnummer { get; }
        }

    }

}
