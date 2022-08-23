using HarmonyLib;
using Klei.CustomSettings;
using ONI_AsteroidBelt_2.AsData.CurrentCluster;
using ONI_AsteroidBelt_2.AsData.Strings;
using ONI_AsteroidBelt_2.Common;
using ProcGenGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ONI_AsteroidBelt_2.AsData.CurrentCluster.ClusterInfo;

namespace ONI_AsteroidBelt_2.AsCluster.ClusterBuilder
{
    internal class WorldBuildCatch
    {
        private static readonly List<KeyValuePair<WorldDescribe.WorldData,WorldInfo>> worldAccessible = new List<KeyValuePair<WorldDescribe.WorldData, WorldInfo>>();

        private static bool IsAsteroidBelt
        {
            get
            {
                return CustomGameSettings.Instance.GetCurrentQualitySetting(CustomGameSettingConfigs.ClusterLayout).id == ("clusters/" + AsStrings.AsWorldString.ClusterString.Name.Key);
            }
        }

        private static void DefaultWorldReLoad()
        {
            worldAccessible.Clear();

            var info = ClusterInfoManager.CurrentCluster;

            var Cluster = ClusterDescribe.ClusterManager.GetDefaultClusterData(info);

            worldAccessible.Add(Cluster.StartWorld);

            foreach (var world in Cluster.InnerWorlds)
                worldAccessible.Add(world);

            foreach (var world in Cluster.OuterWorlds)
                worldAccessible.Add(world);

            Log.Debug("CommonWorldReLoad：世界信息重载");

        }

        private static bool Catch(WorldGen __instance, ref bool __result, ref Sim.Cell[] cells, ref Sim.DiseaseCell[] dc, int baseId)
        {
            //判断是否还能取值
            if (!worldAccessible.Any())
                return false;

            //生成取值
            FormateTerrain.CreatorRandom = new Random(__instance.data.globalTerrainSeed);

            //获取世界
            var world = worldAccessible.First();
            worldAccessible.Remove(world);

            try
            {
                //生成世界
                __result = FormateTerrain.CreatWorld(__instance, ref cells, ref dc, baseId, world.Key.World,world.Value);
            }
            catch (Exception e)
            {
                Log.Error("世界生成错误错误抛出 -> " + e.Message);
                return false;
            }


            return true;
        }

        /// <summary>
        /// 捕获地图生成结果
        /// </summary>
        [HarmonyPatch(typeof(WorldGen), "RenderOffline")]
        private static class WorldGen_RenderOffline_Patch
        {
            public static bool Prefix(WorldGen __instance, ref bool __result, ref Sim.Cell[] cells, ref Sim.DiseaseCell[] dc, int baseId)
            {

                Log.Debug("-------------------世界生成函数捕获----------------------");
                Log.Debug($"Size  X: {__instance.World.size.X}    Y: {__instance.World.size.Y}");
                return !(IsAsteroidBelt && Catch(__instance, ref __result, ref cells, ref dc, baseId));
            }

            public static void Postfix()
            {
                Log.Debug("捕获结束");
            }
        }


        [HarmonyPatch(typeof(Cluster), "BeginGeneration")]
        private static class BeginGenerationPatch
        {
            public static void Prefix()
            {
                DefaultWorldReLoad();
            }
        }
    }
}
