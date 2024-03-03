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

        public void Encode(List<Symbol> symbols)
        {
            if (symbols.Count <= 1)
                return;

            double halfSum = symbols.Sum(s => s.Probability) / 2;
            double sum = 0;

            List<Symbol> firstList = new List<Symbol>();
            List<Symbol> secondList = new List<Symbol>();

            foreach(var item in symbols)
            {
                if (sum + item.Probability <= halfSum || firstList.Count==0)
                    firstList.Add(item);
                else
                    secondList.Add(item);
                sum += item.Probability;
            }

            _addCode(firstList, "0");
            _addCode(secondList, "1");

            Encode(firstList);
            Encode(secondList);
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

        private void SplitSymbols(List<Symbol> symbols)
        {
            if (symbols.Count <= 1)
            {
                return;
            }

            double totalProbability = symbols.Sum(s => s.Probability);
            double currentSum = 0;
            double halfSum = totalProbability / 2;

            var firstHalf = new List<Symbol>();
            var secondHalf = new List<Symbol>();

            foreach (var symbol in symbols)
            {
                if (currentSum + symbol.Probability <= halfSum)
                {
                    firstHalf.Add(symbol);
                }
                else
                {
                    secondHalf.Add(symbol);
                }
                currentSum += symbol.Probability;
            }

            foreach (var symbol in firstHalf)
            {
                symbol.Code += "0";
            }

            foreach (var symbol in secondHalf)
            {
                symbol.Code += "1";
            }

            SplitSymbols(firstHalf);
            SplitSymbols(secondHalf);
        }

        private void _addCode(List<Symbol> symbols,string ncode)
        {
            foreach (var symbol in symbols)
            {
                symbol.Code += ncode;
            }
        }

        public void Show(List<Symbol> list)
        {
            foreach (var item in list)
                Console.WriteLine($"Char:{item.Character}\tProp:{item.Probability}\tCode{item.Code}");
        }

    }
}
