using System;

namespace DFS_BFS
{
    partial class Program
    {
        static void dfs_traversal_recursion(Node node)
        {
            if (node == null)
            {
                return;
            }
            Console.Write(node.Name + " ");

            dfs_traversal_recursion(node.Left);
            dfs_traversal_recursion(node.Right);
        }
    }
}
