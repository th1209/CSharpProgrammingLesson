using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter8A
{
    class Node
    {
        public int Key { get; set; }
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

        public Node(int key)
        {
            Key = key;
            Parent = null;
            Left = null;
            Right = null;
        }

        public bool IsRoot()
        {
            return Parent == null;
        }

        public bool IsLeaf()
        {
            return !IsRoot() && (Left == null && Right == null);
        }

        public int Degree()
        {
            int degree = 0;
            if (Left != null) degree++;
            if (Right != null) degree++;
            return degree;
        }
        public Node Sibling()
        {
            if (Parent == null)
                return null;

            if (Parent.Right != null && this.Key < Parent.Right.Key)
                // このノードが左
                return Parent.Right;

            if (Parent.Left != null && Parent.Left.Key < this.Key)
                // このノードが右
                return Parent.Left;

            return null;
        }

        // public override string ToString()
        // {
        //     return string.Format(
        //         "key {0}: parent = {1}, sibling = {2}, left = {3}, right = {4} degree = {5}, nodeType = {6}"
        //         , Key
        //         , OtherNodeKeyOrDefault(Parent)
        //         , OtherNodeKeyOrDefault(Sibling())
        //         , OtherNodeKeyOrDefault(Left)
        //         , OtherNodeKeyOrDefault(Right)
        //         , Degree()
        //         , NodeType()
        //     );
        // }

        protected int OtherNodeKeyOrDefault(Node node, int defaultKey = -1)
        {
            return (node != null) ? node.Key : defaultKey;
        }

        protected string NodeType()
        {
            if (IsRoot()) return "root";
            if (IsLeaf()) return "leaf";
            return "internal node";
        }
    }

    class BinarySearchTree
    {
        public Node Root { get; set; }
        public BinarySearchTree()
        {
            Root = null;
        }

        public void Insert(Node node)
        {
            Node parent = null;
            Node current = Root;

            while (current != null)
            {
                parent = current;
                if (node.Key < current.Key)
                    current = current.Left;
                else
                    current = current.Right;
            }

            if (parent == null)
            {
                Root = node;
            }

            else if (node.Key < parent.Key)
            {
                parent.Left = node;
                node.Parent = parent;
            }

            else
            {
                parent.Right = node;
                node.Parent = parent;
            }
        }

        public void ApplyPreorder(Node node, Action<Node> func)
        {
            if (node == null) return;

            func(node);
            ApplyPreorder(node.Left, func);
            ApplyPreorder(node.Right, func);
        }

        public void ApplyInorder(Node node, Action<Node> func)
        {
            if (node == null) return;

            ApplyInorder(node.Left, func);
            func(node);
            ApplyInorder(node.Right, func);
        }

        public void ApplyPostorder(Node node, Action<Node> func)
        {
            if (node == null) return;

            ApplyPostorder(node.Left, func);
            ApplyPostorder(node.Right, func);
            func(node);
        }
    }

    class Program
    {
        public static void Solve()
        {
            var tree = new BinarySearchTree();

            int n = int.Parse(Console.ReadLine());

            //debug
            var sw = new Stopwatch();
            sw.Start();
            var curSec = sw.ElapsedMilliseconds;

            for (int i = 0; i < n; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                switch (inputs[0])
                {
                    case "insert":
                        int key = int.Parse(inputs[1]);
                        //Console.WriteLine("Insert:{0}", key);
                        tree.Insert(new Node(key));
                        break;
                    case "print":
                        tree.ApplyInorder(tree.Root, (Node node) => { Console.Write(" {0}", node.Key); });
                        Console.WriteLine("");
                        tree.ApplyPreorder(tree.Root, (Node node) => { Console.Write(" {0}", node.Key); });
                        Console.WriteLine("");
                        break;
                    default:
                        throw new System.IO.IOException("");
                }

                //debug
                if (i % 100 == 0) {
                    Console.WriteLine("{0}ms", sw.ElapsedMilliseconds - curSec);
                    curSec = sw.ElapsedMilliseconds;
                }

            }
        }
        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/08_BinarySearchTree_A_02.txt"));
        //     //var sw = new Stopwatch();
        //     //sw.Start();
        //     Solve();
        //     //Console.WriteLine("{0}ms",sw.ElapsedMilliseconds);
        // }
    }
}