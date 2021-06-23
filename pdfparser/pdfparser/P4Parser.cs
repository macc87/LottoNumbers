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
    public class P4Parser
    {
        /// <summary>
        /// 12/27/17 E 8- 8 - 7 - 1
        /// </summary>
        Regex entryRegex = new Regex(@"\d\d/\d\d/\d\d\s(E|M)\s\d-\s\d\s-\s\d\s-\s\d");
        /// <summary>
        /// 08/10/15
        /// </summary>
        Regex dateRegex = new Regex(@"\d\d/\d\d/\d\d");
        /// <summary>
        /// E
        /// </summary>
        Regex momentRegex = new Regex(@"E|M");
        /// <summary>
        ///  8
        /// </summary>
        Regex numberRegex = new Regex(@"\s\d");

        string pdfPath;
        string outputPath;

        string pdfContent;

        public P4Parser(string pdfC3Name)
        {
            pdfPath = pdfC3Name + ".pdf";
            outputPath = pdfC3Name + ".csv";
        }
        public P4Parser(string pdfInputFilePath, string outputCSVFile)
        {
            pdfPath = pdfInputFilePath;
            outputPath = outputCSVFile;
        }


        public string Init()
        {
            pdfContent = ExtractTextFromPdf(pdfPath);
            return pdfContent;
        }

        public void ToCSV()
        {
            FileStream fs = new FileStream(outputPath, FileMode.OpenOrCreate);
            StreamWriter sw = new StreamWriter(fs);
            sw.WriteLine("Fecha,Momento,D1,U1,D2,U2");

            Regex singleNumberRegex = new Regex(@"\d");

            var entries = entryRegex.Matches(pdfContent);
            foreach (var entry in entries)
            {
                var entryText = entry.ToString();
                var date = dateRegex.Match(entryText).ToString();
                var moment = momentRegex.Match(entryText).ToString();
                var numbers = numberRegex.Matches(entryText);
                var d1 = singleNumberRegex.Match(numbers[0].ToString());
                var u1 = singleNumberRegex.Match(numbers[1].ToString());
                var d2 = singleNumberRegex.Match(numbers[2].ToString());
                var u2 = singleNumberRegex.Match(numbers[3].ToString());
                sw.WriteLine(String.Format("{0},{1},{2},{3},{4},{5}", date, moment, d1, u1, d2, u2));
            }

            sw.Close();
            fs.Close();
        }

        private string ExtractTextFromPdf(string path)
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
