using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WardsPizzeria
{

    public class Program
    {
        public static string path = @"P:\Pizzeria\Pizzalijst.xml";

        private static void Main(string[] args)
        {
             //pizzaList.Add(new Pizza(1, "Margherita", "Kaas, tomatensaus, kruiden", true, 5.0));

            Pizza pizza = new Pizza();
            // pizza.WritePizzasToFile(path);
            if (File.Exists(path))
            {
                pizza.ReadPizzasFromFile(path);
                foreach (Pizza loadedpizza in pizza.PizzaList)
                {
                    Console.Write($"Pizza {loadedpizza.Name} ingredients(");
                    Console.Write($"{loadedpizza.PizzaIngredients}) price € ");
                    Console.Write($"{(double)loadedpizza.OrderPrice} veggie:");
                    Console.WriteLine(loadedpizza.IsVeggie);
                }
            } else
            {
                Console.WriteLine("Currently we don't have any pizza's available for sale...\nDo you want to add some pizza's to the pizzalist?");
                Console.ReadLine();
            }
        }

 
 
    }

}