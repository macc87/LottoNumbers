using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;


namespace plays2csv
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex g = new Regex(@"\d\d/\d\d");
            //Regex g = new Regex(@"\d");

            // 1.
            // Open file for reading.
            using (StreamReader r = new StreamReader("c3.pdf"))
            {
                // 2.
                // Read each line until EOF.
                string line;
                while ((line = r.ReadLine()) != null)
                {
                    // 3.
                    // Do stuff with line.
                    var match = g.Match(line);
                    if(match.Length > 0)
                        Console.WriteLine(match);
                }
            }
        }
    }
}
