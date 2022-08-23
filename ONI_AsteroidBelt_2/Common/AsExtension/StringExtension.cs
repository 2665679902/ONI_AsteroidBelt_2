using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.AsExtension
{
    internal static class StringExtension
    {
        /// <summary>
        /// 删去目标字符串
        /// </summary>
        /// <param name="str">要被删的</param>
        /// <param name="stringToRemove">要删的</param>
        /// <returns>删完的</returns>
        public static string Remove(this string str, string stringToRemove)
        {
            if(str == null || stringToRemove == null || !str.Contains(stringToRemove))
                return str;

            return str.Replace(stringToRemove, "");
        }


    }
}
