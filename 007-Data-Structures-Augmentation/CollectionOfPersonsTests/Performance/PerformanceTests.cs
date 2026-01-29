namespace CollectionOfPersonsTests.Performance;

public class PerformanceTests
{
    [Test]
    [Timeout(250)]
    public void TestPerformance_AddPerson()
    {
        // Arrange
        var persons = new PersonCollection();

        // Act
        AddPersons(5000, persons);
        Assert.That(persons.Count, Is.EqualTo(5000));

        void AddPersons(int count, PersonCollection people)
        {
            for (var i = 0; i < count; i++)
                people.AddPerson(
                    "pesho" + i + "@gmail" + i % 100 + ".com",
                    "Pesho" + i % 100,
                    i % 100,
                    "Sofia" + i % 100);
        }
    }

    [Test]
    [Timeout(200)]
    public void TestPerformance_FindPerson()
    {
        // Arrange
        var persons = new PersonCollection();
        AddPersons(5000, persons);

        // Act
        for (var i = 0; i < 100000; i++)
        {
            var existingPerson = persons.FindPerson("pesho1@gmail1.com");
            Assert.IsNotNull(existingPerson);
            var nonExistingPerson = persons.FindPerson("non-existing email");
            Assert.IsNull(nonExistingPerson);
        }

        void AddPersons(int count, PersonCollection people)
        {
            for (var i = 0; i < count; i++)
                people.AddPerson(
                    "pesho" + i + "@gmail" + i % 100 + ".com",
                    "Pesho" + i % 100,
                    i % 100,
                    "Sofia" + i % 100);
        }
    }

    [Test]
    [Timeout(300)]
    public void TestPerformance_FindPersonsByEmailDomain()
    {
        // Arrange
        var persons = new PersonCollection();
        AddPersons(5000, persons);

        // Act
        for (var i = 0; i < 10000; i++)
        {
            var existingPersons =
                persons.FindPersons("gmail1.com").ToList();
            Assert.That(existingPersons.Count, Is.EqualTo(50));
            var notExistingPersons =
                persons.FindPersons("non-existing email").ToList();
            Assert.That(notExistingPersons.Count, Is.EqualTo(0));
        }

        void AddPersons(int count, PersonCollection people)
        {
            for (var i = 0; i < count; i++)
                people.AddPerson(
                    "pesho" + i + "@gmail" + i % 100 + ".com",
                    "Pesho" + i % 100,
                    i % 100,
                    "Sofia" + i % 100);
        }
    }

    [Test]
    [Timeout(300)]
    public void TestPerformance_FindPersonsByNameAndTown()
    {
        // Arrange
        var persons = new PersonCollection();
        AddPersons(5000, persons);

        // Act
        for (var i = 0; i < 10000; i++)
        {
            var existingPersons =
                persons.FindPersons("Pesho1", "Sofia1").ToList();
            Assert.That(existingPersons.Count, Is.EqualTo(50));
            var notExistingPersons =
                persons.FindPersons("Pesho1", "Sofia5").ToList();
            Assert.That(notExistingPersons.Count, Is.EqualTo(0));
        }

        void AddPersons(int count, PersonCollection people)
        {
            for (var i = 0; i < count; i++)
                people.AddPerson(
                    "pesho" + i + "@gmail" + i % 100 + ".com",
                    "Pesho" + i % 100,
                    i % 100,
                    "Sofia" + i % 100);
        }
    }

    [Test]
    [Timeout(300)]
    public void TestPerformance_FindPersonsByAgeRange()
    {
        // Arrange
        var persons = new PersonCollection();
        AddPersons(5000, persons);

        // Act
        for (var i = 0; i < 2000; i++)
        {
            var existingPersons =
                persons.FindPersons(20, 21).ToList();
            Assert.That(existingPersons.Count, Is.EqualTo(100));
            var notExistingPersons =
                persons.FindPersons(500, 600).ToList();
            Assert.That(notExistingPersons.Count, Is.EqualTo(0));
        }

        void AddPersons(int count, PersonCollection people)
        {
            for (var i = 0; i < count; i++)
                people.AddPerson(
                    "pesho" + i + "@gmail" + i % 100 + ".com",
                    "Pesho" + i % 100,
                    i % 100,
                    "Sofia" + i % 100);
        }
    }

    [Test]
    [Timeout(300)]
    public void TestPerformance_FindPersonsByTownAndAgeRange()
    {
        // Arrange
        var persons = new PersonCollection();
        AddPersons(5000, persons);

        // Act
        for (var i = 0; i < 5000; i++)
        {
            var existingPersons =
                persons.FindPersons(18, 22, "Sofia20").ToList();
            Assert.That(existingPersons.Count, Is.EqualTo(50));
            var notExistingTownPersons =
                persons.FindPersons(20, 30, "Missing town").ToList();
            Assert.That(notExistingTownPersons.Count, Is.EqualTo(0));
            var notExistingAgePersons =
                persons.FindPersons(200, 300, "Sofia1").ToList();
            Assert.That(notExistingTownPersons.Count, Is.EqualTo(0));
        }

        void AddPersons(int count, PersonCollection people)
        {
            for (var i = 0; i < count; i++)
                people.AddPerson(
                    "pesho" + i + "@gmail" + i % 100 + ".com",
                    "Pesho" + i % 100,
                    i % 100,
                    "Sofia" + i % 100);
        }
    }
}