using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Appender;
using log4net.Core;
using System.IO;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Log4net.Common
{
    public class CustomAppender : AppenderSkeleton
    {
        protected override void Append(LoggingEvent loggingEvent)
        {

            //string str = RenderLoggingEvent(loggingEvent);

            var level = loggingEvent.Level;

            var pa = AppDomain.CurrentDomain.BaseDirectory;
            var path = System.Web.HttpContext.Current.Server.MapPath("App_Data");
            var filePath = Path.Combine(path, "1.txt");

            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            var message = JsonConvert.SerializeObject(loggingEvent.MessageObject, Formatting.Indented, timeFormat);

            var obj = JsonConvert.DeserializeObject(message);

            StackTrace st = new StackTrace(1, true);

            StackFrame sf = new StackFrame(1, true);

            //var msg = loggingEvent.MessageObject.ToString();
            var msg = message;

            var ex = loggingEvent.ExceptionObject;
            if (ex != null)
            {
                StackTrace stEx = new StackTrace(ex, true);
                foreach (var item in st.GetFrames())
                {
                    msg += "第" + item.GetFileLineNumber() +
                        "行" + item.GetMethod().DeclaringType.FullName + "." + item.GetMethod().Name + "\\r\\n";
                    msg += "日志级别：" + level + "\\r\\n";
                }
            }

            using (StreamWriter sw = new StreamWriter(filePath))
            {
                sw.Write(msg);
            }
        }




    }
}
