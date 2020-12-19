using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WardsPizzeria
{

    public class Program
    {
        public static List<Pizza> pizzaList = new List<Pizza>();

        private static void Main(string[] args)
        {
            //pizzaList.Add(new Pizza(1, "Margherita", "Kaas, tomatensaus, kruiden", true, 5.0));
            //pizzaList.Add(new Pizza(1, "Funghi", "Kaas, tomatensaus, champignons, kruiden", true, 6.0));
            //pizzaList.Add(new Pizza(1, "Prosciutto", "Kaas, tomatensaus, hesp, kruiden", true, 5.0));
            Program p = new Program();
            // p.WritePizzasToFile();
            p.ReadPizzasFromFile();

  
        }

        public void WritePizzasToFile()
        {
            XmlSerializer toXML = new XmlSerializer(pizzaList.GetType());

            TextWriter toFile = new StreamWriter(@"P:\Pizzeria\Pizzalijst.xml");
            toXML.Serialize(toFile, pizzaList);
            toFile.Close();
        }
        public void WritePizzasToString()
        {
            XmlSerializer toXML = new XmlSerializer(pizzaList.GetType());
            TextWriter toString = new StringWriter();
            toXML.Serialize(toString, pizzaList);
            toString.Close();
        }
        public void ReadPizzasFromFile()
        {
            XmlSerializer toList = new XmlSerializer(pizzaList.GetType());
            StreamReader reader = new StreamReader(@"P:\Pizzeria\Pizzalijst.xml");
            //XmlReader fromFile= XmlReader.Create(@"P:\Pizzeria\Pizzalijst.xml");
            List<Pizza> pizzalist = (List<Pizza>)toList.Deserialize(reader);
            reader.Close();
            foreach (Pizza pizza in pizzaList)
            {
                Console.WriteLine(pizza.Name);
            }
        }
    }

}