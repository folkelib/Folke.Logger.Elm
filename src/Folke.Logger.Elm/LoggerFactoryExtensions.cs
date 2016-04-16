using Folke.Elm;
using Microsoft.Extensions.Logging;

namespace Folke.Logger.Elm
{
    public static class LoggerFactoryExtensions
    {
        public static ILoggerFactory AddElm(this ILoggerFactory loggerFactory, IDatabaseDriver databaseDriver, string connectionString, LogLevel minLevel = LogLevel.Debug)
        {
            loggerFactory.AddProvider(new ElmLoggerProvider(databaseDriver, connectionString, minLevel));
            return loggerFactory;
        }
    }
}
