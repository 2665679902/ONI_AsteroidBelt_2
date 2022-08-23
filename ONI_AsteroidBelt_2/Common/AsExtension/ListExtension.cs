using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.AsExtension
{
    internal static class ListExtension
    {

        public static List<T> Clone<T>(this IList<T> list)where T : ICloneable
        {
            return list.Every((i) => { return (T)i.Clone(); });
        }

        /// <summary>
        /// 对集合里的每一项进行处理并返回一个新的集合
        /// </summary>
        /// <typeparam name="TTarget">目标类型</typeparam>
        /// <typeparam name="TSourse">原类型</typeparam>
        /// <param name="list">原集合</param>
        /// <param name="func">处理方法</param>
        /// <returns>目标集合</returns>
        public static List<TTarget> Every<TTarget,TSourse>(this IList<TSourse> list, Func<TSourse,TTarget> func)
        {
            var res = new List<TTarget>();
            foreach(TSourse sourse in list)
            {
                res.Add(func(sourse));
            }
            return res;
        }

        /// <summary>
        /// 对集合里的每一项进行处理
        /// </summary>
        /// <typeparam name="T">集合类型</typeparam>
        /// <param name="list">原集合</param>
        /// <param name="act">处理方法</param>
        public static void Every<T>(this IList<T> list, Action<T> act)
        {
            foreach (T i in list)
            {
                act(i);
            }
        }

        /// <summary>
        /// 在指定的集合中找到目标，在其后增加一项
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">原集合</param>
        /// <param name="target">目标</param>
        /// <param name="someThingAdd">增加项</param>
        /// <returns>如果成功找到目标并添加返回真， 否则返回假</returns>
        /// <exception cref="ArgumentNullException">输入的参数都不能为 null</exception>
        public static bool AddAfter<T>(this IList<T> list, T target, T someThingAdd)
        {
            if (list == null || target == null || someThingAdd == null)
                throw new ArgumentNullException("AddAfter: SomeThing null");

            if(list.Count == 0 || !list.Contains(target))
                return false;

            int index = list.IndexOf(target);

            if (index < list.Count - 1)
                list.Insert(index + 1, someThingAdd);
            else
                list.Add(someThingAdd);
            return true;
        }
    }
}
