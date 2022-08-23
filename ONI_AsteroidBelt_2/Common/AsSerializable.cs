using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common
{
    public class AsSerializable
    {
        protected static AsSerializable Deserialize(string input)
        {
            return new AsSerializable();
        }

        protected virtual string Serialize()
        {
            return "Base Serialize";
        }

    }
}
