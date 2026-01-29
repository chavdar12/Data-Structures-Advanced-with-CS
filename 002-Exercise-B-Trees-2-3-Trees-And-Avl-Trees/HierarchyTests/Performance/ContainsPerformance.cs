using System.Diagnostics;

namespace HierarchyTests.Performance;

public class ContainsPerformance
{
    [Test]
    public void PerformanceContains_With50000LookUpsInReversedOrderWith50000ElementsWith1Parent()
    {
        var hierarchy = new Hierarchy<int>(-3);

        for (var i = 1; i < 50001; i++) hierarchy.Add(-3, i);

        var timer = new Stopwatch();
        timer.Start();
        for (var i = 50000; i > 0; i--)
            Assert.IsTrue(hierarchy.Contains(i), "Contains method returned wrong value!");

        timer.Stop();
        Assert.IsTrue(timer.ElapsedMilliseconds < 50);
    }


    [Test]
    public void PerformanceContains_With25000ExistingAnd25000NonexistentElements()
    {
        var hierarchy = new Hierarchy<int>(-1);

        for (var i = 1; i < 50001; i = i + 2) hierarchy.Add(i - 2, i);

        var timer = new Stopwatch();
        timer.Start();

        for (var i = 1; i < 50001; i = i + 2)
        {
            Assert.IsTrue(hierarchy.Contains(i), "Contains method returned wrong value!");
            Assert.IsFalse(hierarchy.Contains(i + 1), "Contains method returned wrong value!");
        }

        timer.Stop();
        Assert.IsTrue(timer.ElapsedMilliseconds < 100);
    }
}