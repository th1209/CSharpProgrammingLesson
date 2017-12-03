using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;
using Aoj.ALDS.Chapter9C;
//using Xunit;

namespace Aoj.ALDS.Chapter9CTest
{
    [TestFixture]
    public class PriorityQueueTest
    {
        [Test]
        //[Fact]
        public void TestRandomly()
        {
            var queue = new PriorityQueue<int>();
            //var array = new int[1000];

            var rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int value = rand.Next(-999, 1000);
                queue.Enqueue(value);
                //array[i] = value;
            }

            //var sortedArray = array.OrderBy(item => item);

            var popArray = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                popArray[i] = queue.Dequeue();
            }


            CollectionAssert.IsOrdered(popArray);
        }
    }
}
