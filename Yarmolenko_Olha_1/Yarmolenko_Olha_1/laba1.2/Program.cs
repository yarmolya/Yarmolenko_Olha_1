using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace laba1
{
    class Complex_num
    {
        public double r, i;

        public Complex_num()
        {
            this.r = 0.0;
            this.i = 0.0;
        }


        public static Complex_num operator +(Complex_num a, Complex_num b)
        {
            return new Complex_num()
            {
                r = b.r + a.r,
                i = b.i + a.i
            };
        }

        public static Complex_num operator -(Complex_num a, Complex_num b)
        {
            return new Complex_num()
            {
                r = a.r - b.r,
                i = a.i - b.i
            };
        }
        public static Complex_num operator *(Complex_num a, Complex_num b)
        {
            return new Complex_num()
            {
                r = b.r * a.r - b.i * a.i,
                i = b.i + a.r + a.r * b.i
            };
        }

        public override string ToString()
        {
            return String.Format("{0} + {1}i", this.r, this.i);
        }

        public void PrintLine(Complex_num a)
        {
            Console.WriteLine(a);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;



            Complex_num c1 = new Complex_num();
            Complex_num c2 = new Complex_num();
            Console.Write("Введіть дійсну частину першого комплексного числа: ");
            c1.r = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введіть уявну частину першого комплексного числа: ");
            c1.i = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введіть дійсну частину другого комплексного числа: ");
            c2.r = Convert.ToDouble(Console.ReadLine());
            Console.Write("Введіть уявну частину другого комплексного числа: ");
            c2.i = Convert.ToDouble(Console.ReadLine());
            Console.Clear();
            Console.WriteLine("Введені комплексні числа: ");
            c1.PrintLine(c1);
            c2.PrintLine(c2);
            JSON_Serialization(c1, c2);
            Console.WriteLine("Щоб продовжити, натисніть [ENTER]");
            Console.ReadLine();
            Console.Clear();
            JSON_Deserialization();
            c1.PrintLine(c1);
            c2.PrintLine(c2);
            Console.WriteLine("-----------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("Додавання комплексних чисел:       {0} + {1}i", (c1 + c2).r, (c1 + c2).i);
            Console.WriteLine("Множення комплексних чисел:      {0} + {1}i", (c1 * c2).r, (c1 * c2).i);
            Console.WriteLine("Віднімання комплексних чисел:      {0} + {1}i", (c1 - c2).r, (c1 - c2).i);



        }

        public static void JSON_Serialization(Complex_num c1, Complex_num c2)
        {

            string json1 = JsonConvert.SerializeObject(c1.ToString(), Formatting.Indented);
            string json2 = JsonConvert.SerializeObject(c2.ToString(), Formatting.Indented);
            string path = @"/Users/olyayarmolenko/Projects/laba1.2/complex.json";
            using (TextWriter tw = new StreamWriter(path))
            {
                tw.WriteLine(json1);
                tw.WriteLine(json2);
            };
            Console.WriteLine("Дані успішно збережені у JSON файл");


        }

        public static Complex_num JSON_Deserialization()
        {

            string fileName = @"/Users/olyayarmolenko/Projects/laba1.2/complex.json";


            string json1 = File.ReadLines(fileName).First();
            var com1 = JsonConvert.DeserializeObject<String>(json1);
            string json2 = File.ReadLines(fileName).Skip(1).First();
            var com2 = JsonConvert.DeserializeObject<String>(json2);
            Complex_num c1 = StringToComplex(com1);
            Complex_num c2 = StringToComplex(com2);

            return (c1);
            return (c2);


        }

        public static Complex_num StringToComplex(string s)
        {
            Complex_num c = new Complex_num();
            String pattern = @"[-+]?(\d+)([-+*/])(\d+)";
            foreach (Match m in Regex.Matches(s, pattern))
            {
                c.r = Int32.Parse(m.Groups[1].Value);
                c.i = Int32.Parse(m.Groups[3].Value);
                if (m.Groups[0].Value == "-")
                {
                    c.r = c.r * -1;
                }
                if (m.Groups[2].Value == "-")
                {
                    c.i *= -1;
                }
            }
            return c;
        }
    }







}
