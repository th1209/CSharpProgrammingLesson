using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Aoj.ALDS.Chapter7A
{
    class Program
    {
        public static List<Node> RootedTree;

        public static void SetDepth(Node node, int depth)
        {
            node.Depth = depth;
         
            if (node.LeftChildId() > -1) {
                SetDepth(RootedTree[node.LeftChildId()], depth + 1);
            }

            if (node.RightSiblingId > -1) {
                SetDepth(RootedTree[node.RightSiblingId], depth);
            }
        }

        public class Node
        {
            public int Id { get; set; } 
            public int ParentId { get; set; }
            public int RightSiblingId { get; set; }
            public List<int> ChildIds { get; set; }
            public int Depth { get; set; }


            public Node(int id)
            {
                Id = id;

                ParentId = -1;
                RightSiblingId = -1;

                ChildIds = new List<int>();

                Depth = -1;
            }

            public bool IsRoot()
            {
                return ParentId <= -1;
            }

            public bool IsLeaf()
            {
                return ! IsRoot() && ChildIds.Count() == 0;
            }

            public int LeftChildId()
            {
                return (ChildIds.Count() > 0) ? ChildIds[0] : -1;
            }

            public override string ToString()
            {
                string type = "internal node";
                if (IsRoot()) {
                    type = "root";
                }
                if (IsLeaf()) {
                    type = "leaf";
                }

                string childIds = string.Join(", ", Array.ConvertAll(ChildIds.ToArray(), (i) => { return i.ToString(); }));

                return string.Format(
                    "node {0}: parent = {1}, depth = {2}, {3}, [{4}]", Id, ParentId, Depth, type, childIds
                );
            }
        }

        public static void Solve()
        {
            RootedTree = new List<Node>();

            int n = int.Parse(Console.ReadLine());


            for (int i = 0; i < n; i++)
            {
                RootedTree.Add(new Node(i));
            }

            for (int i = 0; i < n; i++)
            {
                int[] inputs = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                var parentNode = RootedTree[inputs[0]];

                parentNode.ChildIds = inputs.Skip(2).ToList();
                for (int j = 0; j < parentNode.ChildIds.Count(); j++)
                {
                    int childId = parentNode.ChildIds[j];
                    var childNode = RootedTree[childId];

                    childNode.ParentId = parentNode.Id;
                    if (childId != parentNode.ChildIds.Last()) {
                        childNode.RightSiblingId = parentNode.ChildIds[j + 1];
                    }
                }
            }

            Node rootNode = null;
            for (int i = 0; i < n; i++)
            {
                if (RootedTree[i].IsRoot()) {
                    rootNode = RootedTree[i];
                }
            }

            SetDepth(rootNode, 0);

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(RootedTree[i]);
            }
        }

        // public static void Main()
        // {
        //     Console.SetIn(new System.IO.StreamReader("AlgorithmsAndDataStructures/Input/07_Tree_A_01.txt"));
        //     Solve();
        // }
    }
}

