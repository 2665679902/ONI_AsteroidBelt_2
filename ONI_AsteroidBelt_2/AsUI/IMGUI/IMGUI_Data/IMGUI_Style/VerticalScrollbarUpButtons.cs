using static ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.Utility;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Style
{
    internal class VerticalScrollbarUpButtons
    {
        /// <summary>
        /// Kelei风格的 VerticalScrollbarUpButton
        /// </summary>
        public static GUIStyle KL_VerticalScrollbarUpButton {
            get
            {
                return GetKL_VerticalScrollbarUpButton();
            }
        }

        private static GUIStyle GetKL_VerticalScrollbarUpButton()
        {
            var verticalScrollbarUpButtonStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(38, 43, 60),

                textColor = GetColor(147, 154, 180)
            };

            var verticalScrollbarUpButton = new GUIStyle()
            {
                normal = verticalScrollbarUpButtonStyleState,
                border = new RectOffset(15, 15, 15, 15),
                margin = new RectOffset(0, 0, 10, 10),
                padding = new RectOffset(0, 0, 3, 3),
                fixedHeight = 10,
                fixedWidth = 10
            };

            return verticalScrollbarUpButton;
        }
    }
}
