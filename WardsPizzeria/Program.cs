using System;
using System.Xml.Serialization;
using System.IO;
namespace WardsPizzeria
{
    class Program
    {
        static void Main(string[] args)
        {
            Pizza pizza = new Pizza(1,"Margherita", "Kaas, tomatensaus, kruiden", true,5.0);

            XmlSerializer xs = new XmlSerializer(typeof(Pizza));

            TextWriter txtWriter = new StreamWriter(@"P:\Pizzeria\Serialization.xml");

            xs.Serialize(txtWriter, pizza);

            txtWriter.Close();

        }
    }
   

}
