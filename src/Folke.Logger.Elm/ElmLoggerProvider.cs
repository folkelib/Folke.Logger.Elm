using System.Collections.Concurrent;
using Folke.Elm;
using Folke.Elm.Mapping;
using Microsoft.Extensions.Logging;

namespace Folke.Logger.Elm
{
    // This project can output the Class library as a NuGet Package.
    // To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
    public class ElmLoggerProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, ElmLogger> loggers = new ConcurrentDictionary<string, ElmLogger>();
        private readonly ElmLoggerSettings settings;

        public ElmLoggerProvider(IDatabaseDriver databaseDriver, string connectionString)
        {
            settings = new ElmLoggerSettings
            {
                DatabaseDriver = databaseDriver,
                Mapper = new Mapper(),
                ConnectionString = connectionString
            };

            using (var connection = FolkeConnection.Create(databaseDriver, settings.Mapper, connectionString))
            {
                connection.CreateOrUpdateTable<LogEntry>();
            }
        }

        public void Dispose()
        {
        }

        public ILogger CreateLogger(string categoryName)
        {
            return loggers.GetOrAdd(categoryName, name => new ElmLogger(name, settings));
        }
    }
}
