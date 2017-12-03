using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter12A
{
    enum Color
    {
        WHITE, GRAY, BLACK
    }

    class Program
    {
        /// <summary>
        /// プリムのアルゴリズムで最小全域木を求める。
        /// </summary>
        /// <param name="m">頂点の隣接行列。</param>
        /// <param name="n">頂点の個数。</param>
        /// <returns></returns>
        public static int Prim(List<List<int>> m, int n)
        {
            // 隣接する辺のうち、最も小さい値を格納する。
            var d = new List<int>();
            // 対象の頂点の親頂点を格納する(始点の値は-1)。
            var p = new List<int>();
            // 探索済み状態。BLACKで探索済み。
            var color = new List<Color>();

            // 各要素を初期化する。
            for (int i = 0; i < n; i++)
            {
                d.Add(Int32.MaxValue);
                p.Add(-1);
                color.Add(Color.WHITE);
            }

            // 最初は適当な頂点から探索を始めるため、d[0]を0にする。
            d[0] = 0;

            int u;
            int minV;
            while (true)
            {
                u = -1;
                minV = Int32.MaxValue;

                // グレーの頂点のうち、d[i]が最も小さい頂点を求める。
                for (int i = 0; i < n; i++)
                {
                    if (minV > d[i] && color[i] != Color.BLACK) {
                        u = i;
                        minV = d[i];
                    }
                }

                // 全ての頂点を探索し終わった時点でbreak。
                if (u == -1) break;

                // 隣接する頂点の最小距離と親頂点を更新しておく。
                for (int i = 0; i < n; i++)
                {
                    if (color[i] != Color.BLACK && m[u][i] != -1) {
                        if (d[i] > m[u][i]){
                            d[i] = m[u][i];
                            p[i] = u;
                            color[i] = Color.GRAY;
                        }
                    }
                }

                // 今回の頂点は探索済みにしておく。
                color[u] = Color.BLACK;
            }

            // 合計を求める。
            int sum = 0;
            for (int i = 0; i < n; i++)
            {
                if (p[i] != -1) sum += m[i][p[i]];
            }
            return sum;
        }

        public static void Solve()
        {
            int n = int.Parse(Console.ReadLine());

            var m  = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                m.Add(new List<int>());
                string line = Console.ReadLine();
                line = line.Substring(1, line.Length - 1);
                m[i] = Array.ConvertAll(line.Split(' '), int.Parse).ToList();
            }

            int answer = Prim(m, n);
            Console.WriteLine(answer);
        }

        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/12_Graph_A_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}