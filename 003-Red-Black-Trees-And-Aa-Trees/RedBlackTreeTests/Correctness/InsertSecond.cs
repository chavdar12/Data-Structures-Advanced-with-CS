namespace RedBlackTreeTests.Correctness;

[TestFixture]
public class InsertSecond
{
    [Test]
    public void Insert_MultipleElements_ShouldBeInsertedCorrectly()
    {
        var rbt = new RedBlackTree<int>();
        rbt.Insert(5);
        rbt.Insert(12);
        rbt.Insert(18);
        rbt.Insert(37);
        rbt.Insert(48);
        rbt.Insert(60);
        rbt.Insert(80);

        var nodes = new List<int>();
        rbt.EachInOrder(nodes.Add);

        // Assert
        var expectedNodes = new int[] { 5, 12, 18, 37, 48, 60, 80 };

        CollectionAssert.AreEqual(expectedNodes, nodes);
    }

    [Test]
    public void Ceiling_And_Floor_ShouldReturnNeighbours()
    {
        var rbt = new RedBlackTree<int>();
        foreach (var i in new[] { 10, 20, 30, 40 }) rbt.Insert(i);

        Assert.That(rbt.Ceiling(20), Is.EqualTo(30));
        Assert.That(rbt.Ceiling(25), Is.EqualTo(30));
        Assert.That(rbt.Floor(20), Is.EqualTo(10));
        Assert.That(rbt.Floor(25), Is.EqualTo(20));
    }
}
