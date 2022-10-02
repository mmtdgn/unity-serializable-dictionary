
using System.Collections.Generic;
using UnityEngine;

namespace MD.SerializableDictionary
{
    public class SDictionaryBase { }

    [System.Serializable]
    public class SDictionary<TKey, TValue> : SDictionaryBase
    {
        public SDictionary() { }
        public List<SDictionaryElement<TKey, TValue>> Dictionary = new List<SDictionaryElement<TKey, TValue>>();
        
        public SDictionary(Dictionary<TKey, TValue> dictionary)
        {
            foreach (var item in dictionary)
            {
                Dictionary.Add(new SDictionaryElement<TKey, TValue>(item.Key, item.Value));
            }
        }
        [SerializeField] private int ResizeCount = 0;

        /// <returns>Element count in dictionary</returns>
        public int Count
        {
            get
            {
                return Dictionary.Count;
            }
        }

        /// <summary>Get keys from dictionary</summary>
        /// <returns>Returns entire dictionary keys as a list</returns>
        public List<TKey> Keys
        {
            get
            {
                List<TKey> keys = new List<TKey>();
                foreach (var item in Dictionary)
                {
                    keys.Add(item.Key);
                }
                return keys;
            }
        }

        /// <summary>Get values from dictionary </summary>
        /// <returns>Returns entire dictionary values as a list</returns>
        public List<TValue> Values
        {
            get
            {
                List<TValue> values = new List<TValue>();
                foreach (var item in Dictionary)
                {
                    values.Add(item.Val);
                }
                return values;
            }
        }

        /// <summary>Get as System Dictionary</summary>
        public Dictionary<TKey, TValue> GetDictionaryClone
        {
            get
            {
                Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
                for (int i = 0; i < Dictionary.Count; i++)
                {
                    dictionary.Add(Dictionary[i].Key, Dictionary[i].Val);
                }
                return dictionary;
            }
        }

        /// <summary>Set own dictionary to SDictionary</summary>
        public Dictionary<TKey, TValue> SetDictionaryClone
        {
            set
            {
                Dictionary.Clear();
                foreach (var item in value)
                {
                    Dictionary.Add(new SDictionaryElement<TKey, TValue>(item.Key, item.Value));
                }
            }
        }

        /// <summary>Check the dictionary contains the key</summary>
        /// <param name="key">Key to check</param>
        /// <returns>True if contains</returns>
        public bool ContainsKey(TKey key)
        {
            return Dictionary.Find(x => x.Key.Equals(key) == true) != null;
        }

        /// <summary>Get dictionary value with key</summary>
        /// <param name="key">Dictionary key</param>
        /// <returns>Dictionary value</returns>
        public TValue GetValue(TKey key)
        {
            return Dictionary.Find(x => x.Key.Equals(key)).Val;
        }

        /// <summary>Get dictionary value with index</summary>
        /// <param name="key">Dictionary index</param>
        /// <returns>Dictionary value</returns>
        public TValue GetValue(int index)
        {
            return Dictionary[index].Val;
        }

        /// <summary>Setter for dictionary values.<br></br><br></br>/// </summary>
        /// <param name="key">Key for which enum data will be set</param>
        /// <param name="val">New first value to write to dictionary</param>
        public void SetValue(TKey key, TValue val)
        {
            if (Dictionary.Find(x => x.Key.Equals(key)) != null)
            {
                Dictionary.Find(x => x.Key.Equals(key)).Val = val;
            }
            else
            {
                Dictionary.Add(new SDictionaryElement<TKey, TValue>(key, val));
                Debug.LogWarning("Key not found in dictionary, added new key!");
            }
        }

        /// <summary>Setter for dictionary values.<br></br><br></br></summary>
        /// <param name="key">Key for which enum data will be set</param>
        /// <param name="val">New first value to write to dictionary</param>
        public void SetValue(int index, TValue val)
        {
            if (index >= 0 && Dictionary.Count < index)
            {
                Dictionary[index].Val = val;
            }
            else
            {
                throw new System.Exception("Index out of range");
            }
        }

        /// <summary> Add new key value pair to dictionary. </summary>
        /// <param name="key">key to add to dictionary </param>
        /// <param name="val">value to add to dictionary </param>
        public void AddValue(TKey key, TValue val)
        {
            Dictionary.Add(new SDictionaryElement<TKey, TValue>(key, val));
        }

        /// <summary> Add new key value pairs to dictionary. </summary>
        /// <param name="key">keys to add to dictionary </param>
        /// <param name="val">values to add to dictionary </param>
        public void AddValue(TKey[] key, TValue[] val)
        {
            if (key.Length != val.Length)
            {
                throw new System.Exception("Key and value array length must be equal");
            }
            for (int i = 0; i < key.Length; i++)
            {
                Dictionary.Add(new SDictionaryElement<TKey, TValue>(key[i], val[i]));
            }
        }

        /// <summary> Add new element to dictionary. </summary>
        /// <param name="element">element to add to dictionary </param>
        public void AddValue(SDictionaryElement<TKey, TValue> element)
        {
            Dictionary.Add(element);
        }

        /// <summary> Remove element from dictionary. </summary>
        /// <param name="key">which key will be deleted </param>
        public void RemoveValue(TKey key)
        {
            Dictionary.Remove(Dictionary.Find(x => x.Key.Equals(key)));
        }

        /// <summary> Remove element from dictionary. </summary>
        /// <param name="index">index of will be deleted element</param>
        public void RemoveValue(int index)
        {
            Dictionary.RemoveAt(index);
        }

        /// <summary> Remove entire elements from dictionary. </summary>
        public void Clear()
        {
            Dictionary.Clear();
        }
    }
}
