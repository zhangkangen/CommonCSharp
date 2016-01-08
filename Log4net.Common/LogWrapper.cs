using System;
using System.Configuration;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using System.Text;
using log4net;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Xml;

namespace Log4net.Common
{
    public class LogWrapper
    {

        //public static readonly LogWrapper instance = new LogWrapper();

        public static void Config(string path = null)
        {
            string environment = ConfigurationManager.AppSettings["Environment"];

            if (string.IsNullOrEmpty(path))
            {
                path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin", "log4netConfig.xml");
            }
            if (File.Exists(path))
            {
                /*  使用 xDocument读取配置
                XDocument xdoc = XDocument.Load(path);
                XElement xe = xdoc.Root;
                IEnumerable<XElement> eles = xe.Elements("appender");
                foreach (XElement item in eles)
                {
                    if (item.Attribute("name").Value.Equals("RollingLogFileAppender"))
                    {
                        XElement fileElement = item.Element("file");
                        if (fileElement != null)
                        {
                            var filePath = Path.Combine(@"C:\\yunqitech", environment + "\\");
                            fileElement.SetAttributeValue("value", filePath);
                        }
                    }
                }
                xdoc.Save(path);
                */

                XmlDocument xmlDoc = new XmlDocument();
                //xmlDoc.LoadXml(xdoc.ToString());   //XDocument 转XmlDocument
                xmlDoc.Load(path);
                XmlElement root = xmlDoc.DocumentElement;
                XmlNodeList list = root.SelectNodes("appender");

                foreach (XmlNode item in list)
                {
                    XmlNode fileNode = item.SelectSingleNode("file");
                    if (fileNode != null)
                    {
                        XmlElement fileEle = fileNode as XmlElement;

                        var filePath = Path.Combine(@"C:\\yunqitech", environment + "\\");
                        //XmlAttribute attr = xmlDoc.CreateAttribute("value");
                        //attr.Value = filePath;

                        //fileNode.Attributes.Append(attr);

                        fileEle.SetAttribute("value", filePath);

                    }
                }

                log4net.Config.XmlConfigurator.Configure(xmlDoc.DocumentElement);
                //log4net.Config.XmlConfigurator.Configure(new FileInfo(path)); 基本配置 读取xml配置
            }
        }

        static LogWrapper()
        {
            Config();
        }

        private ILog log;

        /// <summary>
        /// Creates a new instance of the logging wrapper by walking the stack to 
        /// find the calling class and configures the log based on this.
        /// </summary>
        public LogWrapper()
        {
            /*
             * Get the calling method, to determine the class name.
             * */
            StackFrame a = new StackFrame(1);
            MethodBase callingMethod = a.GetMethod();
            Type callingType = callingMethod.DeclaringType;
            //log = LogManager.GetLogger(callingType.FullName);
            log = LogManager.GetLogger(callingType.FullName);// LoggingWrapper.GetLoggerMore(callingType.FullName);
        }

        public static log4net.ILog GetLogger(string name)
        {
            return LogManager.GetLogger(name);

        }

        public bool IsDebugEnabled
        {
            get { return log.IsDebugEnabled; }
        }

        public bool IsErrorEnabled
        {
            get { return log.IsErrorEnabled; }
        }

        public bool IsInfoEnabled
        {
            get { return log.IsDebugEnabled; }
        }

        public bool IsWarnEnabled
        {
            get { return log.IsWarnEnabled; }
        }

        #region Debug
        public void Debug(object message, Exception exception)
        {

            log.Debug(message, exception);
        }

        public void Debug(object message)
        {
            var page = GetDebugCallingPage();
            log.Debug(message);
        }

        public void DebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            log.DebugFormat(provider, format, args);
        }

        public void DebugFormat(string format, params object[] args)
        {
            log.DebugFormat(format, args);
        }
        #endregion

        #region Error
        public void Error(object message, Exception exception)
        {
            log.Error(message, exception);
        }

        public void Error(object message)
        {
            log.Error(message);
        }

        public void Error(Exception exception)
        {
            log.Error(null, exception);
        }

        public void ErrorFormat(IFormatProvider provider, string format, params object[] args)
        {
            log.ErrorFormat(provider, format, args);
        }

        public void ErrorFormat(string format, params object[] args)
        {
            log.ErrorFormat(format, args);
        }
        #endregion

        #region Info
        public void Info(object message, Exception exception)
        {
            log.Info(message, exception);
        }

        public void Info(object message)
        {
            log.Info(message);
        }

        public void InfoFormat(IFormatProvider provider, string format, params object[] args)
        {
            log.InfoFormat(provider, format, args);
        }

        public void InfoFormat(string format, params object[] args)
        {
            log.InfoFormat(format, args);
        }
        #endregion

        #region Warn
        public void Warn(object message, Exception exception)
        {
            log.Warn(message, exception);
        }

        public void Warn(object message)
        {
            log.Warn(message);
        }

        public void WarnFormat(IFormatProvider provider, string format, params object[] args)
        {
            log.WarnFormat(provider, format, args);
        }

        public void WarnFormat(string format, params object[] args)
        {
            log.WarnFormat(format, args);
        }

        public void WarnFileNotExist(string message)
        {
            GetLogger(FilenotexistLogName).Warn(message);
        }

        public void WarnFileNotExist(object message, Exception exception)
        {
            GetLogger(FilenotexistLogName).Warn(message, exception);
        }

        public void WarnSecurity(string message)
        {
            GetLogger(SecurityLogName).Warn(message);
        }

        public void WarnSecurity(object message, Exception exception)
        {
            GetLogger(SecurityLogName).Warn(message, exception);
        }

        #endregion

        #region Method Debug (Uses call-stack to output method name)
        /// <summary>
        /// Delegate to allow custom information to be logged
        /// </summary>
        /// <param name="logOutput">Initialized <see cref="StringBuilder"/> object which will be appended to output string</param>
        public delegate void LogOutputMapper(StringBuilder logOutput);

        public void MethodDebugFormat(IFormatProvider provider, string format, params object[] args)
        {
            log.DebugFormat(provider, string.Format("Page: {2}, MethodName: {1}, {0}", format, GetDebugCallingMethod(), GetDebugCallingPage()), args);
        }

        public void MethodDebugFormat(string format, params object[] args)
        {
            log.DebugFormat(string.Format("Page: {2}, MethodName: {1}, {0}", format, GetDebugCallingMethod(), GetDebugCallingPage()), args);
        }

        public void MethodDebug(string message)
        {
            log.Debug(string.Format("Page: {2}, MethodName: {1}, {0}", message, GetDebugCallingMethod(), GetDebugCallingPage()));
        }

        // With Log Prefix

        public void MethodDebugFormat(IFormatProvider provider, string logPrefix, string format, params object[] args)
        {
            log.DebugFormat(provider, string.Format("{0}| {1} , MethodName: {2} , Page: {3}", logPrefix, format, GetDebugCallingMethod(), GetDebugCallingPage()), args);
        }

        public void MethodDebugFormat(string logPrefix, string format, params object[] args)
        {
            log.DebugFormat(string.Format("{0}| Page: {3}, MethodName: {2} , {1}", logPrefix, format, GetDebugCallingMethod(), GetDebugCallingPage()), args);
        }

        public void MethodDebug(string logPrefix, string message)
        {
            log.Debug(string.Format("{0}| Page: {3}, MethodName: {2}, {1}", logPrefix, message, GetDebugCallingMethod(), GetDebugCallingPage()));
        }

        // With Log Prefix and delegate to add custom logging info
        public void MethodDebugFormat(string logPrefix, LogOutputMapper customLogOutput, string format, params object[] args)
        {
            StringBuilder additionalLogData = new StringBuilder();
            if (customLogOutput != null)
                customLogOutput(additionalLogData);

            log.DebugFormat(string.Format("{0}| Page: {3}, MethodName: {2}, {1}, {4}", logPrefix, format, GetDebugCallingMethod(), GetDebugCallingPage(), additionalLogData.ToString()), args);
        }

        /// <summary>
        /// Returns calling method name using current stack 
        /// and assuming that first non Logging method is the parent
        /// </summary>
        /// <returns>Method Name</returns>
        private string GetDebugCallingMethod()
        {
            // Walk up the stack to get parent method
            StackTrace st = new StackTrace();
            if (st != null)
            {
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame sf = st.GetFrame(i);
                    MethodBase method = sf.GetMethod();
                    if (method != null)
                    {
                        string delaringTypeName = method.DeclaringType.FullName;
                        if (delaringTypeName != null && delaringTypeName.IndexOf("Yunqi.Logging") < 0)
                            return method.Name;
                    }
                }
            }

            return "Unknown Method";
        }

        public string CurrentStackTrace()
        {
            StringBuilder sb = new StringBuilder();
            // Walk up the stack to return everything
            StackTrace st = new StackTrace();
            if (st != null)
            {
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame sf = st.GetFrame(i);
                    MethodBase method = sf.GetMethod();
                    if (method != null)
                    {
                        string declaringTypeName = method.DeclaringType.FullName;
                        if (declaringTypeName != null && declaringTypeName.IndexOf("Yunqi.Logging") < 0)
                        {
                            sb.AppendFormat("{0}.{1}(", declaringTypeName, method.Name);

                            ParameterInfo[] paramArray = method.GetParameters();

                            if (paramArray.Length > 0)
                            {
                                for (int j = 0; j < paramArray.Length; j++)
                                {
                                    sb.AppendFormat("{0} {1}", paramArray[j].ParameterType.Name, paramArray[j].Name);
                                    if (j + 1 < paramArray.Length)
                                    {
                                        sb.Append(", ");
                                    }
                                }
                            }
                            sb.AppendFormat(")\n - {0}, {1}", sf.GetFileLineNumber(), sf.GetFileName());
                        }
                    }
                    else
                    {
                        sb.Append("The method returned null\n");
                    }
                }
            }
            else
            {
                sb.Append("Unable to get stack trace");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Returns ASP.NET method name which called current method. 
        /// Uses call stack and assumes that all methods starting with 'ASP.' are the ASP.NET page methods
        /// </summary>
        /// <returns>Class Name of the ASP.NET page</returns>
        private string GetDebugCallingPage()
        {
            // Walk up the stack to get calling method which is compiled ASP.Net page
            StackTrace st = new StackTrace();
            if (st != null)
            {
                for (int i = 0; i < st.FrameCount; i++)
                {
                    StackFrame sf = st.GetFrame(i);
                    MethodBase method = sf.GetMethod();
                    if (method != null && method.DeclaringType != null)
                    {
                        string declaringTypeName = method.DeclaringType.FullName;
                        if (declaringTypeName != null && declaringTypeName.IndexOf("ASP.") == 0)
                            return declaringTypeName;
                    }
                }
            }

            return "Unknown Page";
        }

        #endregion

        #region ILogMore methods

        public void MoreInfo(params object[] traceMessages)
        {
            log.Info(traceMessages);
        }

        public void MoreError(params object[] traceMessages)
        {
            log.Error(traceMessages);
        }

        public void MoreWarn(params object[] traceMessages)
        {
            log.Warn(traceMessages);
        }

        public void MoreDebug(params object[] traceMessages)
        {
            log.Debug(traceMessages);
        }

        public void MoreFatal(params object[] traceMessages)
        {
            log.Fatal(traceMessages);
        }

        public bool IsMoreDebugEnabled
        {
            get { return log.IsDebugEnabled; }
        }

        public bool IsMoreInfoEnabled
        {
            get { return log.IsInfoEnabled; }
        }

        public bool IsMoreErrorEnabled
        {
            get { return log.IsErrorEnabled; }
        }

        public bool IsMoreWarnEnabled
        {
            get { return log.IsWarnEnabled; }
        }

        public bool IsMoreFatalEnabled
        {
            get { return log.IsFatalEnabled; }
        }

        #endregion

        #region Exception Logging
        /// <summary>
        /// Logs exception 
        /// </summary>
        /// <param name="exc">Exception to log</param>
        /// <param name="policyName">Policy name to append to logged exception</param>
        /// <remarks>
        /// Does not rethrow exceptions. Use throw; statement to rethrow original exception within catch() block
        /// </remarks>
        /// <returns>true if successful</returns>
        public bool HandleException(Exception exc, string policyName)
        {
            log.Error(policyName, exc);
            return true;
        }
        #endregion

        #region config

        public const string DefaultApplicationLogFolder = "Yunqi.logs";

        private static string default_trace = Path.Combine(DefaultApplicationLogFolder, "trace.log");
        private static string default_trace_config = Path.Combine(DefaultApplicationLogFolder, @"trace_config.log");
        private static string default_trace_filenotexist = Path.Combine(DefaultApplicationLogFolder, @"trace_filenotexist.log");
        private static string default_trace_security = Path.Combine(DefaultApplicationLogFolder, @"trace_security.log");

        public const string FilenotexistLogName = "Yunqi.FileNotExistLog";
        public const string SecurityLogName = "Yunqi.SecurityLog";

        private static string default_trace_rollminute = Path.Combine(DefaultApplicationLogFolder, @"trace_rollminute.log");
        public const string RollminuteLogName = "Yunqi.RollminuteLog";

        private static string default_trace_perf = Path.Combine(DefaultApplicationLogFolder, "trace_perf.log");
        public const string PerfLoggerName = "Yunqi.Tracing.RaytraceLogger";

        private static string GetDefaultLogConfigurationContent()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("<log4net>");
            builder.AppendLine(@"<appender name=""RollingFileAppender"" type=""log4net.Appender.RollingFileAppender"">");
            builder.AppendLine(@"<file value=" + "\"" + default_trace + "\"" + "/>");
            builder.AppendLine(@"<appendToFile value=""true"" />");
            builder.AppendLine(@"<rollingStyle value=""Size"" />");
            builder.AppendLine(@"<maxSizeRollBackups value=""10"" />");
            builder.AppendLine(@"<maximumFileSize value=""1000KB"" />");
            builder.AppendLine(@"<staticLogFileName value=""true"" />");
            builder.AppendLine(@"<layout type=""log4net.Layout.PatternLayout"">");
            builder.AppendLine(@"<conversionPattern value=""%d [%t] %-5p %c [%x] - %m%n"" />");
            builder.AppendLine(@"</layout>");
            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=""Yunqi.Configuration.BaseConfigurationManager"" />");
            builder.AppendLine(@"<acceptOnMatch value=""false"" />");
            builder.AppendLine(@"</filter>");
            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=""Yunqi.Configuration.DirectoryWatcher"" />");
            builder.AppendLine(@"<acceptOnMatch value=""false"" />");
            builder.AppendLine(@"</filter>");

            //add FilenotExist Filter Start
            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=" + "\"" + FilenotexistLogName + "\"" + "/>");
            builder.AppendLine(@"<acceptOnMatch value=""false"" />");
            builder.AppendLine(@"</filter>");
            //add FilenotExist Filter End            

            //add Security Filter Start
            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=" + "\"" + SecurityLogName + "\"" + "/>");
            builder.AppendLine(@"<acceptOnMatch value=""false"" />");
            builder.AppendLine(@"</filter>");
            //add Security Filter End           

            //add RollminuteLog Start
            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=" + "\"" + RollminuteLogName + "\"" + "/>");
            builder.AppendLine(@"<acceptOnMatch value=""false"" />");
            builder.AppendLine(@"</filter>");
            //add RollminuteLog End

            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=" + "\"" + PerfLoggerName + "\"" + "/>");
            builder.AppendLine(@"<acceptOnMatch value=""false"" />");
            builder.AppendLine(@"</filter>");

            builder.AppendLine(@"</appender>");
            builder.AppendLine();

            //builder.AppendLine(@"<appender name=""RollingFileAppenderConfiguration"" type=""log4net.Appender.RollingFileAppender"">");
            //builder.AppendLine(@"<file value='" + default_trace_config + "' />");
            //builder.AppendLine(@"<appendToFile value=""true"" />");
            //builder.AppendLine(@"<rollingStyle value=""Size"" />");
            //builder.AppendLine(@"<maxSizeRollBackups value=""20"" />");
            //builder.AppendLine(@"<maximumFileSize value=""3000KB"" />");
            //builder.AppendLine(@"<layout type=""log4net.Layout.PatternLayout"">");
            //builder.AppendLine(@"<conversionPattern value=""%d [%t] %-5p %c [%x] - %m%n"" />");
            //builder.AppendLine(@"</layout>");
            //builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            //builder.AppendLine(@"<loggerToMatch value=""Yunqi.Configuration.BaseConfigurationManager"" />");
            //builder.AppendLine(@"<acceptOnMatch value=""true"" />");
            //builder.AppendLine(@"</filter>");
            //builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            //builder.AppendLine(@"<loggerToMatch value=""Yunqi.Configuration.DirectoryWatcher"" />");
            //builder.AppendLine(@"<acceptOnMatch value=""true"" />");
            //builder.AppendLine(@"</filter>");
            //builder.AppendLine(@"<filter type=""log4net.Filter.DenyAllFilter"" />");
            //builder.AppendLine(@"</appender>");
            //builder.AppendLine();

            //builder.AppendLine(@"<appender name=""RollingFileAppenderTracing"" type=""log4net.Appender.RollingFileAppender"">");
            //builder.AppendLine(@"<file value='" + default_trace_perf + "' />");
            //builder.AppendLine(@"<appendToFile value=""true"" />");
            //builder.AppendLine(@"<rollingStyle value=""Size"" />");
            //builder.AppendLine(@"<maxSizeRollBackups value=""30"" />");
            //builder.AppendLine(@"<maximumFileSize value=""3000KB"" />");
            //builder.AppendLine(@"<layout type=""log4net.Layout.PatternLayout"">");
            //builder.AppendLine(@"<conversionPattern value=""%m%n"" />");
            //builder.AppendLine(@"</layout>");
            //builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            //builder.AppendLine(@"<loggerToMatch value=""Yunqi.Tracing.RaytraceLogger"" />");
            //builder.AppendLine(@"<acceptOnMatch value=""true"" />");
            //builder.AppendLine(@"</filter>");
            //builder.AppendLine(@"<filter type=""log4net.Filter.DenyAllFilter"" />");
            //builder.AppendLine(@"</appender>");
            //builder.AppendLine();

            //add FilenotExist Filter Start
            builder.AppendLine(@"<appender name=""RollingFileAppenderFileNotExist"" type=""log4net.Appender.RollingFileAppender"">");
            builder.AppendLine(@"<file value=" + "\"" + default_trace_filenotexist + "\"" + "/>");
            builder.AppendLine(@"<appendToFile value=""true"" />");
            builder.AppendLine(@"<rollingStyle value=""Size"" />");
            builder.AppendLine(@"<maxSizeRollBackups value=""10"" />");
            builder.AppendLine(@"<maximumFileSize value=""1000KB"" />");
            builder.AppendLine(@"<staticLogFileName value=""true"" />");
            builder.AppendLine(@"<layout type=""log4net.Layout.PatternLayout"">");
            builder.AppendLine(@"<conversionPattern value=""%d [%t] %-5p %c [%x] - %m%n"" />");
            builder.AppendLine(@"</layout>");
            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=" + "\"" + FilenotexistLogName + "\"" + "/>");
            builder.AppendLine(@"<acceptOnMatch value=""true"" />");
            builder.AppendLine(@"</filter>");
            builder.AppendLine(@"<filter type=""log4net.Filter.DenyAllFilter"" />");
            builder.AppendLine(@"</appender>");
            builder.AppendLine();
            //add FilenotExist Filter End           

            //add Security Filter Start
            builder.AppendLine(@"<appender name=""RollingFileAppenderSecurity"" type=""log4net.Appender.RollingFileAppender"">");
            builder.AppendLine(@"<file value=" + "\"" + default_trace_security + "\"" + "/>");
            builder.AppendLine(@"<appendToFile value=""true"" />");
            builder.AppendLine(@"<rollingStyle value=""Size"" />");
            builder.AppendLine(@"<maxSizeRollBackups value=""10"" />");
            builder.AppendLine(@"<maximumFileSize value=""1000KB"" />");
            builder.AppendLine(@"<staticLogFileName value=""true"" />");
            builder.AppendLine(@"<layout type=""log4net.Layout.PatternLayout"">");
            builder.AppendLine(@"<conversionPattern value=""%d [%t] %-5p %c [%x] - %m%n"" />");
            builder.AppendLine(@"</layout>");
            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=" + "\"" + SecurityLogName + "\"" + "/>");
            builder.AppendLine(@"<acceptOnMatch value=""true"" />");
            builder.AppendLine(@"</filter>");
            builder.AppendLine(@"<filter type=""log4net.Filter.DenyAllFilter"" />");
            builder.AppendLine(@"</appender>");
            builder.AppendLine();
            //add Security Filter End  

            //add Rollminute Start RollingStyle is  Date,yyyyMMdd-HHmm,per minute
            builder.AppendLine(@"<appender name=""RollingFileAppenderRollminute"" type=""log4net.Appender.RollingFileAppender"">");
            builder.AppendLine(@"<file value=" + "\"" + default_trace_rollminute + "\"" + "/>");
            builder.AppendLine(@"<appendToFile value=""true"" />");
            builder.AppendLine(@"<rollingStyle value=""Date"" />");
            builder.AppendLine(@"<datePattern value=""yyyyMMdd-HHmm"" />");
            builder.AppendLine(@"<staticLogFileName value=""true"" />");
            builder.AppendLine(@"<layout type=""log4net.Layout.PatternLayout"">");
            builder.AppendLine(@"<conversionPattern value=""%m%n"" />");
            builder.AppendLine(@"</layout>");
            builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            builder.AppendLine(@"<loggerToMatch value=" + "\"" + RollminuteLogName + "\"" + "/>");
            builder.AppendLine(@"<acceptOnMatch value=""true"" />");
            builder.AppendLine(@"</filter>");
            builder.AppendLine(@"<filter type=""log4net.Filter.DenyAllFilter"" />");
            builder.AppendLine(@"</appender>");
            builder.AppendLine();
            //add Rollminute End

            //builder.AppendLine(@"<appender name=""ErrorTrackerAppender"" type=""Yunqi.ErrorTracker.Client.ETLogAppender,Yunqi.ErrorTracker.Client"">");
            //builder.AppendLine(@"<filter type=""log4net.Filter.LoggerMatchFilter"">");
            //builder.AppendLine(@"<loggerToMatch value=""Yunqi.Tracing.RaytraceLogger""/>");
            //builder.AppendLine(@"<acceptOnMatch value=""false"" />");
            //builder.AppendLine(@"</filter>");
            //builder.AppendLine(@"<filter type=""log4net.Filter.LevelRangeFilter"">");
            //builder.AppendLine(@"<param name=""LevelMin"" value=""ERROR"" />");
            //builder.AppendLine(@"<param name=""LevelMax"" value=""FATAL"" />");
            //builder.AppendLine(@"</filter>");
            //builder.AppendLine(@"<filter type=""log4net.Filter.DenyAllFilter"" />");
            //builder.AppendLine(@"</appender>");
            //builder.AppendLine();

            builder.AppendLine(@"<root>");
            builder.AppendLine(@"<level value=""ERROR"" />");
            //builder.AppendLine(@"<appender-ref ref=""ErrorTrackerAppender"" />");
            builder.AppendLine(@"<appender-ref ref=""RollingFileAppender"" />");
            //builder.AppendLine(@"<appender-ref ref=""RollingFileAppenderConfiguration"" />");
            //builder.AppendLine(@"<appender-ref ref=""RollingFileAppenderTracing"" />");
            builder.AppendLine(@"<appender-ref ref=""RollingFileAppenderFileNotExist"" />");
            builder.AppendLine(@"<appender-ref ref=""RollingFileAppenderSecurity"" />");

            //default is off            
            // builder.AppendLine(@"<appender-ref ref=""RollingFileAppenderRollminute"" />");
            builder.AppendLine(@"</root>");
            builder.AppendLine(@"</log4net>");
            return builder.ToString();
        }


        #endregion
    }
}
