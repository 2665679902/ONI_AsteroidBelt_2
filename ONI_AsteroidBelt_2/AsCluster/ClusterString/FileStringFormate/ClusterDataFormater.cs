using ONI_AsteroidBelt_2.AsCluster.ClusterDescribe;
using ONI_AsteroidBelt_2.AsCluster.WorldDescribe;
using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.ClusterString.FileStringFormate
{
    internal class ClusterDataFormater
    {
        public static string FormateClusterFile(ClusterData Cluster)
        {
            StringBuilder result = new StringBuilder(
                $"name: STRINGS.CLUSTER_NAMES.{Cluster.Name.ToUpper()}.NAME\n" +
                $"description: STRINGS.CLUSTER_NAMES.{Cluster.Name.ToUpper()}.DESCRIPTION\n" +
                $"requiredDlcId: EXPANSION1_ID\n" +
                $"coordinatePrefix: {Cluster.CoordinatePrefix}\n" +
                $"menuOrder: {Cluster.MenuOrder}\n" +
                $"clusterCategory: {Cluster.ClusterCategory}\n" +
                $"difficulty: {Cluster.Difficulty} \n" +
                $"\n" +
                $"startWorldIndex: {Cluster.StartWorldIndex}\n" +
                $"numRings: {Cluster.NumRings}\n" +
                $"worldPlacements:\n"
                );

            result.Append(
                $"- world: expansion1::worlds/{Cluster.StartWorld.Key.World.Name}\n" +
                $"  buffer: {Cluster.StartWorld.Key.Buffer}\n" +
                $"  locationType: StartWorld\n" +
                $"  allowedRings:\n" +
                $"    min: {Cluster.StartWorld.Key.AllowedRingsMin}\n" +
                $"    max: {Cluster.StartWorld.Key.AllowedRingsMax}\n"
                );


            foreach (var worldData in Cluster.InnerWorlds)
            {
                result.Append(
                    $"- world: expansion1::worlds/{worldData.Key.World.Name}\n" +
                    $"  buffer: {worldData.Key.Buffer}\n" +
                    $"  locationType: InnerCluster\n" +
                    $"  allowedRings:\n" +
                    $"    min: {worldData.Key.AllowedRingsMin}\n" +
                    $"    max: {worldData.Key.AllowedRingsMax}\n"
                    );
            }

            foreach (var worldData in Cluster.OuterWorlds)
            {
                result.Append(
                    $"- world: expansion1::worlds/{worldData.Key.World.Name}\n" +
                    $"  buffer: {worldData.Key.Buffer}\n" +
                    $"  allowedRings:\n" +
                    $"    min: {worldData.Key.AllowedRingsMin}\n" +
                    $"    max: {worldData.Key.AllowedRingsMax}\n"
                    );
            }

            result.Append("\npoiPlacements:");

            foreach (var Poi in Cluster.PoiPlacements)
            {
                result.Append($"\n  - pois:\n");
                foreach (var poiName in Poi.Data)
                    result.Append($"      - {poiName}\n");
                result.Append($"    numToSpawn: {Poi.NumToSpawn}\n");

                if (Poi.AvoidClumping is bool avoidClumping)
                {
                    if (avoidClumping)
                        result.Append($"    avoidClumping: true\n");
                    else
                        result.Append($"    avoidClumping: false\n");
                }

                if (Poi.CanSpawnDuplicates is bool canSpawnDuplicates)
                {
                    if (canSpawnDuplicates)
                        result.Append($"    canSpawnDuplicates: true\n");
                    else
                        result.Append($"    canSpawnDuplicates: false\n");
                }

                result.Append(
                    $"    allowedRings:\n" +
                    $"      min: {Poi.AllowedRingsMin}\n" +
                    $"      max: {Poi.AllowedRingsMax}");
            }

            return result.ToString();
        }

        public static string FormateWorldFile(WorldDescribe.World world)
        {
            StringBuilder result = new StringBuilder(
            $"name: STRINGS.WORLDS.{world.Name.ToUpper()}.NAME\n" +
            $"description: STRINGS.WORLDS.{world.Name.ToUpper()}.DESCRIPTION\n" +
            $"nameTables: [ TERRA ]\n" +
            $"asteroidIcon: asteroid_sandstone_start_kanim\n" +
            $"disableWorldTraits: true\n" +
            $"\n" +
            $"worldTraitScale: 0.06 \n" +
            $"worldsize:\n" +
            $"  X: {world.Width}\n" +
            $"  Y: {world.Height}\n" +
            $"layoutMethod: PowerTree\n" +
            $"\n" +
            $"subworldFiles:\n" +
            $"  - name: subworlds/space/Space\n" +
            $"  - name: expansion1::subworlds/space/SpaceWithNoBorder\n" +
            $"  - name: expansion1::subworlds/space/SpaceWithNoBorder\n");



            foreach (var biomes in world.Biomes)
            {
                result.Append($"  - name: {biomes.BackgroundSubworld}\n");
            }


            if (world.FixedTraits.Any())
            {
                result.Append($"fixedTraits:\n");
                foreach (var trait in world.FixedTraits)
                    result.Append($"  - {trait}\n");
            }

            result.Append(
                $"unknownCellsAllowedSubworlds:\n" +
                $"  - tagcommand: AtTag\n" +
                $"    tag: AtSurface\n" +
                $"    command: Replace\n" +
                $"    subworldNames:\n" +
                $"      - subworlds/space/Space\n" +
                $"  - tagcommand: DistanceFromTag\n" +
                $"    tag: AtSurface\n" +
                $"    minDistance: 1\n" +
                $"    maxDistance: 99\n" +
                $"    command: Replace\n" +
                $"    subworldNames:\n" +
                $"      - expansion1::subworlds/space/SpaceWithNoBorder\n" +
                $"\n" +
                $"worldTemplateRules:");

            return result.ToString();

        }
    }
}
