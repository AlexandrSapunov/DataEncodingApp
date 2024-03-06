using DataEncodingApp.LempelZiva;
using System;
using System.Collections.Generic;
using System.Linq;
using static DataEncodingApp.ShannonFano;

namespace DataEncodingApp
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
            List<Symbol> symbols = new List<Symbol>
            {
                new Symbol { Character = "1", Probability = 0.3, Code = "" },
                new Symbol { Character = "2", Probability = 0.2, Code = "" },
                new Symbol { Character = "3", Probability = 0.15, Code = "" },
                new Symbol { Character = "4", Probability = 0.1, Code = "" },
                new Symbol { Character = "5", Probability = 0.1, Code = "" },
                new Symbol { Character = "6", Probability = 0.05, Code = "" },
                new Symbol { Character = "7", Probability = 0.05, Code = "" },
                new Symbol { Character = "8", Probability = 0.03, Code = "" },
                new Symbol { Character = "9", Probability = 0.02, Code = "" },

            };
            List<Symbol> symbolAlpavit = new List<Symbol>
            {
                new Symbol { Character = " ", Probability = 0.145, Code = "" },
                new Symbol { Character = "о", Probability = 0.095, Code = "" },
                new Symbol { Character = "е", Probability = 0.074, Code = "" },
                new Symbol { Character = "а", Probability = 0.064, Code = "" },
                new Symbol { Character = "и", Probability = 0.064, Code = "" },
                new Symbol { Character = "т", Probability = 0.056, Code = "" },
                new Symbol { Character = "н", Probability = 0.056, Code = "" },
                new Symbol { Character = "с", Probability = 0.047, Code = "" },
                new Symbol { Character = "р", Probability = 0.041, Code = "" },
                new Symbol { Character = "в", Probability = 0.039, Code = "" },
                new Symbol { Character = "л", Probability = 0.036, Code = "" },
                new Symbol { Character = "к", Probability = 0.029, Code = "" },
                new Symbol { Character = "м", Probability = 0.026, Code = "" },
                new Symbol { Character = "д", Probability = 0.026, Code = "" },
                new Symbol { Character = "п", Probability = 0.024, Code = "" },
                new Symbol { Character = "у", Probability = 0.021, Code = "" },
                new Symbol { Character = "я", Probability = 0.019, Code = "" },
                new Symbol { Character = "ы", Probability = 0.016, Code = "" },
                new Symbol { Character = "з", Probability = 0.015, Code = "" },
                new Symbol { Character = "ь,ъ", Probability = 0.015, Code = "" },
                new Symbol { Character = "б", Probability = 0.015, Code = "" },
                new Symbol { Character = "г", Probability = 0.014, Code = "" },
                new Symbol { Character = "ч", Probability = 0.013, Code = "" },
                new Symbol { Character = "й", Probability = 0.010, Code = "" },
                new Symbol { Character = "х", Probability = 0.009, Code = "" },
                new Symbol { Character = "ж", Probability = 0.008, Code = "" },
                new Symbol { Character = "ю", Probability = 0.007, Code = "" },
                new Symbol { Character = "ш", Probability = 0.006, Code = "" },
                new Symbol { Character = "ц", Probability = 0.004, Code = "" },
                new Symbol { Character = "щ", Probability = 0.003, Code = "" },
                new Symbol { Character = "э", Probability = 0.003, Code = "" },
                new Symbol { Character = "ф", Probability = 0.002, Code = "" }
            };

            ShannonFano sf = new ShannonFano();
            sf.Encode(symbols.OrderByDescending(x => x.Probability).ToList());
            sf.Show(symbols);
            Console.WriteLine(sf.CalculateEntropy(symbols));
            Console.WriteLine(sf.CalculateMean(symbols));
            */

            List<Huffman.Element> elements = new List<Huffman.Element>
            {
                new Huffman.Element("1",0.3),
                new Huffman.Element("2",0.2),
                new Huffman.Element("3",0.15),
                new Huffman.Element("4",0.1),
                new Huffman.Element("5",0.1),
                new Huffman.Element("6",0.05),
                new Huffman.Element("7",0.05),
                new Huffman.Element("8",0.03),
                new Huffman.Element("9",0.02),
            };

            Huffman huffman = new Huffman();
            huffman.Encode(elements);
            huffman.Show(elements);

            Console.ReadKey();
        }
    }
}
