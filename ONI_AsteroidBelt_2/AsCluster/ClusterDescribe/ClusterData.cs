using ONI_AsteroidBelt_2.AsCluster.WorldDescribe;
using ONI_AsteroidBelt_2.AsData.CurrentCluster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ONI_AsteroidBelt_2.AsData.CurrentCluster.ClusterInfo;

namespace ONI_AsteroidBelt_2.AsCluster.ClusterDescribe
{
    internal class ClusterData
    {
        public string Name { get; set; }

        public string CoordinatePrefix { get; set; }

        public int MenuOrder { get; set; }

        public int ClusterCategory { get; set; }

        public int Difficulty { get; set; }

        public int StartWorldIndex { get; set; }

        public int NumRings { get; set; }

        public KeyValuePair<WorldData,WorldInfo> StartWorld { get; set; }

        public List<KeyValuePair<WorldData, WorldInfo>> InnerWorlds { get; set; }

        public List<KeyValuePair<WorldData, WorldInfo>> OuterWorlds { get; set; }

        public List<poiData> PoiPlacements { get; set; }
    }
}
