using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoj.ALDS.Chapter6A
{
    class Program
    {
        /// <summary>
        /// 引数arrayを計上ソート(バケツソートする)。
        /// (引数arrayは非破壊。コピーが返される)。
        /// </summary>
        /// <returns></returns>
        public static List<int> CountingSort(List<int> array)
        {
            int min = array.Min();
            int max = array.Max();

            // min ~ maxまでの要素数を持つ辞書を生成(歯抜け値含む)。
            // キーにarrayの値、バリューに要素の出現回数を持つ。
            // 歯抜け値を含んでしまうのが、このソートアルゴリズムの悪いところ。
            // 極端な値が一つだけあると、パフォーマンスが落ちる。
            var counter = new Dictionary<int, int>();
            for (int i = min; i <= max; i++)
            {
                counter[i] = 0;
            }

            // arrayを回して、要素の出現回数をカウントする。
            foreach (var value in array)
            {
                counter[value]++;
            }

            // 再度counterを回して、二個目以上の要素が累積値となるようにする。
            // 後続する要素が、前述する要素よりも後に挿入されるようにするため。
            for (int i = min + 1; i <= max; i++)
            {
                counter[i] += counter[i - 1];
            }

            // return用の配列を、適当な値で初期化しておく。
            var bucketArray = new List<int>();
            for (int i = 0; i < array.Count(); i++)
            {
                bucketArray.Add(0);
            }

            //for (int i = array.Count() - 1; i >= 0; i--)
            for (int i = 0; i < array.Count(); i++)
            {
                int value = array[i];
                int key = counter[value];
                // Console.WriteLine("key:{0}, value{1}", key, value);

                bucketArray[key - 1] = value;

                // 再度同じ要素が見つかった場合に、今回より小さい要素番号に挿入されるよう、デクリメントする。
                counter[value]--;
            }

            return bucketArray;
        }

        public static void Solve()
        {
            Console.ReadLine();
            var list = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse).ToList();

            var bucketList = CountingSort(list);

            string answer = string.Join(" ", bucketList.Select(i => i.ToString()).ToArray());
            Console.WriteLine(answer);
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/06_Sort_A_01.txt"));
        //     Solve();
        // }
    }
}

