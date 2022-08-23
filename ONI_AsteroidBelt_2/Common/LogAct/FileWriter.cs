using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ONI_AsteroidBelt_2.Common.LogAct
{
    internal class FileWriter : IWriter
    {
        public FileWriter(string Path)
        {
            Trace.Listeners.Add(new TextWriterTraceListener(Path));
            Trace.AutoFlush = true;
        }


        private void Formate(LogData message)
        {
            bool first = true;

            void IEWrite(object obj)
            {
                if (obj is IList IE)
                {
                    if (first)
                    {
                        first = false;
                        Trace.WriteLine("");
                    }

                    Trace.Indent();
                    foreach (var item in IE)
                    {
                        IEWrite(item);
                    }
                    Trace.Unindent();
                }
                else
                {
                    Trace.WriteLine(obj.ToString());
                }
            }

            StringBuilder msg = new StringBuilder();
            if (message.NeedTime)
                msg.Append(System.DateTime.Now.ToString() + " ");
            msg.Append($"[ {message.MessageData.level} ] -> ");
            if (message.NeedDetail)
                msg.Append($"{message.MessageData.fileName} : {message.MessageData.callerName}() : in line[{message.MessageData.line,3}]: ");

            Trace.Write(msg);
            IEWrite(message.MessageData.content);
        }

        public void Write(LogData message)
        {
            Formate(message);
        }
    }
}
