using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Text.RegularExpressions;

namespace pdfparser
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length != 3)
            {
                Console.WriteLine("Corriendo por defecto las dos bases de datos");
                Console.WriteLine("Intente algo como esto para correr por separado:");
                Console.WriteLine("    pdfparser c3 c3.pdf c3.csv");
                Console.WriteLine("    pdfparser p4 p4.pdf p4.csv");
                Console.WriteLine("Corriendo c3parser ...");
                C3Parser c3parser = new C3Parser("c3.pdf", "c3.csv");
                c3parser.Init();
                c3parser.ToCSV();
                Console.WriteLine("Corriendo p4parser ...");
                P4Parser p4parser = new P4Parser("p4.pdf", "p4.csv");
                p4parser.Init();
                p4parser.ToCSV();
                Console.WriteLine("Listo, presione enter para continuar");
            }
            else
            {
                if(args[0] == "c3")
                {
                    C3Parser c3parser = new C3Parser(args[1], args[2]);
                    c3parser.Init();
                    c3parser.ToCSV();
                }
                else if (args[0] == "p4")
                {
                    P4Parser p4parser = new P4Parser(args[1], args[2]);
                    p4parser.Init();
                    p4parser.ToCSV();
                }
                Console.WriteLine("Listo, presione enter para continuar");
            }
            Console.ReadLine();
        }

        public static string ExtractTextFromPdf(string path)
        {
            using (PdfReader reader = new PdfReader(path))
            {
                StringBuilder text = new StringBuilder();

                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    text.Append(PdfTextExtractor.GetTextFromPage(reader, i));
                }

                return text.ToString();
            }
        }
    }
}
