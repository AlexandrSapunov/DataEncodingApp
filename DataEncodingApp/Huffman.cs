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
            public Element() { }

            public void GetCode()
            {
                if (Elements.Count == 2)
                {
                    Elements = Elements.OrderByDescending(x => x.Sum).ToList();
                    Elements[0].Code += Code+"1";
                    Elements[1].Code += Code+"0";
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
        public void Show(List<Element> elements)
        {
            Console.WriteLine("result list");
            foreach(var item in elements)
            {
                Console.WriteLine($"Символ:[{item.Symbol}]\tВероятность:[{item.Sum}]\tКод:[{item.Code}]");
            }
        }

        public List<Element> HuffmanMethod(List<Element> elements)
        {
            List<Element> nodes = elements.Select(element => new Element
            {
                Symbol = element.Symbol,
                Sum = element.Sum,
                Code = "",
                Elements = new List<Element>()
            }).ToList();

            while (nodes.Count > 1)
            {
                nodes = nodes.OrderBy(node => node.Sum).ToList();

                Element first = nodes[0];
                Element second = nodes[1];

                foreach (Element node in first.Elements)
                {
                    node.Code = "0" + node.Code;
                }
                first.Elements.Add(first);
                first.Elements.AddRange(second.Elements.Select(node =>
                {
                    node.Code = "1" + node.Code;
                    return node;
                }));

                first.Sum += second.Sum;
                first.Elements.AddRange(second.Elements);
                nodes.Remove(second);
            }

            return nodes;
        }

    }


}
