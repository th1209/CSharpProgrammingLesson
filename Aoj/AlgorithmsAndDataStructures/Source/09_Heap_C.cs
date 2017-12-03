using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter9C
{
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
        private SortOrder _sortOrder;

        public PriorityQueue(SortOrder sortOrder = SortOrder.Asc, int initSize = 128)
        {
            _heap  = new T[initSize];
            _count = 0;
            _sortOrder = sortOrder;
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
                if (Compare(t, _heap[parent]) >= 0)
                //if (t.CompareTo(_heap[parent]) >= 0)
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

                if (right <= _count && Compare(_heap[left], _heap[right]) > 0)
                //if (right <= _count && _heap[left].CompareTo(_heap[right]) > 0)
                    left = right;

                if (Compare(t, _heap[left]) < 0)
                //if (t.CompareTo(_heap[left]) < 0)
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

        public T[] ToArray()
        {
            var array = new T[_count];
            for (int i = 0; i < _count; i++)
            {
                array[i] = _heap[i];
            }
            return array;
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

        private int Compare(T left, T right)
        {
            return (_sortOrder == SortOrder.Asc)
                ? left.CompareTo(right)
                : left.CompareTo(right) * -1;
        }
    }

    class Program
    {
        public static void Solve()
        {
            var queue = new PriorityQueue<int>(SortOrder.Desc);

            while(true)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                switch (inputs[0])
                {
                    case "insert":
                        queue.Enqueue(int.Parse(inputs[1]));
                        break;
                    case "extract":
                        Console.WriteLine(queue.Dequeue());
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
        //     // Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/09_Heap_C_01.txt"));
        //     // var sw = new Stopwatch();
        //     // sw.Start();
        //     Solve();
        //     // Console.WriteLine("{0}ms", sw.ElapsedMilliseconds);
        // }
    }
}