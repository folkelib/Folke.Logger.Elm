﻿using System;
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

        public void Log(LogLevel logLevel, int eventId, object state, Exception exception, Func<object, Exception, string> formatter)
        {
            using (var connection = FolkeConnection.Create(settings.DatabaseDriver, settings.Mapper, settings.ConnectionString))
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var logEntry = new LogEntry
                    {
                        LogLevel = logLevel,
                        Category = categoryName,
                        Content = formatter(state, exception)
                    };
                    connection.Save(logEntry);
                    transaction.Commit();
                }
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScopeImpl(object state)
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