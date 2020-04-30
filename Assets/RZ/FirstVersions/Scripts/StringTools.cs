using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace RZ
{


    // #if UNITY_EDITOR
    //    ////////// EDITOR WINDOW: //////////
    //  using System;
    //     using UnityEditor;
    //     [Serializable]
    //     public class StringUtilitiesWindow : EditorWindow
    //     {
    //         public const string windowName = "String Tools";
    //         public const string fullName = "RZ/" + windowName;
    //         public static Vector2 minimalSize = new Vector2(400, 200);
    //         public static string sourceText = "";
    //         public static string resultText = "";

    //         [MenuItem(fullName)]
    //         static void Initialize()
    //         {
    //             var me = EditorWindow.GetWindow(typeof(StringUtilitiesWindow), false, windowName);
    //             me.minSize = minimalSize;
    //             me.Show();
    //         }

    //         protected void OnEnable()
    //         {
    //             // Load preferenses:
    //             var data = EditorPrefs.GetString(fullName, JsonUtility.ToJson(this, false));
    //             JsonUtility.FromJsonOverwrite(data, this);
    //         }

    //         protected void OnDisable()
    //         {
    //             // Save preferenses:
    //             var data = JsonUtility.ToJson(this, false);
    //             EditorPrefs.SetString(fullName, data);
    //         }

    //         Vector2 scrollOriginal;
    //         Vector2 scrollResult;
    //         string _sourceText;
    //         public void OnGUI()
    //         {
    //             // EditorGUI.BeginChangeCheck();

    //             GUILayout.Label("Source:", EditorStyles.boldLabel);

    //             EditorGUILayout.BeginHorizontal();

    //             EditorGUILayout.BeginVertical();
    //             scrollOriginal = EditorGUILayout.BeginScrollView(scrollOriginal);

    //             sourceText = EditorGUILayout.TextArea(sourceText, GUILayout.ExpandHeight(true));
    //             // sourceText = EditorGUILayout.TextField(sourceText, GUILayout.ExpandHeight(true));
    //             EditorGUILayout.EndScrollView();

    //             GUILayout.Label("Result:", EditorStyles.boldLabel);

    //             scrollResult = EditorGUILayout.BeginScrollView(scrollResult);
    // resultText = 
    // EditorGUILayout.TextArea(resultText, GUILayout.ExpandHeight(true));
    // // GUILayout.Label(resultText, GUILayout.ExpandHeight(true));
    // // resultText = 
    // // EditorGUILayout.TextField(resultText, GUILayout.ExpandHeight(true));
    // EditorGUILayout.EndScrollView();
    // EditorGUILayout.EndVertical();


    //             if (_sourceText != sourceText)resultText = sourceText;
    //             _sourceText = sourceText;


    //             EditorGUILayout.BeginVertical(GUILayout.Width(150));
    //             if (GUILayout.Button("Clear"))
    //             {
    //                 sourceText = "";
    //                 resultText = "";
    //             }


    //             if (GUILayout.Button("Original"))
    //             {
    //                 resultText = sourceText;
    //             }

    //             if (GUILayout.Button("To lower"))
    //             {
    //                 resultText = resultText.ToLower();
    //             }

    //             if (GUILayout.Button("To upper"))
    //             {
    //                 resultText = resultText.ToUpper();
    //             }

    //             if (GUILayout.Button("First to upper"))
    //             {
    //                 resultText = StringUtilities.FirstCharToUpper(resultText);
    //             }

    //             if (GUILayout.Button("Every first to upper"))
    //             {
    //                 resultText = StringUtilities.EveryFirstCharToUpper(resultText);
    //             }


    //             if (GUILayout.Button("Remove doublespaces"))
    //             {
    //                 resultText = resultText.Replace("  ", " ");
    //             }


    //             if (GUILayout.Button("Copy"))
    //             {
    //                 GUIUtility.systemCopyBuffer = resultText;
    //             }

    //             EditorGUILayout.EndVertical();

    //             EditorGUILayout.EndHorizontal();

    //             // EditorGUI.EndChangeCheck();
    //         }
    //     }
    // #endif







    [ExecuteInEditMode]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Text))]
    public class StringTools : MonoBehaviour
    {
        ////////// FOR EDITOR: //////////
        public Text text;
        string oldText;

        public enum CaseMode
        {
            NoChange,
            ToLower,
            ToUpper,
            FirstUpper,
            EveryFirstUpper
        };

        public CaseMode _caseMode = CaseMode.NoChange;

        public CaseMode caseMode
        {
            get { return _caseMode; }
            set
            {
                _caseMode = value;
                text.text = ChangeCase(text.text, value);
            }
        }

        void Reset()
        {
            text = gameObject.GetComponent<Text>();
        }

        void Update()
        {
            if (caseMode != CaseMode.NoChange && oldText != text.text)
            {
                text.text = ChangeCase(text.text, caseMode);
                oldText = text.text;
            }
        }


        ////////// FOR CODE: //////////
        public const string NewLine = "\n";

        /// <summary>
        /// Изменить регистр в строке. 
        /// </summary>
        public static string ChangeCase(string s, CaseMode mode)
        {
            switch (mode)
            {
                default:
                case CaseMode.NoChange:
                    return s;

                case CaseMode.ToLower:
                    return s.ToLower();

                case CaseMode.ToUpper:
                    return s.ToUpper();

                case CaseMode.FirstUpper:
                    return FirstCharToUpper(s);

                case CaseMode.EveryFirstUpper:
                    return EveryFirstCharToUpper(s);
            }
        }

        /// <summary>
        /// Количество вхождений подстроки в строку. 
        /// </summary>
        public static int CountOfSubstring(string input, string substring)
        {
            return input.Split(substring.ToCharArray()).Length - 1;
        }

        /// <summary>
        /// Сделать первый символ заглавным. 
        /// </summary>
        public static string FirstCharToUpper(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                int j = 0;
                while (j < input.Length && input[j] == ' ')
                {
                    j++;
                }
                input = char.ToUpper(input[j]) + input.Remove(j, 1).ToLower();
            }
            return input;
        }

        /// <summary>
        /// Сделать первый символ каждого слова заглавным. 
        /// </summary>
        // public string EveryFirstCharToUpper(string input)
        // {
        //     return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower());
        // }
        public static string EveryFirstCharToUpper(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                //     string[] words;

                //     // BY LINES:
                //     words = input.Split('\n');
                //     input = "";
                //     for (int i = 0; i < words.Length; i++)
                //     {
                //         input += FirstCharToUpper(words[i]) + "\n";
                //     }
                //     input = input.Substring(0, input.Length - 1);

                //     // BY SPACE:
                //     words = input.Split(' ');
                //     input = "";
                //     for (int i = 0; i < words.Length; i++)
                //     {
                //         input += FirstCharToUpper(words[i]) + " ";
                //     }
                //     input = input.Substring(0, input.Length - 1);
                // }
                // return input;





                char[] array = input.ToCharArray();
                // Handle the first letter in the string.  
                if (array.Length >= 1)
                {
                    // if (char.IsLower(array[0]))
                    // {
                    array[0] = char.ToUpper(array[0]);
                    // }
                }
                // Scan through the letters, checking for spaces.  
                // ... Uppercase the lowercase letters following spaces.  
                for (int i = 1; i < array.Length; i++)
                {
                    char c = array[i - 1];
                    if (c == ' ' || c == '\n')
                    {
                        // if (char.IsLower(array[i]))
                        // {
                        array[i] = char.ToUpper(array[i]);
                        // }
                    }
                }
                input = new string(array);
            }
            return input;


        }

        /// <summary>
        /// Разбить строку по строке. 
        /// </summary>
        public static string[] SplitStringByString(string input, string substring)
        {
            return input.Split(new string[] { substring }, System.StringSplitOptions.None);
        }

        /// <summary>
        /// Заключает строку в кавычки. 
        /// </summary>
        public static string InQuotes(string s)
        {
            return "\"" + s + "\"";
        }

        /// <summary>
        /// Получить имя метода. 
        /// </summary>
        public static string NameOf(System.Action method)
        {
            return method.Method.Name;
        }

        /// <summary>
        /// string[] to string like ["a","b","c","d","e"]: 
        /// </summary>
        public static string ArrayToString(string[] array, string separator = ",")
        // public static string ArrayToString<T>(T[] array)
        {
            return "[" + ArrayToStringNoBrackets(array, separator) + "]";
        }

        /// <summary>
        /// string[] to string like "a","b","c","d","e": 
        /// </summary>
        public static string ArrayToStringNoBrackets(string[] array, string separator = ",")
        {
            string s = "";
            for (int i = 0; i < array.Length; i++)
            {
                s += "\"" + array[i] + "\"" + separator;
            }
            if (s.Length > 2) s = s.Remove(s.Length - separator.Length, separator.Length); //Убрать последнюю запятую
            return s;
        }

        /// <summary>
        /// Change "\r\n" to "\n", "\\\"" to "\""
        /// </summary>
        public static string FixSlash(string toFix)
        {
            toFix = toFix.Replace("\\\"", "\"");
            toFix = toFix.Replace("\\r", "");
            toFix = toFix.Replace("\\n", "\n");
            return toFix;
        }

        /// <summary>
        /// Bold string for rich text
        /// </summary>
        public static string Bold(string s)
        {
            return "<b>" + s + "</b>";
        }

        /// <summary>
        /// Italic string for rich text
        /// </summary>
        public static string Italic(string s)
        {
            return "<i>" + s + "</i>";
        }

        /// <summary>
        /// Size of string in pixels for rich text
        /// </summary>
        public static string Size(string s, int size)
        {
            return "<size=" + size + ">" + s + "</size>";
        }

        /// <summary>
        /// Size of string in pixels for rich text
        /// </summary>
        public static string Size(string s, float sizeMultiplier, Text textComponent)
        {
            return Size(s, sizeMultiplier, textComponent.fontSize);
        }

        /// <summary>
        /// Size of string for rich text
        /// </summary>
        public static string Size(string s, float sizeMultiplier, int textComponentSize)
        {
            int size = Mathf.RoundToInt(textComponentSize * sizeMultiplier);
            return "<size=" + size + ">" + s + "</size>";
        }

        /// <summary>
        /// Color of string for rich text
        /// </summary>
        public static string Color(string s, Color color)
        {
            return Color(s, ColorUtility.ToHtmlStringRGBA(color));
        }

        /// <summary>
        /// Color of string for rich text
        /// </summary>
        public static string Color(string s, string hexColor)
        {
            if (!hexColor.StartsWith("#")) hexColor = "#" + hexColor;
            return "<color=" + hexColor + ">" + s + "</color>";
        }

        /// <summary>
        /// Return indexes of UpperCase chars
        /// </summary>
        public static int[] GetUpperCharsIndexes(string s)
        {
            char[] array = s.ToCharArray();
            List<int> indexes = new List<int>();
            for (int i = 0; i < array.Length; i++)
            {
                if (Char.IsUpper(array[i])) indexes.Add(i);
            }
            return indexes.ToArray();
        }
    }
}