using log4net;

namespace HelixProject
{
    public static class Logger
    {
        private static ILog log = LogManager.GetLogger("LOGGER");

        public static ILog Log
        {
            get { return log; }
        }

        public static void InitLogger()
        {
            log4net.Config.XmlConfigurator.Configure();
        }
    }
}
