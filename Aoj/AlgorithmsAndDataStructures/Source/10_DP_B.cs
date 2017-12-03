using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter10B
{
    class Program
    {
        /// <summary>
        /// 最長共通部分列(LongestCommonSubsequence)を解く。
        /// </summary>
        public static int SolveLCS(string str1, string str2)
        {
            int m = str1.Length;
            int n = str2.Length;

            // dpテーブルの生成。
            int[][] c = new int[m + 1][];
            for (int i = 0; i <= m; i++)
            {
                c[i] = new int[n + 1];
            }

            // str1, str2 どちらかが0の場合を初期化しておく。
            for (int i = 0; i <= m; i++)
            {
                c[i][0] = 0;
            }
            for (int i = 0; i <= n; i++)
            {
                c[0][i] = 0;
            }

            // 漸化式を使って解く。
            for (int i = 1; i <= m; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    if (str1[i - 1] == str2[j - 1])
                    {
                        c[i][j] = c[i - 1][j - 1] + 1;
                    }
                    else
                    {
                        c[i][j] = Math.Max(c[i - 1][j], c[i][j - 1]);
                    }
                }
            }

            return c[m][n];
        }

        public static void Solve()
        {
            int n = int.Parse(Console.ReadLine()) * 2;

            for (int i = 0; i < n; i += 2)
            {
                string str1 = Console.ReadLine();
                string str2 = Console.ReadLine();

                int lcs = SolveLCS(str1, str2);

                Console.WriteLine(lcs);
            }
        }


        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/10_DP_B_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}