using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataEncodingApp.LempelZiva
{
    internal class LZ78
    {
        private class Code
        {
            public int DPindex;
            public char Symbol;
            public int DictionaryPosition;

            public Code(int index, char symbol, int dPosition)
            {
                DPindex = index;
                Symbol = symbol;
                DictionaryPosition = dPosition;
            }

            public string GetSymbol(List<Code> items)
            {
                return "";
            }
        }

        private List<Code> _dictionary { get; set; }

        public LZ78()
        {
            _dictionary = new List<Code>();
        }


        public void Encode(string text)
        {
            foreach(var symbol in text)
            {

            }
        }

        private bool _isMatch(char symbol)
        {
            return true;
        }

    }
}
