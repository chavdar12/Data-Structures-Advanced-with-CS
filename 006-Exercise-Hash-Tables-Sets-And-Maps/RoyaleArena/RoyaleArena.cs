using System.Collections;

namespace RoyaleArena;

public class RoyaleArena : IArena
{
    private Dictionary<int, BattleCard> cardsById = new();

    public void Add(BattleCard card)
    {
        if (Contains(card)) return;

        cardsById.Add(card.Id, card);
    }

    public bool Contains(BattleCard card)
    {
        return cardsById.ContainsKey(card.Id);
    }

    public int Count => cardsById.Count;

    public void ChangeCardType(int id, CardType type)
    {
        if (!cardsById.ContainsKey(id)) throw new InvalidOperationException();

        cardsById[id].Type = type;
    }

    public BattleCard GetById(int id)
    {
        if (!cardsById.ContainsKey(id)) throw new InvalidOperationException();

        return cardsById[id];
    }

    public void RemoveById(int id)
    {
        if (!cardsById.ContainsKey(id)) throw new InvalidOperationException();

        cardsById.Remove(id);
    }

    public IEnumerable<BattleCard> GetByCardType(CardType type)
    {
        var found = cardsById.Values
            .Where(x => x.Type == type)
            .OrderByDescending(x => x.Damage)
            .ThenBy(x => x.Id)
            .ToList();

        if (found.Count == 0) throw new InvalidOperationException();

        return found;
    }

    public IEnumerable<BattleCard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
    {
        var found = cardsById.Values
            .Where(x => x.Type == type
                        && x.Damage > lo
                        && x.Damage < hi)
            .OrderByDescending(x => x.Damage)
            .ThenBy(x => x.Id)
            .ToList();

        if (found.Count == 0) throw new InvalidOperationException();

        return found;
    }

    public IEnumerable<BattleCard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
    {
        var found = cardsById.Values
            .Where(x => x.Type == type
                        && x.Damage <= damage)
            .OrderByDescending(x => x.Damage)
            .ThenBy(x => x.Id)
            .ToList();

        if (found.Count == 0) throw new InvalidOperationException();

        return found;
    }

    public IEnumerable<BattleCard> GetByNameOrderedBySwagDescending(string name)
    {
        var found = cardsById.Values
            .Where(x => x.Name == name)
            .OrderByDescending(x => x.Swag)
            .ThenBy(x => x.Id)
            .ToList();

        if (found.Count == 0) throw new InvalidOperationException();

        return found;
    }

    public IEnumerable<BattleCard> GetByNameAndSwagRange(string name, double lo, double hi)
    {
        var found = cardsById.Values
            .Where(x => x.Name == name
                        && x.Swag > lo
                        && x.Swag < hi)
            .OrderByDescending(x => x.Swag)
            .ThenBy(x => x.Id)
            .ToList();

        if (found.Count == 0) throw new InvalidOperationException();

        return found;
    }

    public IEnumerable<BattleCard> FindFirstLeastSwag(int n)
    {
        if (n > Count) throw new InvalidOperationException();

        return cardsById.Values
            .OrderBy(x => x.Swag)
            .Take(n)
            .OrderBy(x => x.Id)
            .ToList();
    }

    public IEnumerable<BattleCard> GetAllInSwagRange(double lo, double hi)
    {
        var found = cardsById.Values
            .Where(x => x.Swag >= lo
                        && x.Swag <= hi)
            .OrderBy(x => x.Swag)
            .ToList();

        if (found.Count == 0) return Enumerable.Empty<BattleCard>();

        return found;
    }


    public IEnumerator<BattleCard> GetEnumerator()
    {
        return cardsById.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}