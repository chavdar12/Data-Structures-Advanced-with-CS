namespace RedBlackTreeTests.Performance;

[TestFixture]
public class FifthTest
{
    [Test]
    [Timeout(600)]
    public void Insert_MultipleElements_ShouldHaveQuickFind()
    {
        var rbt = new RedBlackTree<int>();

        for (var i = 0; i < 100000; i++) rbt.Insert(i);

        Assert.That(rbt.Contains(99999), Is.EqualTo(true));
    }
}