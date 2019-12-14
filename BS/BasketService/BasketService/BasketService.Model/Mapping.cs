using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasketService.Model
{
    public static class Mapping
    {
        public static Item DBConvertToItem(DbDataRecord entry)
        {
            return new Item(Convert.ToInt32(entry["id"].ToString()),
                            entry["name"].ToString(), 
                            Convert.ToInt32(entry["price"].ToString()), 
                            Convert.ToInt32(entry["count"].ToString()),
                            entry["category"].ToString(), 
                            entry["country"].ToString());
        }
    }
}
