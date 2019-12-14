using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Client
{
    [Serializable]
    [XmlRoot("Item")]
    public class Item
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Category")]
        public string Category { get; set; }

        [XmlAttribute("Country")]
        public string Country { get; set; }

        [XmlAttribute("Price")]
        public int Price { get; set; }

        [XmlAttribute("Count")]
        public int Count { get; set; }

        public Item() { }

        public Item(int id, string name, int price, int count, string category = "", string country = "")
        {
            ID = id;
            Name = name;
            Country = country;
            Category = category;
            Price = price;
            Count = count;
        }

        public Item(string name, int price, int count, string category = "", string country = "")
        {
            Name = name;
            Country = country;
            Category = category;
            Price = price;
            Count = count;
        }

        public static List<Item> XmlToItem(XElement xElement)
        {
            List<Item> itemsList = new List<Item>();   
         
            foreach(var it in xElement.Descendants("Item"))
            {
                StringReader reader = new StringReader(it.ToString());
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(Item));
                Item newit = (Item)xmlSerializer.Deserialize(reader);
                itemsList.Add(newit);
            }

            return itemsList;
        }
    }
}
