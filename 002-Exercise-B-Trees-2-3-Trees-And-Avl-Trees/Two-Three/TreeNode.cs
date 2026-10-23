namespace Two_Three;

public class TreeNode<T> where T : IComparable<T>
{
    public T LeftKey;

    // Tracked separately: for value types an empty RightKey (default) is indistinguishable from a real key
    private T rightKey;
    private bool hasRightKey;

    public T RightKey
    {
        get => rightKey;
        set
        {
            rightKey = value;
            hasRightKey = true;
        }
    }

    public TreeNode<T> LeftChild;
    public TreeNode<T> MiddleChild;
    public TreeNode<T> RightChild;

    public TreeNode(T key)
    {
        LeftKey = key;
    }

    public void ClearRightKey()
    {
        rightKey = default;
        hasRightKey = false;
    }

    public bool IsThreeNode()
    {
        return hasRightKey;
    }

    public bool IsTwoNode()
    {
        return !hasRightKey;
    }

    public bool IsLeaf()
    {
        return LeftChild == null && MiddleChild == null && RightChild == null;
    }
}