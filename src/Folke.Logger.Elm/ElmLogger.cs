using System;
using Folke.Elm;
using Microsoft.Extensions.Logging;

namespace Folke.Logger.Elm
{
    public class ElmLogger : ILogger
    {
        private readonly string categoryName;
        private readonly ElmLoggerSettings settings;

        public ElmLogger(string categoryName, ElmLoggerSettings settings)
        {
            this.categoryName = categoryName;
            this.settings = settings;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            using (var connection = FolkeConnection.Create(settings.DatabaseDriver, settings.Mapper, settings.ConnectionString))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var logEntry = new LogEntry
                    {
                        LogLevel = logLevel,
                        Category = categoryName,
                        Content = exception.ToString(), // formatter(state, exception),
                        DateTime = DateTime.UtcNow
                    };
                    connection.Save(logEntry);
                    transaction.Commit();
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= settings.MinLevel;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return new FakeDisposable();
        }

        private class FakeDisposable : IDisposable
        {
            public void Dispose()
            {
            }
        }
    }
}