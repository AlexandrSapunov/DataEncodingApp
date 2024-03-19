using System;
using System.Collections.Generic;
using System.Linq;

namespace DataEncodingApp
{
    public class ShannonFano
    {
        public class Symbol
        {
            public string Character { get; set; }
            public double Probability { get; set; }
            public string Code { get; set; }
        }

        public List<Symbol> Symbols { get; set; }

        public ShannonFano()
        {

        }

        public void Encode(ref List<Symbol> symbols)
        {
            if (symbols.Count < 2)
                return;

            double halfSum = symbols.Sum(s => s.Probability) / 2;
            double sum = 0;

            List<Symbol> firstList = new List<Symbol>();
            List<Symbol> secondList = new List<Symbol>();

            foreach(var item in symbols)
            {
                if (sum < halfSum )
                    firstList.Add(item);
                else
                    secondList.Add(item);
                sum += item.Probability;
            }

            _addCode(firstList, "1");
            _addCode(secondList, "0");

            Encode(ref firstList);
            Encode(ref secondList);
        }

        
        public void EncodeV2(ref List<Symbol> symbols)
        {
            if (symbols.Count() == 1)
                return;
            double hSum = symbols.Sum(x => x.Probability)/2;
            //Console.WriteLine($"Sum:\t{symbols.Sum(x=>x.Probability)}\thalfSum:\t{hSum}");


            List<Symbol> stackOne = new List<Symbol>();
            List<Symbol> stackTwo = new List<Symbol>();
            double sum = 0;

            symbols = symbols.OrderByDescending(s => s.Probability).ToList();
            foreach (var item in symbols)
            {
                sum += item.Probability;
                if (sum <= hSum||stackOne.Count()==0)
                    stackOne.Add(item);
                else
                    stackTwo.Add(item);
                //Console.WriteLine(sum);
            }
           // _showInRow(stackOne);
           // _showInRow(stackTwo);
            _addCode(stackOne, "0");
            _addCode(stackTwo, "1");
            EncodeV2(ref stackOne);
            EncodeV2(ref stackTwo);

        }
        private void _addCode(List<Symbol> symbols,string ncode)
        {
            foreach (var symbol in symbols)
            {
                symbol.Code += ncode;
            }
        }


        public double CalculateEntropy(List<Symbol> symbols)
        {
            double entropy = 0;
            foreach (var symbol in symbols)
            {
                entropy -= symbol.Probability * Math.Log(symbol.Probability, 2);
            }
            return entropy;
        }
        public double CalculateMean(List<Symbol> symbols)
        {
            double mean = 0;
            foreach (var symbol in symbols)
            {
                mean += symbol.Probability * symbol.Code.Length;
            }
            return mean;
        }
        public void Show(List<Symbol> list)
        {
            foreach (var item in list)
                Console.WriteLine($"Char:{item.Character}\tProp:{item.Probability}\tCode{item.Code}");
        }

        private void _showInRow(List<Symbol> symbols)
        {
            Console.WriteLine($"\n\t stack {symbols}\n");
            foreach(var item in symbols)
            {
                Console.Write($"\t[{item.Character}] [{item.Probability}]");
            }
        }

    }
}
