using log4net;
using log4net.Config;

namespace NotepadSharp.Utils
{
    public static class LoggingHelper
    {
        private static readonly log4net.Repository.ILoggerRepository _repository = LogManager.GetRepository();
        public static TextBoxAppender TextBoxAppender { get; private set; }
        
        public static void LogEntry(SystemCategories[] categories, string message)
        {
            XmlConfigurator.Configure();
            ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
            foreach (SystemCategories category in categories)
            {
                log.Logger.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType, _repository.LevelMap[category.ToString()], message, null);
            }
        }

        public static void LogEntry(SystemCategories category, string message)
        {
            LogEntry(new SystemCategories[] { category }, message);
        }
    }
    public enum SystemCategories
    {
        GeneralDebug,
        GeneralInformation,
        GeneralWarning,
        GeneralError,
        GeneralUnknown,
        GeneralUIDebug,
        GeneralUIError
    }
}
