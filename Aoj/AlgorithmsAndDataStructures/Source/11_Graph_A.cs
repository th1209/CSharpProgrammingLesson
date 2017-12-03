using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter11A
{
    class DirectedGraph
    {
        // Map[s][t]と表現する時、 s -> tならば Map[s][t] >= 1 とする。
        private int[][] Map;

        public DirectedGraph(int vertexNum)
        {
            Map = new int[vertexNum + 1][];
            for (int i = 0; i <= vertexNum; i++)
            {
                Map[i] = new int[vertexNum + 1];
            }

            for (int i = 0; i <= vertexNum; i++)
            {
                for (int j = 0; j <= vertexNum; j++)
                {
                    Map[i][j] = 0;
                }
            }
        }

        public void Connect(int from, int to)
        {
            if (OutOfRange(from) || OutOfRange(to))
                throw new ArgumentException("");
            Map[from][to] = 1;
        }

        public int Connected(int from, int to)
        {
            if (OutOfRange(from) || OutOfRange(to))
                throw new ArgumentException("");
            return Map[from][to];
        }

        private bool OutOfRange(int index)
        {
            return index < 0 || index > Map.Count();
        }
    }
    class Program
    {
        public static void Solve()
        {
            int n = int.Parse(Console.ReadLine());

            var graph = new DirectedGraph(n);

            for (int i = 0; i < n; i++)
            {
                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                var from = inputs[0];
                foreach (var to in inputs.Skip(2).ToArray())
                {
                    graph.Connect(from, to);
                }
            }

            var tmp = new string[n];
            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    tmp[j - 1] = graph.Connected(i, j).ToString();
                }
                Console.WriteLine(string.Join(" ", tmp));
            }
        }


        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/11_Graph_A_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}