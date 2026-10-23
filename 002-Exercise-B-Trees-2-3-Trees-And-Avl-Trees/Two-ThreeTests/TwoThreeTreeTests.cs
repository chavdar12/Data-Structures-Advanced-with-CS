namespace Two_ThreeTests;

public class TwoThreeTreeTests
{
    [Test]
    public void TestInsertZero()
    {
        var tree = new TwoThreeTree<string>();
        Assert.That(tree.ToString(), Is.EqualTo(""));
    }

    [Test]
    public void TestInsertSingle()
    {
        var tree = new TwoThreeTree<string>();
        tree.Insert("13");

        Assert.That(tree.ToString(), Is.EqualTo("13 " + Environment.NewLine));
    }

    [Test]
    public void TestInsertMany()
    {
        var tree = new TwoThreeTree<string>();
        tree.Insert("A");
        tree.Insert("B");
        tree.Insert("C");
        Assert.That(tree.ToString(), Is.EqualTo("B " + Environment.NewLine +
                                                "A " + Environment.NewLine +
                                                "C " + Environment.NewLine));
    }

    [Test]
    public void TestInsert13Elements()
    {
        var tree = new TwoThreeTree<string>();

        string[] arr = { "F", "C", "G", "A", "B", "D", "E", "K", "I", "G", "H", "J", "K" };
        for (var i = 0; i < 13; i++) tree.Insert(arr[i]);

        Assert.That(tree.ToString(), Is.EqualTo("D G" + Environment.NewLine +
                                                "B " + Environment.NewLine +
                                                "A " + Environment.NewLine +
                                                "C " + Environment.NewLine +
                                                "F " + Environment.NewLine +
                                                "E " + Environment.NewLine +
                                                "G " + Environment.NewLine +
                                                "I K" + Environment.NewLine +
                                                "H " + Environment.NewLine +
                                                "J " + Environment.NewLine +
                                                "K " + Environment.NewLine));
    }

    [Test]
    public void TestInsertValueTypes_KeepsAllKeys()
    {
        var tree = new TwoThreeTree<int>();
        var keys = Enumerable.Range(0, 200).Select(i => i * 37 % 200).ToArray();
        foreach (var key in keys) tree.Insert(key);

        var printed = tree.ToString()
            .Split((char[])[' ', '\r', '\n'], StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .OrderBy(k => k);

        CollectionAssert.AreEqual(keys.OrderBy(k => k), printed);
    }

    [Test]
    public void TestInsertSingleValueType()
    {
        var tree = new TwoThreeTree<int>();
        tree.Insert(0);

        Assert.That(tree.ToString(), Is.EqualTo("0 " + Environment.NewLine));
    }
}
