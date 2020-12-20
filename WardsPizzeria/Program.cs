using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WardsPizzeria
{

    public class Program
    {
       // public static List<Pizza> PizzaList = new List<Pizza>();

        private static void Main(string[] args)
        {
            //pizzaList.Add(new Pizza(1, "Margherita", "Kaas, tomatensaus, kruiden", true, 5.0));
            //pizzaList.Add(new Pizza(1, "Funghi", "Kaas, tomatensaus, champignons, kruiden", true, 6.0));
            //pizzaList.Add(new Pizza(1, "Prosciutto", "Kaas, tomatensaus, hesp, kruiden", true, 5.0));
            Pizza pizza = new Pizza();
            // p.WritePizzasToFile();
            pizza.ReadPizzasFromFile();

  
        }

 
 
    }

}