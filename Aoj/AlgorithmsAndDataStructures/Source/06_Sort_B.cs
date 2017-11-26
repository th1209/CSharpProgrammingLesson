using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoj.ALDS.Chapter6B
{
    class Program
    {
        /// <summary>
        /// partitionで指定した値を中心に、
        /// それより前の要素が指定した値以下、
        /// それより後の要素が指定した値より大きくなるようにarrayを操作する。
        /// (arrayに対し破壊的操作を行う)。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="first">走査を起点する要素番号。</param>
        /// <param name="last">走査を終点する要素番号(含む)。</param>
        /// <param name="partition">partitionとして使う値の要素番号。</param>
        /// <returns>partition後の対象の値の要素番号。</returns>
        public static int Partition<T>(List<T> array, int first, int last, int partition = -1)
        where T : IComparable<T>
        {
            if (first < 0 || last >= array.Count)
                throw new System.ArgumentException("");

            if (partition == -1)
                // デフォルトでは末尾の要素の値をパーティションに使う。
                partition = last;

            // パーティション対象とする値(lastの位置に移動しておく)。
            T x = array[partition];
            Swap(array, partition, last);

            int i = first;
            for (int j = first; j < last; j++)
            {
                if (array[j].CompareTo(x) <= 0)
                {
                    Swap(array, i, j);
                    // 変数iはSwap時のみ更新するようにして、常に現在の確定位置を指すようにする。
                    i++;
                }
            }

            // 末尾に仮保存していたxを、パーティションの対象位置にSwap。
            Swap(array, i, last);

            return i;
        }

        private static void Swap<T>(List<T> array, int i, int j)
        {
            T tmp = array[i];
            array[i] = array[j];
            array[j] = tmp;
        }

        public static void Solve()
        {
            int n = int.Parse(Console.ReadLine());
            var list = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();


            // foreach
            int pIndex = Partition(list, 0, n - 1);
            for (int i = 0; i < n; i++)
            {
                if (i == pIndex)
                {
                    Console.Write("[{0}]", list[i]);
                }
                else
                {
                    Console.Write("{0}", list[i]);
                }

                if (i < n - 1)
                    Console.Write(" ");
            }
            Console.WriteLine("");
        }
        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/06_Sort_B_01.txt"));
        //     Solve();
        // }
    }
}

