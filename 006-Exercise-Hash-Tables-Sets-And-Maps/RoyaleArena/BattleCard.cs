namespace RoyaleArena;

public class BattleCard(int id, CardType type, string name, double damage, double swag)
    : IComparable<BattleCard>
{
    public int Id { get; set; } = id;

    public CardType Type { get; set; } = type;

    public string Name { get; set; } = name;

    public double Damage { get; set; } = damage;

    public double Swag { get; set; } = swag;

    public int CompareTo(BattleCard other)
    {
        var compare = other.Damage.CompareTo(Damage);

        if (compare == 0) compare = Id.CompareTo(other.Id);

        return compare;
    }

    public override bool Equals(object obj)
    {
        if (this == obj) return true;

        if (obj == null || GetType().Name.CompareTo(obj.GetType().Name) != 0)
            return false;

        var bc = (BattleCard)obj;

        return Id == bc.Id &&
               bc.Damage.Equals(Damage) &&
               bc.Swag.Equals(Swag) &&
               Type.Equals(bc.Type) &&
               Name.Equals(bc.Name);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}