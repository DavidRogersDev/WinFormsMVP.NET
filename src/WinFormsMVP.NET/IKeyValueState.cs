
namespace WinFormsMVP.NET
{
    public interface IKeyValueState
    {
        bool AddItem<T>(string key, T item);
        void Clear();
        T GetItem<T>(string key);
        bool HasItem(string key);
        bool RemoveItem<T>(string key);
        bool UpdateItem<T>(string key, T value);
        bool UpdateItem<T>(string key, T value, T currentValue);
    }
}
