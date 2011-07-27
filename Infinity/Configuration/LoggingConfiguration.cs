using System;
using log4net;

namespace Infinity.Configuration
{
    public class LoggingConfiguration
    {
        public static ILog Logger { get; private set; }

        static LoggingConfiguration()
        {
            Logger = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            log4net.Config.XmlConfigurator.Configure();
        }

        public static void LogAndThrowFatal(Exception e)
        {
            LogFatal(e);
            throw e;
        }

        public static void LogFatal(Exception e)
        { Logger.Fatal(e); }

        public static void LogAndThrowError(Exception e)
        {
            LogError(e);
            throw e;
        }

        public static void LogError(Exception e)
        { Logger.Error(e); }

        public static void LogInfo(string message)
        { Logger.Info(message); }

        public static void LogDebug(string message)
        { Logger.Debug(message); }
    }
}
