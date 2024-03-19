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
            {                                                                   //                  |sum    code|               |sum      code|            |sum     code|
                new Symbol { Character = " ", Probability = 0.145, Code = "" }, //------------------|0,145     0|               |0,145      00|            |0,145    000|
                new Symbol { Character = "о", Probability = 0.095, Code = "" }, // sum = 1,002      |0,24      0| sum = 0,498   |0,24       00|            |0,095    001|
                                                                                //              здесь текущапя сумма <halfsum   |-------------|            |------------|
                new Symbol { Character = "е", Probability = 0.074, Code = "" }, // halfsum =  0,501 |0,314     0| hsum= 0,249   |  0,074    01|            |0,074 ?  010|
                new Symbol { Character = "а", Probability = 0.064, Code = "" }, //                  |0,378     0|               |  0,138    01|sum = 0,258 |0,064 ?  011| sum  0,184          | 0,064    0110
                new Symbol { Character = "и", Probability = 0.064, Code = "" }, //                  |0,442     0|               |  0,202    01|hsum = 0,129|0,128 ?  011| hsum 0,092          | 0,056    0111   01110
                new Symbol { Character = "т", Probability = 0.056, Code = "" }, //------------------|0,498-----0|---------------|  0,258    01|            |0,184 ?  011|                     | 0,12     0111   01111
                new Symbol { Character = "н", Probability = 0.056, Code = "" }, //                  |0,554      |         
                new Symbol { Character = "с", Probability = 0.047, Code = "" }, //                                                                 как должно работать ???
                new Symbol { Character = "р", Probability = 0.041, Code = "" }, //                      (текущая сумма + вероятность след ел.<= полсуммы)    или  (текущая сумма <= пол суммы)?
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
                new Symbol { Character = "ь,ъ",Probability = 0.015,Code = "" },
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
            /*
            ShannonFano sf = new ShannonFano();
            var sortList1 = symbolAlpavit.OrderByDescending(x => x.Probability).ToList();
            var sortList2 = symbols.OrderByDescending(x => x.Probability).ToList();
            sf.EncodeV2(ref sortList1);
            sf.EncodeV2(ref sortList2);
            Console.WriteLine("Alphavit:");
            sf.Show(sortList1);
            Console.WriteLine("first example:");
            sf.Show(sortList2);
            Console.ReadKey();
            //Console.WriteLine(sf.CalculateEntropy(symbolAlpavit));
            //Console.WriteLine(sf.CalculateMean(symbolAlpavit));
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
            huffman.Encode(ref elements);
            huffman.Show(elements);
            
            
            //LZ77 lz = new LZ77(7,9);
            //lz.Encode("ЗЕЛЕНАЯ");
            Console.ReadKey();
        }
    }
}
