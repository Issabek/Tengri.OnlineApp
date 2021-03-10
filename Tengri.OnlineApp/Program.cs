using log4net;
using log4net.Config;
namespace Tengri.OnlineApp
{
    class Program
    {
        private static ILog log = LogManager.GetLogger("LOGGER");
        static void Main(string[] args)
        {
            XmlConfigurator.Configure();
            log.Info("Does work");
            ServiceMenu.tengriUI();
        }
    }
}

