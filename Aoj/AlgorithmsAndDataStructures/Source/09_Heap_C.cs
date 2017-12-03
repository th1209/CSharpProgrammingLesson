using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter9C
{
    // public class PriorityQueue<T> where T : IComparable<T>
    // {
    //     private List<T> Values;

    //     public PriorityQueue()
    //     {
    //         Values = new List<T>();
    //     }

    //     public void Insert(T value)
    //     {
    //         Values.Add(value);

    //         BuildMaxHeap();
    //         //MaxHeapify(1);
    //     }

    //     // public T Max()
    //     // {
    //     //     if (Count() < 1)
    //     //     {
    //     //         return default(T);
    //     //     }

    //     //     return Values[1];
    //     // }

    //     public T Pop(int index)
    //     {
    //         if (! Exists(index))
    //         {
    //             throw new System.ArgumentOutOfRangeException("");
    //         }

    //         T value = Value(index);
    //         Values[index - 1] = Values[Count() - 1];
    //         Values.RemoveAt(Count() - 1);

    //         MaxHeapify(index);

    //         return value;
    //     }

    //     public T Peek(int index)
    //     {
    //         if (! Exists(index))
    //         {
    //             throw new System.ArgumentOutOfRangeException("");
    //         }

    //         return Value(index);
    //     }

    //     public int Count()
    //     {
    //         return Values.Count();
    //     }

    //     private bool Exists(int index)
    //     {
    //         return 1 <= index && index <= Count();
    //     }

    //     // TODO 要らなそうならコメントアウトする。
    //     private void BuildMaxHeap()
    //     {
    //         for (int i = Count() / 2; i >= 1; i--)
    //         {
    //             MaxHeapify(i);
    //         }
    //     }

    //     private void MaxHeapify(int index)
    //     {
    //         int largestIndex = index;
    //         T largestValue = Value(index);

    //         int leftIndex = LeftIndex(index);
    //         if (Exists(leftIndex) && Value(leftIndex).CompareTo(largestValue) > 0)
    //         {
    //             largestIndex = leftIndex;
    //             largestValue = Value(leftIndex);
    //         }

    //         int rightIndex = RightIndex(index);
    //         if (Exists(rightIndex) && Value(rightIndex).CompareTo(largestValue) > 0)
    //         {
    //             largestIndex = rightIndex;
    //             largestValue = Value(rightIndex);
    //         }

    //         if (largestIndex != index)
    //         {
    //             Swap(index, largestIndex);
    //             MaxHeapify(largestIndex);
    //         }
    //     }

    //     private void Swap(int i1, int i2)
    //     {
    //         T tmp = Values[i1 - 1];
    //         Values[i1 - 1] = Values[i2 - 1];
    //         Values[i2 - 1] = tmp;
    //     }

    //     private T Value(int index)
    //     {
    //         if (Exists(index))
    //         {
    //             // ヒープは1始まりで、リストは0始まりなので1ずらす。
    //             return Values[index - 1];
    //         }
    //         return default(T);
    //     }

    //     private int ParentIndex(int index)
    //     {
    //         return index / 2;
    //     }

    //     private int LeftIndex(int index)
    //     {
    //         return index * 2;
    //     }

    //     private int RightIndex(int index)
    //     {
    //         return index * 2 + 1;
    //     }
    // }

    public enum SortOrder
    {
        Asc,
        Desc,
    }

    public class PriorityQueue<T>
    where T : IComparable 
    {
        public int Count {
            get{ return _count; }
        }

        private T[] _heap;
        private int _count;

        public PriorityQueue(SortOrder sortOrder = SortOrder.Asc, int initSize = 128)
        {
            _heap  = new T[initSize];
            _count = 0;
        }

        public void Enqueue(T t)
        {
            int current = _count;

            _count++;

            ResizeIfNeeded();

            while (current > 0)
            {
                int parent = (current - 1) / 2;
                //TODO
                if (t.CompareTo(_heap[parent]) >= 0)
                    break;
                
                 _heap[current] = _heap[parent];
                 current = parent;
            }

            _heap[current] = t;
        }

        public T Dequeue()
        {
            if (_count <= 0)
                throw new InvalidOperationException("");

            _count--;

            T ret = _heap[0];
            T t = _heap[_count];
            _heap[0] = t;

            int current = 0;


            while (current * 2 + 1 <= _count)
            {
                int left  = current * 2 + 1;
                int right = current * 2 + 2;

                if (right <= _count && _heap[left].CompareTo(_heap[right]) > 0)
                    left = right;

                if (t.CompareTo(_heap[left]) < 0)
                    break;

                _heap[current] = _heap[left];
                current = left;
            }

            _heap[current] = t;
            _heap[_count] = default(T);

            return ret;
        }

        public T Peek()
        {
            if (_count <= 0)
                throw new InvalidOperationException("");
            return _heap[0];
        }

        public void Clear()
        {
            while (_heap.Count() > 0)
            {
                Dequeue();
            }
        }

        public bool Contains(T t)
        {
            for (int i = 0; i < _count; i++)
            {
                if (t.Equals(_heap[i]))
                    return true;
            }

            return false;
        }

        private void ResizeIfNeeded()
        {
            if (_count > _heap.Length)
            {
                var newHeap = new T[_heap.Length * 2];
                for (int i = 0; i < _heap.Length; i++)
                {
                    newHeap[i] = _heap[i];
                }
                _heap = newHeap;
            }
        }

        public T[] ToArray()
        {
            var array = new T[_count];
            for (int i = 0; i < _count; i++)
            {
                array[i] = _heap[i];
            }
            return array;
        }
    }

    class Program
    {
        public static void Solve()
        {
            var queue = new PriorityQueue<int>();

            var rand = new Random();
            var count = 100;
            for (int i = 0; i < count; i++)
            {
                int value = rand.Next(-999, 1000);
                queue.Enqueue(value);
            }


            var popArray = new int[count];
            for (int i = 0; i < count; i++)
            {
                popArray[i] = queue.Dequeue();
            }

            Console.WriteLine("end");



            // var queue = new PriorityQueue<int>();

            // while(true)
            // {
            //     string[] inputs = Console.ReadLine().Split(' ');
            //     switch (inputs[0])
            //     {
            //         case "insert":
            //             queue.Insert(int.Parse(inputs[1]));
            //             break;
            //         case "extract":
            //             Console.WriteLine(queue.Pop(1));
            //             break;
            //         case "end":
            //             goto LOOPEND; 
            //         default:
            //             throw new System.IO.IOException("");
            //     }
            // }
            // LOOPEND:
            // ;
        }

        // public static void Main()
        // {
        //     // Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/09_Heap_C_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();
        //     Solve();
        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}