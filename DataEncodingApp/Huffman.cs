using System;
using System.Collections.Generic;
using System.Linq;

namespace DataEncodingApp
{
    public class Huffman
    {
        public class Element
        {
            public string Symbol { get; set; }
            public double Sum {  get; set; }
            public string Code {  get; set; }
            public List<Element> Elements { get; set; }

            public Element(double sum, Element one, Element two)
            {
                Sum = sum;
                Elements = new List<Element> { one, two };
            }
            public Element(string symb, double sum)
            {
                Symbol = symb;
                Sum = sum;
                Elements = new List<Element>();
            }
        }


        public void Encode(List<Element> elements)
        {
            if (elements.Count <= 2)
                return;
            List<Element> tempItems = new List<Element>(elements);

            //Show(elements);

            Element element = new Element(
                tempItems[tempItems.Count() - 1].Sum + tempItems[tempItems.Count() - 2].Sum, //сумма последних двух эелементов
                tempItems[tempItems.Count()-1], //последний элемент
                tempItems[tempItems.Count() - 2] //предпоследний элемент
                );

            tempItems.Remove(tempItems[tempItems.Count() - 1]); //убираем элементы участвующие в сложении
            tempItems.Remove(tempItems[tempItems.Count()-1]);
            tempItems.Add(element); //добавляем получившийся
            tempItems = tempItems.OrderByDescending(x => x.Sum).ToList(); 

            //Console.WriteLine("Result list");
            //Show(tempItems);
            Encode(tempItems);

            GetCode(element.Elements,"1");
            if(tempItems.Count==2)
                tempItems[1].Code+="0";
            element.Code += "1";
            Console.WriteLine($"Current elements:{element.Sum} {element.Code}");
            tempItems.Remove(element);
            tempItems.AddRange(ReturnElement(element));

            Show(tempItems);
            
        }

        private void GetCode(List<Element> elements, string code)
        {
            foreach (var item in elements)
                item.Code += code;
        }
        private List<Element> ReturnElement(Element element)
        {
            return element.Elements;
        }

        public void Show(List<Element> elements)
        {
            Console.WriteLine("result list");
            foreach(var item in elements)
            {
                Console.WriteLine($"Символ:[{item.Symbol}]\tВероятность:[{item.Sum}]\tКод:[{item.Code}]");
            }
        }
    }
}
