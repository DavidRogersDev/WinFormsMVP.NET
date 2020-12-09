using System;
using System.Collections.Concurrent;

namespace WinFormsMVP.NET
{
    public class KeyValueState : IKeyValueState
    {
        readonly ConcurrentDictionary<string, dynamic> _items;

        public KeyValueState()
        {
            _items = new ConcurrentDictionary<string, dynamic>();
        }

        public bool AddItem<T>(string key, T item)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), @"The key cannot be either null, or an empty string.");

            return _items.TryAdd(key, item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public T GetItem<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), @"The key cannot be either null, or an empty string.");

            if (_items.TryGetValue(key, out dynamic value))
                return (T)value;

            return default(T);
        }

        public bool UpdateItem<T>(string key, T value)
        {
            var currentVal = GetItem<T>(key);

            return _items.TryUpdate(key, value, currentVal);
        }

        public bool UpdateItem<T>(string key, T value, T currentValue)
        {
            return _items.TryUpdate(key, value, currentValue);
        }

        public bool HasItem(string key)
        {
            return _items.ContainsKey(key);
        }

        public bool RemoveItem<T>(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key), @"The key cannot be either null, or an empty string.");

            return _items.TryRemove(key, out dynamic value);
        }
    }
}
