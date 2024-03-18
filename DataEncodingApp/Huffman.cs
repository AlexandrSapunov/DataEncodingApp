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

            public void GetCode()
            {
                if (Elements.Count == 2)
                {
                    Elements = Elements.OrderByDescending(x => x.Sum).ToList();
                    Elements[0].Code += Code+"1";
                    Elements[1].Code += Code +"0";
                }
            }
        }


        public void Encode(ref List<Element> elements)
        {
            if (elements.Count() ==1)
                return;
            Element element = new Element(
                elements[elements.Count() - 1].Sum + elements[elements.Count() - 2].Sum, //сумма последних двух эелементов
                elements[elements.Count()-1],  //последний элемент
                elements[elements.Count() - 2] //предпоследний элемент
                );
            elements.Remove(elements[elements.Count() - 1]); //убираем элементы участвующие в сложении
            elements.Remove(elements[elements.Count()-1]);
            elements.Add(element); //добавляем получившийся
            elements = SortElementsBySumDescending(elements);
            Encode(ref elements);


            element.GetCode();
            elements.AddRange(element.Elements);
            elements.Remove(element);
            
        }
        public List<Element> SortElementsBySumDescending(List<Element> elements)
        {
            elements.Sort((x, y) => y.Sum.CompareTo(x.Sum));
            foreach (var element in elements)
            {
                if (element.Elements != null && element.Elements.Any())
                {
                    element.Elements = SortElementsBySumDescending(element.Elements);
                }
            }
            return elements;
        }

        private void GetCode(List<Element> elements, string code)
        {
            foreach (var item in elements)
                item.Code += code;
        }
        private List<Element> ReturnElement(Element element)
        {
            foreach (var item in element.Elements)
                item.Code = element.Code;
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


//Console.WriteLine("Result list");
//Show(tempItems);
//Show(elements);
/*
if (elements.Count() == 2)
{
    element.Code += "1";
    GetCode(element.Elements, "1");
    foreach(var item in elements)
    {
        if (item != element)
        {
            item.Code += "0";
            GetCode(item.Elements, "0");
        }
    }
}

element.GetCode();

elements.AddRange(ReturnElement(element));
elements.Remove(element);
Console.WriteLine($"\n\tSum:[{element.Sum}]\tCode:[{element.Code}]\n");
*/