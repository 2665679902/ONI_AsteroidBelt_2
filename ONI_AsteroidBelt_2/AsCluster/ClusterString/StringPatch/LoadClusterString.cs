using ONI_AsteroidBelt_2.AsData.CurrentCluster;
using ONI_AsteroidBelt_2.AsData.Strings;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.ClusterString.StringPatch
{
    internal class LoadClusterString
    {
        [Load]
        private static void Load()
        {
            EventCentre.Subscribe(EventInventory.StringLoadReady,load);

            void load(object o)
            {
                Strings.Add($"STRINGS.CLUSTER_NAMES.{AsStrings.AsWorldString.ClusterString.Name.Key.ToUpperInvariant()}.NAME", AsStrings.AsWorldString.ClusterString.Name);

                Strings.Add($"STRINGS.CLUSTER_NAMES.{AsStrings.AsWorldString.ClusterString.Discribe.Key.ToUpperInvariant()}.DESCRIPTION", AsStrings.AsWorldString.ClusterString.Discribe);

                foreach (var world in ClusterInfoManager.Worlds)
                {
                    Strings.Add($"STRINGS.WORLDS.{world.Name.ToUpper()}.NAME", world.NameShow);
                    Strings.Add($"STRINGS.WORLDS.{world.Name.ToUpper()}.DESCRIPTION", world.DescriptionShow);
                }
            }
        }
    }
}
