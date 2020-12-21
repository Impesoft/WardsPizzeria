using System;
using System.Collections.Generic;
using System.IO;

namespace WardsPizzeria
{
    public class Program
    {
        public static string Path = @"P:\Pizzeria\Pizzalijst.xml";
        public static string LogPath = @"P:\Pizzeria\Sales.log";
        public static int PizzaID=0;
        public static List<Pizza> PizzaList { get; set; }

        // public static Pizza pizza;
        private static void Main(string[] args)
        {
            //Console.SetWindowSize(120, 20);
            Console.Title = "Pizza App(Admin)";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //pizzaList.Add(new Pizza(1, "Margherita", "Kaas, tomatensaus, kruiden", true, 5.0));

            Pizza pizza = new Pizza();
            if (File.Exists(Path))
            {
                pizza.ReadPizzasFromFile(Path);
                PizzaID = PizzaList.Count;
                foreach (Pizza loadedpizza in Program.PizzaList)
                {
                    Console.Write(loadedpizza.Id+"- ");
                    Console.Write($"Pizza {loadedpizza.Name}");
                    Console.CursorLeft = 25;
                    Console.Write($"ingredients({loadedpizza.PizzaIngredients}) price € ");
                    Console.Write($"{(double)loadedpizza.OrderPrice} veggie:");
                    Console.WriteLine(loadedpizza.IsVeggie);
                }
            }
            else
            {
                Console.WriteLine("Currently we don't have any pizza's available for sale...\nDo you want to add some pizza's to the pizzalist?(Y/N)");
                
                char yesNo = char.ToUpper(Console.ReadKey().KeyChar);
                switch (yesNo)
                {
                    case 'Y':
                        // pizzamaker
                        break;

                    case 'N':
                        // main menu
                        break;
                }
            }
            Console.WriteLine("press any key to continue...");
            Console.ReadKey();
            Menu menu = new Menu(pizza);
        }
    }
}