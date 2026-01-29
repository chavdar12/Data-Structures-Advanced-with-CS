using System.Diagnostics;

namespace HierarchyTests.Performance;

public class ForEachPerformance
{
    [Test]
    public void PerformanceForEach_With55500Elements()
    {
        var start1 = 0;
        var start2 = 5000;
        var start3 = 100000;
        var elements = new List<int>();
        elements.Add(start1);
        var hierarchy = new Hierarchy<int>(start1);

        for (var i = 1; i <= 500; i++)
        {
            elements.Add(i);
            hierarchy.Add(start1, i);
            for (var j = 0; j < 10; j++)
            {
                elements.Add(start2);
                hierarchy.Add(i, start2);
                for (var k = 0; k < 10; k++)
                {
                    elements.Add(start3);
                    hierarchy.Add(start2, start3++);
                }

                start2++;
            }
        }

        elements.Sort();
        var counter = 0;

        var timer = new Stopwatch();
        timer.Start();

        foreach (var element in hierarchy)
            Assert.AreEqual(elements[counter++], element, "Expected element did not match!");

        timer.Stop();
        Assert.IsTrue(timer.ElapsedMilliseconds < 400);

        Assert.AreEqual(counter, hierarchy.Count, "Incorect number of elements returned!");
    }

    [Test]
    public void PerformanceForEach_With55500ElementsInterconnected()
    {
        var start1 = 0;
        var elements = new List<int>();
        elements.Add(start1);
        var hierarchy = new Hierarchy<int>(start1);

        for (var i = 1; i <= 51100; i = i + 511)
        {
            hierarchy.Add(start1, i);
            for (var j = i + 1; j <= i + 510; j = j + 51)
            {
                hierarchy.Add(i, j);
                for (var k = j + 1; k <= j + 50; k++) hierarchy.Add(j, k);
            }
        }

        for (var i = 1; i <= 51100; i = i + 511) elements.Add(i);

        for (var i = 1; i <= 51100; i = i + 511)
        for (var j = i + 1; j <= i + 510; j = j + 51)
            elements.Add(j);

        for (var i = 1; i <= 51100; i = i + 511)
        for (var j = i + 1; j <= i + 510; j = j + 51)
        for (var k = j + 1; k <= j + 50; k++)
            elements.Add(k);

        var counter = 0;

        var timer = new Stopwatch();
        timer.Start();

        foreach (var element in hierarchy)
            Assert.AreEqual(elements[counter++], element, "Expected element did not match!");

        timer.Stop();
        Assert.IsTrue(timer.ElapsedMilliseconds < 400);

        Assert.AreEqual(counter, hierarchy.Count, "Incorect number of elements returned!");
    }
}