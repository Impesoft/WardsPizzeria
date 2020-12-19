using System;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Text;

namespace WardsPizzeria
{

    public enum PizzaSize { small, medium, large };

    public enum PizzaCrust { pan, deep, cheese };

    [Serializable()]
    public class Pizza
    {
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
        
        public Pizza()
        {
            PizzaID++;

            Name = "Margherita   ";
            PizzaIngredients = "ingredient1, ingredient2, ...";
            IsVeggie = false;
            OrderPrice = 4.5;

        }
 
 
    }
}
