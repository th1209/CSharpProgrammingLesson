using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoj.ALDS.Chapter7B
{
    class Program
    {
        public static List<Node> BinaryTree;

        public static void SetDepth(Node node, int depth)
        {
            node.Depth = depth;

            if (node.LeftChildId > -1)
            {
                SetDepth(BinaryTree[node.LeftChildId], depth + 1);
            }

            if (node.RightChildId > -1)
            {
                SetDepth(BinaryTree[node.RightChildId], depth + 1);
            }
        }

        public static int SetHeight(Node node)
        {
            int height1 = 0;
            int height2 = 0;

            if (node.LeftChildId > -1)
            {
                height1 = SetHeight(BinaryTree[node.LeftChildId]) + 1;
            }

            if (node.RightChildId > -1)
            {
                height2 = SetHeight(BinaryTree[node.RightChildId]) + 1;
            }

            node.Height = Math.Max(height1, height2);
            return node.Height;
        }

        public class Node
        {
            public int Id { get; set; }
            public int ParentId { get; set; }
            public int SiblingId { get; set; }
            public int LeftChildId { get; set; }
            public int RightChildId { get; set; }
            public int Depth { get; set; }
            public int Height { get; set; }


            public Node(int id)
            {
                Id = id;

                ParentId = -1;
                SiblingId = -1;
                LeftChildId = -1;
                RightChildId = -1;

                Depth = -1;
            }

            public bool IsRoot()
            {
                return ParentId <= -1;
            }

            public bool IsLeaf()
            {
                return !IsRoot() && (LeftChildId == -1 && RightChildId == -1);
            }

            public int Degree()
            {
                int degree = 0;
                if (LeftChildId > -1) degree++;
                if (RightChildId > -1) degree++;
                return degree;
            }

            public override string ToString()
            {
                string type = "internal node";
                if (IsRoot())
                {
                    type = "root";
                }
                if (IsLeaf())
                {
                    type = "leaf";
                }

                return string.Format(
                    "node {0}: parent = {1}, sibling = {2}, degree = {3}, depth = {4}, height = {5}, {6}"
                    , Id
                    , ParentId
                    , SiblingId
                    , Degree()
                    , Depth
                    , Height
                    , type
                );
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
                currentNode.LeftChildId = inputs[1];
                currentNode.RightChildId = inputs[2];

                if (inputs[1] != -1)
                {
                    var leftNode = BinaryTree[inputs[1]];
                    leftNode.ParentId = currentNode.Id;
                    leftNode.SiblingId = inputs[2];
                }

                if (inputs[2] != -1)
                {
                    var rightNode = BinaryTree[inputs[2]];
                    rightNode.ParentId = currentNode.Id;
                    rightNode.SiblingId = inputs[1];
                }
            }

            Node rootNode = null;
            for (int i = 0; i < n; i++)
            {
                if (BinaryTree[i].IsRoot())
                {
                    rootNode = BinaryTree[i];
                }
            }

            SetDepth(rootNode, 0);
            SetHeight(rootNode);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(BinaryTree[i]);
            }
        }

        // public static void Main()
        // {
        //     Solve();
        // }
    }
}