using static ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.Utility;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Style
{
    internal static class Windows
    {
        /// <summary>
        /// Kelei风格的 Window
        /// </summary>
        public static GUIStyle KL_Window { get { return GetKL_Window(); } } 

        private static GUIStyle GetKL_Window()
        {
            var windowStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(48, 52, 67),

                textColor = GetColor(255, 255, 255)
            };

            var window = new GUIStyle
            {
                normal = windowStyleState,
                border = new RectOffset(0, 0, 0, 0),
                margin = new RectOffset(20, 20, 20, 20),
                padding = new RectOffset(20, 20, 20, 20),
                fontSize = 25,
                fontStyle = FontStyle.Bold,
                alignment = TextAnchor.UpperCenter,
            };

            return window;
        }












    }
}
