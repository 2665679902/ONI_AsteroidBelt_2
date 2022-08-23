using ONI_AsteroidBelt_2.AsData.AssetBundles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static ONI_AsteroidBelt_2.AsData.CurrentCluster.ClusterInfoManager;

namespace ONI_AsteroidBelt_2.AsUI.UGUI.SelectWorldUI.Data
{
    internal class WorldDiscribe
    {
        public static WorldDiscribe GetDefaultWorld(
        string name,
        string discribe,
        int MaxCion = 5, int MinCion = 2,
        double MaxMagnification = 1.5, double MinMagnification = 0.6)
        {
            var res = new WorldDiscribe()
            {
                Name = name,
                Discribe = discribe,
                MaxCion = MaxCion,
                MinCion = MinCion,
                MaxMagnification = MaxMagnification,
                MinMagnification = MinMagnification
            };

            return res;
        }

        public static WorldDiscribe GetDefaultWorld(
            WorldStrings str,
            int MaxCion = 5, int MinCion = 2,
            double MaxMagnification = 1.5, double MinMagnification = 0.6)
        {
            var res = new WorldDiscribe()
            {
                Name = str.Name,
                Discribe = str.DescriptionInSelect,
                MaxCion = MaxCion,
                MinCion = MinCion,
                MaxMagnification = MaxMagnification,
                MinMagnification = MinMagnification
            };

            return res;
        }

        public string Name { get; set; }

        public string Discribe { get; set; }

        public int MaxCion { get; set; }

        public int MinCion { get; set; }

        public double MaxMagnification { get; set; }

        public double MinMagnification { get; set; }

        public Sprite Sprite
        {
            get
            {
                var name = "biomeIcon" + Name;
                var tex = AssetBundleManager.TryGetObject<Texture2D>(name);
                return Sprite.Create(tex, new Rect(0, 0, tex.width, tex.height), Vector2.zero);
            }
        }

    }
}
