using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace WardsPizzeria
{
    

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
            Name = "Margherita   ";
            PizzaIngredients = "ingredient1, ingredient2, ...";
            IsVeggie = false;
            BasePrice = 4.5;
        }

        public List<Pizza> ReadPizzasFromFile(string path)
        {
            XmlSerializer toList = new XmlSerializer(typeof(List<Pizza>));

            StreamReader reader = new StreamReader(path);
            var list = (List<Pizza>)toList.Deserialize(reader);
            reader.Close();

            return list;
        }

        public void WritePizzasToFile(string path)
        {
            XmlSerializer toXML = new XmlSerializer(Program.PizzaList.GetType());

            TextWriter toFile = new StreamWriter(path);
            toXML.Serialize(toFile, Program.PizzaList);
            toFile.Close();
        }
    }
}