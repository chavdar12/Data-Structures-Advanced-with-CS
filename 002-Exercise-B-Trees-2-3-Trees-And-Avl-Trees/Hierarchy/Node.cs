namespace Hierarchy;

public class Node<T>
{
    public T Value { get; private set; }

    public Node<T> Parent { get; set; }

    public List<Node<T>> Children { get; private set; }

    public Node(T value)
    {
        Value = value;
        Children = new List<Node<T>>();
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }
}