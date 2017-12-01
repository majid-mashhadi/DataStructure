using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class TNode
{
    public int data { get; internal set; }
    internal TNode left = null;
    internal TNode right = null;
    internal TNode(int data)
    {
        this.data = data;
    }
    public TNode InsertLeft(int data)
    {
        if (left == null)
            left = new TNode(data);
        return left;
    }
    public TNode InsertRight(int data)
    {
        if (right == null)
            right = new TNode(data);
        return right;
    }
}

public class InorderTraverse
{
    private TNode root;
    private Stack<TNode> stack;
    private HashSet<TNode> visited;
    public InorderTraverse(TNode root)
    {
        this.root = root;
        stack = new Stack<TNode>();
        visited = new HashSet<TNode>();
        if (root != null)
            stack.Push(root);
    }

    private void PushLeftToStack(TNode Current)
    {
        while (Current != null)
        {
            stack.Push(Current);
            Current = Current.left;
        }
    }

    public bool HasNext()
    {
        return stack.Count != 0;
    }

    public TNode Next()
    {
        if (!HasNext()) return null;
        TNode current = stack.Peek();
        if (current.left != null && !visited.Contains(current.left))
        {
            PushLeftToStack(current.left);
        }
        current = stack.Pop();
        if (current.right != null && !visited.Contains(current.right))
            stack.Push(current.right);

        visited.Add(current);
        return current;
    }
}

