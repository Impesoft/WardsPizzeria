using System;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace WardsPizzeria
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Pizza> pizzaList = new List<Pizza>();
            pizzaList.Add(new Pizza(1, "Margherita", "Kaas, tomatensaus, kruiden", true, 5.0));
            pizzaList.Add(new Pizza(1, "Funghi", "Kaas, tomatensaus, champignons, kruiden", true, 6.0));
            pizzaList.Add(new Pizza(1, "Prosciutto", "Kaas, tomatensaus, hesp, kruiden", true, 5.0));
            XmlSerializer toXML = new XmlSerializer(pizzaList.GetType());

          TextWriter fileWriter = new StreamWriter(@"P:\Pizzeria\Serialization.xml");
            TextWriter txtWriter = new StringWriter();
        //    XmlDocument xmlPizzalist = new XmlDocument();
            toXML.Serialize(txtWriter, pizzaList);
            Console.WriteLine(txtWriter.ToString());

            toXML.Serialize(fileWriter, pizzaList);
            txtWriter.Close();
            Console.WriteLine();

        }

    }
   

}
