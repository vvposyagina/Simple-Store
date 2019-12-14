using BasketService.DataAccess;
using BasketService.Model;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BasketService
{ 
    [ServiceBehaviorAttribute(InstanceContextMode = InstanceContextMode.PerSession)]
    public class Basket : IBasket
    {
        Dictionary<int, Tuple<Item, int>> content;
        string address = @"..\..\store.db";
        DataBaseAccess dbaccess;

        public void Start()
        {
            content = new Dictionary<int, Tuple<Item, int>>();
            dbaccess = new DataBaseAccess(address);
        }

        public XElement Search(string query)
        {
            List<Item> searchResults = dbaccess.Search(query);

            XElement xmlResult = new XElement("Items",
                    searchResults.Select(p =>
                        new XElement("Item",
                            new XAttribute("ID", p.ID),
                            new XAttribute("Name", p.Name),
                            new XAttribute("Category", p.Category),
                            new XAttribute("Country", p.Country),
                            new XAttribute("Price", p.Price),
                            new XAttribute("Count", p.Count))));
            return xmlResult;
        }

        public bool AddItem(int ID)
        {
           if(dbaccess.UpdateItemsCount(ID, true))
           {
               Item newItem = dbaccess.SearchID(ID);
               if (content.ContainsKey(ID))
               {
                   content[ID] = Tuple.Create(newItem, content[ID].Item2 + 1);
               }
               else
               {                   
                   content.Add(ID, Tuple.Create(newItem, 1));
               }
               return true;
           }
           return false;
        }

        public bool DeleteItem(int ID)
        {
            if (content.ContainsKey(ID))
            {
                if (dbaccess.UpdateItemsCount(ID, false))
                {
                    Item newItem = dbaccess.SearchID(ID);
                    
                    content[ID] = Tuple.Create(newItem, content[ID].Item2 - 1);
                    
                    if (content[ID].Item2 == 0)
                    {
                        content.Remove(ID);
                    }
                    return true;
                }
            }
            return false;
        }

        public XElement GetCurrentBasket()
        {
            XElement xmlResult = new XElement("Items",
                   content.Keys.Select(p =>
                        new XElement("Item",
                            new XAttribute("ID", content[p].Item1.ID),
                            new XAttribute("Name", content[p].Item1.Name),
                            new XAttribute("Category", content[p].Item1.Category),
                            new XAttribute("Country", content[p].Item1.Country),
                            new XAttribute("Price", content[p].Item1.Price),
                            new XAttribute("Count", content[p].Item2))));
            return xmlResult;
        }

        public int GetTotal()
        {
            int result = 0;

            foreach(var it in content.Values)
            {
                result += it.Item1.Price * it.Item2;
            }

            return result;
        }

        public void PayPurchase()
        {
            XElement xmlList = new XElement("Items",
                   content.Keys.Select(p =>
                        new XElement("Item",
                            new XAttribute("ID", content[p].Item1.ID),
                            new XAttribute("Count", content[p].Item2))));

            int total = GetTotal();
            dbaccess.AddTransaction(DateTime.Now, xmlList, total);            
        }
    }
}
