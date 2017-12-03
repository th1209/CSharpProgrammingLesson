using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter5A
{
    class Program
    {
        // public static List<int> ns;
        // public static List<int> ms;
        // public static int n;
        // public static int q;
        // public static Dictionary<int, bool>[] dp;

        // 以下、普通に再帰でゴリ押したプログラム。
        // public static bool Solve(int current, int answer, int i)
        // {
        //     if (current == answer)
        //         return true;

        //     if (i == n)
        //         return false;

        //     return Solve(current, answer, i + 1) || Solve(current + ns[i], answer, i + 1);
        // }

        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/05_Recursion_A_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     n = int.Parse(Console.ReadLine());
        //     ns = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();

        //     q = int.Parse(Console.ReadLine());
        //     ms = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();

        //     for (int i = 0; i < q; i++)
        //     {
        //         bool result = Solve(0, ms[i], 0);
        //         Console.WriteLine(result ? "yes" : "no");
        //     }

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }

        // 以下、動的計画法バージョン。
        // public static bool Solve(int current, int answer, int i)
        // {
        //     if (dp[i].ContainsKey(current))
        //         return dp[i][current];
            
        //     if (current == answer)
        //         dp[i][current] = true;
        //     else if (i == n)
        //         dp[i][current] = false;
        //     else if (Solve(current, answer, i + 1))
        //         dp[i][current] = true;
        //     else if (Solve(current + ns[i], answer, i + 1))
        //         dp[i][current] = true;
        //     else
        //         dp[i][current] = false;

        //     return dp[i][current];
        // }

        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/05_Recursion_A_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     n = int.Parse(Console.ReadLine());
        //     ns = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();

        //     q = int.Parse(Console.ReadLine());
        //     ms = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();

        //     // 0 ~ n個(n+1)個の要素が必要になることに注意。図を書いてみると明白。
        //     dp = new Dictionary<int, bool>[n + 1];
        //     for (int i = 0; i <= n; i++)
        //     {
        //         dp[i] = new Dictionary<int, bool>();
        //     }

        //     for (int i = 0; i < q; i++)
        //     {
        //         for (int j = 0; j <= n; j++)
        //         {
        //             dp[j].Clear();
        //         }

        //         bool result = Solve(0, ms[i], 0);
        //         Console.WriteLine(result ? "yes" : "no");
        //     }

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}