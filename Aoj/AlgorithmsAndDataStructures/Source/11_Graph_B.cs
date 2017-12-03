using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter11B
{
    class DirectedGraph
    {
        // Map[s][t]と表現する時、 s -> tならば Map[s][t] >= 1 とする。
        private int[][] Map;

        public int[] D;
        public int[] F;
        private int Counter;

        public DirectedGraph(int vertexNum)
        {
            Map = new int[vertexNum][];
            for (int i = 0; i < vertexNum; i++)
            {
                Map[i] = new int[vertexNum];
            }

            for (int i = 0; i < vertexNum; i++)
            {
                for (int j = 0; j < vertexNum; j++)
                {
                    Map[i][j] = 0;
                }
            }

            D = Enumerable.Repeat<int>(0, vertexNum).ToArray();
            F = Enumerable.Repeat<int>(0, vertexNum).ToArray();

            Counter = 0;
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
            return index < 0 || index >= Map.Count();
        }

        public void DepthFirstSearch()
        {
            int root = FindRoot();
            while (root > -1)
            {
                DepthFirstSearchFromRoot(root);
                root = FindRoot();
            }
        }

        private int FindRoot()
        {
            for (int i = 0; i < Map.Count(); i++)
            {
                if (D[i] == 0 && F[i] == 0)
                {
                    return i;
                }
            }

            return -1;
        }

        private void DepthFirstSearchFromRoot(int root)
        {
            var stack = new Stack<int>();
            stack.Push(root);
            D[root] = ++Counter;

            while (stack.Count > 0)
            {
                var current = stack.Peek();

                var next = Next(current);
                if (next > -1)
                {
                    stack.Push(next);
                    D[next] = ++Counter;
                }
                else
                {
                    var previous = stack.Pop();
                    F[previous] = ++Counter;
                }
            }
        }

        private int Next(int current)
        {
            for (int i = 0; i < Map[current].Count(); i++)
            {
                if (Map[current][i] >= 1 && D[i] == 0 && F[i] == 0)
                {
                    return i;
                }
            }
            return -1;
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
                    graph.Connect(from - 1, to - 1);
                }
            }

            graph.DepthFirstSearch();

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("{0} {1} {2}", i + 1, graph.D[i], graph.F[i]);
            }
        }


        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/11_Graph_B_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}