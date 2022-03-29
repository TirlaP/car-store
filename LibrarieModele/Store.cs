using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarClassLibrary
{
    public class Store
    {
        // Catalog of cars
        public List<Car> CarList { get; set; }
        public List<Car> ShoppingList { get; set; }

        public Store()
        {
            CarList = new List<Car>();
            ShoppingList = new List<Car>();
        }

        public decimal Checkout()
        {
            // initialize the total cost
            decimal totalCost = 0;

            foreach (var c in ShoppingList)
            {
                totalCost += c.price;
            }

            ShoppingList.Clear();

            return totalCost;
        }

    }
}
