using HV2020.Eservice.WCF.Felanmalan.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HV2020.Eservice.WCF.Felanmalan
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class FelanmalanService : IFelanmalanService
    {
        public DataSender DataTransfer()
        {
            DataSender bigOBJ = new DataSender();
            
        //public HashSet<AdressData> adresser { get; set; }
            bigOBJ.adresser = ReadAllAdress();
        //public HashSet<GranskadData> granskade { get; set; }
            bigOBJ.granskade = ReadAllGranskad();
        //public HashSet<OrtData> orter { get; set; }
            bigOBJ.orter = ReadAllOrt();
        //public HashSet<PersonData> personer { get; set; }
            bigOBJ.personer = ReadAllPerson();
        //public HashSet<PersonTypData> personTyper { get; set; }
            bigOBJ.personTyper = ReadAllPersonTyp();
        //public HashSet<SynpunktData> synpunkter { get; set; }
            bigOBJ.synpunkter = ReadAllSynpunkt();
        //public HashSet<TelefonData> telefoner { get; set; }
            bigOBJ.telefoner = ReadAllTelefon();
        //public HashSet<VerksamhetData> verksamheter { get; set; }
            bigOBJ.verksamheter = ReadAllVerksamhet();
            return bigOBJ;
        }
        public void CreateAdress(int postnummer, string gata, int personID)
        {
            throw new NotImplementedException();
        }

        public void CreateGranskad(DateTime? datumGranskad, int synpunktID, int? LoginID)
        {
            throw new NotImplementedException();
        }

        public void CreateOrt(int postnummer, string ort)
        {
            //Tvätta data
            Whitelist start = new Whitelist();
            ort = start.CleanInput(ort);

            //TODO kontrollera Längd och innehåll på postnummer

            if (postnummer != null && ort != null) {
                //spara och överför
                using (AnsokanModel db = new AnsokanModel()) {
                    Ort result = new Ort();
                    if (db.Ort.Find(postnummer) == null) {

                        result.Postnummer = postnummer;
                        result.Ort1 = ort;
                        result.Adress = (Adress)db.Adress.Where(e => e.Postnummer == postnummer);

                        db.Ort.Add(result);
                        db.SaveChanges();
                    }
                    else {
                        //TODO update
                    }
                }
            }

            throw new NotImplementedException();
        }

        public void CreatePerson(string epost)
        {
            throw new NotImplementedException();
        }

        public void CreatePersonTyp(int personID, bool personTyp)
        {

        }

        public void CreateSynpunkt(string enhet, DateTime datumMottagit, string synpunkText, short avseende, int personID)
        {
            throw new NotImplementedException();
        }

        public void CreateTelefon(int telefonnummer, int personID)
        {
            //Tvätta data
            Whitelist start = new Whitelist();
            
            if (telefonnummer != null && personID!= null) {
                //spara och överför via objekt.
                Telefon result = new Telefon();
                using (AnsokanModel db = new AnsokanModel()) {

                    if (db.Telefon.Find(telefonnummer) == null) {
                        result.Telefonnummer = telefonnummer;
                        result.PersonID = personID;
                    }
                    else {
                        //TODO Update
                    }
                    db.Telefon.Add(result);
                    db.SaveChanges();
                }
            }
        }

        public void CreateVerksamhet(string enhet, string verksamhet)
        {
            //Tvätta data
            Whitelist start = new Whitelist();
            enhet = start.CleanInput(enhet);
            verksamhet = start.CleanInput(verksamhet);

            if (enhet != null && verksamhet != null) {
                //spara och överför via objekt.
                Verksamhet result = new Verksamhet();

                using (AnsokanModel db = new AnsokanModel()) {
                    if (db.Verksamhet.Find(enhet) == null) {
                        result.Enhet = enhet;
                        result.Verksamhet1 = verksamhet;
                    }
                    else {
                        //TODO Update
                    }
                    db.Verksamhet.Add(result);
                    db.SaveChanges();
                }
            }
        }

        public HashSet<AdressData> ReadAllAdress()
        {
            //Ett HashSet är en Lista med unika element om man passar ett objekt anpassar sig listans storlek efter objektet.
            HashSet<AdressData> resultat = new HashSet<AdressData>();
            using (AnsokanModel db = new AnsokanModel()) {
                var datacollektion = db.Adress.OrderBy(e=>e.Postnummer).ToList();

                datacollektion.ForEach(e => resultat.Add(item: new AdressData(e.Postnummer, e.Gata, e.PersonID)));
            }
            return resultat;
        }

        public HashSet<GranskadData> ReadAllGranskad()
        {

            HashSet<GranskadData> resultat = new HashSet<GranskadData>();
            using (AnsokanModel db = new AnsokanModel()) {
                var datacollektion = db.Granskad.OrderByDescending(e=>e.DatumGranskad).ToList();
                datacollektion.ForEach(e => resultat.Add(item: new GranskadData(e.DokumentNr, e.DatumGranskad)));
            }
            return resultat;
        }

        public HashSet<OrtData> ReadAllOrt()
        {
            HashSet<OrtData> resultat = new HashSet<OrtData>();
            using (AnsokanModel db = new AnsokanModel()) {
                var datacollektion = db.Ort.OrderBy(e=>e.Postnummer).ToList();
                datacollektion.ForEach(e => resultat.Add(item: new OrtData(e.Postnummer, e.Ort1)));
            }
            return resultat;
        }

        public HashSet<PersonData> ReadAllPerson()
        {
            HashSet<PersonData> resultat = new HashSet<PersonData>();
            using (AnsokanModel db = new AnsokanModel()) {
                var datacollektion = db.Person.OrderBy(e=>e.Epost).ToList();

                datacollektion.ForEach(e => resultat.Add(item: new PersonData(e.PersonID, e.Epost)));
            }
            return resultat;
        }

        public HashSet<PersonTypData> ReadAllPersonTyp()
        {
            HashSet<PersonTypData> resultat = new HashSet<PersonTypData>();
            using (AnsokanModel db = new AnsokanModel()) {
                var datacollektion = db.PersonTyp.OrderBy(e=>e.PersonID).ToList();

                datacollektion.ForEach(e => resultat.Add(item: new PersonTypData(e.PersonID, e.PersonTyp1)));
            }
            return resultat;

        }

        public HashSet<SynpunktData> ReadAllSynpunkt()
        {
            HashSet<SynpunktData> resultat = new HashSet<SynpunktData>();
            using (AnsokanModel db = new AnsokanModel()) {
                var datacollektion = db.Synpunkt.OrderBy(e=>e.SynpunktID).ToList();
                datacollektion.ForEach(e => resultat.Add(item: new SynpunktData(e.SynpunktID, e.Enhet, e.Avseende, e.DatumMottagit, e.SynpunkText, e.PersonID)));
            }
            return resultat;
        }

        public HashSet<TelefonData> ReadAllTelefon()
        {
            HashSet<TelefonData> resultat = new HashSet<TelefonData>();
            using (AnsokanModel db = new AnsokanModel()) {
                var datacollektion = db.Telefon.OrderBy(e=>e.Telefonnummer).ToList();

                datacollektion.ForEach(e => resultat.Add(item: new TelefonData(e.Telefonnummer, e.PersonID)));
            }
            return resultat;
        }

        public HashSet<VerksamhetData> ReadAllVerksamhet()
        {
            HashSet<VerksamhetData> resultat = new HashSet<VerksamhetData>();
            using (AnsokanModel db = new AnsokanModel()) {
                var datacollektion = db.Verksamhet.OrderBy(e=>e.Enhet).ToList();

                datacollektion.ForEach(e => resultat.Add(new VerksamhetData(e.Enhet, e.Verksamhet1)));
            }
            return resultat;
        }
    }
}
