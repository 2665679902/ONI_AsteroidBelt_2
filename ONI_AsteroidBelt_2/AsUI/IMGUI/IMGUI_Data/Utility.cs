using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsUI.IMGUI.IMGUI_Data
{
    internal static class Utility
    {
        public static Texture2D GetPicFromColor(double r, double g, double b, double a = 1)
        {
            var color = new Color((float)r / 255, (float)g / 255, (float)b / 255, (float)a);
            Texture2D t = new Texture2D(1024, 1024);
            for (int w = 0; w < 1024; w++)
            {
                for (int h = 0; h < 1024; h++)
                {
                    t.SetPixel(w, h, color);
                }
            }
            t.Apply();
            return t;
        }

        public static Color GetColor(double r, double g, double b, double a = 1)
        {
            var color = new Color((float)r / 255, (float)g / 255, (float)b / 255, (float)a);
            return color;
        }
    }
}
