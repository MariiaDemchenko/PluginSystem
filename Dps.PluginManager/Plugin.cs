using Dps.Infrastructure;

namespace Dps.PluginManager
{
    public class Plugin
    {
        public IFilter Module { get; set; }
        public bool Enabled { get; set; }
        public string Parameter { get; set; }
    }
}
