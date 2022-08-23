using static ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.Utility;
using UnityEngine;
using System;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Style
{
    internal class Buttons
    {
        /// <summary>
        /// Kelei风格的 Button
        /// </summary>
        public static GUIStyle KL_Button { get { return GetKL_Button(); } }

        private static GUIStyle GetKL_Button()
        {
            var buttonStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(135, 69, 102),

                textColor = GetColor(255, 255, 255)
            };

            var buttonAcStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(202, 115, 159),

                textColor = GetColor(255, 255, 255)
            };

            var buttonHoStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(158, 85, 121),

                textColor = GetColor(255, 255, 255)
            };

            var button = new GUIStyle
            {
                normal = buttonStyleState,
                hover = buttonHoStyleState,
                active = buttonAcStyleState,
                fontSize = 30,
                border = new RectOffset(5, 5, 5, 5),
                margin = new RectOffset(0, 0, 10, 10),
                padding = new RectOffset(0, 0, 3, 3),
                alignment = TextAnchor.MiddleCenter,
            };

            return button;
        }

    }
}
