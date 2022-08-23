using static ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.Utility;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Style
{
    internal class VerticalScrollbarThumbs
    {
        /// <summary>
        /// Kelei风格的 VerticalScrollbarThumb
        /// </summary>
        public static GUIStyle KL_VerticalScrollbarThumb {
            get
            {
                return GetKL_verticalScrollbarThumb();
            }
        }

        private static GUIStyle GetKL_verticalScrollbarThumb()
        {
            var verticalScrollbarThumbStyleState = new GUIStyleState()
            {
                background = GetPicFromColor(204, 204, 204),

                textColor = GetColor(147, 154, 180)
            };
            var verticalScrollbarThumb = new GUIStyle
            {
                normal = verticalScrollbarThumbStyleState,
                border = new RectOffset(15, 15, 15, 15),
                margin = new RectOffset(0, 0, 10, 10),
                padding = new RectOffset(0, 0, 3, 3),
                fixedWidth = 15,
            };

            return verticalScrollbarThumb;
        }
    }
}
