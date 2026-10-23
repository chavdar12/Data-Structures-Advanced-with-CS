using System.Text;

namespace Two_Three;

public class TwoThreeTree<T> where T : IComparable<T>
{
    private TreeNode<T> root;

    public void Insert(T key)
    {
        root = Insert(root, key);
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        RecursivePrint(root, sb);

        return sb.ToString();
    }

    private void RecursivePrint(TreeNode<T> node, StringBuilder sb)
    {
        if (node == null) return;

        if (node.LeftKey != null) sb.Append(node.LeftKey).Append(" ");

        if (node.IsThreeNode())
            sb.Append(node.RightKey).Append(Environment.NewLine);
        else
            sb.Append(Environment.NewLine);

        if (node.IsTwoNode())
        {
            RecursivePrint(node.LeftChild, sb);
            RecursivePrint(node.MiddleChild, sb);
        }
        else if (node.IsThreeNode())
        {
            RecursivePrint(node.LeftChild, sb);
            RecursivePrint(node.MiddleChild, sb);
            RecursivePrint(node.RightChild, sb);
        }
    }

    private TreeNode<T> Insert(TreeNode<T> node, T key)
    {
        if (node == null) return new TreeNode<T>(key);

        TreeNode<T> returnNode;

        if (node.IsLeaf()) return Merge(node, new TreeNode<T>(key));

        if (key.CompareTo(node.LeftKey) < 0)
        {
            returnNode = Insert(node.LeftChild, key);

            if (returnNode == node.LeftChild)
                return node;
            else
                return Merge(node, returnNode);
        }

        if (node.IsTwoNode() || key.CompareTo(node.RightKey) < 0)
        {
            returnNode = Insert(node.MiddleChild, key);

            if (returnNode == node.MiddleChild)
                return node;
            else
                return Merge(node, returnNode);
        }

        returnNode = Insert(node.RightChild, key);

        if (returnNode == node.RightChild)
            return node;
        else
            return Merge(node, returnNode);
    }

    private TreeNode<T> Merge(TreeNode<T> a, TreeNode<T> b)
    {
        if (a.IsTwoNode())
        {
            if (a.LeftKey.CompareTo(b.LeftKey) < 0)
            {
                a.RightKey = b.LeftKey;
                a.MiddleChild = b.LeftChild;
                a.RightChild = b.MiddleChild;
            }
            else
            {
                a.RightKey = a.LeftKey;
                a.RightChild = a.MiddleChild;
                a.LeftKey = b.LeftKey;
                a.LeftChild = b.LeftChild;
                a.MiddleChild = b.MiddleChild;
            }

            return a;
        }

        if (a.LeftKey.CompareTo(b.LeftKey) >= 0)
        {
            var mergedNode = new TreeNode<T>(a.LeftKey)
            {
                LeftChild = b,
                MiddleChild = a
            };
            a.LeftChild = a.MiddleChild;
            a.MiddleChild = a.RightChild;
            a.RightChild = null;
            a.LeftKey = a.RightKey;
            a.ClearRightKey();

            return mergedNode;
        }

        if (a.RightKey.CompareTo(b.LeftKey) >= 0)
        {
            b.MiddleChild = new TreeNode<T>(a.RightKey)
            {
                LeftChild = b.MiddleChild,
                MiddleChild = a.RightChild
            };
            a.MiddleChild = b.LeftChild;
            b.LeftChild = a;
            a.ClearRightKey();
            a.RightChild = null;

            return b;
        }

        {
            var mergedNode = new TreeNode<T>(a.RightKey)
            {
                LeftChild = a,
                MiddleChild = b
            };
            a.RightChild = null;
            a.ClearRightKey();

            return mergedNode;
        }
    }
}