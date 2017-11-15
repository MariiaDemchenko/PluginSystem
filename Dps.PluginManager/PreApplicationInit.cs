using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Hosting;
using Dps.PluginManager;
using static Dps.PluginManager.PluginManager;

[assembly: PreApplicationStartMethod(typeof(PreApplicationInit), "InitializePlugins")]

namespace Dps.PluginManager
{
    public class PreApplicationInit
    {
        static PreApplicationInit()
        {
            var pluginsPath = HostingEnvironment.MapPath("~/plugins");

            if (pluginsPath == null)
                throw new DirectoryNotFoundException("plugins");

            PluginFolder = new DirectoryInfo(pluginsPath);
        }

        private static readonly DirectoryInfo PluginFolder;

        public static void InitializePlugins()
        {
            Directory.CreateDirectory(PluginFolder.FullName);

            var domain = AppDomain.CreateDomain("PluginsDomain");
            var finder = (PluginLoader)domain.CreateInstanceFromAndUnwrap(
                Assembly.GetExecutingAssembly().Location, typeof(PluginLoader).FullName);

            var modules = finder.GetPlugins(PluginFolder);

            AppDomain.Unload(domain);

            Plugins.AddRange(modules.Select(m => new Plugin { Module = m, Enabled = false, Parameter = m.DefaultParameter}));
        }
    }
}
