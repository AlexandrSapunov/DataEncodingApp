using System;
using System.Collections.Generic;
using System.Text;

namespace DataEncodingApp.LempelZiva
{
    public class LZ77
    {
        private int _bufferSize; 
        private int _dictionarySize;
        private List<ExecutionLog> Logs { get; set; }

        private char[] Dictionary { get; set; }
        private char[] Buffer { get; set; }

        private class Code
        {
            public int Offset { get; set; }  //смещение (offset) от начала сообщения,
                                             //оно указывает на то,
                                             //сколько символов назад нужно искать совпадение.
            public int Lenght { get; set; }  //длина совпадающей цепочки (length),
                                             //указывает на количество символов,
                                             //которые совпадают с предыдущей цепочкой.
            public string Symbol { get; set; }//следующий символ,
                                              //который не повторяется в предыдущих элементах кодирования.

            public Code(int offset, int leght, string symbol)
            {
                Offset = offset;
                Lenght = leght;
                Symbol = symbol;
            }
            public Code(Code code)
            {
                Offset = code.Offset;
                Lenght = code.Lenght;
                Symbol = code.Symbol;
            }
        }

        public LZ77(int bufferSize, int dictionarySize)
        {
            _bufferSize = bufferSize;
            _dictionarySize = dictionarySize;
            Dictionary = new char[_dictionarySize];
            Buffer = new char[_bufferSize];
            Logs = new List<ExecutionLog>();
        }

        public void Encode(string text)
        {
            bool isBegin = true;
            if (text.Length < _bufferSize)
            {
                Console.WriteLine("Длина строки не может быть больше размера буфера!");
                return;
            }
            _setTextToBuffer(text);
            if (isBegin)
            {
                isBegin = false;
                _FShow(Dictionary, Buffer, true);
            }
            else
                _FShow(Dictionary, Buffer, false);

            //https://intuit.ru/studies/courses/2256/140/lecture/3914
            //https://www.techlibrary.ru/b/2t1j1e1p1c1s1l1j1k_2j.2j._3a1f1p1r1j2g_1j1o1v1p1r1n1a1x1j1j._2004.pdf
        }


        private void _getCode(int textSize)
        {
            for(int i = 0; i < textSize; i++)
            {

            }
        }

        private void _setTextToBuffer(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                Buffer[i] = text[i];
            }
        }
        private class ExecutionLog
        {
            public char[] Dictionary { get; set; }
            public char[] Buffer { get; set; }
            public Code ResultCode { get; set; }

            public ExecutionLog(char[] dictionary, char[] buffer, Code code)
            {
                Dictionary = dictionary;
                Buffer = buffer;
                ResultCode = new Code(code);
            }

        }
        private void _show(char[] arr)
        {
            foreach (var item in arr)
            {
                Console.Write($" {item} ");
            }
        }
        private void _showNumber(int number)
        {
            for (int i = 0; i < number; i++)
            {
                Console.Write($"[{i + 1}]");
            }
        }
        private void _FShow(char[] dictionary, char[] buffer, bool isBegin)
        {
            if (isBegin)
            {
                Console.WriteLine($"\tСловарь({_dictionarySize})\t\t\tБуфер({_bufferSize})");
                _showNumber(_dictionarySize);
                Console.Write("\t");
                _showNumber(_bufferSize);
                Console.WriteLine();
            }
            _show(Dictionary);
            Console.Write("\t");
            _show(Buffer);
        }

    }
}
