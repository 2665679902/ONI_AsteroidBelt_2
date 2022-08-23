using ONI_AsteroidBelt_2.AsData.Strings;
using ONI_AsteroidBelt_2.AsUI.UGUI.SelectWorldUI;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsData.CurrentCluster
{
    internal class ClusterInfoManager
    {
        private static ClusterInfo _currentCluster;

        public static ClusterInfo CurrentCluster
        {
            get {
                if (_currentCluster != null)
                {
                    return _currentCluster;
                }

                EventCentre.Trigger(EventInventory.RefreshUIData, null);

                if (_currentCluster != null)
                {
                    return _currentCluster;
                }

                Log.Infor("CurrentData 信息丢失！尝试返回默认信息 ");

                return new ClusterInfo()
                {
                    StartWorld = new ClusterInfo.WorldInfo() { Name = "Sandstone", Magnification = 1 },
                    InnerWorld_0 = new ClusterInfo.WorldInfo() { Name = "Forest", Magnification = 1 },
                    InnerWorld_1 = new ClusterInfo.WorldInfo() { Name = "Swamp", Magnification = 1 },
                    InnerWorld_2 = new ClusterInfo.WorldInfo() { Name = "Jungle", Magnification = 1 },
                    OuterWorldList = new List<ClusterInfo.WorldInfo>()
                    {
                        new ClusterInfo.WorldInfo() { Name = "Aquatic",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Frozen",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Magma",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Marsh",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Moo",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Ocean",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Oil",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Radioactive",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Regolith",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Rust",Magnification =1 },
                        new ClusterInfo.WorldInfo() { Name = "Wasteland",Magnification =1 }
                    },
                    CoinLast = 10
                };
            }

            set => _currentCluster = value;
        }

        //-------------------------------------------

        public struct WorldStrings
        {
            public string Name { get; set; }

            public string NameShow { get; set; }

            public string DescriptionShow { get; set; }

            public string DescriptionInSelect { get; set; }

        }

        /// <summary>
        /// 根据 WorldInfo 获取 String 信息
        /// </summary>
        /// <param name="infos"></param>
        /// <returns></returns>
        public static List<WorldStrings> GetString(IEnumerable<ClusterInfo.WorldInfo> infos)
        {
            var res = new List<WorldStrings>();
            foreach (var info in infos)
                res.Add(GetString(info));
            return res;
        }

        /// <summary>
        /// 根据 WorldInfo 获取 String 信息
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public static WorldStrings GetString(ClusterInfo.WorldInfo info)
        {
            return GetString(info.Name);
        }

        /// <summary>
        /// 根据名字获取翻译
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static WorldStrings GetString(string name)
        {
            foreach (var worldst in Worlds)
            {
                if (worldst.Name == name)
                    return worldst;
            }
            Log.Error($"AsWorldData_Utils fail to get the string of {name}");

            return Worlds[0];
        }

        /// <summary>
        /// 所有的世界文字模板
        /// </summary>
        public static List<WorldStrings> Worlds { get => BuildWorldStrings(); }

        /// <summary>
        /// 可以作为开始世界的文字模板
        /// </summary>
        public static List<WorldStrings> StartWorlds { get => BuildStartWorldStrings(); }

        private static List<WorldStrings> BuildWorldStrings()
        {
            var res = new List<WorldStrings>()
            {
                new WorldStrings()
                {
                    Name = "Sandstone",
                    NameShow = AsStrings.AsWorldString.SandstoneString.Name,
                    DescriptionShow = AsStrings.AsWorldString.SandstoneString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.SandstoneString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Forest",
                    NameShow = AsStrings.AsWorldString.ForestString.Name,
                    DescriptionShow = AsStrings.AsWorldString.ForestString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.ForestString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Swamp",
                    NameShow = AsStrings.AsWorldString.SwampString.Name,
                    DescriptionShow = AsStrings.AsWorldString.SwampString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.SwampString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Barren",
                    NameShow = AsStrings.AsWorldString.BarrenString.Name,
                    DescriptionShow = AsStrings.AsWorldString.BarrenString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.BarrenString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Aquatic",
                    NameShow = AsStrings.AsWorldString.AquaticString.Name,
                    DescriptionShow = AsStrings.AsWorldString.AquaticString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.AquaticString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Frozen",
                    NameShow = AsStrings.AsWorldString.FrozenString.Name,
                    DescriptionShow = AsStrings.AsWorldString.FrozenString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.FrozenString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Jungle",
                    NameShow = AsStrings.AsWorldString.JungleString.Name,
                    DescriptionShow = AsStrings.AsWorldString.JungleString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.JungleString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Magma",
                    NameShow = AsStrings.AsWorldString.MagmaString.Name,
                    DescriptionShow = AsStrings.AsWorldString.MagmaString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.MagmaString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Marsh",
                    NameShow = AsStrings.AsWorldString.MarshString.Name,
                    DescriptionShow = AsStrings.AsWorldString.MarshString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.MarshString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Moo",
                    NameShow = AsStrings.AsWorldString.MooString.Name,
                    DescriptionShow = AsStrings.AsWorldString.MooString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.MooString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Ocean",
                    NameShow = AsStrings.AsWorldString.OceanString.Name,
                    DescriptionShow = AsStrings.AsWorldString.OceanString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.OceanString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Oil",
                    NameShow = AsStrings.AsWorldString.OilString.Name,
                    DescriptionShow = AsStrings.AsWorldString.OilString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.OilString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Radioactive",
                    NameShow = AsStrings.AsWorldString.RadioactiveString.Name,
                    DescriptionShow = AsStrings.AsWorldString.RadioactiveString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.RadioactiveString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Regolith",
                    NameShow = AsStrings.AsWorldString.RegolithString.Name,
                    DescriptionShow = AsStrings.AsWorldString.RegolithString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.RegolithString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Rust",
                    NameShow = AsStrings.AsWorldString.RustString.Name,
                    DescriptionShow = AsStrings.AsWorldString.RustString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.RegolithString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Wasteland",
                    NameShow = AsStrings.AsWorldString.WastelandString.Name,
                    DescriptionShow = AsStrings.AsWorldString.WastelandString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.WastelandString.SelectDiscribe,
                },
            };
            return res;
        }

        private static List<WorldStrings> BuildStartWorldStrings()
        {
            List<WorldStrings> res = new List<WorldStrings>()
            {
                new WorldStrings()
                {
                    Name = "Sandstone",
                    NameShow = AsStrings.AsWorldString.SandstoneString.Name,
                    DescriptionShow = AsStrings.AsWorldString.SandstoneString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.SandstoneString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Forest",
                    NameShow = AsStrings.AsWorldString.ForestString.Name,
                    DescriptionShow = AsStrings.AsWorldString.ForestString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.ForestString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Frozen",
                    NameShow = AsStrings.AsWorldString.FrozenString.Name,
                    DescriptionShow = AsStrings.AsWorldString.FrozenString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.FrozenString.SelectDiscribe,
                },
                new WorldStrings()
                {
                    Name = "Marsh",
                    NameShow = AsStrings.AsWorldString.MarshString.Name,
                    DescriptionShow = AsStrings.AsWorldString.MarshString.Discribe,
                    DescriptionInSelect = AsStrings.AsWorldString.MarshString.SelectDiscribe,
                }
            };

            return res;
        }
    }
    
}
