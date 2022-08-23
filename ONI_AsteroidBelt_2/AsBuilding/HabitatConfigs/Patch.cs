using Database;
using HarmonyLib;
using Klei;
using ONI_AsteroidBelt_2.AsData.Strings;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using ProcGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TUNING;

namespace ONI_AsteroidBelt_2.AsBuilding.HabitatConfigs
{
    internal class HabitatPatch
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
                Log.Debug("Habitat: GeneratedBuildings -Prefix");

                ROCKETRY.ROCKET_INTERIOR_SIZE = new Vector2I(HabitatData.SpaceWidth, HabitatData.SpaceWidth);
                AddBuildingStrings(ConfigFile.ID, AsStrings.AsBuilding.Habitat.Name, AsStrings.AsBuilding.Habitat.Description, AsStrings.AsBuilding.Habitat.Effect);
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
                Log.Debug("Habitat: Techs -Postfix");
                // 从Techs.Init找就行
                __instance.TryGetTechForTechItem("HabitatModuleSmall").unlockedItemIDs.Add(ConfigFile.ID);
            }
        }

        [HarmonyPatch(typeof(WorldContainer), "PlaceInteriorTemplate")]
        public class PlaceInteriorTemplatePatch
        {
            public static void Postfix(ref WorldContainer __instance, ref string template_name, ref System.Action callback)
            {
                Log.Debug("Habitat: WorldContainer try to PlaceInteriorTemplate -Postfix");

                var overworldCell = Traverse.Create(__instance).Field("overworldCell").GetValue<WorldDetailSave.OverworldCell>();

                overworldCell.zoneType = template_name != "expansion1::interiors/habitat_huge" ? SubWorld.ZoneType.RocketInterior : HabitatData.ZoneType;

            }
        }

        [HarmonyPatch(typeof(ClusterManager), "CreateRocketInteriorWorld")]
        public class CreateRocketInteriorWorldPatch
        {
            public static void Prefix(ref string interiorTemplateName)
            {
                if (interiorTemplateName == "expansion1::interiors/habitat_huge")
                {
                    Log.Debug("Habitat: ClusterManager try to CreateRocketInteriorWorld -Prefix");

                    HabitatLoad.Load();
                }

            }

            public static void Postfix(ref string interiorTemplateName)
            {
                if (interiorTemplateName == "expansion1::interiors/habitat_huge")
                {
                    Log.Debug("Habitat: try to unLoad -Postfix");

                    HabitatLoad.UnLoad();
                }

                ROCKETRY.ROCKET_INTERIOR_SIZE = new Vector2I(32, 32);
            }
        }

    }

}
