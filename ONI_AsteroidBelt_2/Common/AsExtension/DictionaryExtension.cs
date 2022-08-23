using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.AsExtension
{
    internal static class DictionaryExtension
    {
        public static Dictionary<TKey, TValue> Clone<TKey, TValue>(this Dictionary<TKey, TValue> dic) where TValue : ICloneable
        {
            return dic.Every((k,v) => { return new KeyValuePair<TKey, TValue>(k,(TValue)v.Clone());});
        }

        public static Dictionary<TKey, List<TValue>> Clone<TKey, TValue>(this Dictionary<TKey, List<TValue>> dic) where TValue : ICloneable
        {
            return dic.Every((k, l) => { 
                return new KeyValuePair<TKey, List<TValue>>
                (k, l.Every((v) => { return (TValue)v.Clone(); })); });
        }

        public static void Every<TKey,TValue>(this Dictionary<TKey, TValue> list, Action<TKey, TValue> act)
        {
            foreach(var item in list)
            {
                act(item.Key,item.Value);
            }
        }

        public static Dictionary<TKeyOut, TValueOut> Every<TKey, TValue, TKeyOut, TValueOut>(this Dictionary<TKey, TValue> list, Func<TKey, TValue,KeyValuePair<TKeyOut,TValueOut>> Func)
        {
            var res = new Dictionary<TKeyOut, TValueOut>();

            foreach (var item in list)
            {
                var pair = Func(item.Key, item.Value);
                res.Add(pair.Key, pair.Value);
            }

            return res;
        }
    }
}
