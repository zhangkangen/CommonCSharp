using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace Log4net.Common
{
    public class UserIdLayoutConvert : log4net.Layout.Pattern.PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var obj = loggingEvent.MessageObject as LogMsgDto;
            if (obj != null)
            {
                writer.Write(obj.UserId);
            }
        }
    }
}
