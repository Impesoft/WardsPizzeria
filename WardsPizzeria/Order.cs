using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WardsPizzeria
{

   public class Order
    {

        public Pizza OrderedPizza { get; set; }
        public string OrderDate { get; set; } = DateTime.Now.ToString("yyyy MMMM dd");

        public PizzaSize Size { get; set; }
        public PizzaCrust Crust { get; set; }
        public double Price { get; set; } // get will probably only be needed if we (in time) add reductions

        public Order()
        {
            string OrderDate= DateTime.Now.ToString("yyyy MMMM dd");
            Pizza pizza;
            PizzaSize size;
            PizzaCrust crust;
            double price;
        }
        public Order(Pizza pizza, PizzaSize size /*, PizzaCrust crust, double price*/)
        {
            string OrderDate = DateTime.Now.ToString("yyyy MMMM dd");
            OrderedPizza = pizza;
            Size = size;
           // Crust = crust;
           // Price = price;
        }

        public void ReadOrdersFromFile(string path)
        {

            XmlSerializer toList = new XmlSerializer(typeof(List<Order>));

            StreamReader reader = new StreamReader(path);
            Program.OrderList = (List<Order>)toList.Deserialize(reader);

            reader.Close();

        }
        public void WriteOrdersToFile(string path)
        {

            XmlSerializer toXML = new XmlSerializer(Program.OrderList.GetType());
           Program.OrderList = Program.OrderList.OrderBy(p => p.OrderDate ).ThenBy(p => p.OrderedPizza.Id).ToList(); //will need to be ordered by date mostlikely

            TextWriter toFile = new StreamWriter(path);
            toXML.Serialize(toFile, Program.OrderList);
            toFile.Close();
        }
    }
}
