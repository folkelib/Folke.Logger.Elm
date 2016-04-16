using Folke.Elm;
using Folke.Elm.Mapping;

namespace Folke.Logger.Elm
{
    public class ElmLoggerSettings
    {
        public IDatabaseDriver DatabaseDriver { get; set; }
        public Mapper Mapper { get; set; }
        public string ConnectionString { get; set; }
    }
}
