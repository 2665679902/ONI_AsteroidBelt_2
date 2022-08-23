using HarmonyLib;
using ONI_AsteroidBelt_2.AsData.AssetBundles;
using ONI_AsteroidBelt_2.AsData.Strings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ONI_AsteroidBelt_2.Common.AsLoader
{
    internal class Entrance: KMod.UserMod2
    {
        public static Entrance Instance { get; set; }

        public static string ModPath { get; set; }

        public static Harmony EntranceHarmony { get; set; }

        /// <summary>
        /// 模组导入时加载
        /// </summary>
        /// <param name="harmony"></param>
        public override void OnLoad(Harmony harmony)
        {
            ModPath = mod.ContentPath.Replace('/', '\\');

            EntranceHarmony = harmony;

            Instance = this;

            base.OnLoad(harmony);

            AttributeManager.DoEarlyLoad();

        }

        /// <summary>
        /// 于主屏幕生成后延时加载
        /// </summary>
        [HarmonyPatch(typeof(MainMenu), "OnPrefabInit")]
        private static class Database_Techs_Init_Patch
        {
            public static void Postfix()
            {
                AttributeManager.DoLateLoad();

                Log.Debug($"翻译测试：获取语言地区[{Localization.GetLocale()?.Code}]  测试：{AsStrings.Test.TestString}");

                Log.Debug($"AssetBundle测试: {!(AssetBundleManager.TryGetObject<Texture2D>("biomeIconMoo") is null)}");

                Log.Debug("Entrance on load 加载完成");
            }
        }

    }
}
