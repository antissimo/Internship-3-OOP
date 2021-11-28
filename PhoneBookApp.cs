
//Isprike radi kolokvija i toga sta san review proslog domaceg vidija tek kad mi nije ostalo previse vrimena opet san pisa polu hrvatskim polu engleskim 
// i znan da aplikacija crasha na svaki malo promijenjeni input, taj dio mi nije jasan kako tocno radi

using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Threading;
namespace PhoneBook
{


    public enum preference
    {
        favourite,
        normal,
        blocked
    }
    public class Kontakt
    {
        public string _NameAndSurname { get; set; }
        public int _PhoneNumber { get; set; }

        public preference _Preference { get; set; }
        public Kontakt(string NameAndSurname, int PhoneNumber, preference Preference)
        {
            _NameAndSurname = NameAndSurname;
            _PhoneNumber = PhoneNumber;
            _Preference = Preference;



        }
        public void ispisi()
        {
            Console.WriteLine("Ime: " + _NameAndSurname + " Broj: " + _PhoneNumber.ToString() + ", to je " + _Preference.ToString() + " kontakt");
        }
    }
    public enum status
    {
        Ongoing,
        Missed,
        Finished
    }

    public class Poziv
    {
        public DateTime _Time { get; set; }
        public status _State { get; set; }

        public Poziv(DateTime Time, status State)
        {
            _Time = Time;
            _State = State;
        }
        public void ispisi()
        {
            Console.WriteLine("Poziv se dogodio " + _Time + " i sada je " + _State);
        }
    }
    class Program
    {
        static int TrenutniPoziv = 0;

        static void Main(string[] args)
        {



            var PhoneBook = new Dictionary<Kontakt, List<Poziv>>()
            {

                {new Kontakt("Ante Boskovic",916007638,preference.favourite),new List<Poziv>(){new Poziv(new DateTime(2017,11,11,10,30,20),status.Finished),
                                                                                               new Poziv(new DateTime(2021,7,3,11,8,33), status.Missed),
                                                                                               new Poziv(new DateTime(2021,9,28,2,45,21), status.Finished) } },
                {new Kontakt("Marko Markic",987532898,preference.normal),new List<Poziv>(){new Poziv(new DateTime(2020,11,11,10,30,20),status.Missed),
                                                                                               new Poziv(new DateTime(2021,2,4,11,8,33), status.Finished),
                                                                                               new Poziv(new DateTime(2021,8,25,9,31,22), status.Finished) } },
                {new Kontakt("Ivan Ivanovic",923353387,preference.blocked),new List<Poziv>(){new Poziv(new DateTime(2019,12,25,1,1,2),status.Finished),
                                                                                               new Poziv(new DateTime(2021,7,3,11,8,33), status.Finished),
                                                                                               new Poziv(new DateTime(2021,11,23,1,16,52), status.Missed) } },


            };
            IspisMenia(PhoneBook);




        }

        static void IspisMenia(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {
            Console.WriteLine("Izaberite jednu od ovih opcija");
            Console.WriteLine("1 - Ispis svih kontakata");
            Console.WriteLine("2 - Dodavanje novih kontakata u imenik");
            Console.WriteLine("3 - Brisanje kontakata iz imenika");
            Console.WriteLine("4 - Editiranje preference kontakta");
            Console.WriteLine("5 - Upravljanje kontaktom");
            Console.WriteLine("6 - Ispis svih poziva");
            Console.WriteLine("7 - Izlaz iz aplikacije");
            var odabir = Console.ReadLine();
            while (odabir != "7")
            {
                switch (odabir)
                {
                    case "1":
                        IspisSvihKontakata(PhoneBook);
                        break;
                    case "2":
                        DodajNoviKontakt(PhoneBook);
                        break;
                    case "3":
                        IzbrisiKontakt(PhoneBook);
                        break;
                    case "4":
                        EditanjeKontaktPref(PhoneBook);
                        break;
                    case "5":
                        UpravljanjeKontaktom(PhoneBook);
                        break;
                    case "6":
                        IspisPoziva(PhoneBook);
                        break;
                    case "7":
                        break;
                    default:
                        Console.WriteLine("Neispravan unos, molimo unesite broj izmedju 1 i 7");
                        break;




                }
                IspisMenia(PhoneBook);
                odabir = Console.ReadLine();
            }
        }
        static void IspisPoziva(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {
            Console.Clear();
        foreach (var kontaktILista in PhoneBook)
            {
                Console.WriteLine("Pozivi s "+kontaktILista.Key._NameAndSurname+": ");
                foreach(var poziv in kontaktILista.Value)
                {
                    poziv.ispisi();
                }
            }
        }

            static void UpravljanjeKontaktom(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {
            Console.Clear();
            Console.WriteLine("Usli ste u podmenu, izaberite jednu od ovih opcija");
            Console.WriteLine("1 - Ispis svih poziva sa nekom osobom poredanih od najnovijeg do najstarijeg");
            Console.WriteLine("2 - Novi poziv");
            Console.WriteLine("3 - Izlaz iz podmenua");
            var odabir = Console.ReadLine();
            while (odabir != "3")
            {
                switch (odabir)
                {
                    case "1":
                        IspisSvihPoziva(PhoneBook);
                        break;
                    case "2":
                        NoviPoziv(PhoneBook);
                        break;


                }
                IspisMenia(PhoneBook);
                odabir = Console.ReadLine();
            }
        }
        static void NoviPoziv(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {

            Console.WriteLine("Upisite ime osobe koju zelite nazvati");
            String imeKontakta = Console.ReadLine();

            foreach (var kontakt in PhoneBook.Keys)
            {
                if (kontakt._NameAndSurname == imeKontakta)
                {
                    Random rNum = new Random();
                    int randomStatus = rNum.Next(0, 3);
                    status noviStatus;
                    switch (randomStatus)
                    {
                        case 0:
                            noviStatus = status.Finished;
                            var noviPozivFinished = new Poziv(DateTime.Now, noviStatus);
                            Console.WriteLine("Razgovor je zavrsen");

                            PhoneBook[kontakt].Add(noviPozivFinished);
                            break;
                        case 1:
                            Console.WriteLine("Nazalost "+imeKontakta+" nije odgovorio na poziv");
                            noviStatus = status.Missed;
                            var noviPozivMissed = new Poziv(DateTime.Now, noviStatus);
                            PhoneBook[kontakt].Add(noviPozivMissed);
                            break;
                        case 2:
                            if (TrenutniPoziv == 0)
                            {
                                TrenutniPoziv = 1;
                                Random rNum2 = new Random();
                                int randomDuljina = rNum2.Next(0, 20);
                                Stopwatch stopWatch = new Stopwatch();
                                stopWatch.Start();
                                Console.WriteLine("Poziv je trenutno u tijeku...");
                                while (stopWatch.Elapsed.Seconds < randomDuljina) { }

                                TrenutniPoziv = 0;
                                Console.WriteLine("Poziv je gotov nakon "+randomDuljina+" sekunda");
                                noviStatus = status.Finished;
                                var noviPozivOngoing = new Poziv(DateTime.Now, noviStatus);

                                PhoneBook[kontakt].Add(noviPozivOngoing);

                            }
                            else { Console.WriteLine("Drugi poziv je trenutno aktivan"); }
                            break;

                    }
                }

            }
        }
        static void IspisSvihPoziva(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {
            Console.Clear();
            Console.WriteLine("Upisite ime osobe sa kojom zelite saznati pozive");
            String imeKontakta = Console.ReadLine();
            foreach (var kontakt in PhoneBook.Keys)
            {
                if (kontakt._NameAndSurname == imeKontakta)
                {
                    PhoneBook[kontakt].Reverse();

                    foreach (var poziv in PhoneBook[kontakt])
                    {
                        poziv.ispisi();
                    }
                    PhoneBook[kontakt].Reverse();
                }
            }
        }


        static void EditanjeKontaktPref(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {
            Console.Clear();
            Console.WriteLine("Upisi broj kontakta kojeg zelis uredit: ");
            int brojZaUredit = Convert.ToInt32(Console.ReadLine());

            var temp = 0;
            foreach (var item in PhoneBook.Keys.ToList())
            {
                if (item._PhoneNumber == brojZaUredit)
                {
                    temp = 1;
                    Console.WriteLine("Trenutni status kontakta je: " + item._Preference + ", u sto ga zelite promijeniti(opcije su normal,blocked i favourite)");
                    String novoStanje = Console.ReadLine();
                    switch (novoStanje)
                    {
                        case "normal":
                            item._Preference = preference.normal;

                            break;
                        case "blocked":
                            item._Preference = preference.blocked;
                            break;
                        case "favourite":
                            item._Preference = preference.favourite;
                            break;
                        default:
                            Console.WriteLine("Taj status ne postoji");
                            break;
                    }
                }
            }
            if (temp == 0)
            {
                Console.WriteLine("Taj broj nije u kontaktima");
            }

        }
        static void IzbrisiKontakt(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {
            Console.Clear();
            Console.WriteLine("Upisi broj kontakta kojeg zelis izbrisat: ");

            //Broj je unique za razliku od imena
            int brojZaIzbrisat = Convert.ToInt32(Console.ReadLine());
            Console.Clear();
            var temp = 0;
            foreach (var item in PhoneBook.Keys.ToList())
            {
                if (item._PhoneNumber == brojZaIzbrisat)
                {
                    temp = 1;
                    PhoneBook.Remove(item);
                    Console.WriteLine("Kontakt " + item._NameAndSurname + " izbacen je iz imenika");
                }
            }
            if (temp == 0)
            {
                Console.WriteLine("Taj broj nije u kontaktima");
            }
        }
        static void DodajNoviKontakt(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {
            Console.Clear();
            Console.WriteLine("Upisi ime novog kontakta: ");
            String novoIme = Console.ReadLine();
            Console.WriteLine("Upisi broj novog kontakta: ");
            int noviBroj = Convert.ToInt32(Console.ReadLine());
            var noviKontakt = new Kontakt(novoIme, noviBroj, preference.normal);
            var praznaLista = new List<Poziv>();

            var temp = 0;
            foreach (var item in PhoneBook.Keys.ToList())
            {
                if (item._PhoneNumber == noviBroj)
                {
                    temp = 1;
                    Console.Clear();
                    Console.WriteLine("Taj broj je vec u imeniku!");
                    return;
                }
            }
            if (temp == 0)
            { 
                PhoneBook.Add(noviKontakt, praznaLista);
            }






        }
        static void IspisSvihKontakata(Dictionary<Kontakt, List<Poziv>> PhoneBook)
        {
            Console.Clear();
            foreach (var item in PhoneBook.Keys)
            {
                item.ispisi();

            }
        }


    }


}
