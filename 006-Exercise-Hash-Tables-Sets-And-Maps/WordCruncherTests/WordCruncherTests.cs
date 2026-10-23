namespace WordCruncherTests;

public class WordCruncherTests
{
    [Test]
    public void GetPaths_ReturnsAllCombinationsSorted()
    {
        var cruncher = new WordCruncher.WordCruncher(["text", "me", "so", "do", "m", "ran"], "somerandomtext");

        CollectionAssert.AreEqual(new[] { "so me ran do m text" }, cruncher.GetPaths());
    }

    [Test]
    public void GetPaths_WithAlternatives_ReturnsEachOnceInOrder()
    {
        var cruncher = new WordCruncher.WordCruncher(["a", "b", "ab", "a"], "ab");

        CollectionAssert.AreEqual(new[] { "a b", "ab" }, cruncher.GetPaths());
    }

    [Test]
    public void GetPaths_UsesEachWordAtMostAsManyTimesAsGiven()
    {
        Assert.That(new WordCruncher.WordCruncher(["a"], "aa").GetPaths(), Is.Empty);
        CollectionAssert.AreEqual(new[] { "a a" }, new WordCruncher.WordCruncher(["a", "a"], "aa").GetPaths());
    }

    [Test]
    public void GetPaths_NoMatch_ReturnsEmpty()
    {
        Assert.That(new WordCruncher.WordCruncher(["x", "y"], "abc").GetPaths(), Is.Empty);
    }
}
