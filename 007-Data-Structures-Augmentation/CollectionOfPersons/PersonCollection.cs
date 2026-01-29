using Wintellect.PowerCollections;

namespace Collection_of_Persons;

public class PersonCollection : IPersonCollection
{
    private Dictionary<string, Person> peopleByEmail = new();
    private Dictionary<string, SortedSet<Person>> peopleByEmailDomain = new();
    private Dictionary<string, SortedSet<Person>> peopleByNameAndTown = new();
    private OrderedDictionary<int, SortedSet<Person>> peopleByAge = new();

    private Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> peopleByTownAndAge = new();

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (FindPerson(email) != null)
            // Person already exists
            return false;

        var person = new Person()
        {
            Email = email,
            Name = name,
            Age = age,
            Town = town
        };

        // Add by email
        peopleByEmail.Add(email, person);

        // Add by email domain
        peopleByEmailDomain.AppendValueToKey(email.Split('@')[1], person);

        // Add by {name + town}
        peopleByNameAndTown.AppendValueToKey(name + "|!|" + town, person);

        // Add by age
        peopleByAge.AppendValueToKey(age, person);

        // Add by {town + age}
        peopleByTownAndAge.EnsureKeyExists(town);
        peopleByTownAndAge[town].AppendValueToKey(age, person);

        return true;
    }

    public int Count => peopleByEmail.Count;

    public Person FindPerson(string email)
    {
        Person person = null;
        peopleByEmail.TryGetValue(email, out person);

        return person;
    }

    public bool DeletePerson(string email)
    {
        var person = FindPerson(email);

        if (person == null)
            // Person does not exist
            return false;

        // Delete person from peopleByEmail
        peopleByEmail.Remove(email);

        // Delete person from peopleByEmailDomain
        peopleByEmailDomain[email.Split('@')[1]].Remove(person);

        // Delete person from peopleByNameAndTown
        peopleByNameAndTown[person.Name + "|!|" + person.Town].Remove(person);

        // Delete person from peopleByAge
        peopleByAge[person.Age].Remove(person);

        //Delete person from peopleByTownAndAge
        peopleByTownAndAge[person.Town][person.Age].Remove(person);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return peopleByEmailDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        return peopleByNameAndTown.GetValuesForKey(name + "|!|" + town);
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var peopleInRange = this.peopleByAge.Range(startAge, true, endAge, true);

        foreach (var peopleByAge in peopleInRange)
        foreach (var person in peopleByAge.Value)
            yield return person;
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge, string town)
    {
        if (!peopleByTownAndAge.ContainsKey(town))
            // Return an empty sequence of people
            yield break;

        var peopleInRange = peopleByTownAndAge[town].Range(startAge, true, endAge, true);

        foreach (var peopleByAge in peopleInRange)
        foreach (var person in peopleByAge.Value)
            yield return person;
    }
}