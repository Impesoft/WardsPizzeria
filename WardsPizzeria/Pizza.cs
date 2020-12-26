using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WardsPizzeria
{
    public enum PizzaSize { unset, small, medium, large };

    public enum PizzaCrust { unset, pan, deep, cheese };

    [Serializable()]
    public class Pizza
    {
        [XmlElement("Id")]
        public int Id { get; set; }

        [XmlElement("Name")]
        public string Name { get; set; }

        public string PizzaIngredients { get; set; }
        public double BasePrice { get; set; }
        public bool IsVeggie { get; set; }

        public Pizza(int id, string name, string ingredients, bool isVeggie, double basePrice)
        {
            Id = Program.PizzaID;
            Name = name;
            PizzaIngredients = ingredients;
            IsVeggie = isVeggie;
            BasePrice = basePrice;
        }

        public Pizza() //needed for xml constructor
        {
            Id = Program.PizzaID;
            Program.PizzaID++;
            Name = "Margherita   ";
            PizzaIngredients = "ingredient1, ingredient2, ...";
            IsVeggie = false;
            BasePrice = 4.5;
        }

        public void ReadPizzasFromFile(string path)
        {
            XmlSerializer toList = new XmlSerializer(typeof(List<Pizza>));

            StreamReader reader = new StreamReader(path);
            Program.PizzaList = (List<Pizza>)toList.Deserialize(reader);

            reader.Close();
        }

        public void WritePizzasToFile(string path, Pizza receivedpizza)
        {
            Pizza pizza = receivedpizza;
            XmlSerializer toXML = new XmlSerializer(Program.PizzaList.GetType());

            TextWriter toFile = new StreamWriter(path);
            toXML.Serialize(toFile, Program.PizzaList);
            toFile.Close();
        }
    }
}