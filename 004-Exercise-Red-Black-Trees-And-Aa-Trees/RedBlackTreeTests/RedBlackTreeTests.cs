namespace RedBlackTreeTests;

public class RedBlackThreeTests
{
    private RedBlackTree<string> RedBlackTree;

    private string[] input =
    [
        "S",
        "E",
        "A",
        "R",
        "C",
        "H",
        "E",
        "X",
        "A",
        "M",
        "P",
        "L",
        "E"
    ];

    [SetUp]
    public void Setup()
    {
        RedBlackTree = new RedBlackTree<string>();

        for (var i = 0; i < input.Length; i++) RedBlackTree.Insert(input[i]);
    }

    [Test]
    public void TestEachInOrder()
    {
        var expected = new List<string>
        {
            "A",
            "C",
            "E",
            "H",
            "L",
            "M",
            "P",
            "R",
            "S",
            "X"
        };

        Assert.That(RedBlackTree.Count, Is.EqualTo(expected.Count));

        var count = 0;
        RedBlackTree.EachInOrder(n => StringAssert.AreEqualIgnoringCase(n, expected[count++]));

        var list = new List<string>();
        RedBlackTree.EachInOrder(n => list.Add(n));

        CollectionAssert.AreEqual(list, expected);
    }

    [Test]
    public void TestCount()
    {
        Assert.That(RedBlackTree.Count, Is.EqualTo(10));
        Assert.That(new RedBlackTree<string>().Count, Is.EqualTo(0));
    }

    [Test]
    public void TestContains()
    {
        Assert.True(RedBlackTree.Contains("H"));
        Assert.False(RedBlackTree.Contains("Pesho"));
    }

    [Test]
    public void TestInsert()
    {
        Assert.False(RedBlackTree.Contains("Pesho"));
        RedBlackTree.Insert("Pesho");
        Assert.True(RedBlackTree.Contains("Pesho"));
    }

    [Test]
    public void TestSecondInsert()
    {
        Assert.False(RedBlackTree.Contains("Pesho"));
        RedBlackTree.Insert("Pesho");
        Assert.True(RedBlackTree.Contains("Pesho"));
    }

    [Test]
    public void TestDeleteMin()
    {
        RedBlackTree.DeleteMin();
        Assert.AreEqual(9, RedBlackTree.Count);
        Assert.False(RedBlackTree.Contains("A"));
    }

    [Test]
    public void TestDeleteMax()
    {
        RedBlackTree.DeleteMax();
        Assert.That(RedBlackTree.Count, Is.EqualTo(9));
        Assert.False(RedBlackTree.Contains("X"));
    }

    [Test]
    public void TestDelete()
    {
        RedBlackTree.Delete("M");
        Assert.That(RedBlackTree.Count, Is.EqualTo(9));
        Assert.False(RedBlackTree.Contains("M"));
    }

    [Test]
    public void TestSelect()
    {
        var selected = RedBlackTree.Select(3);
        Assert.NotNull(selected);
        Assert.That(selected, Is.EqualTo("H"));
    }

    [Test]
    public void TestRank()
    {
        var rank = RedBlackTree.Rank("H");
        Assert.That(rank, Is.EqualTo(3));
    }

    [Test]
    public void TestCeiling()
    {
        var ceil = RedBlackTree.Ceiling("A");
        Assert.That(ceil, Is.EqualTo("C"));
    }

    [Test]
    public void TestFloor()
    {
        var floor = RedBlackTree.Floor("B");
        Assert.That(floor, Is.EqualTo("A"));
    }
}