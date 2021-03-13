using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;

namespace ConsoleApp2
{
    class Program
    {
        private static Dictionary<string, int> SvcCall(string text)
        {
            using (var client = new ServiceReference.Service1Client())
            {
                var ret = client.GetDict(text);
                Console.WriteLine("Успешное выполнение запроса");
                return ret;
            }
        }
        static void Main(string[] args)
        {
            try
            {
                using (StreamReader sr = new StreamReader("Text.txt"))
                {
                    string text = sr.ReadToEnd();
                    var dict = SvcCall(text);
                    Directory.CreateDirectory("D:\\Temp");
                    using (StreamWriter sw1 = new StreamWriter("D:\\Temp\\Answer.txt", false))
                    {
                        foreach (var key in dict.Keys)
                        {
                            sw1.WriteLine(key + " " + dict[key]);
                        }
                    }
                    Console.WriteLine("Успешное выполнение. Ответ лежит по адресу D:\\Temp");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Ошибка выполнения: " + e.Message);
            }
            Console.ReadLine();
        }
    }
}
