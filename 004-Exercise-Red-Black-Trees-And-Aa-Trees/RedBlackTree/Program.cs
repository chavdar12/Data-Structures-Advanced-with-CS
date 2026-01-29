namespace RedBlackTree;

internal class Program
{
    private static void Main(string[] args)
    {
        var tree = new RedBlackTree<int>();

        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(30);

        Console.WriteLine(tree.Search(20));
        Console.WriteLine(tree.Search(40));

        tree.EachInOrder(Console.WriteLine);

        tree.Delete(20);

        Console.WriteLine(tree.Search(20));

        tree.DeleteMin();
        tree.DeleteMax();
    }
}