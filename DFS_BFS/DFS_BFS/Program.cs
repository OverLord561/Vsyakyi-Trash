using System;
using System.Diagnostics;

namespace DFS_BFS
{
    partial class Program
    {
        //       ,-----------A----------------,
        //  ,----B------,              ,------E------,
        //  C           D              F             G
        //                                           |
        //                                           H

        static void Main(string[] args)
        {
            Stopwatch sp1 = new Stopwatch();
            Stopwatch sp2 = new Stopwatch();

            Node tree = InitializeTree();
            Console.WriteLine(" BFS QUEUE-->");

            bfs_traversial(tree);

            Console.WriteLine("\n\n DFS Requrcive -->");
            sp1.Start();
            dfs_traversal_recursion(tree);
            sp1.Stop();
            Console.WriteLine(sp1.ElapsedTicks);

            Console.WriteLine("\n\n DFS STACK -->");
            sp2.Start();
            dfs_traversal(tree);
            sp2.Stop();
            Console.WriteLine(sp1.ElapsedTicks);

            Console.ReadLine();
        }

        static Node InitializeTree()
        {
            Node tree =
            new Node("A",
                new Node("B",
                    new Node("C"), new Node("D")),
                new Node("E",
                    new Node("F"), new Node("G",
                                        new Node("H"), null)));

            return tree;
        }
    }
}
