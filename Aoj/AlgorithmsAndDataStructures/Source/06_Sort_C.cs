using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoj.ALDS.Chapter6C
{
    class Card : IComparable<Card>
    {
        public string Suit { get; set; }
        public int Number { get; set; }

        public Card(string suit , int number)
        {
            Suit = suit;
            Number = number;
        }

        public int CompareTo(Card other)
        {
            return Number - other.Number;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Suit, Number);
        }
    }
    class Program
    {
        /// <summary>
        /// arrayに対し、マージソートを行う(破壊的操作)。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="left"></param>
        /// <param name="right">(末尾の要素は含まれない)</param>
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

        private static void Merge<T>(List<T> array, int left, int mid, int right)
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

        public static void QuickSort<T>(List<T> array, int first, int last)
        where T : IComparable<T>
        {
            if (first >= last || last < 0)
                return;

            // Partitionで二分する(分割)。
            int pIndex = Partition(array, first, last);

            // 前半部分と後半部分に同じ処理を適用。
            // (この処理でarrayが変更されるので、統治に該当する処理は必要ない。)
            QuickSort(array, first, pIndex - 1);
            QuickSort(array, pIndex + 1, last);
        }

        /// <summary>
        /// partitionで指定した値を中心に、
        /// それより前の要素が指定した値以下、
        /// それより後の要素が指定した値より大きくなるようにarrayを操作する。
        /// (arrayに対し破壊的操作を行う)。
        /// </summary>
        /// <param name="array"></param>
        /// <param name="first">走査の起点とする要素番号。</param>
        /// <param name="last">走査の終点とする要素番号(含む)。</param>
        /// <param name="partition">partitionとして使う値の要素番号。</param>
        /// <returns>partition後の対象の値の要素番号。</returns>
        private static int Partition<T>(List<T> array, int first, int last, int partition = -1)
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

            var m_cards = new List<Card>();
            var q_cards = new List<Card>();
            for (int i = 0; i < n; i++)
            {
                string[] suit_and_num = Console.ReadLine().Split(' ');
                var card = new Card(suit_and_num[0], int.Parse(suit_and_num[1]));
                
                // MEMO:
                // 問題の都合上各インスタンスに破壊的操作が行われないので問題ないが、
                // 二つのlistが、同じ要素を参照しあっていることに注意。
                m_cards.Add(card);
                q_cards.Add(card);
            }

            // 二つの方法でソートする。
            MergeSort(m_cards, 0, n);
            QuickSort(q_cards, 0, n - 1);

            // マージソートと結果が同じなら、
            // クイックソートの結果は安定と言える。
            bool stable = true;
            for (int i = 0; i < n; i++)
            {
                stable = stable 
                    && m_cards[i].Suit == q_cards[i].Suit
                    && m_cards[i].Number == q_cards[i].Number;
            }
            Console.WriteLine("{0}", stable ? "Stable" : "Not stable");

            // Console.WriteLine("Merge Sorted Cards:");
            // m_cards.ForEach(card => Console.WriteLine(card));

            // Console.WriteLine("Quick Sorted Cards:");
            q_cards.ForEach(card => Console.WriteLine(card));
        }
        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/06_Sort_C_03.txt"));
        //     Solve();
        // }
    }
}

