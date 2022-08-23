using static ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.Utility;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Style
{
    internal class VerticalScrollbars
    {
        /// <summary>
        /// Kelei风格的 VerticalScrollbar
        /// </summary>
        public static GUIStyle KL_VerticalScrollbar {
            get
            {
                return GetKL_VerticalScrollbar();
            }
        }

        private static GUIStyle GetKL_VerticalScrollbar()
        {
            var verticalScrollbarStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(38,43,60),
                
                textColor = GetColor(147, 154, 180)
            };

            var verticalScrollbar = new GUIStyle()
            {
                normal = verticalScrollbarStyleState,
                border = new RectOffset(0, 0, 19, 19),
                margin = new RectOffset(1, 4, 4, 4),
                padding = new RectOffset(0, 0, 1, 1),
                fixedWidth = 15,
            };

            return verticalScrollbar;
        }
    }
}
