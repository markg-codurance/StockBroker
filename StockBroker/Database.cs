using System.Collections.Generic;
using System.Linq;

namespace StockBroker
{
    public class Database
    {
        private static Database instance;
        private Dictionary<string,string> entries;

        private Database()
        {
            entries = new Dictionary<string, string>
            {
                {"msft", "1500:$558"}, 
                {"appl", "1200:$782"}, 
                {"alph", "1300:$596"}
            };
        }

        public static Database GetInstance()
        { 
            if (instance == null)
            {
                instance = new Database();
                return instance;
            }

            return instance;
        }

        public string[] GetAll()
        {
            var strings = entries.Select(kvp=> $"{kvp.Key}:{kvp.Value}").ToArray();
            return strings;
        }

        public void Add(string entry)
        {
            var i = entry.IndexOf(":");
            var key = entry.Substring(0, i);
            var value = entry.Substring(i+1);
            
            if (entries.ContainsKey(key))
            {
                entries[key] = value;
            }
            else
            {
                entries.Add(key,value);
            }
        }
    }
}