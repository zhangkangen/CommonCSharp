using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net.Core;

namespace Log4net.Common
{
    public class CustomPatternLayout : log4net.Layout.PatternLayout
    {
        public CustomPatternLayout()
        {
            this.AddConverter("UserId", typeof(UserIdLayoutConvert));
        }
    }
}
