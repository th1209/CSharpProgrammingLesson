using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter12B
{
    enum Color
    {
        WHITE, GRAY, BLACK
    }

    class Program
    {
        /// <summary>
        /// ダイクストラのアルゴリズムにより、始点sから各頂点への最短経路を求める。
        /// </summary>
        /// <param name="m">グラフの隣接行列表現。</param>
        /// <param name="n">頂点の個数。</param>
        /// <param name="s">始点。</param>
        /// <returns></returns>
        public static int[] Dijkstra(int[][]m, int n, int s)
        {
            // 始点sからvまでの最小距離。
            var d = new int[n];

            // 頂点vの最短経路における親の頂点番号。
            var p = new int[n];

            // 頂点vの探索状態。
            var c = new Color[n];

            // 各要素を初期化する。
            for (int i = 0; i < n; i++)
            {
                d[i] = Int32.MaxValue;
                p[i] = -1;
                c[i] = Color.WHITE;
            }

            d[s] = 0;

            while (true)
            {
                int current = -1;
                int minCost = Int32.MaxValue;

                // GRAYの頂点のうち、最もコストが低い頂点を選択。
                for (int i = 0; i < n; i++)
                {
                    if (c[i] != Color.BLACK && d[i] < minCost)
                    {
                        current = i;
                        minCost = d[i];
                    }
                }

                // 全頂点がBLACKになった時点で抜ける。
                if (current == -1) break;

                // 隣接する頂点の最小距離が更新できれば更新し、GRAYに塗り替える。
                for (int next = 0; next < n; next++)
                {
                    if (c[next] != Color.BLACK && m[current][next] != -1)
                    {
                        if (d[current] + m[current][next] < d[next])
                        {
                            d[next] = d[current] + m[current][next];
                            p[next] = current;
                            c[next] = Color.GRAY;
                        }
                    }
                }

                // 現在の頂点を探索済みにする。
                c[current] = Color.BLACK;
            }

            return d;
        }

        public static void Solve()
        {
            int n = int.Parse(Console.ReadLine());

            var m  = new int[n][];
            for (int i = 0; i < n; i++)
            {
                m[i] = Enumerable.Repeat<int>(-1, n).ToArray();
            }

            for (int i = 0; i < n; i++)
            {
                int[] input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                int u = input[0];
                for (int j = 2; j < input.Count(); j += 2)
                {
                    int v = input[j];
                    int d = input[j+1];
                    m[u][v] = d;
                }
            }

            int[] answer = Dijkstra(m, n, 0);
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("{0} {1}", i, answer[i]);
            }
        }

        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/12_Graph_B_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}