using HV2020.Eservice.Klient.MVC.Models.Emil.DataModels;
using System;
namespace HV2020.Eservice.Klient.MVC.Models.Emil.Utilities
{

    public partial class HelperClasses
    {
        public class Keys
        {
            public DataTransferModels.DataReciver FetchKeys()
            {

                FelanmalanServiceReference.DataSender klient = new FelanmalanServiceReference.DataSender();
                DataTransferModels.DataReciver dataListedClasses = new DataTransferModels.DataReciver();
                //Person
                foreach (var item in klient.personer) {
                    dataListedClasses.personer.Add(
                        new DataTransferModels.PersonData(
                            item.PersonID, item.Epost));

                }
                //Telefoner

                foreach (var item in klient.telefoner) {
                    dataListedClasses.telefoner.Add(
                         new DataTransferModels.TelefonData(
                             item.Telefonnummer, item.PersonID));
                    //person FN
                    foreach (var person in klient.personer) {
                        if (person.PersonID == item.PersonID) {
                            item.persondata = person;
                        }
                    }
                }
                //Adress
                foreach (var item in klient.adresser) {
                    dataListedClasses.adresser.Add(
                        new DataTransferModels.AdressData(
                            item.Postnummer, item.Gata, item.PersonID));
                    //person FN
                    //----------------------Fail---------------
                    //foreach (var person in klient.personer) {
                    //    if (person.PersonID == item.PersonID) {
                    //        //item.PersonData = person;
                    //
                    //    }
                    //}
                }
                //Orter
                foreach (var item in klient.orter) {
                    dataListedClasses.orter.Add(
                        new DataTransferModels.OrtData(
                            item.Postnummer, item.Ort));
                    //adress Uppdelad
                    foreach (var adress in klient.adresser) {
                        if (adress.Postnummer == item.Postnummer) {
                            item.AdressData = adress;
                        }
                    }
                }
                //Persontyper
                foreach (var item in klient.personTyper) {
                    dataListedClasses.personTyper.Add(
                        new DataTransferModels.PersonTypData(
                            item.PersonID, item.PersonTyp));
                    //person FN
                    foreach (var person in klient.personer) {
                        if (person.PersonID == item.PersonID) {
                            item.PersonData = person;
                        }
                    }
                }
                //Verksamheter
                foreach (var item in klient.verksamheter) {
                    dataListedClasses.verksamheter.Add(
                         new DataTransferModels.VerksamhetData(
                             item.Enhet, item.Verksamhet));
                }
                //Synpunkter
                foreach (var item in klient.synpunkter) {
                    dataListedClasses.synpunkter.Add(
                        new DataTransferModels.SynpunktData(
                            item.SynpunktID, item.Enhet, item.Avseende, item.DatumMottagit, item.SynpunkText, item.PersonID));
                    //person FN
                    foreach (var person in klient.personer) {
                        if (person.PersonID == item.PersonID) {
                            item.PersonData = person;
                        }
                    }
                    //Verksamhet Uppdelad
                    foreach (var verksamhet in klient.verksamheter) {
                        if (verksamhet.Enhet == item.Enhet) {
                            item.verksamhetData = verksamhet;
                        }
                    }
                }
                //Granskade
                foreach (var item in klient.granskade) {
                    dataListedClasses.granskade.Add(
                        new DataTransferModels.GranskadData(
                            item.DokumentNr, item.DatumGranskad));

                }
                return dataListedClasses;
            }

        }
    }
}