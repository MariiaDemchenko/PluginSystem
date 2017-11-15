using System.Collections.Generic;
using System.Net.Http;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using LinqKit;
using System.Linq;
using System.Web.Http;
using Dps.Infrastructure.Models;
using Dps.PluginManager;

namespace Dps.Web.Controllers
{
    public class ExaminationsController : ApiController
    {
        [System.Web.Mvc.HttpPost]
        public HttpResponseMessage Post (DataSourceLoadOptions loadOptions, [FromBody]List<PluginMeta> pluginMetas)
        {
            var sourcePlugins = PluginManager.PluginManager.Plugins;

            var currentPlugins = (from sp in sourcePlugins
                join pm in pluginMetas
                on sp.Module.Name equals pm.Name
                where pm.Enabled
                select new Plugin
                {
                    Module = sp.Module,
                    Enabled = pm.Enabled,
                    Parameter = pm.Parameter
                }).ToList();

            var predicate = PredicateBuilder.New<Examination>();
            foreach (var plugin in currentPlugins)
            {
                predicate.And(plugin.Module.Filter(plugin.Parameter));
            }

            List<Examination> source;
            using (var context = new DiagnosticsEntities())
            {
                source = !currentPlugins.Any()?
                    context.Examinations.ToList():
                    context.Examinations.Where(predicate).ToList();
            }
            return Request.CreateResponse(DataSourceLoader.Load(source, loadOptions));
        }
    }
}