using ONI_AsteroidBelt_2.Common.AsAttributes;
using ONI_AsteroidBelt_2.Common.AsLoader;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common
{
    internal static class FileUtility
    {
        /// <summary>
        /// 已移动的文件
        /// </summary>
        private static Dictionary<string, string> fileLoaded;

        /// <summary>
        /// 文件工具加载
        /// </summary>
        [Load]
        private static void Load()
        {
            ModPath = Entrance.ModPath;
            fileLoaded = new Dictionary<string, string>();
        }

        /// <summary>
        /// 观察文件是否存在，如果不存在就创建一个
        /// </summary>
        /// <param name="path">路径</param>
        /// <param name="isDirectory">是否是文件夹</param>
        /// <returns>文件是否存在</returns>
        private static bool Touch(string path, bool isDirectory = false)
        {
            if (isDirectory)
                if (Directory.Exists(path))
                    return true;
                else
                {
                    Directory.CreateDirectory(path);
                    Log.Error("文件夹加载失败，尝试创建新文件夹" + path);
                    return false;
                }

            if (File.Exists(path))
                return true;

            Log.Error("文件加载失败，尝试创建新文件" + path);

            var di = Path.GetDirectoryName(path);

            if (!Directory.Exists(di))
                Directory.CreateDirectory(di);
            if(!File.Exists(path))
                File.Create(path).Dispose();
            return false;
        }

        /// <summary>
        /// 模组的根路径
        /// </summary>
        public static string ModPath { get; set; }

        /// <summary>
        /// 将相对路径组合成绝对路径
        /// </summary>
        /// <param name="input">相对路径</param>
        /// <returns>绝对路径</returns>
        public static string Combine(params string[] inputs)
        {
            string res = "";

            for(int i = 0; i < inputs.Count(); i++)
            {
                res = Path.Combine(res, inputs[i]);
            }

            return Path.Combine(ModPath, res);
        }

        /// <summary>
        /// 写入序列化文本
        /// </summary>
        /// <typeparam name="T">可序列化的目标</typeparam>
        /// <param name="path">目标文件夹</param>
        /// <param name="valuePairs">文件名 - 内容</param>
        /// <param name="absolutePath">是否采用绝对路径</param>
        public static void WriteAll<T>(string path, Dictionary<string, T> valuePairs, bool absolutePath = false) where T : AsSerializable
        {
            foreach (var pair in valuePairs)
            {
                WriteIn(
                    Path.Combine(path, pair.Key + ".json"),
                    Utility.Serialize(pair.Value), absolutePath);
            }
        }

        /// <summary>
        /// 写入数据
        /// </summary>
        /// <param name="path">目标文件相对路径</param>
        /// <param name="content">内容</param>
        /// <param name="absolutePath">是否采用绝对路径</param>
        public static void WriteIn(string path, string content, bool absolutePath = false)
        {
            if(!absolutePath)
                path = Combine(path);

            Touch(path);

            File.WriteAllText(path, content);
        }

        /// <summary>
        /// 写入 bytes 数据
        /// </summary>
        /// <param name="path">目标文件路径</param>
        /// <param name="bytes">内容</param>
        /// <param name="absolutePath">是否采用绝对路径</param>
        public static void WriteIn(string path, byte[] bytes, bool absolutePath = false)
        {
            if (!absolutePath)
                path = Combine(path);

            Touch(path);

            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// 以行读入字符串
        /// </summary>
        /// <param name="path">目标文件路径</param>
        /// <param name="absolutePath">是否采用绝对路径</param>
        /// <returns>字符串行，如果没有就返回空</returns>
        public static string[] ReadLines(string path, bool absolutePath = false)
        {
            if (!absolutePath)
                path = Combine(path);

            if (!Touch(path))
                return new string[0];

            return File.ReadLines(path).ToArray();
        }

        /// <summary>
        /// 读取文件名和对应的文件内容
        /// </summary>
        /// <typeparam name="T">可序列化的目标</typeparam>
        /// <param name="path">目标文件路径</param>
        /// <param name="absolutePath">是否采用绝对路径</param>
        /// <returns>文件名-文件内容</returns>
        public static Dictionary<string, T> ReadAll<T>(string path, bool absolutePath = false) where T : AsSerializable
        {
            var pairs = new Dictionary<string, T>();

            path = absolutePath ? path : Combine(path);

            Touch(path, true);

            foreach (var file in Directory.GetFiles(path))
            {
                var text = ReadText(file, true);

                pairs.Add(Path.GetFileNameWithoutExtension(file),Utility.Deserialize<T>(text));

            }

            return pairs;
        }

        /// <summary>
        /// 读入字符串
        /// </summary>
        /// <param name="path">目标文件路径</param>
        /// <param name="absolutePath">是否采用绝对路径</param>
        /// <returns>字符串，如果没有就返回空</returns>
        public static string ReadText(string path, bool absolutePath = false)
        {
            if(!absolutePath)
                path = Combine(path);

            if (!Touch(path))
                return null;

            return File.ReadAllText(path);
        }

        /// <summary>
        /// 读入 byte
        /// </summary>
        /// <param name="path">目标文件路径</param>
        /// <param name="absolutePath">是否采用绝对路径</param>
        /// <returns>bytes，如果没有就返回空</returns>
        public static byte[] ReadByte(string path, bool absolutePath = false)
        {
            if (!absolutePath)
                path = Combine(path);

            if (!Touch(path))
                return null;

            return File.ReadAllBytes(path);
        }

        /// <summary>
        /// 交换文件的内容
        /// </summary>
        /// <param name="pathOne">文件路径1</param>
        /// <param name="pathTwo">文件路径2</param>
        /// <param name="absolutePath">是否采用绝对路径</param>
        public static void Swap(string pathOne, string pathTwo, bool absolutePath = false)
        {
            var one = ReadByte(pathOne);

            var two = ReadByte(pathTwo);

            if (one != null || two != null)
            {
                WriteIn(pathTwo, one ?? new byte[0]);
                WriteIn(pathOne, two ?? new byte[0]);
            }
        }

        /// <summary>
        /// 将文件加载到ONI目标路径
        /// </summary>
        /// <param name="SourseDirectory">原文件相对路径</param>
        /// <param name="TargetDirectory">目标文件相对路径</param>
        public static void LoadFile(string SourseDirectory, string TargetDirectory)
        {
            SourseDirectory = Combine(SourseDirectory);

            TargetDirectory = Path.Combine(Directory.GetCurrentDirectory(), TargetDirectory);

            CopyTo(SourseDirectory, TargetDirectory);
        }

        /// <summary>
        /// 将已加载的文件移除
        /// </summary>
        /// <param name="SourseDirectory">原文件相对路径</param>
        public static void Remove(string SourseDirectory)
        {
            SourseDirectory = Combine(SourseDirectory);

            Delete(SourseDirectory);
        }

        private static void Delete(string SourseDirectory)
        {

            foreach (var file in Directory.GetFiles(SourseDirectory))
            {
                string name = Path.GetFileName(file);

                if (fileLoaded.ContainsKey(name))
                {
                    if (File.Exists(fileLoaded[name]))
                        File.Delete(fileLoaded[name]);

                    fileLoaded.Remove(name);

                }

            }

        }

        private static void CopyTo(string SourseDirectory, string TargetDirectory)
        {
            if (!Directory.Exists(SourseDirectory))
                Directory.CreateDirectory(SourseDirectory);

            foreach (var file in Directory.GetFiles(SourseDirectory))
            {
                string name = Path.GetFileName(file);

                if (File.Exists(Path.Combine(TargetDirectory, name)))
                    File.Delete(Path.Combine(TargetDirectory, name));

                File.Copy(file, Path.Combine(TargetDirectory, name));

                if (!fileLoaded.ContainsKey(name))
                    fileLoaded.Add(name, Path.Combine(TargetDirectory, name));
                else
                    fileLoaded[name] = Path.Combine(TargetDirectory, name);

            }
        }
    }
}
