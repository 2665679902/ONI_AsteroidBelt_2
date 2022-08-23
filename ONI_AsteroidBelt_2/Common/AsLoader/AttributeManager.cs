using ONI_AsteroidBelt_2.Common.AsAttributes;
using ONI_AsteroidBelt_2.Common.AsExtension;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.AsLoader
{
    internal static class AttributeManager
    {
        private static List<MethodInfo> EarlyList;

        private static List<MethodInfo> LateList;

        /// <summary>
        /// 读取标签
        /// </summary>
        private static void ManageLoadList()
        {

            Dictionary<Type, LoadAttribute> loadActs = new Dictionary<Type, LoadAttribute>();

            LoadAttribute TryGetLoadAttribute(Type type)
            {
                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    var attri = method.GetCustomAttribute<LoadAttribute>();
                    if (attri != null)
                        return attri;
                }
                return null;
            }

            MethodInfo TryGetMethods(Type type)
            {
                foreach (var method in type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                {
                    var attri = method.GetCustomAttribute<LoadAttribute>();
                    if (attri != null)
                        return method;
                }
                return null;
            }

            bool IsLoop(LoadAttribute attribute, LoadAttribute last = null)
            {
                if(attribute == last)
                    return true;
                if((last ?? attribute).AfterType == null ||!loadActs.ContainsKey((last ?? attribute).AfterType))
                    return false;

                last = TryGetLoadAttribute((last ?? attribute).AfterType);

                return IsLoop(attribute, last);

            }

            //取出程序集里所有的有Load标签的类

            var AsAssembly = Assembly.GetExecutingAssembly();

            foreach (var cla in AsAssembly.GetTypes())
            {
                var attri = TryGetLoadAttribute(cla);
                if (attri != null)
                    loadActs.Add(cla, attri);
            }

            //为他们排序

            var earlyList = new List<Type>();

            var lateList = new List<Type>();

            var UnKnownDic = new Dictionary<Type, LoadAttribute>();

            foreach (var pair in loadActs)
            {
                var afterType = pair.Value.AfterType;

                if (afterType != null && loadActs.ContainsKey(afterType))
                    UnKnownDic.Add(pair.Key, pair.Value);

                else if (pair.Value.Opportunity == LoadAttribute.LoadOpportunity.early)
                    earlyList.Add(pair.Key);
                else if (pair.Value.Opportunity == LoadAttribute.LoadOpportunity.late)
                    lateList.Add(pair.Key);
            }

            while (UnKnownDic.Any())
            {
                var current = new Dictionary<Type, LoadAttribute>(UnKnownDic);

                foreach (var pair in current)
                {
                    //看一下是不是有包含了
                    if (earlyList.Contains(pair.Value.AfterType))
                    {
                        if (pair.Value.Opportunity == LoadAttribute.LoadOpportunity.early)
                        {
                            earlyList.AddAfter(pair.Value.AfterType, pair.Key);

                            UnKnownDic.Remove(pair.Key);
                        }
                        else
                        {
                            lateList.Add(pair.Key);
                            UnKnownDic.Remove(pair.Key);
                        }
                    }
                    else if (lateList.Contains(pair.Value.AfterType))
                    {
                        lateList.AddAfter(pair.Value.AfterType, pair.Key);

                        UnKnownDic.Remove(pair.Key);
                    }


                    if (IsLoop(pair.Value))//如果出现互相包含的循环,直接加在最后面
                    {
                        if (pair.Value.Opportunity == LoadAttribute.LoadOpportunity.early)
                            earlyList.Add(pair.Key);
                        else
                            lateList.Add(pair.Key);

                        UnKnownDic.Remove(pair.Key);
                    }

                    //没有就跳过
                }
            }
            
            //取出信息加入激活列表

            EarlyList = earlyList.Every(TryGetMethods);

            LateList = lateList.Every(TryGetMethods);
        }

        /// <summary>
        /// 读取标签，进行早期加载
        /// </summary>
        public static void DoEarlyLoad()
        {
            ManageLoadList();

            void DoLoad(MethodInfo method)
            {
                Log.Debug("Loading...  " + method.DeclaringType.Name);

                try
                {
                    method.Invoke(null, null);
                }
                catch (Exception e)
                {
                    Log.Error("Load Erro " + e.Message);
                }
            }

            EarlyList.Every(DoLoad);
        }

        /// <summary>
        /// 进行稍晚的加载
        /// </summary>
        public static void DoLateLoad()
        {
            void DoLoad(MethodInfo method)
            {
                method.Invoke(null, null);
            }

            LateList.Every(DoLoad);
        }
    }
}
