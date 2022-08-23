using ONI_AsteroidBelt_2.AsData.CurrentCluster;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using ONI_AsteroidBelt_2.AsCluster.ClusterDescribe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.AsCluster.WorldDescribe;
using ONI_AsteroidBelt_2.AsUI.UGUI.SelectWorldUI;

namespace ONI_AsteroidBelt_2.AsCluster.ClusterString.FileStringFormate
{
    internal class LoadClusterFile
    {
        [Load(typeof(SelectWorldUIBuilder))]
        private static void Load()
        {
            var current = ClusterDescribe.ClusterManager.GetDefaultClusterData(ClusterInfoManager.CurrentCluster);

            FileUtility.WriteIn($@"worldgen\Clusters\{current.Name}.yaml", ClusterDataFormater.FormateClusterFile(current));

            FileUtility.WriteIn($@"Resources\ClusterData\Intermediary\{current.StartWorld.Key.World.Name}.yaml", ClusterDataFormater.FormateWorldFile(current.StartWorld.Key.World));

            foreach (var world in current.InnerWorlds)
                FileUtility.WriteIn($@"Resources\ClusterData\Intermediary\{world.Key.World.Name}.yaml", ClusterDataFormater.FormateWorldFile(world.Key.World));

            foreach (var world in current.OuterWorlds)
                FileUtility.WriteIn($@"Resources\ClusterData\Intermediary\{world.Key.World.Name}.yaml", ClusterDataFormater.FormateWorldFile(world.Key.World));

            FileUtility.LoadFile(@"Resources\ClusterData\Intermediary", @"OxygenNotIncluded_Data\StreamingAssets\dlc\expansion1\worldgen\worlds");

        }

        [Load(null, LoadAttribute.LoadOpportunity.late)]
        private static void UnLoad()
        {
            FileUtility.Remove(@"Resources\ClusterData\Intermediary");
        }
    }
}
