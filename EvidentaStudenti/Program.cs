using CarClassLibrary;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StocareDate;

namespace CarShopConsoleApp
{
    class Program
    {
        static void Main()
        {
            string numeFisier = ConfigurationManager.AppSettings["NumeFisier"];
            AdministrareMagazin_FisierText adminMasini = new AdministrareMagazin_FisierText(numeFisier);

            Store store1 = new Store();
            Car newCar = new Car();

            int nrMasini = 0;

            Console.WriteLine("###########################################################################################\n" +
                "#\t\t\t\t\t\t\t\t\t\t\t  #");
            Console.WriteLine(
                "#\tWelcome to the car store. \t\t\t\t\t\t\t  #\n" +
                "#\tFirst you must create some car inventory. \t\t\t\t\t  #\n" +
                "#\tThen you may add some cars to the shopping cart. \t\t\t\t  #\n" +
                "#\tFinally you may checkout which will give you a total value of the shopping cart.  #");
            Console.WriteLine("#\t\t\t\t\t\t\t\t\t\t\t  #\n" + "###########################################################################################\n");

            string option;
            do
            {
                Console.WriteLine("Alegeti o optiune\n");
                Console.WriteLine("A. Adauga masina in magazin (Citire de la tastatura)");
                Console.WriteLine("B. Adauga masina in cosul de cumparaturi");
                Console.WriteLine("C. Afiseaza valoarea totala a cosului de cumparaturi");
                Console.WriteLine("D. Cauta masina in magazin (dupa marca, model, pret)");
                Console.WriteLine("F. Afiseaza masini magazin");
                Console.WriteLine("S. Salvare masina in magazin");
                Console.WriteLine("X. Inchidere program");
                option = Console.ReadLine();
                switch (option.ToUpper())
                {
                    case "A":
                        newCar = adaugareInMagazin();

                        break;
                    // Add to cart
                    case "B":
                        Console.WriteLine("Ai ales sa adaugi o masina in cosul de cumparaturi");
                        store1.CarList = adminMasini.GetCars(out nrMasini);
                        displayCarsStore(store1);

                        Console.WriteLine("Pe care masina ai dori sa o cumperi? (numar) ");
                        int carChosen = Convert.ToInt32(Console.ReadLine());

                        store1.ShoppingList.Add(store1.CarList[carChosen]);

                        displayShoppingCart(store1);

                        break;
                    case "C":
                        Console.WriteLine("Ai ales sa termini cumparaturile");
                        displayShoppingCart(store1);
                        Console.WriteLine("Costul total al cumparaturilor dumneavoastra este: $" + store1.Checkout());

                        break;
                    case "D":
                        Console.WriteLine("Care este criteriul dupa care cauti?");
                        Console.WriteLine("(1) Marca\n(2) Model\n(3) Pret");
                        int optiuneCautare = Convert.ToInt32(Console.ReadLine());
                        carSearching(store1, optiuneCautare);

                        break;
                    case "F":
                        store1.CarList = adminMasini.GetCars(out nrMasini);
                        displayCarsStore(store1);

                        break;
                    case "S":
                        int idMasina = nrMasini + 1;
                        newCar.SetIdMasina(idMasina);
                        store1.CarList.Add(newCar);
                        //adaugare student in fisier
                        adminMasini.AddMasina(newCar);
                        nrMasini = nrMasini + 1;
                        break;
                    case "X":
                        return;
                    default:
                        Console.WriteLine("Optiune inexistenta");
                        break;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadLine();
                Console.Clear();

            } while (option.ToUpper() != "X");
        }

        public static void AfisareMasina(Car masina)
        {
            string infoStudent = string.Format("Masina cu id-ul #{0} este modelul: {1} {2} {3}",
                   masina.GetIdMasina(),
                   masina.GetMake() ?? " NECUNOSCUT ",
                   masina.GetModel() ?? " NECUNOSCUT ",
                   masina.GetPrice().ToString() ?? " NECUNOSCUT ");

            Console.WriteLine(infoStudent);
        }

        private static Car adaugareInMagazin()
        {
            Console.WriteLine("Ai ales sa adaugi o masina noua in magazin.");

            string carMake = "";
            string carModel = "";
            decimal carPrice = 0;

            Console.WriteLine("Care este compania care a produs masina? (EX: ford, mercedes, nissan etc.)");
            carMake = Console.ReadLine();

            Console.WriteLine("Care este moedlul masinii? (EX: corvette, focus, ranger etc.)");
            carModel = Console.ReadLine();

            Console.WriteLine("Care este pretul masinii?");
            carPrice = Convert.ToDecimal(Console.ReadLine());

            Car newCar = new Car(0, carMake, carModel, carPrice);

            return newCar;
        }

        private static void displayShoppingCart(Store store1)
        {
            Console.WriteLine("Masinile pe care doresti sa le cumperi:");
            for (int i = 0; i < store1.ShoppingList.Count; i++)
            {
                Console.WriteLine("Car #" + i + ": " + store1.ShoppingList[i]);
            }
        }
        private static void carSearching(Store store, int optiune)
        {
            Console.WriteLine("Masinile pe care doresti sa le cumperi:");

            int gasit = 0;
            if (optiune == 1)
            {
                // Cautam in magazin dupa marca
                string marca = Console.ReadLine();
                for (int i = 0; i < store.CarList.Count; i++)
                {
                    if (store.CarList[i].make == marca)
                    {
                        AfisareMasina(store.CarList[i]);
                        gasit = 1;
                    }
                }
            } else if (optiune == 2)
            {
                // Cautam in magazin dupa model
                string model = Console.ReadLine();
                for (int i = 0; i < store.CarList.Count; i++)
                {
                    if (store.CarList[i].model == model)
                    {
                        AfisareMasina(store.CarList[i]);
                        gasit = 1;
                    }
                }
            } else if (optiune == 3)
            {
                // Cautam in magazin dupa pret
                decimal price = Convert.ToDecimal(Console.ReadLine());
                for (int i = 0; i < store.CarList.Count; i++)
                {
                    if (store.CarList[i].price == price)
                    {
                        AfisareMasina(store.CarList[i]);
                        gasit = 1;
                    }
                }
            }
            
            if (gasit == 0)
            {
                Console.WriteLine("Momentan nu avem masini dupa criteriile dorite de dumneavoastra.");
            }
        }

        private static void displayCarsStore(Store store)
        {
            Console.WriteLine("Masinile din magazin sunt:");
            for (int contor = 0; contor < store.CarList.Count; contor++)
            {
                string infoStudent = string.Format("Masina cu id-ul #{0} este: {1} {2} {3}",
                   store.CarList[contor].GetIdMasina(),
                   store.CarList[contor].GetMake() ?? " NECUNOSCUT ",
                   store.CarList[contor].GetModel() ?? " NECUNOSCUT ",
                   store.CarList[contor].GetPrice().ToString() ?? " NECUNOSCUT ");

                Console.WriteLine(infoStudent);
            }
        }

        
    }
}

