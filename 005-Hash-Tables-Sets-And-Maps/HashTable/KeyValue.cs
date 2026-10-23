namespace HashTable;

public class KeyValue<TKey, TValue>
{
    public TKey Key { get; set; }
    public TValue Value { get; set; }

    public KeyValue(TKey key, TValue value)
    {
        Key = key;
        Value = value;
    }

    public override bool Equals(object other)
    {
        return other is KeyValue<TKey, TValue> element
               && Equals(Key, element.Key)
               && Equals(Value, element.Value);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Key, Value);
    }

    public override string ToString()
    {
        return $" [{Key} -> {Value}]";
    }
}