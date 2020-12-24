using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WardsPizzeria
{
    internal class Menu
    {
        private  List<Order> currentOrder;
        
        public Menu()
        {
            //

        }

        public Menu(Pizza pizza)
        {
            //currentOrder = new List<Order> { };

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
            bool yes = (Char.ToUpper(Console.ReadKey().KeyChar)) == 'Y';
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
            PizzaSize size = PizzaSize.unset;
            currentOrder = new List<Order> { };
            CultureInfo nlBE = CultureInfo.CreateSpecificCulture("nl-BE");
            char yesNo;
            Order o;


            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to our pizzeria:");
                Console.WriteLine("------------------------");

                if (!(Program.PizzaList == null))
                {
                    Console.WriteLine("Our list of pizzas");
                    foreach (Pizza loadedpizza in Program.PizzaList)
                    {
                        string idString = loadedpizza.Id.ToString("00", nlBE); // use 0.00 for price
                        Console.Write(idString + "- ");
                        Console.Write($"Pizza {loadedpizza.Name}");
                        Console.CursorLeft = 25;
                        Console.Write($"ingredients({loadedpizza.PizzaIngredients})");
                        Console.CursorLeft = 100;
                        string priceString = ((double)loadedpizza.OrderPrice).ToString("0.00", nlBE);
                        Console.Write($"price € {priceString}");
                        Console.WriteLine((loadedpizza.IsVeggie ? "(vegetarian)" : ""));
                        //if (loadedpizza.IsVeggie)
                        //{
                        //    Console.WriteLine(" (vegeterian)");
                        //}
                        //else { Console.WriteLine(); }
                    }
                    bool pizzaNotChosen = false;
                    int chosenPizzaId = 0;
                    do
                    {
                        try
                        {
                            chosenPizzaId = Convert.ToInt32(Console.ReadLine());
                            pizzaNotChosen = false;
                        }
                        catch (FormatException)
                        {
                            Console.CursorTop = Console.CursorTop - 1;
                            int currentLineCursor = Console.CursorTop;
                            Console.SetCursorPosition(0, Console.CursorTop);
                            Console.Write(new string(' ', Console.WindowWidth));
                            Console.SetCursorPosition(0, currentLineCursor);
                            pizzaNotChosen = true;
                        }
                    } while (pizzaNotChosen && (chosenPizzaId == 0));

                    try
                    {
                        Pizza chosenPizza = Program.PizzaList.Single(Pizza => Pizza.Id == chosenPizzaId);
                        pizza = chosenPizza;
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("No such pizza.");
                        return;
                    }
                }
                else
                {
                    Console.WriteLine("Pizza List is currently Empty");
                    Console.ReadKey();
                    return;
                }
                Console.WriteLine(pizza.Name + " excellent choice, in what size would you like this pizza?");
                Console.WriteLine($"{(PizzaSize)PizzaSize.large}, {(PizzaSize)PizzaSize.medium} or {(PizzaSize)PizzaSize.small}.(L,M,S)");
                char pizzaSize;
                bool validSelection = false;
                do
                {
                    pizzaSize = Char.ToUpper(Console.ReadKey().KeyChar);
                    switch (pizzaSize)
                    {
                        case 'S':
                            validSelection = true;
                            size = PizzaSize.small;
                            break;

                        case 'M':
                            validSelection = true;
                            size = PizzaSize.medium;
                            break;

                        case 'L':
                            validSelection = true;
                            size = PizzaSize.large;
                            break;
                    }
                } while (!validSelection);

                 o = new Order(pizza, size);
                o.OrderDate = DateTime.Now.ToString("yyyy MMMM dd");
                currentOrder.Add(o);
                Console.CursorLeft = 0;
                Console.WriteLine("Add another order?(Y/N/)");
                 yesNo = char.ToUpper(Console.ReadKey().KeyChar);
            } while(yesNo == 'Y');
            
            Program.OrderList.AddRange(currentOrder);
             o.WriteOrdersToFile(Program.LogPath);
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}