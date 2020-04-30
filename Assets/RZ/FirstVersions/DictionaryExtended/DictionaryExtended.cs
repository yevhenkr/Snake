
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

// BASED ON "SerializableDictionary"
namespace RZ
{
    public abstract class DictionaryExtendedBase<TKey, TValue, TValueStorage> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        [SerializeField]
        TKey[] keys;
        [SerializeField]
        TValueStorage[] values;

        public DictionaryExtendedBase()
        {
        }

        public DictionaryExtendedBase(IDictionary<TKey, TValue> dict) : base(dict.Count)
        {
            foreach (var kvp in dict)
            {
                this[kvp.Key] = kvp.Value;
            }
        }

        protected DictionaryExtendedBase(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected abstract void SetValue(TValueStorage[] storage, int i, TValue value);
        protected abstract TValue GetValue(TValueStorage[] storage, int i);

        public void CopyFrom(IDictionary<TKey, TValue> dict)
        {
            this.Clear();
            foreach (var kvp in dict)
            {
                this[kvp.Key] = kvp.Value;
            }
        }

        public void OnAfterDeserialize()
        {
            if (keys != null && values != null && keys.Length == values.Length)
            {
                this.Clear();
                int n = keys.Length;
                for (int i = 0; i < n; ++i)
                {
                    this[keys[i]] = GetValue(values, i);
                }

                keys = null;
                values = null;
            }

        }

        public void OnBeforeSerialize()
        {
            int n = this.Count;
            keys = new TKey[n];
            values = new TValueStorage[n];

            int i = 0;
            foreach (var kvp in this)
            {
                keys[i] = kvp.Key;
                SetValue(values, i, kvp.Value);
                ++i;
            }
        }

        ////////// RZ: //////////
        /// <summary>
        /// Импортировать из все пары ключ-значение из обычного Dictionary
        /// </summary>
        public static DictionaryExtended<TKey, TValue> ImportDictionary(Dictionary<TKey, TValue> dict)
        {
            DictionaryExtended<TKey, TValue> de = new DictionaryExtended<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> kvp in dict)
            {
                de.Add((TKey)kvp.Key, (TValue)kvp.Value);
            }
            return de;
        }

        /// <summary>
        /// Добавить пару ключ-значение или обновить значение
        /// </summary>
        public void AddOrUpdate(TKey key, TValue value)
        {
            if (this.ContainsKey(key)) this.Remove(key);
            this.Add(key, value);
        }

        /// <summary>
        /// Добавить пару ключ-значение, если ключ отсутствует, иначе игнорировать
        /// </summary>
        public void AddOrIgnore(TKey key, TValue value)
        {
            if (!this.ContainsKey(key)) this.Add(key, value);
        }

        /// <summary>
        /// Обновить значение, если ключ существует, иначе игнорировать
        /// </summary>
        public void UpdateOrIgnore(TKey key, TValue value)
        {
            if (!this.ContainsKey(key)) this.Add(key, value);
        }

        /// <summary>
        /// Получить значание, или добавить и вернуть значение по умолчанию
        /// </summary>
        public TValue GetOrAddDefault(TKey key, TValue defaultValue)
        {
            if (this.ContainsKey(key))
            {
                return this[key];
            }
            else
            {
                this.Add(key, defaultValue);
                return defaultValue;
            }
        }

        /// <summary>
        /// Получить значание
        /// </summary>
        public TValue Get(TKey key)
        {
            return (TValue)this[key];
        }

        /// <summary>
        /// Получить значание, или вернуть значение по умолчанию
        /// </summary>
        public TValue GetOrDefault(TKey key, TValue defaultValue)
        {
            return this.ContainsKey(key) ? this[key] : defaultValue;
        }
    }

    public class DictionaryExtended<TKey, TValue> : DictionaryExtendedBase<TKey, TValue, TValue>
    {
        public DictionaryExtended()
        {
        }

        public DictionaryExtended(IDictionary<TKey, TValue> dict) : base(dict)
        {
        }

        protected DictionaryExtended(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override TValue GetValue(TValue[] storage, int i)
        {
            return storage[i];
        }

        protected override void SetValue(TValue[] storage, int i, TValue value)
        {
            storage[i] = value;
        }
    }

    public static class DictionaryExtended
    {
        public class Storage<T>
        {
            public T data;
        }
    }

    public class DictionaryExtended<TKey, TValue, TValueStorage> : DictionaryExtendedBase<TKey, TValue, TValueStorage> where TValueStorage : DictionaryExtended.Storage<TValue>, new()
    {
        public DictionaryExtended()
        {
        }

        public DictionaryExtended(IDictionary<TKey, TValue> dict) : base(dict)
        {
        }

        protected DictionaryExtended(SerializationInfo info, StreamingContext context) : base(info, context) { }

        protected override TValue GetValue(TValueStorage[] storage, int i)
        {
            return storage[i].data;
        }

        protected override void SetValue(TValueStorage[] storage, int i, TValue value)
        {
            storage[i] = new TValueStorage();
            storage[i].data = value;
        }
    }
}