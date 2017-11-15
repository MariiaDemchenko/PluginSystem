using System.Collections.Generic;

namespace Dps.PluginManager
{
    public class PluginManager
    {
        public static List<Plugin> Plugins { get; set; }

        static PluginManager()
        {
            Plugins = new List<Plugin>();
        }
    }
}
