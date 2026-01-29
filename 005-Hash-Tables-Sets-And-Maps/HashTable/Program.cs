namespace HashTable;

internal class Program
{
    private static void Main(string[] args)
    {
        var table = new HashTable<int, string>();

        table.Add(1, "one");
        table.Add(2, "two");
        table.Add(3, "three");

        Console.WriteLine(table[1]);
    }
}