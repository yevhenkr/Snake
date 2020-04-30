namespace RZ
{
    using System.Collections.Generic;
    using UnityEngine;

    [System.Serializable]
    /// <summary>
    /// RZ save/load options and settings.
    /// </summary>
    public partial class RZOptions
    {
        private static HashSet<string> allOptionsKeys = new HashSet<string>();
        public const string PREFIX = Framework.name + ".";
        public bool isLoaded;

        public string keyName;
        public bool usePrefix;
        public bool usePlayerPrefs;
        public bool autoSaveOnExit;


        public string fullKeyName { get { return usePrefix ? PREFIX + keyName : keyName; } }

        /// <summary>
        /// Create new RZOptions.
        /// </summary>
        // public RZOptions() : this(null, true) { }


        /// <summary>
        /// Create new RZOptions.
        /// </summary>
        public RZOptions(string keyName, bool usePlayerPrefs)
            : this(keyName, usePlayerPrefs, true, false) { }


        /// <summary>
        /// Create new RZOptions.
        /// </summary>
        public RZOptions(string keyName, bool usePlayerPrefs = true,
             bool usePrefix = true, bool removePrevious = false, bool autoSaveOnExit = true)
        {
            if (string.IsNullOrEmpty(keyName))
            {
                RZDebug.LogErrorNoTrace("You did not provide a name. This will overwrite existing records of this type.");
                keyName = this.GetType().ToString();
            }


            this.keyName = keyName;
            this.usePrefix = usePrefix;
            this.usePlayerPrefs = usePlayerPrefs;






            if (removePrevious) Delete();
        }


        /// <summary>
        /// Load from File or PlayerPrefs.
        /// </summary>
        public void Load()
        {
            //            Debug.Log("LOAD: "+fullKeyName);
            if (usePlayerPrefs)
            {
                if (PlayerPrefs.HasKey(fullKeyName))
                {
                    JsonUtility.FromJsonOverwrite(PlayerPrefs.GetString(fullKeyName), this);
                    allOptionsKeys.Add(fullKeyName);
                    isLoaded = true;
                }
            }
            else
            {
                Debug.LogAssertion("In Development");
            }
        }


        /// <summary>
        /// Load to File or PlayerPrefs.
        /// </summary>
        public void Save()
        {
            Debug.Log("SAVE: " + fullKeyName);
            if (usePlayerPrefs)
            {
                PlayerPrefs.SetString(fullKeyName, JsonUtility.ToJson(this, false));
                PlayerPrefs.Save();
            }
            else
            {
                Debug.LogAssertion("In Development");
            }
            allOptionsKeys.Add(fullKeyName);
        }


        /// <summary>
        /// Remove from File or PlayerPrefs.
        /// </summary>
        public void Delete()
        {
            if (usePlayerPrefs)
            {
                PlayerPrefs.DeleteKey(fullKeyName);
            }
            else
            {
                Debug.LogAssertion("In Development");
            }
            allOptionsKeys.Remove(fullKeyName);
        }


        /// <summary>
        /// Remove from File or PlayerPrefs.
        /// </summary>
        public void DeleteAll()
        {
            var en = allOptionsKeys.GetEnumerator();
            if (usePlayerPrefs)
            {
                while (en.MoveNext())
                {
                    string currentKey = en.Current;
                    if (PlayerPrefs.HasKey(currentKey))
                    {
                        PlayerPrefs.DeleteKey(currentKey);
                        allOptionsKeys.Remove(fullKeyName);
                    }
                }
            }
            else
            {
                Debug.LogAssertion("In Development");
            }
        }

        public string ToString(bool prettyPrint)
        { return JsonUtility.ToJson(this, prettyPrint); }

        public override string ToString()
        { return base.ToString() + "|||" + JsonUtility.ToJson(this, false); }

    }

}