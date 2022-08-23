using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsExtension;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsCluster.BiomeDescribe.BandDescribe
{
    internal class Bands : AsSerializable
    {
        public Bands(List<Band> bands)
        {
            BandList = bands;
        }

        public List<Band> BandList { get; set; }

        protected override string Serialize()
        {
            List<string> BandStringList = BandList.Every(Utility.Serialize);
            return Utility.JSerialize(BandStringList);
        }

        protected static new Bands Deserialize(string input)
        {
            try
            {
                var res = Utility.JDeserialize<List<string>>(input);
                return new Bands(res.Every(Utility.Deserialize<Band>));
                 
            }
            catch (Exception e)
            {
                Log.Error("Band DeSerialize failed because: " + e.Message);
                return null;
            }
        }
    }
}
