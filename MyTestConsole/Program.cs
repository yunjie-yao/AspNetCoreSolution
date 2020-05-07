using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace MyTestConsole
{
    class Program
    {
         
        static void Main(string[] args)
        {
            var env = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            List<Person> persons = new List<Person>()
            {
                new Person { PersonID = 1, car = "Ferrari" },
                new Person { PersonID = 1, car = "BMW" },
                new Person { PersonID = 2, car = "Audi" }
            };
            var results = persons.GroupBy(
                p => p.PersonID,
                p => p.car,
                (key, g) => new { PersonId = key, Cars = g.ToList() });

            XElement config = new XElement("config");
            XElement server = new XElement("server");
            XAttribute ip = new XAttribute("ip", "192.168.1.1");
            server.Add(ip);
            XAttribute port = new XAttribute("port", false);
            server.Add(port);
            //server.Add(new XAttribute("name", "oracle"));
            config.Add(server);
            //Console.WriteLine(config.ToString());
            Console.WriteLine(CreateXml());
        }

        public static string CreateXml()
        {
            string xml = "";
            XElement fetch = new XElement("fetch");
            XAttribute version = new XAttribute("version", "1.0");
            XAttribute outputformat = new XAttribute("output-format", "xml-platform");
            XAttribute mapping = new XAttribute("mapping", "logical");
            XAttribute distinct = new XAttribute("distinct", false);
            XAttribute count = new XAttribute("count", 5);
            XAttribute page = new XAttribute("page", 2);
            XAttribute returntotalrecordcount = new XAttribute("returntotalrecordcount", true);
            fetch.Add(version);
            fetch.Add(outputformat);
            fetch.Add(mapping);
            fetch.Add(distinct);
            fetch.Add(count);
            fetch.Add(page);
            fetch.Add(returntotalrecordcount);
            XElement entity = new XElement("entity");
            XAttribute name = new XAttribute("name", "new_obfapplication");//变量
            entity.Add(name);
            string[] array = { "fullname", "jobtitle", "annualincome" };
            for (int i = 0; i < array.Length; i++)
            {
                XElement attribute = new XElement("attribute");
                XAttribute xa = new XAttribute("name", array[i]);
                attribute.Add(xa);
                entity.Add(attribute);
            }
            XElement order = new XElement("order");
            XAttribute orderAttribute = new XAttribute("attribute", "new_name");//变量；排序字段
            XAttribute descending = new XAttribute("descending", false);
            order.Add(orderAttribute);
            order.Add(descending);
            entity.Add(order);
            XElement filter = new XElement("filter");
            XAttribute type = new XAttribute("type", "and");
            filter.Add(type);
            List<Condition> conditions = new List<Condition>
            {
                new Condition
                {
                    Attribute="statecode",
                    Operator="eq",
                    Value=0
                },
                new Condition
                {
                    Attribute="new_sc_shopid",
                    Operator="eq",
                    Value=Guid.Empty
                }
            };
            foreach (Condition con in conditions)
            {
                XElement condition = new XElement("condition");
                XAttribute attribute = new XAttribute("attribute", con.Attribute);
                XAttribute Operator = new XAttribute("operator", con.Operator);
                XAttribute value = new XAttribute("value", con.Value);
                condition.Add(attribute);
                condition.Add(Operator);
                condition.Add(value);
                filter.Add(condition);
            }
            entity.Add(filter);
            fetch.Add(entity);
            //fetch.Save("FetchXml.xml");
            xml = fetch.ToString();
            return xml;

        }
    }

    public class Condition
    {
        public string Attribute { get; set; }
        public string Operator { get; set; }
        public dynamic Value { get; set; }
    }

    public class Person
    {
        public int PersonID { get; set; }
        public string car { get; set; }
    }
}
