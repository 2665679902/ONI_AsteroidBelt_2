using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateClasses;

namespace ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe
{
    internal class Critter: AsSerializable,ICloneable
    {
        public Critter(string name, double possibility)
        {
            Name = name;
            Possibility = possibility;
        }

        public string Name { get; set; }

        public double Possibility { get; set; }

        public Prefab GetPrefab(int x, int y)
        {
            return new Prefab(Name, Prefab.Type.Pickupable, x, y, 0);
        }

        protected static new Critter Deserialize(string input)
        {
            try
            {
                return Utility.JDeserialize<Critter>(input);
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
            return new Critter(Name, Possibility);
        }
    }
}
