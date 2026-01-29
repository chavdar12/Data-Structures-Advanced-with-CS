namespace Two_Three;

internal class Program
{
    private static void Main(string[] args)
    {
        var three = new TwoThreeTree<int>();
        three.Insert(1);
        three.Insert(2);
        three.Insert(3);

        Console.WriteLine(three);
    }
}