namespace HierarchyTests;

public class BaseTest
{
    public IHierarchy<int> Hierarchy { get; private set; }

    public const int DefaultRootValue = 5;

    [SetUp]
    public void Initialize()
    {
        Hierarchy = new Hierarchy<int>(DefaultRootValue);
    }
}