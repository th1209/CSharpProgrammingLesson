using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter9C
{
    class PriorityQueue<T> where T : IComparable<T>
    {
        private List<T> Values;

        public PriorityQueue()
        {
            Values = new List<T>();
        }

        public void Insert(T value)
        {
            Values.Add(value);

            BuildMaxHeap();
            //MaxHeapify(1);
        }

        // public T Max()
        // {
        //     if (Count() < 1)
        //     {
        //         return default(T);
        //     }

        //     return Values[1];
        // }

        public T Pop(int index)
        {
            if (! Exists(index))
            {
                throw new System.ArgumentOutOfRangeException("");
            }

            T value = Value(index);
            Values[index - 1] = Values[Count() - 1];
            Values.RemoveAt(Count() - 1);

            MaxHeapify(index);

            return value;
        }

        public T Peek(int index)
        {
            if (! Exists(index))
            {
                throw new System.ArgumentOutOfRangeException("");
            }

            return Value(index);
        }

        public int Count()
        {
            return Values.Count();
        }

        private bool Exists(int index)
        {
            return 1 <= index && index <= Count();
        }

        // TODO 要らなそうならコメントアウトする。
        private void BuildMaxHeap()
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

        private T Value(int index)
        {
            if (Exists(index))
            {
                // ヒープは1始まりで、リストは0始まりなので1ずらす。
                return Values[index - 1];
            }
            return default(T);
        }

        private int ParentIndex(int index)
        {
            return index / 2;
        }

        private int LeftIndex(int index)
        {
            return index * 2;
        }

        private int RightIndex(int index)
        {
            return index * 2 + 1;
        }
    }

    class Program
    {
        public static void Solve()
        {
            var queue = new PriorityQueue<int>();

            while(true)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                switch (inputs[0])
                {
                    case "insert":
                        queue.Insert(int.Parse(inputs[1]));
                        break;
                    case "extract":
                        Console.WriteLine(queue.Pop(1));
                        break;
                    case "end":
                        goto LOOPEND; 
                    default:
                        throw new System.IO.IOException("");
                }
            }
            LOOPEND:
            ;
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/09_Heap_C_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();
        //     Solve();
        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}