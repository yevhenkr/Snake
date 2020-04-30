using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    public class HashMap<TKey, TValue> : Hashtable
    {
        /// <summary>
        /// Импортировать из все пары ключ-значение из обычного Dictionary
        /// </summary>
        public static Dictionary<TKey, TValue> ImportDictionary(Dictionary<TKey, TValue> dictionary)
        {
            Dictionary<TKey, TValue> de = new Dictionary<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
            {
                de.Add((TKey)kvp.Key, (TValue)kvp.Value);
            }
            return de;
        }

        // /// <summary>
        // /// Добавить пару ключ-значение или обновить значение
        // /// </summary>
        // public void AddOrUpdate(TKey key, TValue value)
        // {
        //     if (this.ContainsKey(key)) this.Remove(key);
        //     this.Add(key, value);
        // }

        // /// <summary>
        // /// Добавить пару ключ-значение, если ключ отсутствует, иначе игнорировать
        // /// </summary>
        // public void AddOrIgnore(TKey key, TValue value)
        // {
        //     if (!this.ContainsKey(key)) this.Add(key, value);
        // }

        // /// <summary>
        // /// Обновить значение, если ключ существует, иначе игнорировать
        // /// </summary>
        // public void UpdateOrIgnore(TKey key, TValue value)
        // {
        //     if (!this.ContainsKey(key)) this.Add(key, value);
        // }

        // /// <summary>
        // /// Получить значание, или добавить и вернуть значение по умолчанию
        // /// </summary>
        // public TValue GetOrAddDefault(TKey key, TValue defaultValue)
        // {
        //     if (this.ContainsKey(key))
        //     {
        //         return (TValue)this[key];
        //     }
        //     else
        //     {
        //         this.Add(key, defaultValue);
        //         return defaultValue;
        //     }
        // }

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
            return (TValue)(this.ContainsKey(key) ? this[key] : defaultValue);
        }
    }
}