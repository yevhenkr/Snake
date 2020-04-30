using UnityEngine;
using UnityEngine.Serialization;
using System;

namespace RZ
{
#if UNITY_EDITOR
    using UnityEditor;

    [CustomEditor(typeof(CanvasTools), true)]
    public class CanvasToolsEditor : Editor
    {
        CanvasTools me;
        Canvas canvas;

        bool _differentOrders;

        public override void OnInspectorGUI()
        {
            // DrawDefaultInspector();

            me = (CanvasTools)target;
            canvas = me.GetComponent<Canvas>();

            EditorGUILayout.Space();
            DifferentOrders();
            EditorGUILayout.Space();

        }

        void DifferentOrders()
        {
            me.differentSortOrders = EditorGUILayout.ToggleLeft("Different sort orders (play/edit)", me.differentSortOrders, EditorStyles.boldLabel);

            EditorGUI.indentLevel++;
            EditorGUI.BeginChangeCheck();
            EditorGUI.BeginDisabledGroup(!me.differentSortOrders);

            EditorGUILayout.LabelField("Active mode:", me.orderIsPlay ? "Play" : "Edit");

            EditorGUILayout.BeginHorizontal();
            me.playSortOrder = EditorGUILayout.IntField("Play sort order", me.playSortOrder);
            me.orderIsPlay = GUILayout.Toggle(me.orderIsPlay, UseText(me.orderIsPlay), "Button");
            EditorGUILayout.EndHorizontal();

            EditorGUILayout.BeginHorizontal();
            me.editSortOrder = EditorGUILayout.IntField("Edit sort order", me.editSortOrder);
            me.orderIsPlay = !GUILayout.Toggle(!me.orderIsPlay, UseText(!me.orderIsPlay), "Button");
            EditorGUILayout.EndHorizontal();

            EditorGUI.EndDisabledGroup();
            EditorGUI.indentLevel--;
            bool change = EditorGUI.EndChangeCheck();

            if (me.differentSortOrders)
            {
                if (change)
                {
                    if (me.orderIsPlay) canvas.sortingOrder = me.playSortOrder;
                    if (!me.orderIsPlay) canvas.sortingOrder = me.editSortOrder;
                    me.SetOrder(me.orderIsPlay);
                }
                else
                {
                    if (canvas.sortingOrder != me.playSortOrder && canvas.sortingOrder != me.editSortOrder)
                    {
                        me.playSortOrder = canvas.sortingOrder;

                        if (me.showWarning)
                        {
                            string warning = "Value in " + me.gameObject.name + " will be transferred\n"
                            + "FROM: " + "Canvas.sortingOrder TO: CanvasTools.playSortOrder. \n"
                            + "Because " + Framework.name + ".CanvasTools.differentSortOrders is enabled.\n";

                            Debug.LogWarning(warning);

                            me.SetOrder(true);

                            me.showWarning = false;
                        }
                    }
                }
            }
        }

        // ✔ ✓ ☐ ☑
        string UseText(bool active) { return active ? "☑ Use" : "☐ Use"; }
    }
#endif

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    [RequireComponent(typeof(Canvas))]
    public class CanvasTools : MonoBehaviour
    {
        [SerializeField]
        [FormerlySerializedAs("differentSortOrders")]
        bool _differentSortOrders = false;
        public bool differentSortOrders
        {
            get { return _differentSortOrders; }
            set
            {
                if (value != _differentSortOrders)
                {
                    if (value)
                    {
                        playSortOrder = GetComponent<Canvas>().sortingOrder;
                    }
                    // else
                    // {
                    //     GetComponent<Canvas>().sortingOrder = playSortOrder;
                    // }
                    SetOrder(true);
                    _differentSortOrders = value;
                }
            }
        }


        [HideInInspector] public int playSortOrder = 0;
        [HideInInspector] public int editSortOrder = 0;

        void Reset()
        {
            // var canvas = GetComponent<Canvas>();
            // playSortOrder = canvas.sortingOrder;
            // editSortOrder = canvas.sortingOrder;
        }

        void Awake()
        {
            if (differentSortOrders) SetOrder(true);
        }

#if UNITY_EDITOR
        void OnValidate()
        {
            // if (differentSortOrders && !Application.isPlaying && orderIsPlay) SetOrder(false);
            if (differentSortOrders && !Application.isPlaying) SetOrder(false);
        }
#endif

        [HideInInspector] [NonSerialized] public bool showWarning = true;
        [HideInInspector] [NonSerialized] public bool orderIsPlay = true;
        public void SetOrder(bool toPlay)
        {
            orderIsPlay = toPlay;
            showWarning = true;
            GetComponent<Canvas>().sortingOrder = toPlay ? playSortOrder : editSortOrder;
        }
    }
}