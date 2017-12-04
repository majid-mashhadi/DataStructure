using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    
    class BinarySearchTree
    {
        public class Node
        {
            public int data;
            public Node left;
            public Node right;
            public Node parent;
            public Node(int data)
                : this(data, null)
            {
            }
            public Node(int data, Node parent)
            {
                this.data = data;
                left = null;
                right = null;
                this.parent = parent;
            }
            public Node Add(int data)
            {
                if (this.data > data)
                {
                    if (left != null) return left.Add(data);
                    left = new Node(data, this);
                    return left;
                }
                else
                {
                    if (right != null) return right.Add(data);
                    right = new Node(data, this);
                    return right;
                }
            }
        }
        Node root;
        public BinarySearchTree()
        {
            root = null;
        }
        public Node Add (int data)
        {
            if ( root == null)
            {
                root = new Node(data);
                return root;
            }
            return root.Add(data);
            
        }

        public Node getNode(int data)
        {
            Node node = root;
            while (node != null && node.data != data)
            {
                if (node.data < data)
                    node = node.right;
                else
                    node = node.left;
            }
            return node;
        }
        public void Delete(int data )
        {
            Node node = getNode(data);
            if (node == null) return;

            if (node.left == null)
                Transplant(node, node.right);
            else if (node.right == null)
                Transplant(node, node.left);
            else
            {
                Node temp = TreeMinimum(node.right);
                if ( temp.parent != node)
                {
                    Transplant(temp, temp.right);
                    temp.right = node.right;
                    temp.right.parent = node;
                }
                Transplant(node, temp);
                temp.left = node.left;
                temp.left.parent = node;
            }
        }

        private Node TreeMinimum(Node root)
        {
            while ( root.left != null)
            {
                root = root.left;
            }
            return root;
        }

        void Transplant(Node u, Node v)
        {
            if (u.parent == null)
                root = v;
            else if ( u == u.parent.left)
            {
                u.parent.left = v;
            }
            else
            {
                u.parent.right = v;
            }
            if ( v != null)
            {
                v.parent = u.parent;
            }
        }

        public void InorderTravese()
        {
            Node temp = root;
            traverseInOrder(temp);
        }
        private void traverseInOrder(Node root)
        {
            if (root == null) return;
            traverseInOrder(root.left);
            Console.WriteLine(root.data);
            traverseInOrder(root.right);
        }
    }
}
