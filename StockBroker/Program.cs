/*//////////////
/// I tried to keep the syntax basic so it can be 'lifted' into Java land.
/// -----------------------------------
/// This is an imaginary system that is called by another system, e.g. it isn't intended to be
/// used by a human. It is a portfolio updater for a stock broker. A stock broker attempts to sell
/// shares in stocks to a client and then will submit the shares traded throughout the day to an exchange
/// keeping the profit that has been accrued throughout the day's trading.
/// 
/// This code is for updating the stock level that a broker currently has in a preset portfolio cache.
/// It needs to be updated to include a price update feature, but currently it is a mess :-(
/// You should go through the process of tidying the code so that you can add the [NEW FEATURE].
/// There is *just one* Approval added, execute by running the program.
/// -----------------------------------
/// You are a consultant just placed on this client, his 15 year old son made this system in his bedroom 
/// during the summer holidays. It works ( but this exercise was a rush job so possibility of bugs remains ) 
/// just work on fixing code style, structure, naming, and add lack of constructs while you surround it with 
/// more tests. The database is just pretend, don't worry about that code, just focus on the Updater.
/// -----------------------------------
/// Most importantly, have fun!
///////////////*/

using System;
using System.IO;
using System.Text;

namespace StockBroker
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = new string[] {"msft:500", "appl:200", "alph:100"};
            }

            var app = new Approval();
            app.Verify(args);
        }
    }

    public class Approval
    {
        // might want to put this in a known location at the moment it is in the binaries output
        // but also at the solution level.
        string expected = "expected.txt"; 

        public void Verify(string[] input)
        {
            string[] result = Execute(input); // perform update and save
            
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string line in result)
            {
                stringBuilder.AppendLine(line);
            }

            var expectedOutput = File.ReadAllText(expected);
            var actualOutput = stringBuilder.ToString();
            if (actualOutput != expectedOutput)
            {
                Console.WriteLine();
                Console.WriteLine("Failed:");
                Console.WriteLine();
                Console.WriteLine($"Output expected: {expectedOutput}");
                Console.WriteLine($"====================================");
                Console.WriteLine($"Output was: {actualOutput}");
                Console.WriteLine();
                Console.WriteLine("Doesn't work, try again...");
            }
            else
            {
                Console.WriteLine("All passed!");
            }
        }

        public string[] Execute(string[] input)
        {
            Updater updater = new Updater();
            
            foreach (var i in input)
            {
                updater.Update(i);
            }
            
            updater.Persist();
            return updater.GetAll();;
        }

    }

}