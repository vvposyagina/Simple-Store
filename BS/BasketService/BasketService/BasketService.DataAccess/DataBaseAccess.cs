using BasketService.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace BasketService.DataAccess
{
    public class DataBaseAccess
    {
        SQLiteConnection connection;   
     
        public DataBaseAccess(string address)
        {
            connection = new SQLiteConnection(String.Format("DataSource={0};", address));
        }

        public List<Item> Search(string query)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(String.Format("SELECT * FROM items WHERE name LIKE '%{0}%'", query), connection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<Item> searchResults = new List<Item>();

            foreach (DbDataRecord entry in reader)
            {
                searchResults.Add(Mapping.DBConvertToItem(entry));
            }

            connection.Close();
            return searchResults;                   
        }

        public Item SearchID(int ID)
        {
            connection.Open();
            SQLiteCommand command = new SQLiteCommand(String.Format("SELECT * FROM items WHERE id={0}", ID), connection);
            SQLiteDataReader reader = command.ExecuteReader();
            List<Item> searchResults = new List<Item>();

            foreach (DbDataRecord entry in reader)
            {
                searchResults.Add(Mapping.DBConvertToItem(entry));
            }
            connection.Close();

            if (searchResults.Count > 0)
                return searchResults[0];
            else
                return null;    
        }

        public bool UpdateItemsCount(int id, bool isDecreasing)
        {
            connection.Open();
            DataTable table = new DataTable();
            String sqlQuery = String.Format("SELECT count FROM items WHERE id='{0}'", id);
            SQLiteDataAdapter adapter = new SQLiteDataAdapter(sqlQuery, connection);
            adapter.Fill(table);

            if (table.Rows.Count == 0)
                return false;

            int count = Convert.ToInt32(table.Rows[0][0].ToString());

            if (isDecreasing && count > 0)
            {
                count--;
                SQLiteCommand commandUpdate = new SQLiteCommand(String.Format("UPDATE items SET count={0} WHERE id={1}", count, id), connection);
                SQLiteDataReader reader = commandUpdate.ExecuteReader();
                connection.Close();
                return true;
            }

            if(!isDecreasing)
            {
                count++;
                SQLiteCommand commandUpdate = new SQLiteCommand(String.Format("UPDATE items SET count={0} WHERE id={1}", count, id), connection);
                SQLiteDataReader reader = commandUpdate.ExecuteReader();
                connection.Close();
                return true;
            }  

            connection.Close();
            return false;          
        }

        public void AddTransaction(DateTime date, XElement data, int total)
        {
            connection.Open();
            SQLiteCommand commandInsert = new SQLiteCommand(String.Format("INSERT INTO transactions (date, content, total) VALUES ('{0}', '{1}', '{2}')", date.ToString(), data, total.ToString()), connection);
            SQLiteDataReader reader = commandInsert.ExecuteReader();
            connection.Close();
        }
    }
}
