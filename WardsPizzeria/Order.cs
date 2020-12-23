using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WardsPizzeria
{

   public class Order
    {

        public Pizza OrderedPizza { get; set; }
        public PizzaSize Size { get; set; }
        public PizzaCrust Crust { get; set; }
        public double Price { get; set; } // get will probably only be needed if we (in time) add reductions

        public Order()
        {
            Pizza pizza;
            PizzaSize size;
            PizzaCrust crust;
            double price;
        }
        public void ReadOrdersFromFile(string path)
        {

            XmlSerializer toList = new XmlSerializer(typeof(List<Order>));

            StreamReader reader = new StreamReader(path);
            Program.PizzaList = (List<Pizza>)toList.Deserialize(reader);

            reader.Close();

        }
        public void WriteOrdersToFile(string path)
        {
            XmlSerializer toXML = new XmlSerializer(Program.OrderList.GetType());

            TextWriter toFile = new StreamWriter(path);
            toXML.Serialize(toFile, Program.OrderList);
            toFile.Close();
        }
    }
}
