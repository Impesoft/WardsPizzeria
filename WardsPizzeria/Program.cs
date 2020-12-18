using System;
using System.Xml.Serialization;
using System.IO;

namespace WardsPizzeria
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee bs = new Employee();

            XmlSerializer xs = new XmlSerializer(typeof(Employee));

            TextWriter txtWriter = new StreamWriter(@ "D:\Serialization.xml");

            xs.Serialize(txtWriter, bs);

            txtWriter.Close();

        }
    }
    public class Employee
    {
        public int Id = 1;
        public String name = "John Smith";
        public string subject = "Physics";
    }

}
