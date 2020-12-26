using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WardsPizzeria
{
    public class Program
    {
        // set required doc paths
        public static string Path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Pizzalijst.xml";

        public static string LogPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Saleslog.xml";
        public static int PizzaID = 0;
        public static List<Pizza> PizzaList { get; set; } = new List<Pizza> { };
        public static List<Order> OrderList { get; set; } = new List<Order> { };

        // public static Pizza pizza;
        private static void Main(string[] args)
        {
            Console.SetWindowSize(140, 30);
            Console.Title = "Pizza App(Admin)";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            //pizzaList.Add(new Pizza(1, "Margherita", "Kaas, tomatensaus, kruiden", true, 5.0));
            Order order = new Order();
            if (File.Exists(LogPath))
            {
                order.ReadOrdersFromFile(LogPath);
                OrderList = OrderList.OrderBy(p => p.OrderedPizza.Id).ToList();
            }
            Pizza pizza = new Pizza();
            if (File.Exists(Path))
            {
                pizza.ReadPizzasFromFile(Path);
                PizzaList = PizzaList.OrderBy(p => p.Id).ToList();

                PizzaID = PizzaList[PizzaList.Count - 1].Id;
            }
            else
            {
                Console.WriteLine("Currently we don't have any pizza's available for sale...\nDo you want to add some pizza's to the pizzalist?(Y/N)");

                char yesNo = char.ToUpper(Console.ReadKey().KeyChar);
                switch (yesNo)
                {
                    case 'Y':

                        // pizzamaker
                        Menu Creator = new Menu();
                        Creator.PizzaCreator(pizza);

                        break;

                    case 'N':
                        // main menu
                        break;
                }
            }
            //Console.WriteLine("press any key to continue...");
            //Console.ReadKey();
            Menu menu = new Menu(pizza);
        }
    }
}