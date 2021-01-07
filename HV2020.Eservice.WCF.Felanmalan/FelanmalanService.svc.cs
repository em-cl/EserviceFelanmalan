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
        //----------------------------Create----------------------------------
        public void CreateAdress(int postnummer, string gata, int personID)
        {

            //spara och överför via objekt.
            Whitelist start = new Whitelist();
            gata = start.CleanInput(gata);
            Adress result = new Adress();
            using (AnsokanModel db = new AnsokanModel()) {
                result.Postnummer = postnummer;
                result.Gata = gata;
                result.PersonID = personID;
                db.Adress.Add(result);
                db.SaveChanges();
            }
        }

        public void CreateGranskad(DateTime? datumGranskad, int synpunktID, int? LoginID)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                Granskad resultat = new Granskad();
                if (datumGranskad != null) {
                    resultat.DatumGranskad = datumGranskad;
                }
                if (LoginID != null) {
                    resultat.LoginID = LoginID;
                }
                resultat.SynpunktID = synpunktID;
                db.Granskad.Add(resultat);
                db.SaveChanges();
            }
        }

        public void CreateOrt(int postnummer, string ort)
        {
            //Tvätta string data
            Whitelist start = new Whitelist();
            ort = start.CleanInput(ort);

            if (ort != "") {
                //spara och överför
                using (AnsokanModel db = new AnsokanModel()) {
                    Ort result = new Ort();
                    if (db.Ort.Find(postnummer) == null) {
                        result.Postnummer = postnummer;
                        result.Ort1 = ort;
                        db.Ort.Add(result);
                        db.SaveChanges();
                    }
                    else {
                        UpdateOrt(postnummer, ort);
                    }
                }
            }
        }

        public void CreatePerson(string epost)
        {
            //Tvätta data
            Whitelist start = new Whitelist();
            epost = start.CleanInput(epost);

            //TODO kontrollera Längd och innehåll på postnummer

            if (epost != "") {
                //spara och överför
                using (AnsokanModel db = new AnsokanModel()) {
                    Person result = new Person();
                    result.Epost = epost;
                    db.Person.Add(result);
                    db.SaveChanges();
                }
            }
        }

        public void CreatePersonTyp(int personID, bool personTyp)
        {
            //spara och överför
            using (AnsokanModel db = new AnsokanModel()) {
                PersonTyp result = new PersonTyp();
                result.PersonID = personID;
                result.PersonTyp1 = personTyp;
                db.PersonTyp.Add(result);
                db.SaveChanges();
            }
        }

        public void CreateSynpunkt(string enhet, DateTime datumMottagit, string synpunkText, short avseende, int personID)
        {
            Whitelist cleaner = new Whitelist();
            enhet = cleaner.CleanInput(enhet);
            synpunkText = cleaner.CleanInput(synpunkText);
            Synpunkt resultat = new Synpunkt();
            using (AnsokanModel db = new AnsokanModel()) {
                resultat.Avseende = avseende;
                resultat.DatumMottagit = datumMottagit;
                resultat.Enhet = enhet;
                resultat.SynpunkText = synpunkText;
                resultat.PersonID = personID;
                db.Synpunkt.Add(resultat);
                db.SaveChanges();
            }
        }
        public void CreateTelefon(int telefonnummer, int personID)
        {

            //spara och överför via objekt.
            Telefon result = new Telefon();
            using (AnsokanModel db = new AnsokanModel()) {

                if (db.Telefon.Find(telefonnummer) == null) {
                    result.Telefonnummer = telefonnummer;
                    result.PersonID = personID;
                }
                else {
                    UpdateTelefon(telefonnummer, personID);
                }
                db.Telefon.Add(result);
                db.SaveChanges();
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
                        UpdateVerksamhet(enhet, verksamhet);
                    }
                    db.Verksamhet.Add(result);
                    db.SaveChanges();
                }
            }
        }
        
        //delete------------------------------------------------------------
        
        //Delete
        /// <summary>
        /// var osäker med sammansatta nycklar 
        /// så gjorde fält för att den garanterat 
        /// ska vara unik
        /// </summary>
        /// <param name="postnummer"></param>
        /// <param name="gata"></param>
        /// <param name="personID"></param>
        public void DeleteAdress(int postnummer, string gata, int personID)
        {
            Whitelist start = new Whitelist();
            gata = start.CleanInput(gata);
            //öppna databas
            using (AnsokanModel db = new AnsokanModel()) {
                //hitta med id från hiddenfield
                List<Adress> adresses = db.Adress.ToList();
                foreach (Adress item in adresses) {
                    if (item.Postnummer == postnummer && item.PersonID == personID && item.Gata == gata) {
                        db.Adress.Remove(item);
                        break;
                    }
                }
                //spara
                db.SaveChanges();
            }//stäng databas
        }

        public void DeleteGranskad(int dokumentNr)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                Granskad resultat = db.Granskad.Find(dokumentNr);
                db.Granskad.Remove(resultat);
                db.SaveChanges();
            }
        }

        public void DeleteOrt(int postnummer)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                Ort resultat = db.Ort.Find(postnummer);
                db.Ort.Remove(resultat);
                db.SaveChanges();
            }
        }

        public void DeletePerson(int personID)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                Person resultat = db.Person.Find(personID);
                if (resultat.Telefon == null && resultat.Synpunkt == null && resultat.Adress == null && resultat.PersonTyp == null) {
                    db.Person.Remove(resultat);
                } else {
                    foreach (var item in db.Telefon.ToList()) {
                        if (item.PersonID == resultat.PersonID) {
                        db.Telefon.Remove(item);
                        }
                    }
                    foreach (var item in db.Synpunkt.ToList()) {
                        if (item.PersonID == resultat.PersonID) {
                            db.Synpunkt.Remove(item);
                        } 
                    }
                    foreach (var item in db.Adress.ToList()) {
                        if(item.PersonID == resultat.PersonID) {
                            db.Adress.Remove(item);
                        }
                    }
                    if (null!=db.PersonTyp.Find(personID)) {
                        db.PersonTyp.Remove(db.PersonTyp.Find(personID));
                    }
                    db.Person.Remove(resultat);
                }
                    db.SaveChanges();
            }
        }

        public void DeletePersonTyp(int personID)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                PersonTyp resultat = db.PersonTyp.Find(personID);
                db.PersonTyp.Remove(resultat);
                db.SaveChanges();
            }
        }

        public void DeleteSynpunkt(int SynpunktsID)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                Synpunkt resultat = db.Synpunkt.Find(SynpunktsID);
                if (resultat.Granskad == null) {
                    db.Synpunkt.Remove(resultat);
                    db.SaveChanges();
                }
                else {
                    foreach (var item in db.Granskad.ToList()) {
                        if (item.SynpunktID == resultat.SynpunktID) {
                            DeleteGranskad(item.DokumentNr);
                        }
                    }
                    db.Synpunkt.Remove(resultat);
                    db.SaveChanges();
                }
            }
        }
        
        public void DeleteTelefon(int telefonnummer)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                Telefon resultat = db.Telefon.Find(telefonnummer);
                db.Telefon.Remove(resultat);
                db.SaveChanges();
            }
        }
        //lite lite osäker här med på unik därför dubbla parametrar
        public void DeleteVerksamhet(string enhet, string verksamhet)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                foreach (var item in db.Verksamhet.ToList()) {
                    if (item.Enhet == enhet && verksamhet == item.Verksamhet1) {
                        db.Verksamhet.Remove(item);
                    }
                    db.SaveChanges();
                }
            }
        }
        
        //----------------Read-----------------------
        public HashSet<AdressData> ReadAllAdress()
        {
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
        
        //----------------------Update----------------------------------------
        public void UpdateAdress(int postnummer, string gata, int personID)
        {
            Whitelist start = new Whitelist();
            gata = start.CleanInput(gata);
            //öppna databas
            using (AnsokanModel db = new AnsokanModel()) {
                //hitta med id från hiddenfield
                Adress adress = db.Adress.Find(postnummer);
                db.Adress.Attach(adress);
                if (gata != adress.Gata) {
                    //eftersom användarinmatningar går genom whitelist
                    adress.Gata = gata;
                }
                if (personID != adress.PersonID) {
                    adress.PersonID = personID;
                }
                //spara
                db.SaveChanges();
            }//stäng databas
        }

        public void UpdateGranskad(int dokumentNr, DateTime? datumGranskad, int synpunktID, int? LoginID)
        {
            //öppna databas
            using (AnsokanModel db = new AnsokanModel()) {
                Granskad resultat = db.Granskad.Find(dokumentNr);
                db.Granskad.Attach(resultat);
                if (datumGranskad != resultat.DatumGranskad && datumGranskad != null) {
                    resultat.DatumGranskad = datumGranskad;
                }
                if (synpunktID != resultat.SynpunktID) {
                    resultat.SynpunktID = synpunktID;
                }
                if (LoginID != resultat.LoginID && LoginID != null) {
                    resultat.LoginID = LoginID;
                }

                //spara
                db.SaveChanges();
            }
        }

        public void UpdateOrt(int postnummer, string ort)
        {
            Whitelist start = new Whitelist();
            ort = start.CleanInput(ort);
            using (AnsokanModel db = new AnsokanModel()) {
                Ort resultat = db.Ort.Find(postnummer);
                db.Ort.Attach(resultat);
                if (resultat.Ort1 != ort && ort != "") {
                    resultat.Ort1 = ort;
                }
                db.SaveChanges();
            }
        }

        public void UpdatePerson(int personID, string epost)
        {
            Whitelist start = new Whitelist();
            epost = start.CleanInput(epost);
            using (AnsokanModel db = new AnsokanModel()) {
                Person person = db.Person.Find(personID);
                db.Person.Attach(person);
                if (epost != person.Epost && epost != "") {
                    person.Epost = epost;
                }
                db.SaveChanges();
            }
        }

        public void UpdatePersonTyp(int personID, bool personTyp)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                PersonTyp person = db.PersonTyp.Find(personID);
                db.PersonTyp.Attach(person);
                if (personTyp != person.PersonTyp1) {
                    person.PersonTyp1 = personTyp;
                }
                db.SaveChanges();
            }
        }

        public void UpdateSynpunkt(int SynpunktsID, string enhet, DateTime datumMottagit, string synpunkText, short avseende, int personID)
        {
            Whitelist start = new Whitelist();
            enhet = start.CleanInput(enhet);
            synpunkText = start.CleanInput(synpunkText);
            using (AnsokanModel db = new AnsokanModel()) {
                Synpunkt synpunkt = db.Synpunkt.Find(SynpunktsID);
                db.Synpunkt.Attach(synpunkt);
                if (enhet != synpunkt.Enhet && enhet != "") {
                    synpunkt.Enhet = enhet;
                }
                if (datumMottagit != synpunkt.DatumMottagit && datumMottagit != null) {
                    synpunkt.DatumMottagit = datumMottagit;
                }
                if (synpunkText != synpunkt.SynpunkText && synpunkText != "") {
                    synpunkt.SynpunkText = synpunkText;
                }
                if (avseende != synpunkt.Avseende) {
                    synpunkt.Avseende = avseende;
                }
                if (personID != synpunkt.PersonID) {
                    synpunkt.PersonID = personID;
                }
                db.SaveChanges();
            }
        }

        public void UpdateTelefon(int telefonnummer, int personID)
        {
            using (AnsokanModel db = new AnsokanModel()) {
                Telefon telefon = db.Telefon.Find(telefonnummer);
                db.Telefon.Attach(telefon);
                if (telefon.PersonID != personID) {
                    telefon.PersonID = personID;
                }
                db.SaveChanges();
            }
        }

        public void UpdateVerksamhet(string enhet, string verksamhet)
        {
            Whitelist start = new Whitelist();
            verksamhet = start.CleanInput(verksamhet);
            enhet = start.CleanInput(enhet);
            using (AnsokanModel db = new AnsokanModel()) {
                Verksamhet verksamhetobj = db.Verksamhet.Find(enhet);
                db.Verksamhet.Attach(verksamhetobj);
                if (verksamhetobj.Verksamhet1 != verksamhet && verksamhet != "") {
                    verksamhetobj.Verksamhet1 = verksamhet;
                }
                db.SaveChanges();
            }
        }
    }
}
