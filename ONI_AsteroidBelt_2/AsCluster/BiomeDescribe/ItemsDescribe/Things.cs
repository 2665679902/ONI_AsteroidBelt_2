using ONI_AsteroidBelt_2.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateClasses;

namespace ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.ItemsDescribe
{
    internal class Things : AsSerializable
    {
        public Things(string name, int amount)
        {
            Name = name;
            Amount = amount;
        }

        public string Name { get; set; } = "";

        public int Amount { get; set; } = 1;

        public Prefab GetPrefab(int x, int y)
        {
            return new Prefab(Name, Prefab.Type.Pickupable, x, y, 0, _units: Amount);
        }

        protected static new Things Deserialize(string input)
        {
            try
            {
                return Utility.JDeserialize<Things>(input);
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
    }
}
