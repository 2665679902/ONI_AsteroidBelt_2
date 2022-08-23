using ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Skin;
using ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Style;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data
{
    public static class Data
    {
        /// <summary>
        /// Kelei风格的 系列皮肤组件
        /// </summary>
        public static class KL_Styles
        {
            /// <summary>
            /// Kelei风格的Window
            /// </summary>
            public static GUIStyle Window { get { return Windows.KL_Window; } }

            /// <summary>
            /// Kelei风格的TextArea
            /// </summary>
            public static GUIStyle TextArea { get { return TextAreas.KL_TextArea; } }

            /// <summary>
            /// Kelei风格的VerticalScrollbarThumb
            /// </summary>
            public static GUIStyle VerticalScrollbarThumb { get { return VerticalScrollbarThumbs.KL_VerticalScrollbarThumb; } }

            /// <summary>
            /// Kelei风格的VerticalScrollbar
            /// </summary>
            public static GUIStyle VerticalScrollbar { get { return VerticalScrollbars.KL_VerticalScrollbar; } }

            /// <summary>
            /// Kelei风格的VerticalScrollbarUpButton
            /// </summary>
            public static GUIStyle VerticalScrollbarUpButton { get { return VerticalScrollbarUpButtons.KL_VerticalScrollbarUpButton; } }

            /// <summary>
            /// Kelei风格的VerticalScrollbarDownButton
            /// </summary>
            public static GUIStyle VerticalScrollbarDownButton { get { return VerticalScrollbarDownButtons.KL_VerticalScrollbarDownButton; } }

            /// <summary>
            /// Kelei风格的Button
            /// </summary>
            public static GUIStyle Button { get { return Buttons.KL_Button; } }

        }

        /// <summary>
        /// Kelei风格的默认皮肤
        /// </summary>
        public static GUISkin KL_DefaultSkin { get { return DefaultSkin.KL_Skin; } }

    }
}
