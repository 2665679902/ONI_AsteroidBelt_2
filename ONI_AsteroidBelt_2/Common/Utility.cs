using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common
{
    internal class Utility
    {
        /// <summary>
        /// 用来产生随机数
        /// </summary>
        public static Random Random { get; private set; } = new Random();

        /// <summary>
        /// 从给定的几个值中随机取一个
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values">给定的值</param>
        /// <returns></returns>
        public static T RandGet<T>(params T[] values)
        {
            if(values == null || values.Length == 0)
                return default;

            return values[Random.Next(0, values.Length)];
        }

        /// <summary>
        /// 序列化一个可序列化的对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="serializable">可序列化的对象</param>
        /// <returns>结果</returns>
        public static string Serialize<T>(T serializable) where T:AsSerializable
        {
            var type = typeof(T);
            var method = type.GetMethod("Serialize", BindingFlags.Instance | BindingFlags.NonPublic);
            if(method != null)
            {
                return method.Invoke(serializable, null) as string;
            }

            Log.Error("Utility Serialize failed");
            return null;

        }

        /// <summary>
        /// 封装了 Newtonsoft.Json 的序列化行为，减少引用，简单生活！
        /// </summary>
        /// <param name="o">要序列化的</param>
        /// <returns>返回字符</returns>
        public static string JSerialize(object o)
        {
            return JsonConvert.SerializeObject(o);
        }

        /// <summary>
        /// 反序列化一个可以反序列化的对象
        /// </summary>
        /// <typeparam name="T">可以序列化的类型</typeparam>
        /// <param name="input">输入字符串</param>
        /// <returns>分序列化结果，失败返回 null</returns>
        public static T Deserialize<T>(string input) where T : AsSerializable
        {
            try
            {
                var type = typeof(T);

                var method = type.GetMethod("Deserialize", BindingFlags.Static | BindingFlags.NonPublic);

                return method.Invoke(null, new object[] { input }) as T;
            }
            catch(Exception e)
            {
                Log.Error("Utility Deserialize failed beacuse: " + e.Message);
                return null;
            }
        }

        /// <summary>
        /// 封装了 Newtonsoft.Json 的反序列化行为，减少引用，简单生活！
        /// </summary>
        /// <typeparam name="T">反序列化的目标类型</typeparam>
        /// <param name="str">反序列化</param>
        /// <returns>反序列化的结果</returns>
        public static T JDeserialize<T>(string str)
        {
            try
            {
                var res = JsonConvert.DeserializeObject<T>(str);
                return res;
            }
            catch (Exception e)
            {
                Log.Fatal($"Utility JDeserialize {str} failed beacuse " + e.Message);
                throw new Exception();
            }
        }
    }
}
