using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ONI_AsteroidBelt_2.AsData.AssetBundles
{
    public static class AssetBundleManager
    {
        private static readonly Dictionary<string, AssetBundle> assetBundles = new Dictionary<string, AssetBundle>();

        private static readonly List<UnityEngine.Object> gameObjects = new List<UnityEngine.Object>();

        [Load(typeof(Log))]
        private static void Load()
        {
            ReLoad();
        }

        /// <summary>
        /// 加载所有的AssetBundle
        /// </summary>
        public static void ReLoad(bool forceReLoad = false)
        {
            if (assetBundles.Count != 0 && !forceReLoad)
            {
                LoadObjects();

                return;
            }
            else if (assetBundles.Count != 0 && forceReLoad)
            {
                assetBundles.Clear();
                AssetBundle.UnloadAllAssetBundles(false);
            }


            foreach (var file in Directory.GetFiles(FileUtility.Combine(@"Resources\AssetBundles")))
            {
                var name = Path.GetFileNameWithoutExtension(file);
                var bundle = AssetBundle.LoadFromFile(file);
                if (bundle != null)
                {
                    assetBundles.Add(name, bundle);
                    //Log.Debug($"Load {file}");
                }
            }

            LoadObjects();
        }

        /// <summary>
        /// 加载所有的 AssetBundle 内的 UnityEngine.Object
        /// </summary>
        private static void LoadObjects()
        {
            gameObjects.Clear();
            foreach (var ab in assetBundles)
            {
                foreach (UnityEngine.Object obj in ab.Value.LoadAllAssets())
                {
                    gameObjects.Add(obj);
                    //Log.Debug($"get obj {obj.name}");
                }
            }
        }

        /// <summary>
        /// 查找 object
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="assetBundleName">如果有，则在此 AssetBundle 中查找</param>
        /// <returns>查找 object 结果，如果不存在则返回 null </returns>
        public static UnityEngine.Object TryGetObject(string name, string assetBundleName = null)
        {
            if (assetBundleName != null)
            {
                if (assetBundles.ContainsKey(assetBundleName))
                {
                    var res = assetBundles[assetBundleName].LoadAsset(name);
                    return res;
                }

                Log.Error($"AssetBundleManager report: can't find object {name} in {assetBundleName}");

                return null;
            }

            foreach (var obj in gameObjects)
            {
                if (obj.name == name)
                {
                    return obj;
                }
            }

            Log.Error($"AssetBundleManager report: can't find object {name}");

            return null;
        }

        /// <summary>
        /// 查找 object
        /// </summary>
        /// <typeparam name="T">目标类型</typeparam>
        /// <param name="name">名字</param>
        /// <param name="assetBundleName">如果有，则在此 AssetBundle 中查找</param>
        /// <returns>查找结果，如果不存在则返回 null</returns>
        public static T TryGetObject<T>(string name, string assetBundleName = null) where T : UnityEngine.Object
        {
            var obj = TryGetObject(name, assetBundleName);
            return obj as T;
        }
    }
}
