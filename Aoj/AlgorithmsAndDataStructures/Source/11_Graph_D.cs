using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter11D
{
    class Program
    {
        public static int[] color;

        public static void AssignColor(List<int>[] friends, int[] color)
        {
            int id = 0;
            for (int i = 0; i < friends.Count(); i++)
            {
                if (color[i] == -1)
                {
                    DepthFirstSearch(i, id, friends);
                }
                id++;
            }
        }

        private static void DepthFirstSearch(int index, int id, List<int>[] friends)
        {
            color[index] = id;
            foreach (var friend in friends[index])
            {
                if (color[friend] == -1) {
                    DepthFirstSearch(friend, id, friends);
                }
            }
        }

        public static void Solve()
        {
            int[] n_m = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int n = n_m[0];
            int m = n_m[1];

            var friends = new List<int>[n];
            for (int i = 0; i < n; i++)
            {
                friends[i] = new List<int>();
            }

            for (int i = 0; i < m; i++)
            {
                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                friends[inputs[0]].Add(inputs[1]);
                friends[inputs[1]].Add(inputs[0]);
            }

            color = Enumerable.Repeat<int>(-1, n).ToArray();

            AssignColor(friends, color);

            int q = int.Parse(Console.ReadLine());

            for (int i = 0; i < q; i++)
            {
                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                Console.WriteLine(color[inputs[0]] == color[inputs[1]] ? "yes" : "no");
            }
        }


        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/11_Graph_D_01.txt"));
        //     var sw = new Stopwatch();
        //     sw.Start();

        //     Solve();

        //     Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}