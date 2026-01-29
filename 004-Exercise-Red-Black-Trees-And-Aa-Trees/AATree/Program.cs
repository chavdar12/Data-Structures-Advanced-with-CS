namespace AATree;

internal class Program
{
    private static void Main(string[] args)
    {
        var tree = new AATree<int>();

        tree.Insert(10);
        tree.Insert(20);
        tree.Insert(30);

        Console.WriteLine(tree.Search(20));
        Console.WriteLine(tree.Search(40));

        tree.InOrder(Console.WriteLine);

        tree.Clear();

        Console.WriteLine(tree.IsEmpty());
    }
}