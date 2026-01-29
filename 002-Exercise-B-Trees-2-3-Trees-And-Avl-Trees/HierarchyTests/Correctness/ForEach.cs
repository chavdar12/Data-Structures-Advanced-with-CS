namespace HierarchyTests.Correctness;

public class ForEach : BaseTest
{
    [Test]
    public void ForEach_WithOnlyRoot_ShouldIterateOnlyThroughRoot()
    {
        var count = 0;
        var collection = new int[] { DefaultRootValue };
        foreach (var element in Hierarchy)
            Assert.AreEqual(collection[count++], element, "Expected element did not match!");
    }

    [Test]
    public void ForEach_WithMultipleElements_ShouldIterateOverCollectionOnlyOnce()
    {
        var count = 0;
        Hierarchy.Add(DefaultRootValue, 50);
        Hierarchy.Add(DefaultRootValue, 70);
        Hierarchy.Add(70, 100);
        Hierarchy.Add(50, 200);
        Hierarchy.Add(70, 120);
        Hierarchy.Add(70, 110);
        Hierarchy.Add(110, 0);
        Hierarchy.Add(200, 201);
        Hierarchy.Add(201, 202);
        Hierarchy.Add(50, 300);
        var collection = new int[] { DefaultRootValue, 50, 70, 200, 300, 100, 120, 110, 201, 0, 202 };

        foreach (var element in Hierarchy)
            Assert.AreEqual(collection[count++], element, "Expected element did not match!");

        Assert.AreEqual(count, Hierarchy.Count, "Incorrect count of elements returned!");
    }

    [Test]
    public void ForEach_WithMultipleElements_ShouldIterateThroughThemInCorrectOrder()
    {
        Hierarchy.Add(DefaultRootValue, -10);
        Hierarchy.Add(DefaultRootValue, 10);
        Hierarchy.Add(-10, -11);
        Hierarchy.Add(-10, -12);
        Hierarchy.Add(10, 11);
        Hierarchy.Add(10, 12);
        Hierarchy.Add(-11, -13);
        Hierarchy.Add(-11, -14);
        Hierarchy.Add(-12, -15);
        Hierarchy.Add(-12, -16);
        Hierarchy.Add(11, 13);
        var count = 0;
        var collection = new int[] { DefaultRootValue, -10, 10, -11, -12, 11, 12, -13, -14, -15, -16, 13 };
        foreach (var element in Hierarchy)
            Assert.AreEqual(collection[count++], element, "Expected element did not match!");
    }
}