using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter10A
{
    class Program
    {
        public static int[] dp;
        public static int Fibonacci(int n)
        {
            if (n < 0)
                throw new System.ArgumentException("");

            if (dp[n] > 0)
                return dp[n];

            if (n == 0 || n == 1)
            {
                dp[n] = 1;
                return dp[n];
            }

            dp[n] = Fibonacci(n - 2) + Fibonacci(n - 1);

            return dp[n];
        }

        public static void Solve()
        {
            int n = int.Parse(Console.ReadLine());

            dp = new int[n + 1];

            int f = Fibonacci(n);
            Console.WriteLine(f);
        }

        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/10_DP_A_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}