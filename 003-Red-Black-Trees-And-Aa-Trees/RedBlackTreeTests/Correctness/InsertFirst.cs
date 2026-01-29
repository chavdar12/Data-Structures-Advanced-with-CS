namespace RedBlackTreeTests.Correctness;

[TestFixture]
internal class InsertFirst
{
    [Test]
    public void Insert_SingleElement_ShouldIncreaseCount()
    {
        var rbt = new RedBlackTree<int>();
        rbt.Insert(5);

        Assert.That(rbt.Count(), Is.EqualTo(1));
    }
}