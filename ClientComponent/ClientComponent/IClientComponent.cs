namespace ClientComponent;

public interface IClientComponent<TKey, TValue>
{
    Task<KeyValuePair<TKey, TValue>?> Get(TKey key);
    Task<KeyValuePair<TKey, TValue>?> Set(TKey key, TValue value);
}