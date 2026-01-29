namespace HierarchyTests.Correctness;

public class GetParent : BaseTest
{
    [Test]
    public void GetParent_WithNonExistentElement_ShouldThrowException()
    {
        Assert.Throws<ArgumentException>(() => { Hierarchy.GetParent(-17); });
    }

    [Test]
    public void GetParent_WithRoot_ShouldReturnDefault()
    {
        var result = Hierarchy.GetParent(DefaultRootValue);

        Assert.AreEqual(default(int), result, "GetParent command returned a wrong result!");
    }

    [Test]
    public void GetParent_WithANodeWithAParent_ShouldReturnParentValue()
    {
        Hierarchy.Add(DefaultRootValue, 17);
        Hierarchy.Add(DefaultRootValue, 20);
        Hierarchy.Add(17, 22);
        Hierarchy.Add(20, 15);
        Hierarchy.Add(20, -33);

        var result = Hierarchy.GetParent(-33);

        Assert.AreEqual(20, result, "GetParent command returned a wrong result!");
    }
}