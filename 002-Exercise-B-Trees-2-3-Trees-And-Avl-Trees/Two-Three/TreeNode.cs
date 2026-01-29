namespace Two_Three;

public class TreeNode<T> where T : IComparable<T>
{
    public T LeftKey;
    public T RightKey;

    public TreeNode<T> LeftChild;
    public TreeNode<T> MiddleChild;
    public TreeNode<T> RightChild;

    public TreeNode(T key)
    {
        LeftKey = key;
    }

    public bool IsThreeNode()
    {
        return RightKey != null;
    }

    public bool IsTwoNode()
    {
        return RightKey == null;
    }

    public bool IsLeaf()
    {
        return LeftChild == null && MiddleChild == null && RightChild == null;
    }
}