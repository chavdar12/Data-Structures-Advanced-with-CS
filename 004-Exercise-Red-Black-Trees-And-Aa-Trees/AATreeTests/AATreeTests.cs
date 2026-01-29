namespace AATreeTests;

public class AaTreeTests
{
    private AATree<int> AATree;

    private int[] input =
    [
        18,
        13,
        1,
        7,
        42,
        73,
        56,
        24,
        6,
        2,
        74,
        69,
        55
    ];

    [SetUp]
    public void Setup()
    {
        AATree = new AATree<int>();

        foreach (var integer in input) AATree.Insert(integer);
    }


    [Test]
    public void TestCountNodes()
    {
        Assert.That(AATree.CountNodes(), Is.EqualTo(13));
        Assert.That(new AATree<int>().CountNodes(), Is.EqualTo(0));
    }

    [Test]
    public void TestIsEmpty()
    {
        Assert.IsFalse(AATree.IsEmpty());
        Assert.IsTrue(new AATree<int>().IsEmpty());
    }

    [Test]
    public void TestSearch()
    {
        Assert.IsTrue(AATree.Search(73));
        Assert.False(new AATree<int>().Search(100));
    }

    [Test]
    public void TestPreOrder()
    {
        var actual = new List<int>();
        AATree.PreOrder(n => actual.Add(n));

        var expected = new List<int>() { 13, 6, 1, 2, 7, 56, 42, 18, 24, 55, 73, 69, 74 };

        Assert.That(actual.Count, Is.EqualTo(expected.Count));
        CollectionAssert.AreEqual(expected, actual);
    }

    [Test]
    public void TestInOrder()
    {
        var numbers = new List<int>();
        AATree.InOrder(n => numbers.Add(n));
        var expected = new List<int>() { 1, 2, 6, 7, 13, 18, 24, 42, 55, 56, 69, 73, 74 };

        Assert.That(numbers.Count, Is.EqualTo(expected.Count));
        CollectionAssert.AreEqual(expected, numbers);
    }

    [Test]
    public void TestPostOrder()
    {
        var actual = new List<int>();
        AATree.PostOrder(n => actual.Add(n));

        var expected = new List<int>() { 2, 1, 7, 6, 24, 18, 55, 42, 69, 74, 73, 56, 13 };

        Assert.That(actual.Count, Is.EqualTo(expected.Count));
        CollectionAssert.AreEqual(expected, actual);
    }
}