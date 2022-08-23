using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe
{
    internal class Template : AsSerializable,ICloneable
    {
        public string Name { get; set; } = "";

        public int Min { get; set; } = 1;

        public int Max { get; set; } = 1;

        public override string ToString()
        {
            return $"Template Name {Name}, Min {Min}, Max {Max}";
        }

        public virtual TemplateContainer GetTemplate()
        {
            return TemplateCache.GetTemplate(Name);
        }

        protected static new Template Deserialize(string input)
        {
            try
            {
                return Utility.JDeserialize<Template>(input);
            }
            catch (Exception e)
            {
                Log.Error("Band DeSerialize failed because: " + e.Message);
                return null;
            }
        }

        protected override string Serialize()
        {
            return Utility.JSerialize(this);
        }

        object ICloneable.Clone()
        {
            return new Template()
            {
                Name = Name,
                Min = Min,
                Max = Max,
            };
        }
    }
}
