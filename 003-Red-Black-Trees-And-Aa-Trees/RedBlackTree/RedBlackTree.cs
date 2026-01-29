namespace RedBlackTree;

public class RedBlackTree<T>
    : IBinarySearchTree<T> where T : IComparable
{
    private const bool Red = true;
    private const bool Black = false;

    private Node root;

    private RedBlackTree(Node node)
    {
        PreOrderCopy(node);
    }

    public RedBlackTree()
    {
    }

    public void Insert(T element)
    {
        root = Insert(element, root);
        root.Color = Black;
    }

    public bool Contains(T element)
    {
        var current = FindElement(element);

        return current != null;
    }

    public void EachInOrder(Action<T> action)
    {
        EachInOrder(root, action);
    }

    public IBinarySearchTree<T> Search(T element)
    {
        var current = FindElement(element);

        return new RedBlackTree<T>(current);
    }

    public void DeleteMin()
    {
        if (root == null) throw new InvalidOperationException();

        root = DeleteMin(root);
    }

    public T Select(int rank)
    {
        var node = Select(rank, root);
        if (node == null) throw new InvalidOperationException();

        return node.Value;
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        var queue = new Queue<T>();

        Range(root, queue, startRange, endRange);

        return queue;
    }

    public void Delete(T element)
    {
        if (root == null) throw new InvalidOperationException();

        root = Delete(element, root);
    }

    public void DeleteMax()
    {
        if (root == null) throw new InvalidOperationException();

        root = DeleteMax(root);
    }

    public int Count()
    {
        return Count(root);
    }

    public int Rank(T element)
    {
        return Rank(element, root);
    }

    public T Ceiling(T element)
    {
        return Select(Rank(element) + 1);
    }

    public T Floor(T element)
    {
        return Select(Rank(element) - 1);
    }

    private Node DeleteMin(Node node)
    {
        if (node.Left == null) return node.Right;

        node.Left = DeleteMin(node.Left);
        node.Count = 1 + Count(node.Left) + Count(node.Right);

        return node;
    }

    private Node FindElement(T element)
    {
        var current = root;

        while (current != null)
            if (current.Value.CompareTo(element) > 0)
                current = current.Left;
            else if (current.Value.CompareTo(element) < 0)
                current = current.Right;
            else
                break;

        return current;
    }

    private void PreOrderCopy(Node node)
    {
        if (node == null) return;

        Insert(node.Value);
        PreOrderCopy(node.Left);
        PreOrderCopy(node.Right);
    }

    private Node Insert(T element, Node node)
    {
        if (node == null)
            node = new Node(element);
        else if (element.CompareTo(node.Value) < 0)
            node.Left = Insert(element, node.Left);
        else if (element.CompareTo(node.Value) > 0) node.Right = Insert(element, node.Right);

        if (IsRed(node.Right) && !IsRed(node.Left)) node = RotateLeft(node);

        if (IsRed(node.Left) && IsRed(node.Left.Left)) node = RotateRight(node);

        if (IsRed(node.Left) && IsRed(node.Right)) FlipColors(node);

        node.Count = 1 + Count(node.Left) + Count(node.Right);
        return node;
    }

    private void Range(Node node, Queue<T> queue, T startRange, T endRange)
    {
        if (node == null) return;

        var nodeInLowerRange = startRange.CompareTo(node.Value);
        var nodeInHigherRange = endRange.CompareTo(node.Value);

        if (nodeInLowerRange < 0) Range(node.Left, queue, startRange, endRange);

        if (nodeInLowerRange <= 0 && nodeInHigherRange >= 0) queue.Enqueue(node.Value);

        if (nodeInHigherRange > 0) Range(node.Right, queue, startRange, endRange);
    }

    private void EachInOrder(Node node, Action<T> action)
    {
        if (node == null) return;

        EachInOrder(node.Left, action);
        action(node.Value);
        EachInOrder(node.Right, action);
    }

    private int Count(Node node)
    {
        if (node == null) return 0;

        return node.Count;
    }

    private Node Delete(T element, Node node)
    {
        if (node == null) return null;

        var compare = element.CompareTo(node.Value);

        if (compare < 0)
        {
            node.Left = Delete(element, node.Left);
        }
        else if (compare > 0)
        {
            node.Right = Delete(element, node.Right);
        }
        else
        {
            if (node.Right == null) return node.Left;

            if (node.Left == null) return node.Right;

            var temp = node;
            node = FindMin(temp.Right);
            node.Right = DeleteMin(temp.Right);
            node.Left = temp.Left;
        }

        node.Count = Count(node.Left) + Count(node.Right) + 1;

        return node;
    }

    private Node FindMin(Node node)
    {
        if (node.Left == null) return node;

        return FindMin(node.Left);
    }

    private Node DeleteMax(Node node)
    {
        if (node.Right == null) return node.Left;

        node.Right = DeleteMax(node.Right);
        node.Count = 1 + Count(node.Left) + Count(node.Right);

        return node;
    }

    private int Rank(T element, Node node)
    {
        if (node == null) return 0;

        var compare = element.CompareTo(node.Value);

        if (compare < 0) return Rank(element, node.Left);

        if (compare > 0) return 1 + Count(node.Left) + Rank(element, node.Right);

        return Count(node.Left);
    }

    private Node Select(int rank, Node node)
    {
        if (node == null) return null;

        var leftCount = Count(node.Left);

        if (leftCount == rank) return node;

        if (leftCount > rank) return Select(rank, node.Left);

        return Select(rank - (leftCount + 1), node.Right);
    }

    private bool IsRed(Node node)
    {
        if (node == null) return false;

        return node.Color == Red;
    }

    private Node RotateLeft(Node node)
    {
        var temp = node.Right;

        node.Right = temp.Left;
        temp.Left = node;
        temp.Color = node.Color;
        node.Color = Red;
        node.Count = 1 + Count(node.Left) + Count(node.Right);

        return temp;
    }

    private Node RotateRight(Node node)
    {
        var temp = node.Left;

        node.Left = temp.Right;
        temp.Right = node;
        temp.Color = node.Color;
        node.Color = Red;
        node.Count = 1 + Count(node.Left) + Count(node.Right);

        return temp;
    }

    private void FlipColors(Node node)
    {
        node.Color = Red;
        node.Left.Color = Black;
        node.Right.Color = Black;
    }

    private class Node
    {
        public Node(T value)
        {
            Value = value;
            Color = Red;
        }

        public T Value { get; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public int Count { get; set; }

        public bool Color { get; set; }
    }
}