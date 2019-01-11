using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFS_BFS
{
    partial class Program
    {
        static void bfs_traversial(Node node)
        {
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(node);


            while (queue.Count > 0) {
                node = queue.Dequeue();
                Console.Write(node.Name + " ");

                if (node.Left != null) {
                    queue.Enqueue(node.Left);
                }

                if (node.Right != null)
                {
                    queue.Enqueue(node.Right);
                }

            }
        }
    }
}
