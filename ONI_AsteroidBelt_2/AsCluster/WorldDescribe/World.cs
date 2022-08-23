using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe;
using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.WorldDescribe
{
    internal class World: AsSerializable,ICloneable
    {
        /// <summary>
        /// 世界的高度
        /// </summary>
        public int Height { get; set; } = 100;

        /// <summary>
        /// 世界的宽度
        /// </summary>
        public int Width { get; set; } = 90;

        /// <summary>
        /// 世界包含的生态
        /// </summary>
        public List<Biome> Biomes { get; set; } = new List<Biome>();

        /// <summary>
        /// 世界里包含的模板
        /// </summary>
        public List<Template> Templates { get; set; } = new List<Template>();

        /// <summary>
        /// 世界的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 是否有补充条款
        /// </summary>
        public List<string> FixedTraits { get; set; } = new List<string>();

        private struct WorldString
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
            public List<string> Biomes { get; set; }

            /// <summary>
            /// 世界里包含的模板
            /// </summary>
            public List<string> Templates { get; set; }

            /// <summary>
            /// 世界的名字
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 是否有补充条款
            /// </summary>
            public List<string> FixedTraits { get; set; }
        }

        protected static new World Deserialize(string input)
        {

            try
            {
                var str = Utility.JDeserialize<WorldString>(input);
                return new World()
                {
                    Height = str.Height,
                    Width = str.Width,
                    Biomes = str.Biomes.Every((s) => { return BiomeManager.BoimeData[s]; }),
                    Templates = str.Templates.Every(Utility.Deserialize<Template>),
                    Name = str.Name,
                    FixedTraits = str.FixedTraits
                };
            }
            catch (Exception e)
            {
                Log.Error("World DeSerialize failed because: " + e.Message);
                return null;
            }

        }

        protected override string Serialize()
        {
            var res = new WorldString()
            {
                Height = Height,
                Width = Width,
                Name = Name,
                Biomes = Biomes.Every((b) => { return b.Name; }),
                Templates = Templates.Every(Utility.Serialize),
                FixedTraits = FixedTraits,
            };
            return Utility.JSerialize(res);
        }

        object ICloneable.Clone()
        {
            return new World() { Name = Name, Biomes = Biomes.Clone(), FixedTraits = FixedTraits.Clone(), Height = Height, Templates = Templates.Clone(), Width = Width };
        }
    }
}
