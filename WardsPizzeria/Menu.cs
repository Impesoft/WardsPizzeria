using System;
using System.Collections.Generic;
using System.Text;

namespace WardsPizzeria
{
    class Menu
    {
        public Menu(Pizza pizza)
        {
            char chosenKey;
            do
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("---------\n");
                Console.WriteLine("(N) Make New pizza." +
                                "\n(O) Order pizza('s)." +
                                "\n(L). View saleslog." +
                                "\n(Q) Quit the application completely");

                chosenKey = (Char.ToUpper(Console.ReadKey().KeyChar));
                switch (chosenKey)
                {
                    case 'N':
                        PizzaCreator();
                        break;
                    case 'O':
                        OrderPizzas(pizza);
                        break;
                    case 'L':
                        SalesLog();
                        break;
                }
            } while (chosenKey != 'Q');
        }
        public void PizzaCreator()
        {
            Console.Clear();
            Console.WriteLine("Pizza Creator:");
            Console.ReadKey();

        }
        public void SalesLog()
        {
            Console.Clear(); 
            Console.WriteLine("Sales log");
            Console.ReadKey();

        }
        public void OrderPizzas(Pizza pizza)
        {
            Console.Clear();
            Console.WriteLine("Welcome to our pizzeria:");
            Console.WriteLine("------------------------");
            Console.WriteLine("Our list of pizzas");
                foreach (Pizza loadedpizza in pizza.PizzaList)
                {
                    Console.Write($"Pizza {loadedpizza.Name}");
                    Console.CursorLeft = 25;
                    Console.Write($"ingredients({loadedpizza.PizzaIngredients}) price € ");
                    Console.Write($"{(double)loadedpizza.OrderPrice} veggie:");
                    Console.WriteLine(loadedpizza.IsVeggie);
                }
            Console.ReadKey();

        }
    }
}
