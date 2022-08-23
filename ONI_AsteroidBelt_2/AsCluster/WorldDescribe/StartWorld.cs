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
    internal class StartWorld: World, ICloneable
    {
        public StartWorld()
        {

        }

        public StartWorld(World world)
        {
            Width = world.Width;
            Height = world.Height;
            Biomes = world.Biomes;
            Templates = world.Templates;
            Name = world.Name;
            FixedTraits = world.FixedTraits;
        }

        /// <summary>
        /// 要在开局的时候直接刷在地上的物品 -> 物品名字：数量
        /// </summary>
        public List<Things> StartingItems { get; set; } = new List<Things>();

        //开始世界的中心点
        public Vector2I Center { get; set; } = new Vector2I(0, 0);

        //开始世界的模型
        public string StartingTemplate { private get; set; }

        //获得模型
        public TemplateContainer GetStartingTemplate() { return TemplateCache.GetTemplate(StartingTemplate); }

        private struct StartWorldString
        {
            public List<string> StartingItems;

            public string StartingTemplate;

            public string baseString;
        }

        protected static new StartWorld Deserialize(string input)
        {

            try
            {
                var str = Utility.JDeserialize<StartWorldString>(input);

                var world = Utility.Deserialize<World>(str.baseString);

                return new StartWorld(world)
                {
                    StartingItems = str.StartingItems.Every(Utility.Deserialize<Things>),
                    StartingTemplate = str.StartingTemplate,
                };
            }
            catch (Exception e)
            {
                Log.Error("StartWorld DeSerialize failed because: " + e.Message);
                return null;
            }

        }

        protected override string Serialize()
        {
            var res = new StartWorldString()
            {
                StartingItems = StartingItems.Every(Utility.Serialize),
                StartingTemplate = StartingTemplate,
                baseString = base.Serialize()
            };
            return Utility.JSerialize(res);
        }

        object ICloneable.Clone()
        {
            var world = new World() { Name = Name, Biomes = Biomes, FixedTraits = FixedTraits, Height = Height, Templates = Templates, Width = Width };
            return new StartWorld(world)
            {
                StartingItems = StartingItems,
                Center = Center,
                StartingTemplate = StartingTemplate
            };
        }
    }
}
