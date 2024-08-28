using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace leetcode.Solutions
{
    public class Node
    {
        public int val;
        public IList<Node> children;

        public Node() { }

        public Node(int _val)
        {
            val = _val;
        }

        public Node(int _val, IList<Node> _children)
        {
            val = _val;
            children = _children;
        }
        public static IList<int> Postorder(Node root)
        {
            if(root == null)
                return null;

            IList<int> nodes = new List<int>();

            DFSpostorder(root);

            void DFSpostorder(Node node)
            {
                if (node.children == null)
                    return;

                foreach (Node child in node.children)
                    DFSpostorder(child);

                nodes.Add(node.val);
            }

            return nodes;
        }
    }
}
