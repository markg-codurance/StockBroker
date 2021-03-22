using System;

namespace StockBroker
{
    // This is responsible for updating the shares a stock broker has remaining in his portfolio
    public class Updater
    {
        // current portfolio cache of prices for each of the stock
        public string[] entries = new string[]
        {
            "msft:1500:$558",
            "appl:1200:$782",
            "alph:1300:$596",
        };

        
        // update the remaining shares for a given entry
        public void Update(string entry)
        {
            for (int index = 0; index < entries.Length; index++)
            {
                string[] share = entries[index].Split(':');
                if (share[0] == entry.Split(":")[0])
                {
                    if (Convert.ToInt32(share[1]) < 1)
                    {
                        throw new Exception();
                    }

                    string val = (Convert.ToInt32(share[1]) - Convert.ToInt32(entry.Split(":")[1])).ToString();

                    // [NEW FEATURE]: when the stock level is below 100 the price should increase by 10%
                    entries[index] = share[0] + ":" + val + ":" + share[2];
                }
            }
        }

        public void Persist()
        {
            var database = Database.GetInstance();
            for (int i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                database.Add(entry);
            }
        }

        public string[] GetAll()
        {
            return Database.GetInstance().GetAll();
        }
    }
}