namespace RedBlackTree;

internal class Program
{
    private static void Main(string[] args)
    {
        var three = new RedBlackTree<int>();
        three.Insert(3);
        three.Insert(2);
        three.Insert(1);

        three.EachInOrder(Console.WriteLine);

        Console.WriteLine(three.Contains(2));

        var copy = three.Search(2);
        copy.EachInOrder(Console.WriteLine);

        three.DeleteMin();
    }
}