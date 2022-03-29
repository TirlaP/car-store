using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassLibrary
{
    public class Car
    {
        //constante
        private const char SEPARATOR_PRINCIPAL_FISIER = ';';

        private const int ID = 0;
        private const int MAKE = 1;
        private const int MODEL = 2;
        private const int PRICE = 3;

        //proprietati auto-implemented
        private int idMasina; //identificator unic masina

        public string make { get; set; }
        public string model { get; set; }
        public decimal price { get; set; }

        // Methods

        // The Default constructor ( no parameters )
        public Car()
        {
            make = "nothing yet";
            model = "nothing yet";
            price = 0.00M;
        }

        // Constructor with parameters
        public Car(string make, string model, decimal price)
        {
            this.make = make;
            this.model = model;
            this.price = price;
        }

        // Constructor with parameters
        public Car(int idMasina, string make, string model, decimal price)
        {
            this.idMasina = idMasina;
            this.make = make;
            this.model = model;
            this.price = price;
        }

        //constructor cu un singur parametru de tip string care reprezinta o linie dintr-un fisier text
        public Car(string linieFisier)
        {
            var dateFisier = linieFisier.Split(SEPARATOR_PRINCIPAL_FISIER);

            //ordinea de preluare a campurilor este data de ordinea in care au fost scrise in fisier prin apelul implicit al metodei ConversieLaSir_PentruFisier()
            idMasina = Convert.ToInt32(dateFisier[ID]);
            make = dateFisier[MAKE];
            model = dateFisier[MODEL];
            price = Convert.ToDecimal(dateFisier[PRICE]);
        }

        public string ConversieLaSir_PentruFisier()
        {
            string obiectMasinaPentruFisier = string.Format("{1}{0}{2}{0}{3}{0}{4}{0}",
                SEPARATOR_PRINCIPAL_FISIER,
                idMasina.ToString(),
                (make ?? " NECUNOSCUT "),
                (model ?? " NECUNOSCUT "),
                (price.ToString() ?? " NECUNOSCUT "));

            return obiectMasinaPentruFisier;
        }

        public int GetIdMasina()
        {
            return idMasina;
        }

        public string GetMake()
        {
            return make;
        }

        public string GetModel()
        {
            return model;
        }

        public decimal GetPrice()
        {
            return price;
        }

        public void SetIdMasina(int idMasina)
        {
            this.idMasina = idMasina;
        }

        // If we want to print the car we have to implement a method ToString
        override public string ToString()
        {
            return make + " " + model + " $" + price;
        }
    }
}
