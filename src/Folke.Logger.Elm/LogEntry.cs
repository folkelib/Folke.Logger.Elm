using System;
using System.ComponentModel.DataAnnotations;
using Folke.Elm;
using Microsoft.Extensions.Logging;

namespace Folke.Logger.Elm
{
    public class LogEntry : IFolkeTable
    {
        /// <summary>Gets or sets the unique identifier</summary>
        public int Id { get; set; }

        /// <summary>Gets or sets the category</summary>
        public string Category { get; set; }

        /// <summary>Gets or sets the creation date</summary>
        public DateTime DateTime { get; set; }

        /// <summary>Gets or sets the log level</summary>
        public LogLevel LogLevel { get; set; }

        /// <summary>Gets or sets the content of the log entry</summary>
        [MaxLength(5000)]
        public string Content { get; set; }
    }
}
