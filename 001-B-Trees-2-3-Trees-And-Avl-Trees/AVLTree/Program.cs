namespace AVLTree;

internal class Program
{
    private static void Main(string[] args)
    {
        var avl = new AVL<int>();

        avl.Insert(1);
        avl.Insert(2);
        avl.Insert(3);
        avl.Insert(4);
        avl.Insert(5);
        avl.Insert(6);

        avl.EachInOrder(Console.WriteLine);

        Console.WriteLine(avl.Contains(3));
        Console.WriteLine(avl.Contains(7));

        avl.Insert(7);

        Console.WriteLine(avl.Contains(7));
    }
}