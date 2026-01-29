namespace AVL;

internal class Program
{
    private static void Main(string[] args)
    {
        var alv = new AVL<int>();

        alv.Insert(1);
        alv.Insert(2);
        alv.Insert(3);
    }
}