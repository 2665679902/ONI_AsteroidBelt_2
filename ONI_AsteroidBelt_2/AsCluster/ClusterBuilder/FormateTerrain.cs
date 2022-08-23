using HarmonyLib;
using ONI_AsteroidBelt_102.AsWorldBuilder.Creator.Acts;
using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe;
using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe;
using ONI_AsteroidBelt_2.AsCluster.WorldDescribe;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsExtension;
using ProcGenGame;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ONI_AsteroidBelt_2.AsData.CurrentCluster.ClusterInfo;

namespace ONI_AsteroidBelt_2.AsCluster.ClusterBuilder
{
    internal class FormateTerrain
    {
        public static Random CreatorRandom { get; set; }

        public struct WorldList
        {
            /// <summary>
            /// 世界的高度
            /// </summary>
            public int Height { get; set; }

            /// <summary>
            /// 世界的宽度
            /// </summary>
            public int Width { get; set; }

            /// <summary>
            /// 世界包含的生态
            /// </summary>
            public List<BiomeData> Biomes { get; set; }

            /// <summary>
            /// 世界里包含的模板
            /// </summary>
            public List<Template> Templates { get; set; }

            /// <summary>
            /// 世界的名字
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 是否有补充条款
            /// </summary>
            public List<string> FixedTraits { get; set; }

            public WorldDescribe.World World { get; set; }
        }

        public static bool CreatWorld(WorldGen gen, ref Sim.Cell[] cells, ref Sim.DiseaseCell[] dc, int baseId, WorldDescribe.World worldData, WorldInfo info)
        {
            Log.Debug("获取世界信息成功");

            var world = new WorldList()
            {
                Height = worldData.Height,
                Width = worldData.Width,
                FixedTraits = worldData.FixedTraits,
                Templates = worldData.Templates,
                Name = worldData.Name,
                Biomes = worldData.Biomes.Every((b) => { return BiomeManager.GetDefaultBiomeData(b, info, CreatorRandom); }),
                World = worldData
            };

            // 把方法里要用的私有属性弄出来
            var worldGen = Traverse.Create(gen);
            var data = gen.data;
            var myRandom = worldGen.Field("myRandom").GetValue<SeededRandom>();// 随机种子用的

            //几个用于表示状态的参数
            var updateProgressFn = worldGen.Field("successCallbackFn").GetValue<WorldGen.OfflineCallbackFunction>();
            var errorCallback = worldGen.Field("errorCallback").GetValue<Action<OfflineWorldGen.ErrorInfo>>();
            var running = worldGen.Field("running");

            Log.Debug("获取世界资源");

            running.SetValue(true);


            // 初始化 noise maps
            Log.Debug("初始化 noise maps");
            updateProgressFn(STRINGS.UI.WORLDGEN.GENERATENOISE.key, 0f, WorldGenProgressStages.Stages.NoiseMapBuilder);
            var noiseMap = NoiseMapper.GenerateNoiseMap(world.Width, world.Height, CreatorRandom);
            updateProgressFn(STRINGS.UI.WORLDGEN.GENERATENOISE.key, 0.9f, WorldGenProgressStages.Stages.NoiseMapBuilder);

            //分配格子
            Log.Debug("分配格子");
            CellsSeparater.SeparateCells(world, CreatorRandom);


            // 设置生物群落
            Log.Debug("Setting biomes");
            BiomeSetter.SetBiomes(data.overworldCells, world);

            // 重写 WorldGen.RenderToMap 函数，该函数调用默认地形和边界生成，放置要素并生成动植物
            cells = new Sim.Cell[Grid.CellCount];
            var bgTemp = new float[Grid.CellCount];
            dc = new Sim.DiseaseCell[Grid.CellCount];

            // 初始化地形
            Log.Debug("Clearing terrain");
            updateProgressFn(STRINGS.UI.WORLDGEN.CLEARINGLEVEL.key, 0f, WorldGenProgressStages.Stages.ClearingLevel);
            TerrainSetter.ClearTerrain(cells, bgTemp, dc);
            updateProgressFn(STRINGS.UI.WORLDGEN.CLEARINGLEVEL.key, 1f, WorldGenProgressStages.Stages.ClearingLevel);

            // 绘制自定义地形
            Log.Debug("Drawing terrain");
            updateProgressFn(STRINGS.UI.WORLDGEN.PROCESSING.key, 0f, WorldGenProgressStages.Stages.Processing);
            TerrainSetter.DrawCustomTerrain(data, cells, bgTemp, dc, noiseMap, world, CreatorRandom);
            updateProgressFn(STRINGS.UI.WORLDGEN.PROCESSING.key, 0.9f, WorldGenProgressStages.Stages.Processing);


            // 绘制出生点 和 模板生态
            var templateSpawnTargets = new List<KeyValuePair<Vector2I, TemplateContainer>>();
            if (world.World is StartWorld startWorld)
            {
                Log.Debug("Generating starting template");
                var startPos = startWorld.Center;
                data.gameSpawnData.baseStartPos = startPos;//设置出生点
                TemplateContainer startingBaseTemplate = startWorld.GetStartingTemplate();

                Log.Debug("Adding starting items");
                var itemPos = new Vector2I(3, 1);
                Log.Debug("获取初始items：" + startWorld.StartingItems.Count());
                foreach (var entry in startWorld.StartingItems) // Add custom defined starting items
                    startingBaseTemplate.pickupables.Add(entry.GetPrefab(itemPos.x, itemPos.y));
                Log.Debug("尝试加入中心模块");
                templateSpawnTargets.Add(new KeyValuePair<Vector2I, TemplateContainer>(startPos, startingBaseTemplate));

            }

            // Tempelate
            Log.Debug("Placing Tempelate");
            TempelateMananger.AddTempelates(templateSpawnTargets, world, CreatorRandom);

            // Draw borders
            Log.Debug("Drawing borders");
            updateProgressFn(STRINGS.UI.WORLDGEN.DRAWWORLDBORDER.key, 0f, WorldGenProgressStages.Stages.DrawWorldBorder);
            ISet<Vector2I> borderCells = WorldBorderManager.DrawWorldBorders(cells, world);
            updateProgressFn(STRINGS.UI.WORLDGEN.DRAWWORLDBORDER.key, 1f, WorldGenProgressStages.Stages.DrawWorldBorder);

            // Settle simulation
            // 这会将单元格写入世界，然后执行两个游戏帧的模拟，然后保存游戏
            Log.Debug("Settling sim");
            running.SetValue(WorldGenSimUtil.DoSettleSim(gen.Settings, ref cells, ref bgTemp, ref dc, updateProgressFn, data, templateSpawnTargets, errorCallback, baseId));

            // Place templates, pretty much just the printing pod
            //把模板放进世界
            Log.Debug("Placing templates");
            var claimedCells = new Dictionary<int, int>();
            foreach (KeyValuePair<Vector2I, TemplateContainer> keyValuePair in templateSpawnTargets)
                data.gameSpawnData.AddTemplate(keyValuePair.Value, keyValuePair.Key, ref claimedCells);

            // 添加植物、生物和物品
            Log.Debug("Adding critters, etc");
            updateProgressFn(STRINGS.UI.WORLDGEN.PLACINGCREATURES.key, 0f, WorldGenProgressStages.Stages.PlacingCreatures);
            CritterPlacer.PlaceSpawnables(cells, data.gameSpawnData.pickupables, world, CreatorRandom);
            updateProgressFn(STRINGS.UI.WORLDGEN.PLACINGCREATURES.key, 100f, WorldGenProgressStages.Stages.PlacingCreatures);

            Log.Debug("end one");

            // Finish and save
            Log.Debug("Saving world");
            gen.SaveWorldGen();
            updateProgressFn(STRINGS.UI.WORLDGEN.COMPLETE.key, 1f, WorldGenProgressStages.Stages.Complete);

            return true;
        }
    }
}
