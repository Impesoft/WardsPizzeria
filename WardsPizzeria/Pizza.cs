using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Xml;

namespace WardsPizzeria
{

    public enum PizzaSize { small, medium, large };

    public enum PizzaCrust { pan, deep, cheese };

    [Serializable()]
    public class Pizza
    {
        [XmlRoot("ArrayOfPizza", Namespace = "http://www.w3.org/2001/XMLSchema")]
        public class MyRoot
        {
           // public KMCModelExtCovPriceInquiry Inquiry { get; set; }
        }
        public List<Pizza>PizzaList { get; set; }
        [XmlElement("Pizza")]

        public static int PizzaID;
        [XmlElement("ListOFPizzas")]

        public string Name { get; set; }
        public string PizzaIngredients { get; set; }
        public double OrderPrice { get; set; }
        public bool IsVeggie { get; set; }
        public Pizza(int id, string name, string ingredients, bool isVeggie, double orderPrice)
        {
            PizzaID++;
            Name = name;
            PizzaIngredients = ingredients;
            IsVeggie = isVeggie;
            OrderPrice = orderPrice;

        }
        
        public Pizza() //needed for xml constructor
        {
            PizzaID++;

            Name = "Margherita   ";
            PizzaIngredients = "ingredient1, ingredient2, ...";
            IsVeggie = false;
            OrderPrice = 4.5;

        }
     
        public void ReadPizzasFromFile(string path)
        {

            XmlSerializer toList = new XmlSerializer(typeof(List<Pizza>));

            StreamReader reader = new StreamReader(path);
            PizzaList = (List<Pizza>)toList.Deserialize(reader);

            reader.Close();
    
        }
        public void WritePizzasToFile(string path)
        {
            XmlSerializer toXML = new XmlSerializer(PizzaList.GetType());

            TextWriter toFile = new StreamWriter(path);
            toXML.Serialize(toFile, PizzaList);
            toFile.Close();
        }

    }
}
