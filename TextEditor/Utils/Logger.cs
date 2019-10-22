using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Repository.Hierarchy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScintillaNET.Demo.Utils
{
    public static class Logger
    {
        public static ILog Log { get; } = LogManager.GetLogger("LOGGER");
        public static MemoryAppender MemoryAppender { get; private set; }

        public static void InitLogger()
        {
            XmlConfigurator.Configure(); 
            Hierarchy hierarchy = LogManager.GetRepository() as Hierarchy;
            MemoryAppender = hierarchy.Root.GetAppender("MemoryAppender") as MemoryAppender;
        }
    }
}
