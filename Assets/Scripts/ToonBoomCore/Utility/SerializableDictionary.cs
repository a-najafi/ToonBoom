
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField] private List<TKey> keys = new List<TKey>();
    [SerializeField] private List<TValue> values = new List<TValue>();

    private Dictionary<TKey, TValue> dictionary;

    // Convert lists into a Dictionary when needed
    public Dictionary<TKey, TValue> ToDictionary()
    {
        if (dictionary == null)
        {
            dictionary = new Dictionary<TKey, TValue>();
            for (int i = 0; i < keys.Count; i++)
            {
                dictionary[keys[i]] = values[i];
            }
        }
        return dictionary;
    }

    // Allow modifications
    public void Add(TKey key, TValue value)
    {
        if (!keys.Contains(key))
        {
            keys.Add(key);
            values.Add(value);
        }
    }

    public void Remove(TKey key)
    {
        int index = keys.IndexOf(key);
        if (index >= 0)
        {
            keys.RemoveAt(index);
            values.RemoveAt(index);
        }
    }
}
