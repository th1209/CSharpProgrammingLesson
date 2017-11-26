using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter4C
{
    class Program
    {
        public static void Solve()
        {
            var dic = new Dictionary<string, int>();

            int n = int.Parse(Console.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                switch (inputs[0])
                {
                    case "insert":
                        if (! dic.ContainsKey(inputs[1]))
                            dic[inputs[1]] = 1;
                        break;
                    case "find":
                        Console.WriteLine(dic.ContainsKey(inputs[1]) ? "yes" : "no");
                        break;
                    default:
                        throw new System.IO.IOException("");
                }
            }
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/04_Search_C_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}