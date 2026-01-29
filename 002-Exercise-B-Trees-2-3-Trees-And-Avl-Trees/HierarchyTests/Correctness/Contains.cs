namespace HierarchyTests.Correctness;

public class Contains : BaseTest
{
    [Test]
    public void Contains_WithANonExistentElement_ShouldReturnFalse()
    {
        var result = Hierarchy.Contains(1);

        Assert.AreEqual(false, result, "Contains command returned a wrong result!");
    }

    [Test]
    public void Contains_WithASingleElement_ShouldReturnTrue()
    {
        var result = Hierarchy.Contains(DefaultRootValue);

        Assert.AreEqual(true, result, "Contains command returned a wrong result!");
    }

    [Test]
    public void Contains_WithMultipleSearchesForASingleElement_ShouldReturnConsistentResult()
    {
        Hierarchy.Add(DefaultRootValue, 666);

        Assert.AreEqual(true, Hierarchy.Contains(666), "Contains command returned wrong result!");
        Assert.AreEqual(true, Hierarchy.Contains(666), "Contains command returned wrong result!");
        Assert.AreEqual(true, Hierarchy.Contains(666), "Contains command returned wrong result!");
        Assert.AreEqual(true, Hierarchy.Contains(666), "Contains command returned wrong result!");
    }

    [Test]
    public void Contains_WithBothExistingAndNonexsitantElements_ShouldReturnCorrectResults()
    {
        Hierarchy.Add(DefaultRootValue, 666);
        Hierarchy.Add(666, 6666);
        Hierarchy.Add(6666, 66666);

        Assert.IsTrue(Hierarchy.Contains(666), "Contains command returned wrong result!");
        Assert.IsFalse(Hierarchy.Contains(667), "Contains command returned wrong result!");
        Assert.IsTrue(Hierarchy.Contains(6666), "Contains command returned wrong result!");
        Assert.IsFalse(Hierarchy.Contains(6665), "Contains command returned wrong result!");
        Assert.IsFalse(Hierarchy.Contains(-17000), "Contains command returned wrong result!");
        Assert.IsTrue(Hierarchy.Contains(66666), "Contains command returned wrong result!");
    }

    [Test]
    public void Contains_WithAnExistingElementWithMultipleElements_ShouldReturnTrue()
    {
        Hierarchy.Add(DefaultRootValue, 2);
        Hierarchy.Add(DefaultRootValue, 3);
        Hierarchy.Add(2, 4);
        Hierarchy.Add(3, 6);
        Hierarchy.Add(4, 7);

        var result = Hierarchy.Contains(6);

        Assert.AreEqual(true, result, "Contains command returned a wrong result!");
    }
}