using System;
using System.Collections;
using System.Collections.Generic;

namespace DFS_BFS
{
    partial class Program
    {
        static void dfs_traversal(Node node)
        {
            Stack<Node> stack = new Stack<Node>();
            stack.Push(node);

            while (stack.Count > 0) {
                node = stack.Pop();

                Console.Write(node.Name + " ");

                if (node.Right != null)
                {
                    stack.Push(node.Right);
                }
                if (node.Left != null)
                {
                    stack.Push(node.Left);
                }

            }

        }
    }
}
