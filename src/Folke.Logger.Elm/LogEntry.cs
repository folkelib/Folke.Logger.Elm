using System;
using Folke.Elm;
using Microsoft.Extensions.Logging;

namespace Folke.Logger.Elm
{
    public class LogEntry : IFolkeTable
    {
        public int Id { get; set; }

        public string Category { get; set; }

        public DateTime DateTime { get; set; }

        public LogLevel LogLevel { get; set; }

        public string Content { get; set; }
    }
}
