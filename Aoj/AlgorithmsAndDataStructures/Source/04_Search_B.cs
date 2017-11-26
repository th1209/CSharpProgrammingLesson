using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter4B
{
    class Program
    {
        /// <summary>
        /// 二分探索して listからvalueのキーを探す。
        /// 見つからない場合は-1を返す。
        /// </summary>
        public static int BinarySearch<T>(List<T> list, T value) where T : IComparable<T>
        {
            int left = 0;
            int right = list.Count;

            while (left < right)
            {
                int mid = (left + right) / 2;
                int compare = value.CompareTo(list[mid]);
                if (compare == 0)
                    return mid;
                else if (compare < 0)
                    right = mid;
                else
                    left = mid + 1;
            }

            return -1;
        }

        public static void Solve()
        {
            //int n = int.Parse(Console.ReadLine());
            Console.ReadLine();
            var list1 = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();
            int q = int.Parse(Console.ReadLine());
            var list2 = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();

            int counter = 0;
            for (int i = 0; i < q; i++)
            {
                if (BinarySearch(list1, list2[i]) >= 0)
                {
                    counter++;
                }
            }

            Console.WriteLine(counter);
        }

        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/04_Search_B_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();

        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}