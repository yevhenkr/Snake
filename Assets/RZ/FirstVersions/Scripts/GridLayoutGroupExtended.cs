using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

namespace RZ
{

#if UNITY_EDITOR
    using UnityEditor;
    [CustomEditor(typeof(GridLayoutGroupExtended))]
    public class GridLayoutGroupExtendedEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // GridLayoutGroupExtended me = (GridLayoutGroupExtended)target;
            DrawDefaultInspector();
            // EditorGUILayout.BeginHorizontal();
            // me.fitHorizontal = EditorGUILayout.Toggle("Fit Horizontal", me.fitHorizontal);
            // me.fitVertical = EditorGUILayout.Toggle("Fit Vertical", me.fitVertical);
            // EditorGUILayout.EndHorizontal();
        }
    }
#endif

    public class GridLayoutGroupExtended : GridLayoutGroup
    {
        [SerializeField] public bool fitHorizontal = false;
        [SerializeField] public bool fitVertical = false;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            if (fitHorizontal)
            {
                float m;
                switch (constraint)
                {
                    case (Constraint.Flexible):
                        m = (float)Math.Ceiling(Math.Sqrt(transform.childCount));
                        break;

                    default:
                    case (Constraint.FixedColumnCount):
                        m = constraintCount;
                        break;

                    case (Constraint.FixedRowCount):
                        m = (float)Math.Ceiling((double)transform.childCount / constraintCount);
                        break;
                }

                float width = rectTransform.rect.width - padding.horizontal - (spacing.x * (m - 1));
                m_CellSize.x = width / m;
            }
        }

        public override void CalculateLayoutInputVertical()
        {
            base.CalculateLayoutInputVertical();

            if (fitVertical)
            {
                float m;
                switch (constraint)
                {
                    case (Constraint.Flexible):
                        m = (float)Math.Ceiling(Math.Sqrt(transform.childCount));
                        break;

                    case (Constraint.FixedColumnCount):
                        m = (float)Math.Ceiling((double)transform.childCount / constraintCount);
                        break;

                    default:
                    case (Constraint.FixedRowCount):
                        m = constraintCount;
                        break;
                }

                float height = rectTransform.rect.height - padding.vertical - (spacing.y * (m - 1));
                m_CellSize.y = height / m;
            }
        }

        public override float preferredHeight
        {
            get { return fitVertical ? rectTransform.rect.height : base.preferredHeight; }
        }

        public override float minHeight
        {
            get { return fitVertical ? rectTransform.rect.height : base.minHeight; }
        }

        public override float preferredWidth
        {
            get { return fitHorizontal ? rectTransform.rect.width : base.preferredWidth; }
        }

        public override float minWidth
        {
            get { return fitHorizontal ? rectTransform.rect.width : base.minWidth; }
        }

        //        protected override void OnValidate() { base.OnValidate(); }
    }
}