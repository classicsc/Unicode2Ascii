using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Mono.Options;

namespace Unicode2Ascii
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = null;
            string output = null;
            var options = new OptionSet {
                {"f=", "input file", f => input = f},
                {"o=", "output file", f => output = f},
            };
            List<string> extra;
            try {
                extra = options.Parse (args);
            } catch (OptionException e) {
                Console.Write("Invalid arguments");
                return;
            }
            string inputFile = input;
            string outputFile = output;
            using (StreamReader reader = new StreamReader(inputFile, Encoding.Unicode))
            {
                using (StreamWriter writer = new StreamWriter(outputFile, false, new UTF8Encoding(false)))
                {
                    CopyContents(reader, writer);
                }
            }
        }

        static void CopyContents(TextReader input, TextWriter output)
        {
            char[] buffer = new char[8192];
            int len;
            while ((len = input.Read(buffer, 0, buffer.Length)) != 0)
            {
                output.Write(buffer, 0, len);
            }
        }
            
    }
}
