using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.BandDescribe;
using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.BiomePattern;
using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using ONI_AsteroidBelt_2.Common.AsExtension;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ONI_AsteroidBelt_2.AsData.CurrentCluster.ClusterInfo;

namespace ONI_AsteroidBelt_2.AsCluster.BiomeDescribe
{
    internal class BiomeManager
    {
        private static Dictionary<string, List<Band>> _bandData = new Dictionary<string, List<Band>>();

        private static Dictionary<string, Biome> _boimeData = new Dictionary<string, Biome>();

        public static Dictionary<string, Biome> BoimeData { get => _boimeData.Clone(); }

        public static Dictionary<string, List<Band>> BandData { get => _bandData.Clone(); }
        

        //------------------------------------------------------------------------

        [Load(typeof(Log))]
        private static void Load()
        {
            //尝试写入默认的Bands列表
            WriteInDefaultBands();
            _bandData = FileUtility.ReadAll<Bands>(@"Resources\ClusterData\Bands").Every
            ((k, v) => { return new KeyValuePair<string, List<Band>>(k, v.BandList); });
            //尝试写入默认的Boimes
            WriteInDefaultBoimes();
            _boimeData = FileUtility.ReadAll<Biome>(@"Resources\ClusterData\Boimes");
        }

        private static void WriteInDefaultBands()
        {
            var bands = new Dictionary<string, Bands>
            {
                {
                    "Sandstone",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Water, density: 2f),
                    new Band(0.2, SimHashes.Water, density: 2f),
                    new Band(0.2, SimHashes.Water, density: 2f),
                    new Band(0.2, SimHashes.Water, density: 2f),
                    new Band(0.2, SimHashes.Oxygen, density: 2f),
                    new Band(0.2, SimHashes.Dirt),
                    new Band(0.2, SimHashes.Algae, density: 4f,disease:Band.DiseaseID.SlimeGerms),
                    new Band(0.2, SimHashes.IronOre),
                    new Band(0.2, SimHashes.SandStone),
                    new Band(0.2, SimHashes.SandStone),
                    new Band(0.2, SimHashes.SandStone),
                    new Band(0.2, SimHashes.Carbon),
                    new Band(0.2, SimHashes.SandStone),
                    new Band(0.2, SimHashes.Fossil),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.Cuprite),
                    new Band(0.2, SimHashes.Dirt),
                    new Band(0.2, SimHashes.Oxygen, density: 2f),
                    new Band(0.2, SimHashes.CarbonDioxide)
                    })
                },// 水 氧气 泥土 菌泥 铁矿 砂石 二氧化碳 沉积岩 化肥 铜矿

                {
                    "Forest",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Water, density: 2f),
                    new Band(0.2, SimHashes.Water, density: 2f),
                    new Band(0.2, SimHashes.Water, density: 2f),
                    new Band(0.2, SimHashes.Oxygen, density: 2f),
                    new Band(0.2, SimHashes.Dirt),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Dirt),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Dirt),
                    new Band(0.2, SimHashes.SandStone),
                    new Band(0.2, SimHashes.Carbon),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.AluminumOre),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Oxygen, density: 2f),
                    new Band(0.2, SimHashes.Dirt),
                    new Band(0.2, SimHashes.Oxygen, density: 2f),
                    new Band(0.2, SimHashes.CarbonDioxide)
                    })
                },// 水 氧气 泥土 火成岩 沙子 铝矿 二氧化碳

                {
                    "Swamp",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Cobaltite),
                    new Band(0.2, SimHashes.Mud),
                    new Band(0.2, SimHashes.DirtyWater),
                    new Band(0.2, SimHashes.ToxicSand),
                    new Band(0.2, SimHashes.Cobaltite),
                    new Band(0.2, SimHashes.Mud),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.DirtyWater),
                    new Band(0.2, SimHashes.Mud),
                    new Band(0.2, SimHashes.ToxicSand),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.Phosphorite)
                    })
                },//污染土 钴矿 泥巴 co2 磷矿
                {
                    "Barren",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Obsidian),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.Carbon),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.SandStone),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Iron),
                    new Band(0.2, SimHashes.Obsidian)
                    })
                },//黑曜石 花岗岩 火成岩 碳 铁
                {
                    "Aquatic",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Water),
                    new Band(0.2, SimHashes.Graphite),
                    new Band(0.2, SimHashes.Water),
                    new Band(0.2, SimHashes.Oxygen),
                    new Band(0.2, SimHashes.Water),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Water),
                    new Band(0.2, SimHashes.Water),
                    new Band(0.2, SimHashes.Water),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Graphite),
                    new Band(0.2, SimHashes.CarbonDioxide)
                    })
                },// 水 石墨

                {
                    "Frozen",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.LiquidOxygen),
                    new Band(0.2, SimHashes.Ice),
                    new Band(0.2, SimHashes.SolidCarbonDioxide),
                    new Band(0.2, SimHashes.LiquidOxygen),
                    new Band(0.2, SimHashes.BrineIce),
                    new Band(0.2, SimHashes.SedimentaryRock),
                    new Band(0.2, SimHashes.SedimentaryRock),
                    new Band(0.2, SimHashes.LiquidOxygen),
                    new Band(0.2, SimHashes.SedimentaryRock),
                    new Band(0.2, SimHashes.LiquidOxygen),
                    new Band(0.2, SimHashes.BrineIce),
                    new Band(0.2, SimHashes.SedimentaryRock)
                    })
                },//固液态co2 冰 液氧 浓盐冰 雪 污染冰

                {
                    "Jungle",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Phosphorite),
                    new Band(0.2, SimHashes.Carbon),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.IronOre),
                    new Band(0.2, SimHashes.Algae),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.ChlorineGas),
                    new Band(0.2, SimHashes.IronOre),
                    new Band(0.2, SimHashes.Hydrogen),
                    new Band(0.2, SimHashes.ChlorineGas),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.IronOre),
                    new Band(0.2, SimHashes.Phosphorite),
                    })
                },//磷矿 c 火成岩 Fe 藻类 H 氯


                {
                    "Magma",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Magma),
                    new Band(0.2, SimHashes.Obsidian),
                    new Band(0.2, SimHashes.Magma),
                    new Band(0.2, SimHashes.Obsidian),
                    new Band(0.2, SimHashes.Magma),
                    new Band(0.2, SimHashes.Obsidian),
                    new Band(0.2, SimHashes.Magma),
                    new Band(0.2, SimHashes.Obsidian),
                    new Band(0.2, SimHashes.Magma),
                    new Band(0.2, SimHashes.Obsidian),
                    new Band(0.2, SimHashes.Magma),
                    new Band(0.2, SimHashes.Obsidian)
                    })
                },// 岩浆 黑曜石

                {
                    "Niobium",
                    new Bands(new List<Band>
                    {
                        new Band(0.2, SimHashes.Obsidian,density:0.6),
                        new Band(0.2, SimHashes.Niobium,density:0.6),
                        new Band(0.2, SimHashes.Obsidian,density:0.6),
                        new Band(0.2, SimHashes.Niobium,density:0.6),
                        new Band(0.2, SimHashes.Obsidian,density:0.6),
                        new Band(0.2, SimHashes.Niobium,density:0.6),
                        new Band(0.2, SimHashes.Obsidian,density:0.6),
                        new Band(0.2, SimHashes.Niobium,density:0.6),
                        new Band(0.2, SimHashes.Obsidian,density:0.6),
                        new Band(0.2, SimHashes.Niobium,density:0.6),
                        new Band(0.2, SimHashes.Obsidian,density:0.6),
                        new Band(0.2, SimHashes.Niobium,density:0.6)
                    })
                },//黑曜石 铌

                {
                    "Marsh",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Algae),
                    new Band(0.2, SimHashes.Clay),
                    new Band(0.2, SimHashes.SlimeMold,disease: Band.DiseaseID.SlimeGerms),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.SedimentaryRock),
                    new Band(0.2, SimHashes.SedimentaryRock),
                    new Band(0.2, SimHashes.Algae),
                    new Band(0.2, SimHashes.SlimeMold),
                    new Band(0.2, SimHashes.Clay),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.GoldAmalgam),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.GoldAmalgam),
                    })
                },//藻类 粘菌 沉积岩 金 受污染的氧气 co2

                {
                    "Moo",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.BleachStone),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.BleachStone),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.SolidCarbonDioxide),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.BleachStone),
                    new Band(0.2, SimHashes.IgneousRock)
                    })
                },//氯 火成岩

                {
                    "Ocean",
                    new Bands(new List<Band>
                    {   
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.SedimentaryRock),
                    new Band(0.2, SimHashes.BleachStone),
                    new Band(0.2, SimHashes.Sand),
                    new Band(0.2, SimHashes.SaltWater),
                    new Band(0.2, SimHashes.SedimentaryRock),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.SedimentaryRock),
                    new Band(0.2, SimHashes.Hydrogen),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.Granite),
                    new Band(0.2, SimHashes.BleachStone),
                    new Band(0.2, SimHashes.Hydrogen),
                    })
                },//花岗岩 沉积岩 漂白石 沙子 盐水 H co2

                {
                    "Oil",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.Diamond),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Fossil),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.CrudeOil),
                    new Band(0.2, SimHashes.SourGas),
                    new Band(0.2, SimHashes.CrudeOil),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.CrudeOil),
                    new Band(0.2, SimHashes.SourGas),
                    new Band(0.2, SimHashes.Fossil),
                    new Band(0.2, SimHashes.IronOre),
                    new Band(0.2, SimHashes.IronOre),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.CrudeOil),
                    new Band(0.2, SimHashes.Lead),
                    new Band(0.2, SimHashes.Diamond)
                    })
                },//  钻石 原油 铅 

                {
                    "Radioactive",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.UraniumOre),
                    new Band(0.2, SimHashes.Ice),
                    new Band(0.2, SimHashes.Sulfur),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.UraniumOre),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.Wolframite),
                    new Band(0.2, SimHashes.Rust),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.Ice)
                    })
                },//铀矿 冰 硫 铁锈 土 漂白石 co2 黑钨矿

                {
                    "Regolith",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Regolith),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Regolith),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.IronOre),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.Cuprite),
                    new Band(0.2, SimHashes.Regolith),
                    new Band(0.2, SimHashes.Rust)
                    })
                },//

                {
                    "Rust",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.IronOre),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Rust),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.ChlorineGas),
                    new Band(0.2, SimHashes.Rust),
                    new Band(0.2, SimHashes.ChlorineGas),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Snow),
                    new Band(0.2, SimHashes.CarbonDioxide),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Rust),
                    new Band(0.2, SimHashes.IronOre),
                    })
                },//铁锈 镁铁质岩 氯 盐水 雪 铁

                {
                    "Wasteland",
                    new Bands(new List<Band>
                    {
                    new Band(0.2, SimHashes.SandStone),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Cuprite),
                    new Band(0.2, SimHashes.IgneousRock),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Oxygen),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Oxygen),
                    new Band(0.2, SimHashes.MaficRock),
                    new Band(0.2, SimHashes.Sulfur),
                    new Band(0.2, SimHashes.Oxygen)
                    })
                }//砂石 火成岩 硫 氧 铜矿 镁铁质岩
            };

            FileUtility.WriteAll(@"Resources\ClusterData\Bands", bands);
        }

        private static void WriteInDefaultBoimes()
        {
            var bands = new Dictionary<string, Biome>
            {
                {
                    "Sandstone",
                    new Biome(
                        "subworlds/sandstone/SandstoneStart",
                        -1,"Sandstone",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("BasicSingleHarvestPlant", 0.03) ,//米虱木
                            new Critter("PrickleFlower",0.02 ),//毛刺花
                            new Critter("PrickleGrass",0.06) ,//诱人荆棘
                            new Critter("BasicForagePlantPlanted",0.05)//淤泥根
                        },//松鼠 米虱木 诱人荆棘 毛刺花  淤泥根

                        new List<Critter>//spawnablesOnCeil
                        {

                        },

                        new List<Critter>//spawnablesInGround
                        {
                            new Critter("Hatch",0.0001) ,//哈奇
                            new Critter("BasicForagePlant", 0.003),//淤泥根
                            new Critter("BasicSingleHarvestPlantSeed",0.002),//米虱木种子
                            new Critter("PrickleFlowerSeed", 0.006) ,//毛刺花种子
                            new Critter("ColdBreatherSeed",0.005)//冰息萝卜
                        },

                        new List<Critter>//spawnablesInLiquid
                        {

                        },

                        new List<Critter>//spawnablesInAir
                        {
                            new Critter("LightBug",0.0005),//发光虫
                        }//发光虫
                        )
                },

                {
                    "Forest",
                    new Biome(
                        "expansion1::subworlds/forest/med_ForestStart",
                        -1,"Forest",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("Squirrel",0.0001) ,//松鼠
                            new Critter("ForestTree",0.0001) ,//树
                            new Critter("Oxyfern", 0.025) ,//氧齿蕨
                            new Critter("LeafyPlant",0.02 ),//欢乐叶
                            new Critter("ForestForagePlantPlanted",0.07) ,//六角根
                        },//树 氧齿蕨 欢乐叶 六角根 

                        new List<Critter>//spawnablesOnCeil
                        {

                        },

                        new List<Critter>//spawnablesInGround
                        {
                            new Critter("OxyfernSeed",0.0001) ,//氧齿蕨
                            new Critter("LeafyPlantSeed", 0.03),//欢乐叶
                        },

                        new List<Critter>//spawnablesInLiquid
                        {

                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Swamp",
                    new Biome(
                        "expansion1::subworlds/swamp/SwampStart",
                        -1,"Swamp",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("STATERPILLAR",0.0002) ,//蛞蝓
                            new Critter("SwampForagePlantPlanted",0.02) ,//甜菜根
                            new Critter("WineCups",0.02) ,//飞机杯
                        },//蛞蝓 甜菜根 飞机杯 

                        new List<Critter>//spawnablesOnCeil
                        {

                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {

                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },
                {
                    "Barren",
                    new Biome(
                        "subworlds/barren/BarrenGranite",
                        -1,"Barren",
                        new List<Critter>//spawnablesOnFloor
                        {
                        },

                        new List<Critter>//spawnablesOnCeil
                        {

                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {

                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },
                {
                    "Aquatic",
                    new Biome(
                        "expansion1::subworlds/ocean/med_OceanDeep",
                        -1,"Aquatic",
                        new List<Critter>//spawnablesOnFloor
                        {
                        },

                        new List<Critter>//spawnablesOnCeil
                        {

                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                            new Critter("Pacu",0.0001) ,//帕库鱼
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }//发光虫
                        )
                },

                {
                    "Frozen",
                    new Biome(
                        "subworlds/barren/BarrenGranite",
                        42,"Frozen",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("ColdWheat",0.0001) ,//冰霜麦粒 
                            new Critter("ColdBreather",0.01) ,//冰萝卜
                        },

                        new List<Critter>//spawnablesOnCeil
                        {

                        },

                        new List<Critter>//spawnablesInGround
                        {
                            new Critter("ColdWheat",0.0001) ,//冰霜麦粒 
                            new Critter("ColdBreather",0.01) ,//冰萝卜
                        },

                        new List<Critter>//spawnablesInLiquid
                        {

                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Jungle",
                    new Biome(
                        "subworlds/jungle/Jungle",
                        320,"Jungle",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("SwampLily",0.0001) ,//芳香百合
                            new Critter("Drecko",0.0003) ,//毛鳞壁虎
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                            new Critter("SpiceVine",0.0001) ,//火椒藤
                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {

                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Magma",
                    new Biome(
                        "expansion1::subworlds/magma/MagmaSurface",
                        1820,"Magma",
                        new List<Critter>//spawnablesOnFloor
                        {
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Niobium",
                    new Biome(
                        "expansion1::subworlds/magma/MagmaSurface",
                        1820,"Niobium",
                        new List<Critter>//spawnablesOnFloor
                        {
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Marsh",
                    new Biome(
                        "subworlds/marsh/HotMarsh",
                        310,"Marsh",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("BulbPlant",0.01) ,//同伴芽
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                            new Critter("BulbPlantSeed",0.01) ,//同伴芽
                            new Critter("BasicFabricMaterialPlantSeed",0.0001) ,//芦苇
                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                            new Critter("Puft",0.0001) ,//喷浮飞鱼
                        }
                        )
                },

                {
                    "Moo",
                    new Biome(
                        "expansion1::subworlds/moo/MooCore",
                        42,"Moo",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("GasGrass",0.01) ,//释气草
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                            new Critter("Moo",0.0002) ,//海牛
                        }
                        )
                },

                {
                    "Ocean",
                    new Biome(
                        "subworlds/ocean/Ocean",
                        -1,"Ocean",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("SeaLettuce",0.0001) ,//水草
                            new Critter("Crab",0.0001) ,//沙泥蟹
                            new Critter("SaltPlant",0.0003) ,//沙盐藤
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Oil",
                    new Biome(
                        "expansion1::subworlds/oil/OilSparse",
                        340,"Oil",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("EVILFLOWER",0.0001) ,//花
                            new Critter("OilFloaterDecor", 0.0001) ,//浮油生物
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                            new Critter("CactusPlantSeed",0.01) ,//雀跃掌
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Radioactive",
                    new Biome(
                        "expansion1::subworlds/radioactive/med_Radioactive",
                        240,"Radioactive",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("BeeHive",0.002) ,//bee
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                            new Critter("CritterTrapPlantSeed",0.02) ,//动物捕草
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Regolith",
                    new Biome(
                        "expansion1::subworlds/regolith/BarrenDust",
                        -1,"Regolith",
                        new List<Critter>//spawnablesOnFloor
                        {
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                            new Critter("MOLE",0.0002) ,//田鼠
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Rust",
                    new Biome(
                        "expansion1::subworlds/rust/med_Rust",
                        -1,"Rust",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("BeanPlant",0.0001) ,//小吃芽
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },

                {
                    "Wasteland",
                    new Biome(
                        "expansion1::subworlds/wasteland/WastelandWorm",
                        320,"Wasteland",
                        new List<Critter>//spawnablesOnFloor
                        {
                            new Critter("Cylindrica",0.0001) ,//极乐刺
                            new Critter("DivergentBeetle",0.0001) ,//甲虫
                            new Critter("WormPlant",0.0003) ,//虫果
                        },

                        new List<Critter>//spawnablesOnCeil
                        {
                        },

                        new List<Critter>//spawnablesInGround
                        {
                        },

                        new List<Critter>//spawnablesInLiquid
                        {
                        },

                        new List<Critter>//spawnablesInAir
                        {
                        }
                        )
                },
            };

            FileUtility.WriteAll(@"Resources\ClusterData\Boimes", bands);
        }

        //------------------------------------------------------------------------
        /// <summary>
        /// 默认的生态随机生成模式
        /// </summary>
        /// <param name="Data"></param>
        /// <param name="random"></param>
        private static void SwitchPattern(ref BiomeData Data, System.Random random)
        {
            T RandGet<T>(params T[] values)
            {
                if (values == null || values.Length == 0)
                    return default;

                return values[random.Next(0, values.Length)];
            }

            BiomeLocationPattern LocationRand()
            {
                return RandGet(
                    BiomeLocationPattern.Middle,
                    BiomeLocationPattern.Middle,
                    BiomeLocationPattern.Middle,
                    BiomeLocationPattern.Up,
                    BiomeLocationPattern.Left,
                    BiomeLocationPattern.Right,
                    BiomeLocationPattern.Down,
                    BiomeLocationPattern.Down); ;
            }

            BiomeBehaviorPattern BehaviorRand()
            {
                return RandGet(
                    BiomeBehaviorPattern.Circular, 
                    BiomeBehaviorPattern.Circular, 
                    BiomeBehaviorPattern.Circular, 
                    BiomeBehaviorPattern.Bulk, 
                    BiomeBehaviorPattern.Bulk,
                    BiomeBehaviorPattern.LowFull,
                    BiomeBehaviorPattern.Punctate);
            }

            if (Data.BiomeType.IsStartBiome)
            {
                Data.LocationPattern = BiomeLocationPattern.Middle;
                Data.BehaviorPattern = BiomeBehaviorPattern.Circular;
                return;
            }

            switch (Data.BiomeType.Name)
            {
                case "Aquatic":
                    Data.LocationPattern = BiomeLocationPattern.Up;
                    Data.BehaviorPattern = BiomeBehaviorPattern.LowFull;
                    break;

                case "Oil":
                    Data.LocationPattern = BiomeLocationPattern.Down;
                    Data.BehaviorPattern = BehaviorRand();
                    break;
                default:
                    Data.LocationPattern = LocationRand();
                    Data.BehaviorPattern = BehaviorRand();
                    break ;
            }

        }

        /// <summary>
        /// 从默认的生成模式获得一个 Data
        /// </summary>
        /// <param name="biome"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static BiomeData GetDefaultBiomeData(Biome biome,WorldInfo info, System.Random random)
        {
            var res = new BiomeData()
            {
                BiomeType = biome
            };

            SwitchPattern(ref res, random);

            res.BiomeType.ResourceModifier = info.Magnification;

            return res;
        }
    }
}
