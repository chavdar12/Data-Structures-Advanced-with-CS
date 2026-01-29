namespace RedBlackTreeTests.Performance;

[TestFixture]
internal class InsertFourth
{
    [Test]
    [Timeout(500)]
    public void Insert_MultipleElements_ShouldBeFast()
    {
        var rbt = new RedBlackTree<int>();

        for (var i = 0; i < 100000; i++) rbt.Insert(i);
    }
}