namespace RZ
{

#if UNITY_EDITOR

    using UnityEditor;
    using UnityEngine;

    public partial class RZEditorGUI
    {

        public partial class Options : RZOptions
        {
            public Color indicatorLabelColor_HARD = new Color(0.8f, 0.1f, 0.6f, 1);
            public Color indicatorLabelColor_BAD = new Color(0.8f, 0.2f, 0.2f, 1);
            public Color indicatorLabelColor_GOOD = new Color(0.1f, 0.6f, 0.1f, 1);
            public Color indicatorLabelColor_BUGS = new Color(0.7f, 0.5f, 0.1f, 1);
            public Color indicatorLabelColor_CHEAT = new Color(0.7f, 0.5f, 0.1f, 1);
            public Color indicatorLabelColor_DANGER = new Color(0.8f, 0.2f, 0.2f, 1);

            public float indicatorLabelOffAlpha = 0.3f;
            public float indicatorLabelOnAlpha = 1f;
            public GUIStyle indicatorLabelStyle = null;
        }


        /// <summary>
        /// Contains standarts IndicatorLabels.
        /// </summary>
        public static partial class IndicatorLabels
        {

            /// <summary>Draw on/off (luminous) label 'HARD'.</summary>
            /// <param name="isOn">TRUE: light, else look like off.</param>
            public static void HARD(bool isOn)
            { IndicatorLabel("HARD", options.indicatorLabelColor_HARD, isOn); }


            /// <summary>Draw on/off (luminous) label 'BAD'.</summary>
            /// <param name="isOn">TRUE: light, else look like off.</param>
            public static void BAD(bool isOn)
            { IndicatorLabel("BAD", options.indicatorLabelColor_BAD, isOn); }


            /// <summary>Draw on/off (luminous) label 'GOOD'.</summary>
            /// <param name="isOn">TRUE: light, else look like off.</param>
            public static void GOOD(bool isOn)
            { IndicatorLabel("GOOD", options.indicatorLabelColor_GOOD, isOn); }


            /// <summary>Draw on/off (luminous) label 'BUGS'.</summary>
            /// <param name="isOn">TRUE: light, else look like off.</param>
            public static void BUGS(bool isOn)
            { IndicatorLabel("BUGS", options.indicatorLabelColor_BUGS, isOn); }


            /// <summary>Draw on/off (luminous) label 'CHEAT'.</summary>
            /// <param name="isOn">TRUE: light, else look like off.</param>
            public static void CHEAT(bool isOn)
            { IndicatorLabel("CHEAT", options.indicatorLabelColor_CHEAT, isOn); }


            /// <summary>Draw on/off (luminous) label 'DANGER'.</summary>
            /// <param name="isOn">TRUE: light, else look like off.</param>
            public static void DANGER(bool isOn)
            { IndicatorLabel("DANGER", options.indicatorLabelColor_CHEAT, isOn); }

        }


        /// <summary>Draw on/off (luminous) label look like indicator.</summary>
        /// <param name="text">Label text.</param>
        /// <param name="color">Label text color (ignore alpha).</param>
        /// <param name="isOn">TRUE: light, else look like off.</param>
        /// <param name="expandWidth">ExpandWidth of label.</param>
        public static void IndicatorLabel(string text, Color color, bool isOn,
            bool expandWidth = false)
        {
            if (options.indicatorLabelStyle == null)
            { options.indicatorLabelStyle = new GUIStyle(EditorStyles.boldLabel); }

            color.a = isOn ?
                options.indicatorLabelOnAlpha : options.indicatorLabelOffAlpha;

            options.indicatorLabelStyle.normal.textColor = color;
            options.indicatorLabelStyle.hover.textColor = color;

            GUILayout.Label(text, options.indicatorLabelStyle,
                GUILayout.Height(18), GUILayout.ExpandWidth(expandWidth));
        }

    }

#endif

}