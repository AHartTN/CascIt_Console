using System.Collections.Generic;
using System.Linq;

namespace CascIt_Console
{
    #region Imports

    using System;
    using System.CodeDom;
    using System.Text;
    using System.Text.RegularExpressions;

    #endregion
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.SetBufferSize(1024, short.MaxValue - 1);

            //IReadOnlyCollection<int> range = Enumerable.Range(48, 57)
            //                                           .Union(Enumerable.Range(97, 122))
            //                                           .Union(new[] { 45, 95 }).ToArray();
            //IReadOnlyCollection<char> rangeChars = range.Select(s => (char)s).ToArray();

            //int maxLength = 5;

            //List<string> permutations = new List<string>();

            //for (int i = 1; i <= maxLength; i++)
            //{
            //    IEnumerable<IEnumerable<char>> test = StringGeneration.GetPermutations(rangeChars, i);

            //    permutations.AddRange(test.Select(t => new string(t.ToArray())));
            //}

            //permutations = permutations.Distinct().ToList();

            //Console.WriteLine($"{permutations.Count} total permutations");

            //string[] urls = { CASCEngine.test0, CASCEngine.test1, CASCEngine.test2, CASCEngine.test3, CASCEngine.test4, CASCEngine.test5, CASCEngine.test6, CASCEngine.test7 };

            //foreach(string url in urls)
            //{
            //    string result = Traffic.HTTPGET(url);
            //    if (!result.IsNullOrWhiteSpace())
            //    {
            //        using (StreamWriter sw = new StreamWriter(File.OpenWrite(string.Format(@"C:\Users\Anthony\Desktop\UrlOutput\Output_{0}.txt", "XXXX"))))
            //        {
            //            sw.WriteLine(result);
            //        }
            //        Console.WriteLine(result);
            //    }
            //}

            CASCEngine.ProcessEverything();

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("APPLICATION HAS FINISHED! PRESS ANY KEY TO EXIT!");
            Console.ReadKey();
        }
    }
}