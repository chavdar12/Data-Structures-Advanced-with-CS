using System.Diagnostics;

namespace HierarchyTests.Performance;

public class GetParentPerformance
{
    [Test]
    public void PerformanceGetParent_With50000ElementsWith1ParentInReversedOrder()
    {
        var hierarchy = new Hierarchy<int>(0);

        for (var i = 1; i < 50001; i++) hierarchy.Add(0, i);

        var timer = new Stopwatch();
        timer.Start();

        for (var i = 50000; i > 0; i--)
            Assert.AreEqual(0, hierarchy.GetParent(i), "Expected parent did not match!");

        timer.Stop();
        Assert.IsTrue(timer.ElapsedMilliseconds < 400);
    }

    [Test]
    public void PerformanceGetParent_With50000ElementsWith5000Parents()
    {
        var counter = 5001;
        var hierarchy = new Hierarchy<int>(-88);
        for (var i = 1; i <= 5000; i++)
        {
            hierarchy.Add(-88, i);
            for (var j = 0; j < 10; j++) hierarchy.Add(i, counter++);
        }

        counter = 5001;
        var timer = new Stopwatch();
        timer.Start();

        for (var i = 1; i <= 5000; i++)
        {
            Assert.AreEqual(-88, hierarchy.GetParent(i), "Expected parent did not match!");
            for (var j = 0; j < 10; j++)
                Assert.AreEqual(i, hierarchy.GetParent(counter++), "Expected parent did not match!");
        }

        timer.Stop();
        Assert.IsTrue(timer.ElapsedMilliseconds < 200);
    }
}