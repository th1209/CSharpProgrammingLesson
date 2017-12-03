using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter9A
{
    class Heap<T> where T : IComparable<T>
    {
        private List<T> Values;

        public Heap()
        {
            Values = new List<T>();
        }

        public void Add(T value)
        {
            Values.Add(value);
        }

        public int Count()
        {
            return Values.Count();
        }

        public int ParentIndex(int index)
        {
            return index / 2;
        }

        public int LeftIndex(int index)
        {
            return index * 2;
        }

        public int RightIndex(int index)
        {
            return index * 2 + 1;
        }

        public bool Exists(int index)
        {
            return 1 <= index && index <= Count();
        }

        public T Value(int index)
        {
            if (Exists(index))
            {
                // ヒープは1始まりで、リストは0始まりなので1ずらす。
                return Values[index - 1];
            }
            return default(T);
        }
    }

    class Program
    {
        public static void Solve()
        {
            var heap = new Heap<int>();

            int n = int.Parse(Console.ReadLine());

            int[] keys = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            foreach (var key in keys)
            {
                heap.Add(key);
            }

           for (int i = 1; i <= n; i++)
           {
                string output = string.Format("node {0}: ", i);

                output += string.Format("key = {0}, ", heap.Value(i));

                int parentI = heap.ParentIndex(i);
                if (heap.Exists(parentI))
                    output += string.Format("parent key = {0}, ", heap.Value(parentI));
                
                int leftI = heap.LeftIndex(i);
                if (heap.Exists(leftI))
                    output += string.Format("left key = {0}, ", heap.Value(leftI));

                int rightI = heap.RightIndex(i);
                if (heap.Exists(rightI))
                    output += string.Format("right key = {0}, ", heap.Value(rightI));

                Console.WriteLine(output);
           }
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/09_Heap_A_01.txt"));
        //     var sw = new Stopwatch();
        //     sw.Start();
        //     Solve();
        //     Console.WriteLine("{0}ms",sw.ElapsedMilliseconds);
        // }
    }
}