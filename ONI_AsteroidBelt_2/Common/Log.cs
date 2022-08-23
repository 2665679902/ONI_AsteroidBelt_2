using ONI_AsteroidBelt_2.Common.AsAttributes;
using ONI_AsteroidBelt_2.Common.LogAct;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common
{
    internal class Log
    {
        /// <summary>
        /// Log显示等级
        /// </summary>
        [Flags]
        public enum LogLevel
        {
            /// <summary>
            /// 调试级
            /// </summary>
            Debug,

            /// <summary>
            /// 一般级
            /// </summary>
            Infor,

            /// <summary>
            /// 错误级
            /// </summary>
            Error,

            /// <summary>
            /// 崩溃级
            /// </summary>
            Fatal
        }

        private static LogLevel _level;

        public static LogLevel Level { get { return _level; } set { _level = value; } }

        [Load(typeof(FileUtility))]
        private static void Load()
        {
            LogAct.Logger.Load();
        }

        public static bool NeedTime { get => LogAct.Logger.NeedTime; set => LogAct.Logger.NeedTime = value; }

        public static bool NeedDetail { get => LogAct.Logger.NeedDetail; set => LogAct.Logger.NeedDetail = value; }

        public static bool IsAble { get; set; } = true;

        public static void OpenFile()
        {
            LogAct.Logger.OpenFile();
        }


        /// <summary>
        /// 写入一条Debug信息
        /// </summary>
        /// <param name="message">要写入的信息</param>
        /// <param name="callerName">调用函数的名字</param>
        /// <param name="fileName">调用文件的名字</param>
        /// <param name="line">调用时的行数</param>
        public static void Debug(
                object message = null,
                [CallerMemberName] string callerName = null,
                [CallerFilePath] string fileName = null,
                [CallerLineNumber] int line = 0)
        {
            if (!IsAble)
                return;

            if (LogLevel.Debug < _level)
                return;

            if (message == null)
                message = "";

            LogAct.Logger.Debug(message, callerName, fileName, line);
        }

        /// <summary>
        /// 写入一条Infor信息
        /// </summary>
        /// <param name="message">要写入的信息</param>
        /// <param name="callerName">调用函数的名字</param>
        /// <param name="fileName">调用文件的名字</param>
        /// <param name="line">调用时的行数</param>
        public static void Infor(
                object message = null,
                [CallerMemberName] string callerName = null,
                [CallerFilePath] string fileName = null,
                [CallerLineNumber] int line = 0)
        {
            if (!IsAble)
                return;

            if (LogLevel.Infor < _level)
                return;

            if (message == null)
                message = "";

            LogAct.Logger.Infor(message, callerName, fileName, line);
        }

        /// <summary>
        /// 写入一条Error信息
        /// </summary>
        /// <param name="message">要写入的信息</param>
        /// <param name="callerName">调用函数的名字</param>
        /// <param name="fileName">调用文件的名字</param>
        /// <param name="line">调用时的行数</param>
        public static void Error(
                object message = null,
                [CallerMemberName] string callerName = null,
                [CallerFilePath] string fileName = null,
                [CallerLineNumber] int line = 0)
        {
            if (!IsAble)
                return;

            if (LogLevel.Error < _level)
                return;

            if (message == null)
                message = "";

            LogAct.Logger.Error(message, callerName, fileName, line);
        }

        /// <summary>
        /// 写入一条Fatal信息
        /// </summary>
        /// <param name="message">要写入的信息</param>
        /// <param name="callerName">调用函数的名字</param>
        /// <param name="fileName">调用文件的名字</param>
        /// <param name="line">调用时的行数</param>
        public static void Fatal(
                object message = null,
                [CallerMemberName] string callerName = null,
                [CallerFilePath] string fileName = null,
                [CallerLineNumber] int line = 0)
        {
            if (!IsAble)
                return;

            if (LogLevel.Fatal < _level)
                return;

            if (message == null)
                message = "";

            LogAct.Logger.Fatal(message, callerName, fileName, line);
            throw new Exception($"Log throw exception : {message}");
        }
    }
}
