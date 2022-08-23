using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.BandDescribe;
using ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.BiomeDescribe
{
    internal class Biome : AsSerializable,ICloneable
    {
        /// <summary>
        /// BaseBiome的默认元素分配方案
        /// </summary>
        private static Band[] P_baseAssignment(Band[] bandData)
        {
            int num = 10000;

            double[] doubles = new double[bandData.Count()];

            double total = 0;

            Band[] res = new Band[num];

            for (int i = 0; i < bandData.Count(); i++)
            {
                total += bandData[i].Weight;
                doubles[i] = bandData[i].Weight;
            }

            double rate = num / total;

            int current = 0;

            for (int i = 0; i < doubles.Count(); i++)
            {
                for (int j = 0; j < doubles[i] * rate && current < res.Count(); j++)
                {
                    res[current++] = bandData[i];
                }
            }

            if (current < res.Count())
                for (int i = current; i < res.Count(); i++)
                {
                    res[i] = bandData[bandData.Count()];
                }


            return res;
        }

        /// <summary>
        /// BaseBiome现在持有的元素分配方案
        /// </summary>
        public static Func<Band[], Band[]> BaseAssignment { get; set; } = P_baseAssignment;

        //----------------------------------------------------------------

        public Biome(
            string backgroundSubworld,
            double defaultTemperature,
            string BandsName,
            List<Critter> spawnablesOnFloor,
            List<Critter> spawnablesOnCeil,
            List<Critter> spawnablesInGround,
            List<Critter> spawnablesInLiquid,
            List<Critter> spawnablesInAir,
            string name = null
            )
        {
            BackgroundSubworld = backgroundSubworld;
            DefaultTemperature = defaultTemperature;
            Name = name ?? BandsName;

            this.BandsName = BandsName;
            if (BiomeManager.BandData.ContainsKey(BandsName))
                this.bands = BiomeManager.BandData[BandsName].ToArray();
            else
                Log.Fatal($"Boime - BandsName lost : {BandsName}");


            SpawnablesOnFloor = spawnablesOnFloor ?? new List<Critter>();
            SpawnablesOnCeil = spawnablesOnCeil ?? new List<Critter>();
            SpawnablesInGround = spawnablesInGround ?? new List<Critter>();
            SpawnablesInLiquid = spawnablesInLiquid ?? new List<Critter>();
            SpawnablesInAir = spawnablesInAir ?? new List<Critter>();
            ResourceModifier = 1;
            //获取基础分配方案
            currentAssignment = BaseAssignment;
            //生成分配结果
            bandResult = Assignment.Invoke(bands);
        }

        public bool IsStartBiome{ get; set; } = false;

        /// <summary>
        /// 生态的名字
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// 世界生成时介入的质量变化
        /// </summary>
        public double ResourceModifier { get; set; }

        /// <summary>
        /// 以什么世界作为背景
        /// </summary>
        public string BackgroundSubworld { get; set; }

        /// <summary>
        /// 默认温度
        /// </summary>
        public double DefaultTemperature { get; set; }

        public string BandsName { get; private set; }

        /// <summary>
        /// 当前的分配方案
        /// </summary>
        private Func<Band[], Band[]> currentAssignment;

        /// <summary>
        /// 设定新的分配方案会重置分配的结果（但是会调用.Equals函数，如果和上一个方案相等就会取消调用）
        /// </summary>
        public Func<Band[], Band[]> Assignment
        {
            get { return currentAssignment; }
            set
            {
                if (!currentAssignment.Equals(value))
                {
                    currentAssignment = value;
                    bandResult = value(bands);
                }
            }
        }

        /// <summary>
        /// 生态包含的物质组
        /// </summary>
        private readonly Band[] bands;

        /// <summary>
        /// 记录分配物质的分配结果，由Assignment方案产生
        /// </summary>
        private Band[] bandResult;

        /// <summary>
        /// 可以刷在地上的东西
        /// </summary>
        public List<Critter> SpawnablesOnFloor { get; set; }

        /// <summary>
        /// 可以刷在天花板上的东西
        /// </summary>
        public List<Critter> SpawnablesOnCeil { get; set; }

        /// <summary>
        /// 可以刷在地里的东西
        /// </summary>
        public List<Critter> SpawnablesInGround { get; set; }

        /// <summary>
        /// 可以刷在水里的东西
        /// </summary>
        public List<Critter> SpawnablesInLiquid { get; set; }

        /// <summary>
        /// 可以刷在空中的东西
        /// </summary>
        public List<Critter> SpawnablesInAir { get; set; }

        /// <summary>
        /// 获取一个元素，由一个【0，1】随机值在元素列表的条带中取出
        /// </summary>
        /// <param name="f">随机值</param>
        /// <returns>元素</returns>
        public Band GetBand(double f)
        {
            var res = (int)(f * 10000) < 10000 ? (int)(f * 10000) : 9999;
            return bandResult[res];
        }

        /// <summary>
        /// 获得元素的物理属性
        /// </summary>
        /// <param name="band">元素</param>
        /// <param name="modifier">倍率</param>
        /// <returns>物理属性</returns>
        public Sim.PhysicsData GetPhysicsData(Band band, double modifier = 1)
        {
            //如果band.temperature < 0(没有设定过温度) 并且 默认温度 > 0(设定过温度) 就返回默认温度，否则返回设定温度
            double temperature = band.Temperature < 0 && DefaultTemperature > 0 ? DefaultTemperature : band.Temperature;
            return GetPhysicsData(band.GetElement(), modifier * band.Density, temperature * AsClusterConfig.TempretureModifier);
        }

        /// <summary>
        /// 获得元素的物理属性
        /// </summary>
        /// <param name="element">元素</param>
        /// <param name="modifier">倍率</param>
        /// <param name="temperature">温度</param>
        /// <returns>物理属性</returns>
        private Sim.PhysicsData GetPhysicsData(Element element, double modifier = 1, double temperature = -1)
        {
            Sim.PhysicsData defaultData = element.defaultValues;
            return new Sim.PhysicsData()
            {
                mass = (float)(ResourceModifier * defaultData.mass * modifier * (element.IsSolid ? AsClusterConfig.ResourceModifier : 1)),
                temperature = (float)(temperature == -1 ? defaultData.temperature : temperature),
                pressure = defaultData.pressure
            };
        }

        private struct BoimeString
        {
            /// <summary>
            /// 生态的名字
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 世界生成时介入的质量变化
            /// </summary>
            public double ResourceModifier { get; set; }

            /// <summary>
            /// 以什么世界作为背景
            /// </summary>
            public string BackgroundSubworld { get; set; }

            /// <summary>
            /// 默认温度
            /// </summary>
            public double DefaultTemperature { get; set; }

            /// <summary>
            /// 生态的名字
            /// </summary>
            public string BandsName { get; set; }

            /// <summary>
            /// 可以刷在地上的东西
            /// </summary>
            public List<string> SpawnablesOnFloor { get; set; }

            /// <summary>
            /// 可以刷在天花板上的东西
            /// </summary>
            public List<string> SpawnablesOnCeil { get; set; }

            /// <summary>
            /// 可以刷在地里的东西
            /// </summary>
            public List<string> SpawnablesInGround { get; set; }

            /// <summary>
            /// 可以刷在水里的东西
            /// </summary>
            public List<string> SpawnablesInLiquid { get; set; }

            /// <summary>
            /// 可以刷在空中的东西
            /// </summary>
            public List<string> SpawnablesInAir { get; set; }
        }

        protected static new Biome Deserialize(string input)
        {
            List<Critter> Trans(List<string> strings)
            {
                var list = new List<Critter>();
                foreach (var item in strings)
                {
                    var c = Utility.Deserialize<Critter>(item);

                    if (c != null)
                        list.Add(c);
                    else
                        Log.Error($"Boime DeSerialize failed {item}");
                }
                return list;
            }

            try
            {
                var res = Utility.JDeserialize<BoimeString>(input);
                return new Biome(
                    res.BackgroundSubworld,
                    res.DefaultTemperature,
                    res.BandsName,
                    Trans(res.SpawnablesOnFloor),
                    Trans(res.SpawnablesOnCeil),
                    Trans(res.SpawnablesInGround),
                    Trans(res.SpawnablesInLiquid),
                    Trans(res.SpawnablesInAir),
                    res.Name);
            }
            catch (Exception e)
            {
                Log.Error("Boime DeSerialize failed because: " + e.Message);
                return null;
            }

        }

        protected override string Serialize()
        {
            var stTarget = new BoimeString()
            {
                Name = Name,
                ResourceModifier = ResourceModifier,
                BackgroundSubworld = BackgroundSubworld,
                DefaultTemperature = DefaultTemperature,
                BandsName = BandsName,
                SpawnablesOnFloor = new List<string>(),
                SpawnablesOnCeil = new List<string>(),
                SpawnablesInGround = new List<string>(),
                SpawnablesInLiquid = new List<string>(),
                SpawnablesInAir = new List<string>(),
            };

            foreach (var c in SpawnablesOnFloor)
                stTarget.SpawnablesOnFloor.Add(Utility.Serialize(c));

            foreach (var c in SpawnablesOnCeil)
                stTarget.SpawnablesOnCeil.Add(Utility.Serialize(c));

            foreach (var c in SpawnablesInGround)
                stTarget.SpawnablesInGround.Add(Utility.Serialize(c));

            foreach (var c in SpawnablesInLiquid)
                stTarget.SpawnablesInLiquid.Add(Utility.Serialize(c));

            foreach (var c in SpawnablesInAir)
                stTarget.SpawnablesInAir.Add(Utility.Serialize(c));

            return Utility.JSerialize(stTarget);
        }

        object ICloneable.Clone()
        {
            return new Biome(BackgroundSubworld, DefaultTemperature, BandsName, SpawnablesOnFloor.Clone(), SpawnablesOnCeil.Clone(), SpawnablesInGround.Clone(), SpawnablesInLiquid.Clone(), SpawnablesInAir.Clone(), Name);
        }
    }
}
