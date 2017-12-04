using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class TreeNode<T>
    {
        public T data;
        public TreeNode<T> right;
        public TreeNode<T> left;
        public TreeNode(T data)
        {
            this.data = data;
        }
        public TreeNode<T> addLeft(T data)
        {
            if (left == null) left = new TreeNode<T>(data);
            return left;
        }
        public TreeNode<T> addRight(T data)
        {
            if (right == null) right = new TreeNode<T>(data);
            return right;
        }

    }

    class BinaryTree<T>
    {
        public TreeNode<T> root;
        public BinaryTree(T data)
        {
            root = new TreeNode<T>(data);

        }


        public void TraverseInOrder()
        {
            TraverseInOrderHelper(root);
        }

        private void TraverseInOrderHelper(TreeNode<T> root)
        {
            if (root == null) return;
            TraverseInOrderHelper(root.left);
            Console.WriteLine(root.data);
            TraverseInOrderHelper(root.right);
        }

        public void TraversePreOrder()
        {
            TraversePreOrderHelper(root);
        }

        private void TraversePreOrderHelper(TreeNode<T> root)
        {
            if (root == null) return;
            Console.WriteLine(root.data);
            TraversePreOrderHelper(root.left);
            TraversePreOrderHelper(root.right);
        }

        public void TraversePostOrder()
        {
            TraversePostOrderHelper(root);
        }

        private void TraversePostOrderHelper(TreeNode<T> root)
        {
            if (root == null) return;
            TraversePostOrderHelper(root.left);
            TraversePostOrderHelper(root.right);
            Console.WriteLine(root.data);
        }

        public void MorristTraverse()
        {
            TreeNode<T> current = root;
            while (current != null)
            {
                if (current.left == null)
                {
                    Console.WriteLine(current.data);
                    current = current.right;
                }
                else
                {
                    TreeNode<T> predecessor = current.left;
                    while (predecessor.right != null && predecessor.right != current)
                    {
                        predecessor = predecessor.right;
                    }
                    if (predecessor.right == null)
                    {
                        predecessor.right = current;
                        current = current.left;
                    }
                    else
                    {
                        predecessor.right = null;
                        Console.WriteLine(current.data);
                        current = current.right;
                    }
                }
            }
        }

        public void TravserLevelRevese()
        {
            Stack<TreeNode<T>> s1 = new Stack<TreeNode<T>>();
            Stack<TreeNode<T>> s2 = new Stack<TreeNode<T>>();
            s1.Push(root);
            TreeNode<T> temp;
            while (s1.Count != 0 || s2.Count != 0)
            {
                while (s1.Count != 0)
                {
                    temp = s1.Pop();
                    if (temp.left != null)
                        s2.Push(temp.left);
                    if (temp.right != null)
                        s2.Push(temp.right);
                    Console.Write(temp.data + " -> ");
                }
                while (s2.Count != 0)
                {
                    temp = s2.Pop();
                    if (temp.right != null)
                        s1.Push(temp.right);
                    if (temp.left != null)
                        s1.Push(temp.left);
                    Console.Write(temp.data + " -> ");
                }
            }
        }

        public void TraverseLevel()
        {
            Queue<TreeNode<T>> nextToVisit = new Queue<TreeNode<T>>();
            Console.WriteLine();

            nextToVisit.Enqueue(root);
            TreeNode<T> temp;
            while (nextToVisit.Count > 0)
            {
                temp = nextToVisit.Dequeue();
                Console.Write(temp.data + " -> ");
                if (temp.left != null)
                    nextToVisit.Enqueue(temp.left);
                if (temp.right != null)
                    nextToVisit.Enqueue(temp.right);
            }
        }

        public void TraverseBoundries()
        {
            HashSet<TreeNode<T>> visited = new HashSet<TreeNode<T>>();
            TraverseLeft(root, visited);
            TraverseLeaves(root.left, visited);
            TraverseLeaves(root.right, visited);
            TraverseRight(root, visited);
        }
        private void TraverseLeft(TreeNode<T> root, HashSet<TreeNode<T>> visited)
        {
            if (root == null) return;
            printNode(root, visited);
            if (root.left != null)
                TraverseLeft(root.left, visited);
            else if (root.right != null)
                TraverseLeft(root.right, visited);
        }
        private void TraverseRight(TreeNode<T> root, HashSet<TreeNode<T>> visited)
        {
            if (root == null) return;
            if (root.right != null)
            {
                TraverseRight(root.right, visited);
                printNode(root, visited);
            }
            else if (root.left != null)
            {
                TraverseRight(root.left, visited);
                printNode(root, visited);
            }
        }
        private void TraverseLeaves(TreeNode<T> root, HashSet<TreeNode<T>> visited)
        {
            if (root == null) return;
            if (root.left != null) TraverseLeaves(root.left, visited);
            if (root.left == null && root.right == null)
            {
                printNode(root, visited);
            }
            if (root.right != null) TraverseLeaves(root.right, visited);
        }
        private void printNode(TreeNode<T> node, HashSet<TreeNode<T>> visited)
        {
            if (!visited.Contains(node))
            {
                visited.Add(node);
                Console.Write(node.data + " -> ");
            }
        }
        public void ConverToLinkedList()
        {
            LinkedList<T> list = new LinkedList<T>();
            ConvertToLinkedList(root, list);
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine(list.ElementAt(i));
            }
            Console.ReadLine();
        }
        private void ConvertToLinkedList(TreeNode<T> root, LinkedList<T> list)
        {
            if (root == null) return;
            ConvertToLinkedList(root.left, list);
            list.AddLast(root.data);
            ConvertToLinkedList(root.right, list);
        }
    }
    public class BinaryTreeIterator<T>
    {
        private TreeNode<T> Current = null;
        public BinaryTreeIterator(TreeNode<T> root)
        {
            this.Current = root;
        }

        private bool HasNext()
        {
            return Current != null;
        }
        public TreeNode<T> Next()
        {
            if (Current.left == null)
            {
                TreeNode<T> temp = Current;
                Current = Current.right;
                return temp;
            }
            else
            {
                TreeNode<T> predecessor = Current.left;
                while (predecessor.right != null && predecessor.right != Current)
                {
                    predecessor = predecessor.right;
                }
                if (predecessor.right == null)
                {
                    predecessor.right = Current;
                    return Next();
                }
                else
                {
                    predecessor.right = null;
                    TreeNode<T> temp = Current;
                    Current = Current.right;
                    return temp;
                }
            }
        }
    }



}
