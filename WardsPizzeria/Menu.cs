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
            //Console.Write("Give pizza ID name: ");
            //I think this shouldn't be definable by user, but be an autoincrement value this way no duplicates can be created
            // pizza.id = Program.PizzaID;
            Console.Write("Give pizza name: ");
            pizza.Name = Console.ReadLine();
            Console.Write("Give base pizza price: ");
            pizza.OrderPrice = Convert.ToDouble(Console.ReadLine());
            Console.Write("Give the ingredients (separted with commas):");
            string stringIngedients = Console.ReadLine();
            Console.Write("Veggie (Y/N):");
            char yesNo = (Char.ToUpper(Console.ReadKey().KeyChar));

           // char yesNo = (Convert.ToChar(Console.ReadKey()));
            //char.ToUpper(yesNo);
            pizza.IsVeggie = (yesNo == 'Y');
            // add new pizza to list
            Program.PizzaID++;
            pizza.Id = Program.PizzaID;
            Program.PizzaList.Add(pizza);
            pizza.WritePizzasToFile(Program.Path);
            Console.WriteLine("\nWritten to Pizzalist file, would you like to add another one?(Y/N)");
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
                foreach (Pizza loadedpizza in Program.PizzaList)
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
