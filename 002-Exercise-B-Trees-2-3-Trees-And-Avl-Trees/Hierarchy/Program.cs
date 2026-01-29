namespace Hierarchy;

internal class Program
{
    private static void Main(string[] args)
    {
        var hierarchy = new Hierarchy<int>(5);
        hierarchy.Add(5, 6);
        hierarchy.Add(5, 7);
        hierarchy.Add(5, 8);
        hierarchy.Add(6, 9);

        Console.WriteLine(hierarchy.GetParent(9));
        Console.WriteLine(hierarchy.GetChildren(5).Count());

        hierarchy.Remove(6);
    }
}