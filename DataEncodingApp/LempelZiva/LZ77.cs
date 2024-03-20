﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DataEncodingApp.LempelZiva
{
    public class LZ77
    {
        private int _IndexSymbInText = 0;

        private int _bufferSize; 
        private int _dictionarySize;
        private List<ExecutionLog> Logs { get; set; }
        private string _text;

        private char[] charDictionary { get; set; }
        private char[] charBuffer { get; set; }

        private class Code
        {
            public int Offset { get; set; }  //смещение (offset) от начала сообщения,
                                             //оно указывает на то,
                                             //сколько символов назад нужно искать совпадение.
            public int Lenght { get; set; }  //длина совпадающей цепочки (length),
                                             //указывает на количество символов,
                                             //которые совпадают с предыдущей цепочкой.
            public char Symbol { get; set; }//следующий символ,
                                              //который не повторяется в предыдущих элементах кодирования.

            public Code(int offset, int leght, char symbol)
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
            charDictionary = new char[_dictionarySize];
            charBuffer = new char[_bufferSize];
            Logs = new List<ExecutionLog>();
        }

        public void Encode(string text)
        {
            _text = text;
            bool isBegin = true;

            _setTextToBuffer(text);
            if (isBegin)
            {
                _FShow(charDictionary, charBuffer, isBegin);
                isBegin = false;
            }
            else
                _FShow(charDictionary, charBuffer, isBegin);
            while (_IndexSymbInText <= text.Length)
            {
                //перенести n-количество сиволов в словарь
                //если символы встречаются добавляем его с началом словоря где встречается эти символы
                //[1] [2] [3] [4] [5] [6] [7] [8] [9] | [1] [2] [3] [4] [5] [6] [7] |  Код
                // _   _   _   _   _   _   З   Е   Л  |  Е   Н   А   Я   _   З   Е  | (7,1,H)
                // _   _   _   _   З   Е   Л   Е   Н  |  А   Я   _   З   Е   _   _  | 

                int FirstSymbol = _findMatch(charBuffer[0]);
                int Lenght = _nMatch(charBuffer, FirstSymbol);

                Code gCode = new Code(FirstSymbol, Lenght, charBuffer[0]);
                ExecutionLog log = new ExecutionLog(charDictionary, charBuffer, gCode);
                Logs.Add(log);
                _FShow(charDictionary, charBuffer,gCode, isBegin);
                _moveBuffer(gCode);
            }
        }


        private void _moveBuffer(Code code)
        {
            if (code.Lenght != 0)
            {
                char[] tempsubString = new char[code.Lenght + 1];
                int subStringIndex = 0;
                for (int i = code.Offset; i < code.Offset + code.Lenght; i++)
                {
                    tempsubString[subStringIndex] = charDictionary[i];
                    subStringIndex++;
                }
                //Console.WriteLine($"\n{string.Join("", tempsubString)}");

            }
            else
            {
                _moveDictionaryDefault(_moveBufferDefault()) ;
            }

        }

        private void _moveDictionaryDefault(char symbol)
        {
            for(int i = 1; i < charDictionary.Length; i++)
            {
                charDictionary[i-1]= charDictionary[i];
            }
            charDictionary[charDictionary.Length-1]= symbol;
        }
        private char _moveBufferDefault()
        {
            char symbol = charBuffer[0];
            for (int i = 1; i < charBuffer.Length; i++)
            {
                charBuffer[i - 1] = charBuffer[i];
            }
            if (_IndexSymbInText != 0)
                charBuffer[charBuffer.Length - 1] = _text[_IndexSymbInText];
            else
                charBuffer[charBuffer.Length - 1] = '\0';
            return symbol;
        }

        private int _findMatch(char symbol) //поиск первого совпадающего символа
        {
            for(int i = 0; i < charDictionary.Length; i++)
            {
                if (charDictionary[i] == symbol)
                    return i;
            }
            return 0;
        }

        private int _nMatch(char[] chars, int fSymbol)
        {
            int lenght = 0;
            int bufferi = 0;
            for(int i = fSymbol; i < charDictionary.Length; i++)
            {
                if (charDictionary[i] == charBuffer[0])
                {
                    lenght++;
                    bufferi++;
                }
                else
                    return lenght;
            }
            return lenght;

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
        private void _FShow(char[] dictionary, char[] buffer,Code code, bool isBegin)
        {
            if (isBegin)
            {
                Console.WriteLine($"\tСловарь({_dictionarySize})\t\t\tБуфер({_bufferSize})\tКод");
                _showNumber(_dictionarySize);
                Console.Write("\t");
                _showNumber(_bufferSize);
                Console.WriteLine();
            }
            _show(charDictionary);
            Console.Write("\t");
            _show(charBuffer);
            Console.Write("\t");
            Console.Write($"({code.Offset},{code.Lenght},{code.Symbol})");
            Console.WriteLine();
        }

        private void _FShow(char[] dictionary, char[] buffer, bool isBegin)
        {
            if (isBegin)
            {
                Console.WriteLine($"\tСловарь({_dictionarySize})\t\t\tБуфер({_bufferSize})\tКод");
                _showNumber(_dictionarySize);
                Console.Write("\t");
                _showNumber(_bufferSize);
                Console.WriteLine();
            }
            _show(charDictionary);
            Console.Write("\t");
            _show(charBuffer);
            Console.WriteLine();
        }


        private void _setTextToBuffer(string text)
        {
            //int max = text.Length > Buffer.Length ? Buffer.Length : text.Length;
            int max = 0;
            if (charBuffer.Length < text.Length)
            {
                max = charBuffer.Length;
                _IndexSymbInText = charBuffer.Length;
            }
            else
            {
                max = text.Length;
                _IndexSymbInText = 0;
            }

            for (int i = 0; i < max; i++)
            {
                charBuffer[i] = text[i];
            }
        }
    }
}
