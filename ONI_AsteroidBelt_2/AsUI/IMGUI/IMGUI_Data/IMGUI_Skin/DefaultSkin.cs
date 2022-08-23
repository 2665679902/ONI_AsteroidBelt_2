using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data.IMGUI_Skin
{
    internal class DefaultSkin
    {
        /// <summary>
        /// Kelei风格的皮肤
        /// </summary>
        public static GUISkin KL_Skin { get => GetKL_Skin();  }

        private static GUISkin GetKL_Skin()
        {
            var skin = ScriptableObject.CreateInstance<GUISkin>();

            skin.window = Data.KL_Styles.Window;

            skin.textArea = Data.KL_Styles.TextArea;

            skin.verticalScrollbarThumb = Data.KL_Styles.VerticalScrollbarThumb;

            skin.verticalScrollbar = Data.KL_Styles.VerticalScrollbar;

            skin.verticalScrollbarUpButton = Data.KL_Styles.VerticalScrollbarUpButton;

            skin.verticalScrollbarDownButton = Data.KL_Styles.VerticalScrollbarDownButton;

            skin.button = Data.KL_Styles.Button;

            return skin;
        }

    }
}
