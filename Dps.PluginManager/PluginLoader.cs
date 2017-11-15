using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using Dps.Infrastructure;

namespace Dps.PluginManager
{
    [Serializable]
    public class PluginLoader
    {
        public List<IFilter> GetPlugins(DirectoryInfo pluginFolder)
        {
            var plugins = new List<IFilter>();
            var assemblies = pluginFolder.GetFiles("*.dll", SearchOption.AllDirectories)
                .Select(x => AssemblyName.GetAssemblyName(x.FullName))
                .Select(x => Assembly.Load(x.FullName));

            foreach (var assembly in assemblies)
            {
                var type = assembly.GetTypes().FirstOrDefault(t => t.GetInterface(typeof(IFilter).Name) != null);
                if (type != null)
                {
                    BuildManager.AddReferencedAssembly(assembly);
                    plugins.Add((IFilter)Activator.CreateInstance(type));
                }
            }
            return plugins;
        }
    }
}
