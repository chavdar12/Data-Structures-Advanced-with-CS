namespace WordCruncher;

internal class Program
{
    private static void Main(string[] args)
    {
        var input = Console.ReadLine().Split(", ");
        var target = Console.ReadLine();

        var wc = new WordCruncher(input, target);

        foreach (var path in wc.GetPaths()) Console.WriteLine(path);
    }
}