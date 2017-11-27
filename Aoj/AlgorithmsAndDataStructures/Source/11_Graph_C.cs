using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter11C
{
    class DirectedGraph
    {
        // Map[s][t]と表現する時、 s -> tならば Map[s][t] >= 1 とする。
        private int[][] Map;

        public int[] D;

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

            D = Enumerable.Repeat<int>(-1, vertexNum).ToArray();
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

        public void BreadthFirstSearch(int index)
        {
            var queue = new Queue<int>();
            queue.Enqueue(index);
            
            D[index] = 0;

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                List<int> neighbors = FindNeighbors(current);

                foreach (var neighbor in neighbors)
                {
                    queue.Enqueue(neighbor);
                    D[neighbor] = D[current] + 1;
                }
            }
        }

        private List<int> FindNeighbors(int index)
        {
            var neighbors = new List<int>();
            for (int i = 0; i < Map[index].Count(); i++)
            {
                if (Map[index][i] >= 1 && D[i] <= -1)
                    neighbors.Add(i);
            }
            return neighbors;
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

            graph.BreadthFirstSearch(0);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("{0} {1}", i + 1, graph.D[i]);
            }
        }


        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/11_Graph_C_02.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}