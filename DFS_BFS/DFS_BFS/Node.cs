namespace DFS_BFS
{
    public class Node
    {
        public string Name;
        public Node Left;
        public Node Right;

        public Node(string name, Node left, Node right)
        {
            this.Name = name;
            this.Left = left;
            this.Right = right;
        }

        public Node(string name)
        {
            this.Name = name;
            this.Left = null;
            this.Right = null;
        }
    }
}
