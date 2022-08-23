using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.LogAct
{
    internal interface IWriter
    {
        void Write(LogData message);
    }
}
