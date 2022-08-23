using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.LogAct
{
    internal class Logger
    {
        public static string FilePath { get; set; }

        public static bool NeedTime { get; set; }

        public static bool NeedDetail { get; set; }


        public static List<IWriter> writers = new List<IWriter>();

        private static LogData GetData(Message message)
        {
            var data = new LogData()
            {
                NeedTime = NeedTime,
                NeedDetail = NeedDetail,
                Path = FilePath,
                MessageData = message
            };
            return data;
        }

        public static void Load()
        {
            FilePath = FileUtility.ModPath;

            if (File.Exists(FileUtility.Combine(@"Log\AsteroidBelt.log")))
                File.Delete(FileUtility.Combine(@"Log\AsteroidBelt.log"));

            writers.Add(new FileWriter(FileUtility.Combine(@"Log\AsteroidBelt.log")));
            writers.Add(new UIWriter());
        }

        public static void OpenFile()
        {
            Process.Start("explorer.exe", FileUtility.Combine(@"Log"));
        }

        internal static void Debug(object message, string callerName, string fileName, int line)
        {
            Message debugMessage = new Message(message, callerName, fileName, line, LogLevel.Debug);
            foreach (var writer in writers)
                writer.Write(GetData(debugMessage));
        }

        internal static void Infor(object message, string callerName, string fileName, int line)
        {
            Message debugMessage = new Message(message, callerName, fileName, line, LogLevel.Infor);
            foreach (var writer in writers)
                writer.Write(GetData(debugMessage));
        }

        internal static void Error(object message, string callerName, string fileName, int line)
        {
            Message debugMessage = new Message(message, callerName, fileName, line, LogLevel.Error);
            foreach (var writer in writers)
                writer.Write(GetData(debugMessage));
        }

        internal static void Fatal(object message, string callerName, string fileName, int line)
        {
            Message debugMessage = new Message(message, callerName, fileName, line, LogLevel.Fatal);
            foreach (var writer in writers)
                writer.Write(GetData(debugMessage));
        }
    }
}
