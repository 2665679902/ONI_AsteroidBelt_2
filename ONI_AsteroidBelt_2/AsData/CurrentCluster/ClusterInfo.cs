using ONI_AsteroidBelt_2.AsData.Strings;
using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsData.CurrentCluster
{
    internal class ClusterInfo: AsSerializable
    {
        [Serializable]
        private struct ClusterInfoString
        {
            public List<string> Names { get; set; }
            public List<double> Magnification { get; set; }

            public int coin;
        }

        protected static new ClusterInfo Deserialize(string input)
        {
            var str = Utility.JDeserialize<ClusterInfoString>(input);

            var res = new ClusterInfo
            {
                StartWorld = new WorldInfo { Name = str.Names[0], Magnification = str.Magnification[0] },
                InnerWorld_0 = new WorldInfo { Name = str.Names[1], Magnification = str.Magnification[1] },
                InnerWorld_1 = new WorldInfo { Name = str.Names[2], Magnification = str.Magnification[2] },
                InnerWorld_2 = new WorldInfo { Name = str.Names[3], Magnification = str.Magnification[3] },
                OuterWorldList = new List<WorldInfo>()
            };
            
            for (var i = 4; i < str.Names.Count; i++)
            {
                res.OuterWorldList.Add(new WorldInfo() { Name = str.Names[i], Magnification = str.Magnification[i] });
            }
            res.CoinLast = str.coin;

            return res;
        }

        protected override string Serialize()
        {
            var res = new ClusterInfoString() { Names = new List<string>(), Magnification = new List<double>() };

            foreach (var info in TotalWorlds)
            {
                res.Names.Add(info.Name);

                res.Magnification.Add(info.Magnification);
            }
            res.coin = CoinLast;

            return Utility.JSerialize(res);
        }

        public struct WorldInfo
        {
            public string Name { get; set; }

            public double Magnification { get; set; }
        }

        public string RichText(bool detial)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"<color=aqua>{AsStrings.UI.UGUI.SelectWorldUI.StartWorld}: </color>{ClusterInfoManager.GetString(StartWorld.Name).NameShow}");
            stringBuilder.AppendLine($"<color=lime>{AsStrings.UI.UGUI.SelectWorldUI.InnerWorld} 1: </color>{ClusterInfoManager.GetString(InnerWorld_0.Name).NameShow}");
            stringBuilder.AppendLine($"<color=lime>{AsStrings.UI.UGUI.SelectWorldUI.InnerWorld} 2: </color>{ClusterInfoManager.GetString(InnerWorld_1.Name).NameShow}");
            stringBuilder.AppendLine($"<color=lime>{AsStrings.UI.UGUI.SelectWorldUI.InnerWorld} 3: </color>{ClusterInfoManager.GetString(InnerWorld_2.Name).NameShow}");
            stringBuilder.AppendLine($"");

            if (detial)
            {
                stringBuilder.AppendLine($"{AsStrings.UI.UGUI.SelectWorldUI.OuterWorlds}: ");
                foreach (var world in OuterWorldList)
                {
                    stringBuilder.Append($"<size=25> {ClusterInfoManager.GetString(world.Name).NameShow}</size>");
                }
                stringBuilder.AppendLine($"");
                stringBuilder.AppendLine($"{AsStrings.UI.UGUI.SelectWorldUI.CoinLast}:<color=orange> {CoinLast} </color>\n<size=25>({AsStrings.UI.UGUI.SelectWorldUI.CoinTip})</size>");

            }
            else
            {
                stringBuilder.AppendLine($"{AsStrings.UI.UGUI.SelectWorldUI.CoinLast}:<color=orange> {CoinLast} </color>");
            }
            return stringBuilder.ToString();
        }

        public List<WorldInfo> TotalWorlds
        {
            get
            {
                var list = new List<WorldInfo>();

                if (StartWorld.Name != null)
                {
                    list.Add(StartWorld);
                }
                if (InnerWorld_0.Name != null)
                {
                    list.Add(InnerWorld_0);
                }
                if (InnerWorld_1.Name != null)
                {
                    list.Add(InnerWorld_1);
                }
                if (InnerWorld_2.Name != null)
                {
                    list.Add(InnerWorld_2);
                }
                if (OuterWorldList != null)
                {
                    foreach (var item in OuterWorldList)
                    {
                        list.Add(item);
                    }
                }

                return list;
            }
        }

        public WorldInfo StartWorld { get; set; }

        public WorldInfo InnerWorld_0 { get; set; }

        public WorldInfo InnerWorld_1 { get; set; }

        public WorldInfo InnerWorld_2 { get; set; }

        public List<WorldInfo> OuterWorldList { get; set; }

        public int CoinLast { get; set; }
    }
}
