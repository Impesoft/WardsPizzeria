using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace WardsPizzeria
{
    internal class Menu
    {
        private List<Order> currentOrder;

        public Menu()
        {
        }

        public Menu(Pizza pizza)
        {
            char chosenKey;
            do
            {
                PrintMainMenu();
                chosenKey = (char.ToUpper(Console.ReadKey().KeyChar));

                switch (chosenKey)
                {
                    case 'N':  //New
                        CreatePizza(pizza);
                        break;

                    case 'O': //Order
                        OrderPizzas(pizza);
                        break;

                    case 'L': //SalesLog
                        SalesLog();
                        break;
                }
            }
            while (chosenKey != 'Q'); //Quit
        }

        private void PrintMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Main Menu");
            Console.WriteLine("---------\n");
            Console.WriteLine("(N) Make New pizza.");
            Console.WriteLine("(O) Order pizza('s).");
            Console.WriteLine("(L) View saleslog.");
            Console.WriteLine("(Q) Quit the application completely.");
        }

        public void CreatePizza(Pizza pizza)
        {
            bool addNewPizza;

            do
            {
                Pizza localPizza = new Pizza();
                Program.PizzaID++;

                Console.Clear();
                Console.WriteLine("Pizza Creator:");
                Console.WriteLine("--------------");

                Console.Write("Pizza name: ");
                localPizza.Name = Console.ReadLine();

                Console.Write("Base pizza price: ");
                localPizza.BasePrice = Convert.ToDouble(Console.ReadLine());

                Console.Write("Give the ingredients (separated with commas):");
                localPizza.PizzaIngredients = Console.ReadLine();

                Console.Write("Veggie (Y/N):");
                char isVeggie = (char.ToUpper(Console.ReadKey().KeyChar));
                localPizza.IsVeggie = (isVeggie == 'Y');

                // add new pizza to list
                localPizza.Id = Program.PizzaID;
                Program.PizzaList.Add(localPizza);
                pizza.WritePizzasToFile(GlobalVariables.Path);

                Console.WriteLine();
                Console.WriteLine("Written to Pizzalist file.");
                Console.WriteLine("Would you like to add another one ? (Y / N)");

                addNewPizza = char.ToUpper(Console.ReadKey().KeyChar) == 'Y';
            } while (addNewPizza);
            return;
        }

        public void SalesLog()
        {
            Console.Clear();
            Console.WriteLine("Sales log");

            foreach (Order order in Program.OrderList)
            {
                Console.Write($"{order.OrderDate}: ");
                Console.Write($"Price: €{order.Price} ");
                Console.Write($"{order.OrderedPizza.Name} ({order.Size}) ");
                Console.Write($"Ingrediënten:({order.OrderedPizza.PizzaIngredients})");
                Console.Write(order.OrderedPizza.IsVeggie ? "(vegetarian)" : "");
                Console.WriteLine();
            }

            Console.WriteLine($"Pizzas order for a total of € {Program.OrderList.Sum(item => item.Price)}");
            Console.ReadKey();
        }

        public void OrderPizzas(Pizza pizza)
        {
            PizzaSize size = PizzaSize.Unset;
            currentOrder = new List<Order>();
            CultureInfo nlBE = CultureInfo.CreateSpecificCulture("nl-BE");
            char addAnotherPizza;
            Order order;

            do
            {
                Console.Clear();
                Console.WriteLine("Welcome to our pizzeria:");
                Console.WriteLine("------------------------");

                if (Program.PizzaList != null)
                {
                    PrintMenu(nlBE);

                    Console.WriteLine();
                    Console.WriteLine($"What would be the pizza of you choice? enter the (number 1 to {Program.PizzaList.Count})");
                    bool pizzaNotChosen;
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
                    }
                    while (pizzaNotChosen && (chosenPizzaId == 0));

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

                Console.WriteLine($"{pizza.Name} excellent choice, in what size would you like this pizza?");
                Console.WriteLine($"{PizzaSize.Large}, {PizzaSize.Medium} or {PizzaSize.Small}.(L,M,S)");
                char pizzaSize;
                bool isSelectionValid = false;

                do
                {
                    pizzaSize = char.ToUpper(Console.ReadKey().KeyChar);
                    switch (pizzaSize)
                    {
                        case 'S':
                            isSelectionValid = true;
                            size = PizzaSize.Small;

                            break;

                        case 'M':
                            isSelectionValid = true;
                            size = PizzaSize.Medium;
                            break;

                        case 'L':
                            isSelectionValid = true;
                            size = PizzaSize.Large;
                            break;
                    }
                }
                while (!isSelectionValid);

                order = new Order(pizza, size)
                {
                    Price = pizza.BasePrice + ((int)size - 1) * .5,
                    OrderDate = DateTime.Now.ToString("yyyy MMMM dd")
                };

                currentOrder.Add(order);
                Console.CursorLeft = 0;
                Console.WriteLine("Add another order?(Y/N/)");
                addAnotherPizza = char.ToUpper(Console.ReadKey().KeyChar);
            } 
            while (addAnotherPizza == 'Y');

            foreach (Order pizzaOrder in currentOrder)
            {
                Console.Write($"Price € {pizzaOrder.Price} ");
                Console.Write($"{pizzaOrder.OrderedPizza.Name} ({pizzaOrder.Size}) ");
                Console.Write($"ingrediënten ({pizzaOrder.OrderedPizza.PizzaIngredients})");
                Console.WriteLine((pizzaOrder.OrderedPizza.IsVeggie ? "(vegetarian)" : ""));
            }
            Console.WriteLine($"that's {currentOrder.Count} pizzas for a total of € {(double)currentOrder.Sum(item => item.Price)}");
            Program.OrderList.AddRange(currentOrder);
            order.WriteOrdersToFile(GlobalVariables.LogPath);
            Console.WriteLine("Order finished, press Enter to return to main menu");
            Console.ReadLine();
            return;
        }

        private void PrintMenu(CultureInfo cultureInfo)
        {
            Console.WriteLine("Our list of pizzas");

            foreach (Pizza loadedpizza in Program.PizzaList)
            {
                string idString = loadedpizza.Id.ToString("00", cultureInfo); // use 0.00 for price
                Console.Write(idString + "- ");
                
                Console.Write($"Pizza {loadedpizza.Name}");
                Console.CursorLeft = 25;
                
                Console.Write($"ingredients({loadedpizza.PizzaIngredients})");
                Console.CursorLeft = 100;
                
                string priceString = (loadedpizza.BasePrice).ToString("0.00", cultureInfo);
                Console.Write($"price € {priceString}");
                Console.WriteLine((loadedpizza.IsVeggie ? "(vegetarian)" : ""));
            }
        }
    }
}