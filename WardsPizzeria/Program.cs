using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WardsPizzeria
{
    public class Program
    {
        public static int PizzaID = 0;
        public static List<Pizza> PizzaList { get; set; } = new List<Pizza> { };
        public static List<Order> OrderList { get; set; } = new List<Order> { };

        private static void Main(string[] args)
        {
            Console.SetWindowSize(140, 30);
            Console.Title = "Pizza App(Admin)";
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Order order = new Order();

            if (File.Exists(GlobalVariables.LogPath))
            {
                OrderList = order.ReadOrdersFromFile(GlobalVariables.LogPath);
                OrderList = OrderList.OrderBy(p => p.OrderedPizza.Id).ToList();
            }

            Pizza pizza = new Pizza();

            if (File.Exists(GlobalVariables.Path))
            {
                PizzaList = pizza.ReadPizzasFromFile(GlobalVariables.Path);
                PizzaList = PizzaList.OrderBy(p => p.Id).ToList();
                PizzaID = PizzaList[PizzaList.Count - 1].Id;
            }
            else
            {
                Console.WriteLine("Currently we don't have any pizza's available for sale...");
                Console.WriteLine("Do you want to add some pizza's to the pizzalist?(Y/N)");
                char addNewPizza = char.ToUpper(Console.ReadKey().KeyChar);

                switch (addNewPizza)
                {
                    case 'Y':
                        // pizzamaker
                        Menu Creator = new Menu();
                        Creator.CreatePizza(pizza);
                        break;

                    case 'N':
                        // main menu
                        break;
                }
            }

            Menu menu = new Menu(pizza);
        }
    }
}