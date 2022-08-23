using static ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.Utility;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Style
{
    internal class VerticalScrollbarDownButtons
    {
        /// <summary>
        /// Kelei风格的 VerticalScrollbarDownButton
        /// </summary>
        public static GUIStyle KL_VerticalScrollbarDownButton {
            get
            {
                return GetKL_VerticalScrollbarDownButton();
            }
        }

        private static GUIStyle GetKL_VerticalScrollbarDownButton()
        {
            var verticalScrollbarDownButtonStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(38, 43, 60),

                textColor = GetColor(147, 154, 180)
            };

            var verticalScrollbarDownButton = new GUIStyle()
            {
                normal = verticalScrollbarDownButtonStyleState,
                border = new RectOffset(15, 15, 15, 15),
                margin = new RectOffset(0, 0, 10, 10),
                padding = new RectOffset(0, 0, 3, 3),
                fixedHeight = 10,
                fixedWidth = 10
            };

            return verticalScrollbarDownButton;
        }
    }
}
