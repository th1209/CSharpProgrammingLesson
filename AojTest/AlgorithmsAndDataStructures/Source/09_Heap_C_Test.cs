using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using NUnit.Framework;
using Aoj.ALDS.Chapter9C;

namespace Aoj.ALDS.Chapter9CTest
{
    [TestFixture]
    public class PriorityQueueTest
    {
        [Test]
        public void TestAscendingCase()
        {
            var queue = new PriorityQueue<int>(SortOrder.Asc);

            var rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int value = rand.Next(-999, 1000);
                queue.Enqueue(value);
            }

            var popArray = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                popArray[i] = queue.Dequeue();
            }

            Assert.That(popArray, Is.Ordered);
        }

        [Test]
        public void TestDescendingCase()
        {
            var queue = new PriorityQueue<int>(SortOrder.Desc);

            var rand = new Random();
            for (int i = 0; i < 1000; i++)
            {
                int value = rand.Next(-999, 1000);
                queue.Enqueue(value);
            }

            var popArray = new int[1000];
            for (int i = 0; i < 1000; i++)
            {
                popArray[i] = queue.Dequeue();
            }

            Assert.That(popArray, Is.Ordered.Descending);
        }
    }
}
