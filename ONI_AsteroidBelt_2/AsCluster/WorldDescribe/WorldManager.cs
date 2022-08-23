using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe;
using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using ONI_AsteroidBelt_2.Common.AsExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.WorldDescribe
{
    internal class WorldManager
    {
        public enum WorldType
        {
            StartWorld,
            InnerWorld_0,
            InnerWorld_1,
            InnerWorld_2,
            Outer
        }
        private static Dictionary<string, World> _worldData;

        public static Dictionary<string, World> WorldData { get => _worldData.Clone(); }

        [Load(typeof(BiomeManager))]
        private static void Load()
        {
            WriteInDefaultWorlds();
            _worldData = FileUtility.ReadAll<World>(@"Resources\ClusterData\Worlds");
        }

        private static void WriteInDefaultWorlds()
        {
            var worlds = new Dictionary<string, World>()
            {
                {
                    "Sandstone",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Sandstone"]},
                        Templates = new List<Template>
                        {
                            new Template() { Name = "expansion1::poi/poi_geyser_dirty_slush", Max = 1, Min = 1 }
                        },
                        Name = "Sandstone",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Forest",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Forest"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/big_volcano", Max = 2, Min = 1 }
                        },
                        Name = "Forest",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Swamp",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Swamp"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/slush_water", Max = 2, Min = 1 }
                        },
                        Name = "Swamp",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Barren",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Barren"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/molten_iron", Max = 2, Min = 1 }
                        },
                        Name = "Barren",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Aquatic",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Aquatic"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/hot_water", Max = 2, Min = 1 }
                        },
                        Name = "Aquatic",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Frozen",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Frozen"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/liquid_co2", Max = 2, Min = 1 }
                        },
                        Name = "Frozen",
                        FixedTraits = new List<string>
                        {

                        }
                    }


                },

                {
                    "Jungle",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Jungle"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/chlorine_gas", Max = 2, Min = 1 }
                        },
                        Name = "Jungle",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Magma",
                    new World()
                    {
                        Biomes = new List<Biome>
                        {
                            BiomeManager.BoimeData["Magma"],
                            BiomeManager.BoimeData["Magma"],
                            BiomeManager.BoimeData["Magma"],
                            BiomeManager.BoimeData["Niobium"]
                        },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "expansion1::geysers/molten_niobium", Max = 2, Min = 1 }
                        },
                        Name = "Magma",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Marsh",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Marsh"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/molten_gold", Max = 2, Min = 1 },
                            new Template() { Name = "expansion1::poi/sap_tree_room", Max = 1, Min = 1 }
                        },
                        Name = "Marsh",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Moo",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Moo"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "expansion1::geysers/slush_salt_water", Max = 2, Min = 1 },
                            new Template() { Name = "expansion1::geysers/molten_tungsten", Max = 2, Min = 1 }
                        },
                        Name = "Moo",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Oil",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Oil"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/oil_drip", Max = 2, Min = 1 }
                        },
                        Name = "Oil",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Ocean",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Ocean"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/salt_water", Max = 2, Min = 1 }
                        },
                        Name = "Ocean",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Radioactive",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Radioactive"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "expansion1::geysers/molten_cobalt", Max = 2, Min = 1 }
                        },
                        Name = "Radioactive",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Regolith",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Regolith"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/molten_copper", Max = 2, Min = 1 }
                        },
                        Name = "Regolith",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Rust",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Rust"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "geysers/methane", Max = 2, Min = 1 }
                        },
                        Name = "Rust",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },

                {
                    "Wasteland",
                    new World()
                    {
                        Biomes = new List<Biome>{ BiomeManager.BoimeData["Wasteland"] },
                        Templates = new List<Template>
                        {
                            new Template() { Name = "expansion1::geysers/liquid_sulfur", Max = 2, Min = 1 }
                        },
                        Name = "Wasteland",
                        FixedTraits = new List<string>
                        {

                        }
                    }
                },
            };
            FileUtility.WriteAll(@"Resources\ClusterData\Worlds", worlds);
        }

        //------------------------------------------------------------------------

        public static WorldData GetDefaultWorldData(string worldName, WorldType worldType, int coinLast)
        {
            if (!WorldData.ContainsKey(worldName))
            {
                Log.Error($"WorldManager can't find world: {worldName}");
                worldName = "Sandstone";
            }

            var Data = new WorldData
            {
                World = WorldData[worldName]
            };

            switch (worldType)
            {
                case WorldType.StartWorld:
                    Data.World = new StartWorld(Data.World)
                    {
                        StartingTemplate = "bases/sandstoneBase",
                        StartingItems = new List<Things> 
                        { 
                            new Things("Gold", coinLast * 500) ,
                            new Things("FieldRation", 60 + coinLast)
                        }
                    };
                    foreach(var boime in Data.World.Biomes)
                    {
                        boime.IsStartBiome = true;
                    }
                    Log.Debug(Data.World.Templates);
                    Data.World.Templates.Add(new Template() { Name = "expansion1::poi/warp/teleporter", Max = 1, Min = 1 });
                    Data.World.Templates.Add(new Template() { Name = "expansion1::poi/warp/sender", Max = 1, Min = 1 });
                    Data.World.Templates.Add(new Template() { Name = "expansion1::poi/warp/receiver", Max = 1, Min = 1 });
                    Data.AllowedRingsMin = 0;
                    Data.AllowedRingsMax = 0;
                    Data.Buffer = 2;
                    Log.Debug("Data.World.Templates ---> " + Data.World.Templates.Count);
                    Log.Debug(Data.World.Templates);
                    break;

                case WorldType.InnerWorld_2:
                    Data.World = new StartWorld(Data.World)
                    {
                        StartingTemplate = "expansion1::bases/warpworldForestBase",
                        StartingItems = new List<Things>
                        {
                        }
                    };
                    foreach (var boime in Data.World.Biomes)
                    {
                        boime.IsStartBiome = true;
                    }
                    Data.AllowedRingsMin = 2;
                    Data.AllowedRingsMax = 4;
                    Data.Buffer = 2;
                    break;

                case WorldType.InnerWorld_0:
                    Data.AllowedRingsMin = 2;
                    Data.AllowedRingsMax = 4;
                    Data.Buffer = 2;
                    break;

                case WorldType.InnerWorld_1:
                    Data.AllowedRingsMin = 2;
                    Data.AllowedRingsMax = 4;
                    Data.Buffer = 2;
                    break;

                default:
                    Data.AllowedRingsMin = 4;
                    Data.AllowedRingsMax = 13;
                    Data.Buffer = 4;
                    break;
            };

            return Data;
        }
    }
}
