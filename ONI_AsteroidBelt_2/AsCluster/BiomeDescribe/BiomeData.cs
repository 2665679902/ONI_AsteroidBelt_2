using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.BiomePattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.BiomeDescribe
{
    internal class BiomeData
    {
        /// <summary>
        /// 描述的生态对象
        /// </summary>
        public Biome BiomeType { get; set; }

        /// <summary>
        /// 包含该生态的格子
        /// </summary>
        public HashSet<Vector2I> Cells { get; set; } = new HashSet<Vector2I>();

        /// <summary>
        /// 生态的构造偏好
        /// </summary>
        public BiomeBehaviorPattern BehaviorPattern { get; set; }

        /// <summary>
        /// 生态的位置偏好
        /// </summary>
        public BiomeLocationPattern LocationPattern { get; set; }
    }
}
