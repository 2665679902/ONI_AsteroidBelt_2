using static ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.Utility;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Style
{
    internal class TextAreas
    {
        /// <summary>
        /// Kelei风格的 TextArea
        /// </summary>
        public static GUIStyle KL_TextArea { get {return GetKL_TextArea(); } }

        private static GUIStyle GetKL_TextArea()
        {
            var textAreaStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(62, 67, 87),

                textColor = GetColor(147, 154, 180)
            };

            var textArea = new GUIStyle
            {
                normal = textAreaStyleState,
                border = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(20, 20, 20, 20),
                padding = new RectOffset(20, 20, 20, 20),
                fontSize = 20,
                fontStyle = FontStyle.Normal,
                alignment = TextAnchor.UpperLeft,
                wordWrap = true,
                richText = true,
                stretchHeight = true,
                stretchWidth = true,
            };

            return textArea;
        }



    }
}
