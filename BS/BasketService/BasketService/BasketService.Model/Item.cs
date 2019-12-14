using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Model
{
    [Serializable]
    [DataContract]
    public class Item
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Category { get; set; }

        [DataMember]
        public string Country { get;  set; }

        [DataMember]
        public int Price { get; set; }

        [DataMember]
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

        public Item(string name, int price, int count, string category ="", string country = "")
        {
            Name = name;
            Country = country;
            Category = category;
            Price = price;
            Count = count;
        }
        
    }
}
