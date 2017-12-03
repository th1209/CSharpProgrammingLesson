using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoj.ALDS.Chapter7C
{
    class Program
    {
        public static List<Node> BinaryTree;

        public class Node
        {
            public int Id { get; set; } 
            public int ParentId { get; set; }
            public int LeftChildId { get; set; }
            public int RightChildId { get; set; }

            public Node(int id)
            {
                Id = id;

                ParentId     = -1;
                LeftChildId  = -1;
                RightChildId = -1;
            }

            public bool IsRoot()
            {
                return ParentId <= -1;
            }

            public bool IsLeaf()
            {
                return ! IsRoot() && (LeftChildId == -1 && RightChildId == -1);
            }
        }

        public static void Solve()
        {
            BinaryTree = new List<Node>();

            int n = int.Parse(Console.ReadLine());


            for (int i = 0; i < n; i++)
            {
                BinaryTree.Add(new Node(i));
            }

            for (int i = 0; i < n; i++)
            {
                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

                var currentNode = BinaryTree[inputs[0]];
                currentNode.LeftChildId  = inputs[1];
                currentNode.RightChildId = inputs[2];

                if (inputs[1] != -1) {
                    var leftNode = BinaryTree[inputs[1]];
                    leftNode.ParentId = currentNode.Id;
                }

                if (inputs[2] != -1) {
                    var rightNode = BinaryTree[inputs[2]];
                    rightNode.ParentId = currentNode.Id;
                }
            }

            Node rootNode = null;
            for (int i = 0; i < n; i++)
            {
                if (BinaryTree[i].IsRoot()) {
                    rootNode = BinaryTree[i];
                }
            }

            Console.WriteLine("Preorder");
            PrintPreorder(rootNode);
            Console.WriteLine("");

            Console.WriteLine("Inorder");
            PrintInorder(rootNode);
            Console.WriteLine("");

            Console.WriteLine("Postorder");
            PrintPostorder(rootNode);
            Console.WriteLine("");
        }

        public static void PrintPreorder(Node node)
        {
            Console.Write(" {0}", node.Id);
            if (node.LeftChildId > -1)
                PrintPreorder(BinaryTree[node.LeftChildId]);
            if (node.RightChildId > -1)
                PrintPreorder(BinaryTree[node.RightChildId]);
        }

        public static void PrintInorder(Node node)
        {
            if (node.LeftChildId > -1)
                PrintInorder(BinaryTree[node.LeftChildId]);
            Console.Write(" {0}", node.Id);
            if (node.RightChildId > -1)
                PrintInorder(BinaryTree[node.RightChildId]);
        }

        public static void PrintPostorder(Node node)
        {
            if (node.LeftChildId > -1)
                PrintPostorder(BinaryTree[node.LeftChildId]);
            if (node.RightChildId > -1)
                PrintPostorder(BinaryTree[node.RightChildId]);
            Console.Write(" {0}", node.Id);
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/07_Tree_C_01.txt"));
        //     Solve();
        // }
    }
}

