namespace AATree;

public class Node<T> where T : IComparable<T>
{
    public Node(T element)
    {
        Value = element;
        Level = 1;
        Count = 1;
    }

    public T Value { get; set; }

    public Node<T> Right { get; set; }

    public Node<T> Left { get; set; }

    public int Level { get; set; }

    public int Count { get; set; }
}