using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Aoj.ALDS.Chapter8C
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

        public Node Find(Node node)
        {
            Node current = Root;

            while (current != null)
            {
                if (node.Key == current.Key)
                    break;

                if (node.Key < current.Key)
                    current = current.Left;
                else
                    current = current.Right;
            }

            return current;
        }

        /// <summary>
        /// 二分探索木の削除は、以下のアルゴリズムに従う。
        /// 1.子がいない: 対象ノードを削除。
        /// 2.子が一つ: 対象ノードを削除し、親と子を結びつける。
        /// 3.子が二つ: 対象ノードの次節点を削除し、対象ノードのキーに、次節点のキーを代入する。
        ///            ※次節点: 木を中間順巡回した場合の、対象ノードの次のノード。
        /// </summary>
        
        public void Delete(Node node)
        {
            Node deleted = null;
            if (node.Left == null || node.Right == null)
                // 子が一つ以下の場合は、node自身が削除対象。
                deleted = node;
            else
                // 子が二つの場合は、次節点が削除対象。
                deleted = GetSuccessor(node);

            Node child = null;
            if (deleted.Left != null)
                child = deleted.Left;
            else
                child = deleted.Right;

            if (child != null)
                // (子が一つの場合だけここを通る)
                child.Parent = deleted.Parent;

            if (deleted.Parent == null)
                Root = child;
            else if (deleted == deleted.Parent.Left)
                deleted.Parent.Left = child;
            else
                deleted.Parent.Right = child;

            if (deleted != node)
                // (子が二つの場合)
                // 最後にキーを移し替える
                node.Key = deleted.Key;
        }

        /// <summary>
        /// ノードの削除用に、次節点を求める。
        /// </summary>
        /// <returns></returns>
        protected Node GetSuccessor(Node node)
        {
            if (node.Right != null)
                // 右に子を持つ場合、右部分木の最小値が次節点。
                return GetMinimum(node.Right);
            
            // 右に子を持たない場合、まだ右の子を出力していない親を求める。
            Node parent = node.Parent;
            while (parent != null && node == parent.Right)
            {
                node = parent;
                parent = parent.Parent;
            }
            return parent;
        }

        protected Node GetMinimum(Node node)
        {
            if (node.Left == null)
                return node;
            return GetMinimum(node.Left);
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

            for (int i = 0; i < n; i++)
            {
                string[] inputs = Console.ReadLine().Split(' ');
                int key = 0;
                Node foundNode = null;
                switch (inputs[0])
                {
                    case "insert":
                        key = int.Parse(inputs[1]);
                        tree.Insert(new Node(key));
                        break;
                    case "find":
                        key = int.Parse(inputs[1]);
                        foundNode = tree.Find(new Node(key));
                        Console.WriteLine(foundNode != null ? "yes" : "no");
                        break;
                    case "delete":
                        key = int.Parse(inputs[1]);
                        foundNode = tree.Find(new Node(key));
                        tree.Delete(foundNode);
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
            }
        }
        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("Aoj/AlgorithmsAndDataStructures/Input/08_BinarySearchTree_C_02.txt"));
        //     //var sw = new Stopwatch();
        //     //sw.Start();
        //     Solve();
        //     //Console.WriteLine("{0}ms",sw.ElapsedMilliseconds);
        // }
    }
}