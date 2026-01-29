namespace RedBlackTreeTests.Correctness;

[TestFixture]
internal class InsertThird
{
    [Test]
    public void Insert_MultipleElements_ShouldBeBalanced()
    {
        var rbt = new RedBlackTree<int>();
        rbt.Insert(5);
        rbt.Insert(12);
        rbt.Insert(18);
        rbt.Insert(37);
        rbt.Insert(48);
        rbt.Insert(60);
        rbt.Insert(80);

        Assert.That(rbt.Search(12).Count(), Is.EqualTo(3));
        Assert.That(rbt.Search(60).Count(), Is.EqualTo(3));
    }
}