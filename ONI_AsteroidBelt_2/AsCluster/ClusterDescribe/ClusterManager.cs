using ONI_AsteroidBelt_2.AsCluster.WorldDescribe;
using ONI_AsteroidBelt_2.AsData.CurrentCluster;
using ONI_AsteroidBelt_2.AsData.Strings;
using ONI_AsteroidBelt_2.Common.AsExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.ClusterDescribe
{
    internal class ClusterManager
    {
        public static ClusterData GetDefaultClusterData(ClusterInfo info)
        {
            var clusterData = new ClusterData()
            {
                Name = AsStrings.AsWorldString.ClusterString.Name.Key,
                CoordinatePrefix = "AS-B",
                MenuOrder = 10,
                ClusterCategory = 2,
                Difficulty = 5,
                StartWorldIndex = 0,
                NumRings = 12,
                StartWorld = new KeyValuePair<WorldData, ClusterInfo.WorldInfo>(WorldManager.GetDefaultWorldData(info.StartWorld.Name, WorldManager.WorldType.StartWorld, info.CoinLast), info.StartWorld),
                InnerWorlds = new List<KeyValuePair<WorldData, ClusterInfo.WorldInfo>>
                {
                    new KeyValuePair<WorldData, ClusterInfo.WorldInfo>(WorldManager.GetDefaultWorldData(info.InnerWorld_0.Name, WorldManager.WorldType.InnerWorld_0, info.CoinLast), info.InnerWorld_0),
                    new KeyValuePair<WorldData, ClusterInfo.WorldInfo>(WorldManager.GetDefaultWorldData(info.InnerWorld_1.Name, WorldManager.WorldType.InnerWorld_1, info.CoinLast), info.InnerWorld_1),
                    new KeyValuePair<WorldData, ClusterInfo.WorldInfo>(WorldManager.GetDefaultWorldData(info.InnerWorld_2.Name, WorldManager.WorldType.InnerWorld_2, info.CoinLast), info.InnerWorld_2),

                },
                OuterWorlds = info.OuterWorldList.Every((i) => { return new KeyValuePair<WorldData, ClusterInfo.WorldInfo>(WorldManager.GetDefaultWorldData(i.Name, WorldManager.WorldType.Outer, info.CoinLast), i); }),
                PoiPlacements = GetDefaultpoiList()
            };
            return clusterData;
        }

        private static List<poiData> GetDefaultpoiList()
        {

            return new List<poiData>()
            {
                HarvestablePOIs(),
                HarvestableSpacePOI_SandyOreField(),
                HarvestableSpacePOI_OrganicMassField(),
                HarvestableSpacePOI_GildedAsteroidField(),
                HarvestableSpacePOI_RadioactiveGasCloud(),
                HarvestableSpacePOI_RockyAsteroidField(),
                HarvestableSpacePOI_CarbonAsteroidField(),
                ArtifactSpacePOI_GravitasSpaceStation1(),
                ArtifactSpacePOI_RussellsTeapot(),
                ArtifactSpacePOI_GravitasSpaceStation2()
            };
        }

        private static poiData HarvestablePOIs()
        {
            var res = new poiData()
            {
                Data = new List<string> { "TemporalTear" },
                NumToSpawn = 1,
                AvoidClumping = false,
                AllowedRingsMax = 10,
                AllowedRingsMin = 8,
                CanSpawnDuplicates = null
            };
            return res;
        }
        private static poiData HarvestableSpacePOI_SandyOreField()
        {
            var res = new poiData()
            {
                Data = new List<string> { "HarvestableSpacePOI_SandyOreField" },
                NumToSpawn = 1,
                AvoidClumping = null,
                AllowedRingsMax = 3,
                AllowedRingsMin = 2,
                CanSpawnDuplicates = null
            };
            return res;
        }
        private static poiData HarvestableSpacePOI_OrganicMassField()
        {
            var res = new poiData()
            {
                Data = new List<string> { "HarvestableSpacePOI_OrganicMassField" },
                NumToSpawn = 1,
                AvoidClumping = null,
                AllowedRingsMax = 7,
                AllowedRingsMin = 5,
                CanSpawnDuplicates = null
            };
            return res;
        }
        private static poiData HarvestableSpacePOI_GildedAsteroidField()
        {
            var res = new poiData()
            {
                Data = new List<string>
                {
                    "HarvestableSpacePOI_GildedAsteroidField" ,
                    "HarvestableSpacePOI_GlimmeringAsteroidField",
                    "HarvestableSpacePOI_HeliumCloud",
                    "HarvestableSpacePOI_OilyAsteroidField",
                    "HarvestableSpacePOI_FrozenOreField"
                },
                NumToSpawn = 5,
                AvoidClumping = null,
                AllowedRingsMax = 11,
                AllowedRingsMin = 8,
                CanSpawnDuplicates = null
            };
            return res;
        }
        private static poiData HarvestableSpacePOI_RadioactiveGasCloud()
        {
            var res = new poiData()
            {
                Data = new List<string> { "HarvestableSpacePOI_RadioactiveGasCloud", "HarvestableSpacePOI_RadioactiveAsteroidField" },
                NumToSpawn = 2,
                AvoidClumping = true,
                AllowedRingsMax = 11,
                AllowedRingsMin = 10,
                CanSpawnDuplicates = null
            };
            return res;
        }
        private static poiData HarvestableSpacePOI_RockyAsteroidField()
        {
            var res = new poiData()
            {
                Data = new List<string>
                {
                    "HarvestableSpacePOI_RockyAsteroidField" ,
                    "HarvestableSpacePOI_InterstellarIceField",
                    "HarvestableSpacePOI_InterstellarOcean",
                    "HarvestableSpacePOI_ForestyOreField",
                    "HarvestableSpacePOI_SwampyOreField",
                    "HarvestableSpacePOI_OrganicMassField"
                },
                NumToSpawn = 5,
                AvoidClumping = null,
                AllowedRingsMax = 7,
                AllowedRingsMin = 5,
                CanSpawnDuplicates = true
            };
            return res;
        }
        private static poiData HarvestableSpacePOI_CarbonAsteroidField()
        {
            var res = new poiData()
            {
                Data = new List<string>
                {
                    "HarvestableSpacePOI_CarbonAsteroidField" ,
                    "HarvestableSpacePOI_MetallicAsteroidField",
                    "HarvestableSpacePOI_SatelliteField",
                    "HarvestableSpacePOI_IceAsteroidField",
                    "HarvestableSpacePOI_GasGiantCloud",
                    "HarvestableSpacePOI_ChlorineCloud",
                    "HarvestableSpacePOI_OxidizedAsteroidField",
                    "HarvestableSpacePOI_SaltyAsteroidField",
                    "HarvestableSpacePOI_OxygenRichAsteroidField",
                    "HarvestableSpacePOI_GildedAsteroidField",
                    "HarvestableSpacePOI_GlimmeringAsteroidField",
                    "HarvestableSpacePOI_HeliumCloud",
                    "HarvestableSpacePOI_OilyAsteroidField",
                    "HarvestableSpacePOI_FrozenOreField",
                    "HarvestableSpacePOI_RadioactiveGasCloud",
                    "HarvestableSpacePOI_RadioactiveAsteroidField",
                },
                NumToSpawn = 10,
                AvoidClumping = null,
                AllowedRingsMax = 11,
                AllowedRingsMin = 7,
                CanSpawnDuplicates = true
            };
            return res;
        }
        private static poiData ArtifactSpacePOI_GravitasSpaceStation1()
        {
            var res = new poiData()
            {
                Data = new List<string>
                {
                    "ArtifactSpacePOI_GravitasSpaceStation1" ,
                    "ArtifactSpacePOI_GravitasSpaceStation4",
                    "ArtifactSpacePOI_GravitasSpaceStation6",
                },
                NumToSpawn = 1,
                AvoidClumping = true,
                AllowedRingsMax = 3,
                AllowedRingsMin = 2,
                CanSpawnDuplicates = null
            };
            return res;
        }
        private static poiData ArtifactSpacePOI_RussellsTeapot()
        {
            var res = new poiData()
            {
                Data = new List<string>
                {
                    "ArtifactSpacePOI_RussellsTeapot"
                },
                NumToSpawn = 1,
                AvoidClumping = true,
                AllowedRingsMax = 11,
                AllowedRingsMin = 9,
                CanSpawnDuplicates = null
            };
            return res;
        }
        private static poiData ArtifactSpacePOI_GravitasSpaceStation2()
        {
            var res = new poiData()
            {
                Data = new List<string>
                {
                    "ArtifactSpacePOI_GravitasSpaceStation2" ,
                    "ArtifactSpacePOI_GravitasSpaceStation3",
                    "ArtifactSpacePOI_GravitasSpaceStation5",
                    "ArtifactSpacePOI_GravitasSpaceStation7",
                    "ArtifactSpacePOI_GravitasSpaceStation8"

                },
                NumToSpawn = 4,
                AvoidClumping = true,
                AllowedRingsMax = 11,
                AllowedRingsMin = 4,
                CanSpawnDuplicates = null
            };
            return res;
        }
    }
}
