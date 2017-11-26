using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter5B
{
    class Program
    {
        public static int counter = 0;
        public static void Merge<T>(List<T> array, int left, int mid, int right)
        where T : IComparable<T>
        {
            int n1 = mid - left;
            int n2 = right - mid;

            if (n1 < 0 || n2 < 0)
            {
                throw new System.ArgumentException("");
            }

            var leftArray = new List<T>();
            var rightArray = new List<T>();
            for (int i = 0; i < n1; i++)
            {
                leftArray.Add(array[left + i]);
            }
            for (int i = 0; i < n2; i++)
            {
                rightArray.Add(array[mid + i]);
            }

            int lc = 0;
            int rc = 0;
            for (int i = left; i < right; i++)
            {
                // TODO あくまで回答のためであり、実用を考えると以下のカウントアップは不要。
                counter++;

                if (rc >= n2 || (lc < n1 && leftArray[lc].CompareTo(rightArray[rc]) <= 0))
                {
                    array[i] = leftArray[lc];
                    lc++;
                }
                else
                {
                    array[i] = rightArray[rc];
                    rc++;
                }
            }
        }

        public static void MergeSort<T>(List<T> array, int left, int right)
        where T : IComparable<T>
        {
            if (left + 1 < right)
            {
                int mid = (left + right) / 2;
                MergeSort(array, left, mid);
                MergeSort(array, mid, right);
                Merge(array, left, mid, right);
            }
        }

        public static void Solve()
        {
            Console.ReadLine();

            var list = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();

            MergeSort(list, 0, list.Count());

            string answer = string.Join(" ", list.Select(i => i.ToString()).ToArray());
            Console.WriteLine(answer);
            Console.WriteLine(counter);
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/05_Recursion_B_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();
        //     Solve();

        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}