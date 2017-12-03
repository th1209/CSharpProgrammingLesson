using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter9B
{
    class Heap<T> where T : IComparable<T>
    {
        private List<T> Values;

        public Heap()
        {
            Values = new List<T>();
        }

        /// <summary>
        /// ヒープに値を挿入する(挿入時にソートし直す)。
        /// </summary>
        /// <param name="value"></param>
        public void Insert(T value)
        {
            Values.Add(value);
            BuildMaxHeap();
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

        public void BuildMaxHeap()
        {
            for (int i = Count() / 2; i >= 1; i--)
            {
                MaxHeapify(i);
            }
        }

        private void MaxHeapify(int index)
        {
            int largestIndex = index;
            T largestValue = Value(index);

            int leftIndex = LeftIndex(index);
            if (Exists(leftIndex) && Value(leftIndex).CompareTo(largestValue) > 0)
            {
                largestIndex = leftIndex;
                largestValue = Value(leftIndex);
            }

            int rightIndex = RightIndex(index);
            if (Exists(rightIndex) && Value(rightIndex).CompareTo(largestValue) > 0)
            {
                largestIndex = rightIndex;
                largestValue = Value(rightIndex);
            }

            if (largestIndex != index)
            {
                Swap(index, largestIndex);
                MaxHeapify(largestIndex);
            }
        }

        private void Swap(int i1, int i2)
        {
            T tmp = Values[i1 - 1];
            Values[i1 - 1] = Values[i2 - 1];
            Values[i2 - 1] = tmp;
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

                // 結果は異なってしまうけど、実際にheapを使うなら以下の処理を使うべきだろう。
                //heap.Insert(key);
            }

            heap.BuildMaxHeap();

            for (int i = 1; i <= n; i++)
            {
                Console.Write(" {0}", heap.Value(i));
            }
            Console.WriteLine("");
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/09_Heap_B_01.txt"));
        //     var sw = new Stopwatch();
        //     sw.Start();
        //     Solve();
        //     Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}