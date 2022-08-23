using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.LogAct
{
    internal class LogData
    {
        public bool NeedTime { get; set; }

        public bool NeedDetail { get; set; }

        public string Path { get; set; }

        public Message MessageData { get; set; }

        public LogData() { }

        public string FormateToRichText()
        {
            StringBuilder msg = new StringBuilder();
            if (NeedTime)
                msg.Append(System.DateTime.Now.ToString() + " ");
            switch (MessageData.level)
            {
                case LogLevel.Debug:
                    msg.Append($"[<color=black> {MessageData.level} </color>] -> ");
                    break;
                case LogLevel.Infor:
                    msg.Append($"[<color=blue> {MessageData.level} </color>] -> ");
                    break;
                case LogLevel.Error:
                    msg.Append($"[<color=orange> {MessageData.level} </color>] -> ");
                    break;
                case LogLevel.Fatal:
                    msg.Append($"[<color=red> {MessageData.level} </color>] -> ");
                    break;
                default:
                    msg.Append($"[<color=black> {MessageData.level} </color>] -> ");
                    break;
            }

            if (NeedDetail)
                msg.Append($"{MessageData.fileName} : {MessageData.callerName}() : in line[{MessageData.line,3}]: ");
            msg.Append(MessageData.content);

            return msg.ToString();
        }

        public string Formate()
        {
            StringBuilder msg = new StringBuilder();
            if (NeedTime)
                msg.Append(System.DateTime.Now.ToString() + " ");
            msg.Append($"[ {MessageData.level} ] -> ");
            if (NeedDetail)
                msg.Append($"{MessageData.fileName} : {MessageData.callerName}() : in line[{MessageData.line,3}]: ");
            msg.Append(MessageData.content);

            return msg.ToString();
        }

    }

    [Serializable]
    public struct Message
    {
        /// <summary>
        /// 消息的具体内容
        /// </summary>
        public readonly object content;

        /// <summary>
        /// 调用方法的名字
        /// </summary>
        public readonly string callerName;

        /// <summary>
        /// 调用的文件名
        /// </summary>
        public readonly string fileName;

        /// <summary>
        /// 调用代码所在行
        /// </summary>
        public readonly int line;

        /// <summary>
        /// 该信息等级
        /// </summary>
        public readonly LogLevel level;

        public Message(object content, string callerName, string fileName, int line, LogLevel level)
        {
            this.content = content;
            this.callerName = callerName;
            this.fileName = fileName;
            this.line = line;
            this.level = level;
        }
    }


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
}
