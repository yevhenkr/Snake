namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;

    public partial class RZEditorGUI
    {

        public partial class Options : RZOptions
        {
            public sbyte indicatorOfRedrawState = 0;
            public sbyte indicatorOfRedrawStep = 6;
            public GUIStyle indicatorOfRedrawStyle = null;
            public string[] indicatorOfRedrawGraphics =
                    { "■□□□□□", "□■□□□□", "□□■□□□", "□□□■□□", "□□□□■□", "□□□□□■" };
            // { "■□□□□□", "□■□□□□", "□□■□□□", "□□□■□□", "□□□□■□", "□□□□□■" };
            // { "[┌]", "[┐]", "[┘]", "[└]" };
        }

        /// <summary>
        /// Draw indicator of Inspector window redraw.
        /// </summary>
        public static void IndicatorOfRedreaw()
        {
            options.indicatorOfRedrawState =
                IndicatorOfRedreaw(options.indicatorOfRedrawState);
        }

        /// <summary>
        /// Draw indicator of Inspector window redraw.
        /// </summary>
        public static sbyte IndicatorOfRedreaw(sbyte state)
        {
            if (options.indicatorOfRedrawStyle == null)
            {
                options.indicatorOfRedrawStyle = new GUIStyle(EditorStyles.boldLabel);
                options.indicatorOfRedrawStyle.normal.textColor = Colors.indicators;
                options.indicatorOfRedrawStyle.hover.textColor = Colors.indicators;
            }

            sbyte i = (sbyte)
                (state / options.indicatorOfRedrawStep);

            if (i < options.indicatorOfRedrawGraphics.Length - 1)
                state++;
            else
                state = 0;

            EditorGUILayout.LabelField(
                options.indicatorOfRedrawGraphics[i], options.indicatorOfRedrawStyle,
                    GUILayout.MaxWidth(50), GUILayout.Height(18));

            return state;
        }

    }

#endif

}