using Folke.Elm;
using Folke.Elm.Mapping;
using Microsoft.Extensions.Logging;

namespace Folke.Logger.Elm
{
    public class ElmLoggerSettings
    {
        public IDatabaseDriver DatabaseDriver { get; set; }
        public Mapper Mapper { get; set; }
        public string ConnectionString { get; set; }
        public LogLevel MinLevel { get; set; }
    }
}
