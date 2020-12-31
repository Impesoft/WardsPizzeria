using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        }

        public Order(Pizza pizza, PizzaSize size /*, PizzaCrust crust, double price*/)
        {
            OrderedPizza = pizza;
            Size = size;
            // Crust = crust;
            // Price = price;
        }

        public List<Order> ReadOrdersFromFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Order>));

            StreamReader reader = new StreamReader(path);
            var list = (List<Order>)serializer.Deserialize(reader);
            reader.Close();

            return list;
        }

        public void WriteOrdersToFile(string path)
        {
            XmlSerializer serializer = new XmlSerializer(Program.OrderList.GetType());

            Program.OrderList = Program.OrderList
                .OrderBy(p => p.OrderDate)
                .ThenBy(p => p.OrderedPizza.Id).ToList(); //will need to be ordered by date most likely

            TextWriter writer = new StreamWriter(path);
            serializer.Serialize(writer, Program.OrderList);
            writer.Close();
        }
    }
}