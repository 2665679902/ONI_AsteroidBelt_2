using Database;
using HarmonyLib;
using ONI_AsteroidBelt_2.AsData.Strings;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUNING;

namespace ONI_AsteroidBelt_102.AsBuilding.NoseconeConfigs
{
    internal class NoseconePatch
    {
        private static void AddBuildingStrings(string buildingId, string name, string description, string effect)
        {
            Strings.Add(new string[]
            {
                "STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".NAME",
                STRINGS.UI.FormatAsLink(name, buildingId)
            });
            Strings.Add(new string[]
            {
                "STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".DESC",
                description
            });
            Strings.Add(new string[]
            {
                "STRINGS.BUILDINGS.PREFABS." + buildingId.ToUpperInvariant() + ".EFFECT",
                effect
            });
        }

        private static void AddRocketModuleToBuildList(string moduleId, string placebehind = "")
        {
            if (!SelectModuleSideScreen.moduleButtonSortOrder.Contains(moduleId))
            {
                int num = -1;
                if (placebehind != "")
                {
                    num = SelectModuleSideScreen.moduleButtonSortOrder.IndexOf(placebehind);
                }
                int index = (num == -1) ? SelectModuleSideScreen.moduleButtonSortOrder.Count : (num + 1);
                SelectModuleSideScreen.moduleButtonSortOrder.Insert(index, moduleId);
            }
        }


        [Load(typeof(Log))]
        private static void LoadString()
        {
            EventCentre.Subscribe(EventInventory.StringLoadReady, load);

            void load(object o)
            {
                Log.Debug("Nosecone: GeneratedBuildings -Prefix");
                AddBuildingStrings(ConfigFile.ID, AsStrings.AsBuilding.Nosecone.Name, AsStrings.AsBuilding.Nosecone.Description, AsStrings.AsBuilding.Nosecone.Effect);
                int num = BUILDINGS.PLANORDER.FindIndex((PlanScreen.PlanInfo x) => x.category == "Base");
                if (num == -1)
                {
                    return;
                }
                ModUtil.AddBuildingToPlanScreen(new HashedString(num), ConfigFile.ID);
                AddRocketModuleToBuildList(ConfigFile.ID);
            }
        }

        [HarmonyPatch(typeof(Techs), "Init")]
        public static class Database_Techs_Init_Patch
        {
            public static void Postfix(ref Techs __instance)
            {
                Log.Debug("Nosecone: Techs -Postfix");
                __instance.TryGetTechForTechItem("HabitatModuleSmall").unlockedItemIDs.Add(ConfigFile.ID);
            }
        }

    }
}
