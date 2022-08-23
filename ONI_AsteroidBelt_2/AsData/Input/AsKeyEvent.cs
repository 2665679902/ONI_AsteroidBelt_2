using HarmonyLib;
using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsData.Input
{
    internal class AsKeyEvent
    {
        [HarmonyPatch(typeof(KInputController), "QueueButtonEvent")]
        private static class KInputController_Patch
        {
            public static void Prefix(KInputController.KeyDef key_def, bool is_down)
            {
                if (!is_down)
                {
                    EventCentre.Trigger(EventInventory.GetKeyEvent, key_def);
                }
            }
        }
    }
}
