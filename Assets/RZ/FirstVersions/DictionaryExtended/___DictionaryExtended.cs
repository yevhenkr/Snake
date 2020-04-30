// RZ.Dictionary

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RZ
{
    [System.Serializable]
    public class ___DictionaryExtended<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
    {
        /// <summary>
        /// Импортировать из все пары ключ-значение из обычного Dictionary
        /// </summary>
        public static ___DictionaryExtended<TKey, TValue> ImportDictionary(Dictionary<TKey, TValue> dictionary)
        {
            ___DictionaryExtended<TKey, TValue> de = new ___DictionaryExtended<TKey, TValue>();
            foreach (KeyValuePair<TKey, TValue> kvp in dictionary)
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


        ////////// FOR SERIALIZE: //////////
        [SerializeField]
        private List<TKey> keys = new List<TKey>();

        [SerializeField]
        private List<TValue> values = new List<TValue>();

        /// <summary>
        /// Перед сериализацией - сохранить словарь в списки
        /// </summary>
        public void OnBeforeSerialize()
        {
            keys.Clear();
            values.Clear();
            foreach (KeyValuePair<TKey, TValue> pair in this)
            {
                keys.Add(pair.Key);
                values.Add(pair.Value);
            }
        }

        /// <summary>
        /// после сериализации - загрузить словарь из списков
        /// </summary>
        public void OnAfterDeserialize()
        {
            this.Clear();

            if (keys.Count != values.Count)
                throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

            for (int i = 0; i < keys.Count; i++)
                this.Add(keys[i], values[i]);

            // Попытка освободить память:
            keys.Clear();
            values.Clear();
        }
    }
}