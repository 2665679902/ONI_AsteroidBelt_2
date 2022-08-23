using HarmonyLib;
using ONI_AsteroidBelt_2.Common;
using ONI_AsteroidBelt_2.Common.AsAttributes;
using ONI_AsteroidBelt_2.Common.AsExtension;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.AsData.Strings
{
    /// <summary>
    /// 管理翻译
    /// </summary>
    public static class AsStringManager
    {
        private static Dictionary<int, string> _strings;

        private static void Load()
        {
            Log.Debug("AsString Load");

            var locale = Localization.GetLocale();

            void FormateToDic(string[] lines)
            {
                if (lines == null)
                {
                    Log.Error("AsStringManager 字符串获取失败");
                    return;
                }

                _strings = new Dictionary<int, string>();
                for (int i = 0; i < lines.Length; i++)
                {
                    if (lines[i].StartsWith("msgid "))
                    {
                        if (lines[i + 1].StartsWith("msgstr "))
                        {
                            var key = lines[i].Remove("msgid ").Remove("\"");
                            var value = lines[++i].Remove("msgstr ").Remove("\"");
                            if (key != "")
                            {
                                //Log.Debug($" get pair Key{key} : value{value}");
                                _strings.Add(key.Remove("\\n").GetHashCode(), value);
                            }
                        }
                        else
                        {
                            var key = new StringBuilder(lines[i].Remove("msgid ").Remove("\""));
                            for (int j = i + 1; j < lines.Length; j++)
                            {
                                if (!lines[j].StartsWith("msgstr "))
                                    key.Append(lines[j].Replace("\"", ""));
                                else
                                {
                                    var value = new StringBuilder(lines[j].Remove("msgstr ").Remove("\""));
                                    for (int k = j + 1; k < lines.Length; k++)
                                    {
                                        if (!lines[k].StartsWith("#: "))
                                            value.Append(lines[k].Replace("\"", ""));
                                        else
                                        {
                                            //Log.Debug($"[{key.ToString().Replace("\\n", "").GetHashCode()}] get pair Key{key.Replace("\\n", "")} : value{value.ToString().TrimEnd('\n')}");
                                            _strings.Add(key.ToString().Remove("\\n").GetHashCode(), value.ToString().TrimEnd('\n'));
                                            break;
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    }

                }
            }

            if (locale == null)
            {
                return;
            }

            var path = $@"Resources\Translations\{locale.Code}.po";

            FormateToDic(FileUtility.ReadLines(path));

            EventCentre.Trigger(EventInventory.StringLoadReady, null);

            Log.Debug("Trigger !");
        }

        [HarmonyPatch(typeof(GeneratedBuildings), "LoadGeneratedBuildings")]
        public static class GeneratedBuildings_LoadGeneratedBuildings_Patch
        {
            public static void Prefix()
            {
                Load();
            }

        }


        /// <summary>
        /// 查找文字翻译
        /// </summary>
        /// <param name="inPut">输入</param>
        /// <returns>翻译结果，找不到就返回原文</returns>
        public static string Get(string inPut)
        {
            if (inPut == null)
                return null;

            if (_strings == null || !_strings.ContainsKey(inPut.Replace("\n", "").GetHashCode()))
            {
                Log.Infor($"尝试查找翻译 {inPut.Replace("\n", "")} [{inPut.Replace("\n", "").GetHashCode()}]失败！失败原因 -> _strings == null:{_strings != null} _strings.ContainsKey(inPut){_strings.ContainsKey(inPut.Replace("\n", "").GetHashCode())}");
                return inPut;
            }

            return _strings[inPut.Replace("\n", "").GetHashCode()].Replace("\\n", "\n");
        }
    }
}
