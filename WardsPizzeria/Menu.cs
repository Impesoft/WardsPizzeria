using System;
using System.Collections.Generic;
using System.Globalization;
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
                        PizzaCreator(pizza);
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
        public void PizzaCreator(Pizza pizza)
        {
            Console.Clear();
            Console.WriteLine("Pizza Creator:");
            Console.WriteLine("--------------");

            Console.Write("Give pizza name: ");
            pizza.Name = Console.ReadLine();
            Console.Write("Give base pizza price: ");
            pizza.OrderPrice = Convert.ToDouble(Console.ReadLine());
            Console.Write("Give the ingredients (separted with commas):");
            string stringIngedients = Console.ReadLine();
            pizza.PizzaIngredients = stringIngedients;
            Console.Write("Veggie (Y/N):");
            char yesNo = (Char.ToUpper(Console.ReadKey().KeyChar));
            pizza.IsVeggie = (yesNo == 'Y');

            // add new pizza to list
            Program.PizzaID++;
            pizza.Id = Program.PizzaID;
            Program.PizzaList.Add(pizza);
            pizza.WritePizzasToFile(Program.Path);
            Console.WriteLine("\nWritten to Pizzalist file, would you like to add another one?(Y/N)");
            bool yes = (Char.ToUpper(Console.ReadKey().KeyChar))=='Y';
            if (yes) PizzaCreator(pizza);
        }
        public void SalesLog()
        {
            Console.Clear(); 
            Console.WriteLine("Sales log");
            Console.ReadKey();

        }
        public void OrderPizzas(Pizza pizza)
        {
            CultureInfo nlBE = CultureInfo.CreateSpecificCulture("nl-BE");
            Console.Clear();
            Console.WriteLine("Welcome to our pizzeria:");
            Console.WriteLine("------------------------");
            Console.WriteLine("Our list of pizzas");
            foreach (Pizza loadedpizza in Program.PizzaList)
            {
               string idString = loadedpizza.Id.ToString("00", nlBE); // use 0.00 for price
                Console.Write(idString + "- ");
                Console.Write($"Pizza {loadedpizza.Name}");
                Console.CursorLeft = 25;
                Console.Write($"ingredients({loadedpizza.PizzaIngredients})");
                Console.CursorLeft = 100;
                string priceString = ((double)loadedpizza.OrderPrice).ToString("00.00", nlBE);
                Console.Write($"price € {priceString}");
                if (loadedpizza.IsVeggie)
                {
                    Console.WriteLine(" (vegeterian)");
                }
                else { Console.WriteLine(); }
            }
            Console.ReadKey();

        }
       
    }
}
