using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.BandDescribe
{
    internal class Band : AsSerializable,ICloneable
    {
        public enum DiseaseID { NONE, FoodGerms, SlimeGerms, PollenGerms, ZombieSpores, RadiationPoisoning };

        /// <summary>
        /// 元素质量比重
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 元素的ID
        /// </summary>
        public SimHashes ElementId { get; set; }

        /// <summary>
        /// 默认的温度
        /// </summary>
        public double Temperature { get; set; }

        /// <summary>
        /// 密度比例：和标准密度的比例
        /// </summary>
        public double Density { get; set; }

        /// <summary>
        /// 在这种生态单位里的病毒
        /// </summary>
        private readonly DiseaseID disease;

        public Band(double weight, SimHashes elementId, double temperature = -1, double density = 1, DiseaseID disease = DiseaseID.NONE)
        {
            Weight = weight;
            ElementId = elementId;
            Temperature = temperature;
            Density = density;
            this.disease = disease;
        }

        public byte Disease
        {
            get
            {
                switch (disease)
                {
                    case DiseaseID.NONE: return byte.MaxValue;
                    case DiseaseID.SlimeGerms: return Db.Get().Diseases.GetIndex(Db.Get().Diseases.SlimeGerms.id);
                    case DiseaseID.PollenGerms: return Db.Get().Diseases.GetIndex(Db.Get().Diseases.FoodGerms.id);
                    case DiseaseID.ZombieSpores: return Db.Get().Diseases.GetIndex(Db.Get().Diseases.ZombieSpores.id);
                    case DiseaseID.RadiationPoisoning: return Db.Get().Diseases.GetIndex(Db.Get().Diseases.RadiationPoisoning.id);
                    default: return byte.MaxValue;
                }

            }
        }

        /// <summary>
        /// 获得所记录的元素
        /// </summary>
        /// <returns></returns>
        public Element GetElement() { return ElementLoader.FindElementByHash(ElementId); }

        private struct BandString
        {
            /// <summary>
            /// 元素质量比重
            /// </summary>
            public double Weight { get; set; }

            /// <summary>
            /// 元素的ID
            /// </summary>
            public string ElementId { get; set; }

            /// <summary>
            /// 默认的温度
            /// </summary>
            public double Temperature { get; set; }

            /// <summary>
            /// 密度比例：和标准密度的比例
            /// </summary>
            public double Density { get; set; }

            /// <summary>
            /// 在这种生态单位里的病毒
            /// </summary>
            public string disease;
        }

        protected override string Serialize()
        {
            BandString bandString = new BandString
            {
                Weight = Weight,
                ElementId = ElementId.ToString(),
                Temperature = Temperature,
                Density = Density,
                disease = disease.ToString()
            };
            return Utility.JSerialize(bandString);
        }

        protected static new Band Deserialize(string input)
        {
            try
            {
                var res = Utility.JDeserialize<BandString>(input);
                SimHashes eid = (SimHashes)Enum.Parse(typeof(SimHashes), res.ElementId);
                DiseaseID dis = (DiseaseID)Enum.Parse(typeof(DiseaseID), res.disease);
                return new Band(res.Weight, eid, res.Temperature, res.Density, dis);
                 
            }
            catch (Exception e)
            {
                Log.Error("Band DeSerialize failed because: " + e.Message);
                return null;
            }
        }

        object ICloneable.Clone()
        {
            return new Band(Weight, ElementId, Temperature, Density, disease);
        }
    }
}
