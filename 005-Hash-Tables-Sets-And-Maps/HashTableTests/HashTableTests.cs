namespace HashTableTests;

public class HashTableTests
{
    [Test]
    public void Add_EmptyHashTable_NoDuplicates_ShouldAddElement()
    {
        // Arrange
        var hashTable = new HashTable<string, int>();

        // Act
        KeyValue<string, int>[] elements =
        {
            new("Peter", 5),
            new("Maria", 6),
            new("George", 4),
            new("Kiril", 5)
        };
        foreach (var element in elements) hashTable.Add(element.Key, element.Value);

        // Assert
        var actualElements = hashTable.ToList();
        CollectionAssert.AreEquivalent(elements, actualElements);
    }

    [Test]
    public void Add_EmptyHashTable_Duplicates_ShouldThrowException()
    {
        // Arrange
        Assert.Throws<ArgumentException>(delegate()
        {
            var hashTable = new HashTable<string, string>
                { { "Peter", "first" }, { "Peter", "second" } };
        });
    }

    [Test]
    public void Add_1000_Elements_Grow_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, int>(1);

        // Act
        var expectedElements = new List<KeyValue<string, int>>();
        for (var i = 0; i < 1000; i++)
        {
            hashTable.Add("key" + i, i);
            expectedElements.Add(new KeyValue<string, int>("key" + i, i));
        }

        // Assert
        var actualElements = hashTable.ToList();
        CollectionAssert.AreEquivalent(expectedElements, actualElements);
    }

    [Test]
    public void AddOrReplace_WithDuplicates_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, int>();

        // Act
        hashTable.AddOrReplace("Peter", 555);
        hashTable.AddOrReplace("Maria", 999);
        hashTable.AddOrReplace("Maria", 123);
        hashTable.AddOrReplace("Maria", 6);
        hashTable.AddOrReplace("Peter", 5);

        // Assert
        KeyValue<string, int>[] expectedElements =
        {
            new("Peter", 5),
            new("Maria", 6)
        };
        var actualElements = hashTable.ToList();
        CollectionAssert.AreEquivalent(expectedElements, actualElements);
    }

    [Test]
    public void Count_Empty_Add_Remove_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, int>();

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(0));

        // Act & Assert
        hashTable.Add("Peter", 555);
        hashTable.AddOrReplace("Peter", 555);
        hashTable.AddOrReplace("Ivan", 555);
        Assert.That(hashTable.Count, Is.EqualTo(2));

        // Act & Assert
        hashTable.Remove("Peter");
        Assert.That(hashTable.Count, Is.EqualTo(1));

        // Act & Assert
        hashTable.Remove("Peter");
        Assert.That(hashTable.Count, Is.EqualTo(1));

        // Act & Assert
        hashTable.Remove("Ivan");
        Assert.That(hashTable.Count, Is.EqualTo(0));
    }

    [Test]
    public void Get_ExistingElement_ShouldReturnTheValue()
    {
        // Arrange
        var hashTable = new HashTable<int, string> { { 555, "Peter" } };

        // Act
        var actualValue = hashTable.Get(555);

        // Assert
        Assert.That(actualValue, Is.EqualTo("Peter"));
    }

    [Test]
    public void Get_NonExistingElement_ShouldThrowException()
    {
        // Arrange
        var hashTable = new HashTable<int, string>();

        // Act
        Assert.Throws<KeyNotFoundException>(delegate() { hashTable.Get(12345); });
    }

    [Test]
    public void Indexer_ExistingElement_ShouldReturnTheValue()
    {
        // Arrange
        var hashTable = new HashTable<int, string> { { 555, "Peter" } };

        // Act
        var actualValue = hashTable[555];

        // Assert
        Assert.That(actualValue, Is.EqualTo("Peter"));
    }

    [Test]
    public void Indexer_NonExistingElement_ShouldThrowException()
    {
        // Arrange
        var hashTable = new HashTable<int, string>();

        // Act
        Assert.Throws<KeyNotFoundException>(delegate()
        {
            var value = hashTable[12345];
        });
    }

    [Test]
    public void Indexer_AddReplace_WithDuplicates_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, int>
        {
            ["Peter"] = 555,
            ["Maria"] = 999,
            ["Maria"] = 123,
            ["Maria"] = 6,
            ["Peter"] = 5
        };

        // Act

        // Assert
        KeyValue<string, int>[] expectedElements =
        [
            new KeyValue<string, int>("Peter", 5),
            new KeyValue<string, int>("Maria", 6)
        ];
        var actualElements = hashTable.ToList();
        CollectionAssert.AreEquivalent(expectedElements, actualElements);
    }

    [Test]
    public void Capacity_Grow_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, int>(2);

        // Assert
        Assert.That(hashTable.Capacity, Is.EqualTo(2));

        // Act
        hashTable.Add("Peter", 123);
        hashTable.Add("Maria", 456);

        // Assert
        Assert.That(hashTable.Capacity, Is.EqualTo(4));

        // Act
        hashTable.Add("Tanya", 555);
        hashTable.Add("George", 777);

        // Assert
        Assert.That(hashTable.Capacity, Is.EqualTo(8));
    }

    [Test]
    public void TryGetValue_ExistingElement_ShouldReturnTheValue()
    {
        // Arrange
        var hashTable = new HashTable<int, string> { { 555, "Peter" } };

        // Act
        var result = hashTable.TryGetValue(555, out var value);

        // Assert
        Assert.That(value, Is.EqualTo("Peter"));
        Assert.IsTrue(result);
    }

    [Test]
    public void TryGetValue_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        var hashTable = new HashTable<int, string>();

        // Act
        var result = hashTable.TryGetValue(555, out var value);

        // Assert
        Assert.IsFalse(result);
    }

    [Test]
    public void Find_ExistingElement_ShouldReturnTheElement()
    {
        // Arrange
        var hashTable = new HashTable<string, DateTime>();
        var name = "Maria";
        var date = new DateTime(1995, 7, 18);
        hashTable.Add(name, date);

        // Act
        var element = hashTable.Find(name);

        // Assert
        var expectedElement = new KeyValue<string, DateTime>(name, date);
        Assert.That(element, Is.EqualTo(expectedElement));
    }

    [Test]
    public void Find_NonExistingElement_ShouldReturnNull()
    {
        // Arrange
        var hashTable = new HashTable<string, DateTime>();

        // Act
        var element = hashTable.Find("Maria");

        // Assert
        Assert.IsNull(element);
    }

    [Test]
    public void ContainsKey_ExistingElement_ShouldReturnTrue()
    {
        // Arrange
        var hashTable = new HashTable<DateTime, string>();
        var date = new DateTime(1995, 7, 18);
        hashTable.Add(date, "Some value");

        // Act
        var containsKey = hashTable.ContainsKey(date);

        // Assert
        Assert.IsTrue(containsKey);
    }

    [Test]
    public void ContainsKey_NonExistingElement_ShouldReturnFalse()
    {
        // Arrange
        var hashTable = new HashTable<DateTime, string>();
        var date = new DateTime(1995, 7, 18);

        // Act
        var containsKey = hashTable.ContainsKey(date);

        // Assert
        Assert.IsFalse(containsKey);
    }

    [Test]
    public void Remove_ExistingElement_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, double> { { "Peter", 12.5 }, { "Maria", 99.9 } };

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(2));

        // Act
        var removed = hashTable.Remove("Peter");

        // Assert
        Assert.IsTrue(removed);
        Assert.That(hashTable.Count, Is.EqualTo(1));
    }

    [Test]
    public void Remove_NonExistingElement_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, double> { { "Peter", 12.5 }, { "Maria", 99.9 } };

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(2));

        // Act
        var removed = hashTable.Remove("George");

        // Assert
        Assert.IsFalse(removed);
        Assert.That(hashTable.Count, Is.EqualTo(2));
    }

    [Test]
    public void Remove_5000_Elements_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, int>();
        var keys = new List<string>();
        var count = 5000;
        for (var i = 0; i < count; i++)
        {
            var key = Guid.NewGuid().ToString();
            keys.Add(key);
            hashTable.Add(key, i);
        }

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(count));

        // Act & Assert
        keys.Reverse();
        foreach (var key in keys)
        {
            hashTable.Remove(key);
            count--;
            Assert.That(hashTable.Count, Is.EqualTo(count));
        }

        // Assert
        var expectedElements = new List<KeyValue<string, int>>();
        var actualElements = hashTable.ToList();
        CollectionAssert.AreEquivalent(expectedElements, actualElements);
    }

    [Test]
    public void Clear_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, int>();

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(0));

        // Act
        hashTable.Clear();

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(0));

        // Arrange
        hashTable.Add("Peter", 5);
        hashTable.Add("George", 7);
        hashTable.Add("Maria", 3);

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(3));

        // Act
        hashTable.Clear();

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(0));
        var expectedElements = new List<KeyValue<string, int>>();
        var actualElements = hashTable.ToList();
        CollectionAssert.AreEquivalent(expectedElements, actualElements);

        hashTable.Add("Peter", 5);
        hashTable.Add("George", 7);
        hashTable.Add("Maria", 3);

        // Assert
        Assert.That(hashTable.Count, Is.EqualTo(3));
    }

    [Test]
    public void Keys_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, double>();

        // Assert
        CollectionAssert.AreEquivalent(new string[0], hashTable.Keys.ToArray());

        // Arrange
        hashTable.Add("Peter", 12.5);
        hashTable.Add("Maria", 99.9);
        hashTable["Peter"] = 123.45;

        // Act
        var keys = hashTable.Keys;

        // Assert
        string[] expectedKeys = { "Peter", "Maria" };
        CollectionAssert.AreEquivalent(expectedKeys, keys.ToArray());
    }

    [Test]
    public void Values_ShouldWorkCorrectly()
    {
        // Arrange
        var hashTable = new HashTable<string, double>();

        // Assert
        CollectionAssert.AreEquivalent(Array.Empty<string>(), hashTable.Values.ToArray());

        // Arrange
        hashTable.Add("Peter", 12.5);
        hashTable.Add("Maria", 99.9);
        hashTable["Peter"] = 123.45;

        // Act
        var values = hashTable.Values;

        // Assert
        double[] expectedValues = { 99.9, 123.45 };
        CollectionAssert.AreEquivalent(expectedValues, values.ToArray());
    }
}