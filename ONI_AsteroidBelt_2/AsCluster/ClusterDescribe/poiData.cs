using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.ClusterDescribe
{
    internal class poiData
    {
        public List<string> Data { get; set; }

        public int NumToSpawn { get; set; }

        public bool? AvoidClumping { get; set; }

        public bool? CanSpawnDuplicates { get; set; }

        public int AllowedRingsMin { get; set; }

        public int AllowedRingsMax { get; set; }
    }
}
