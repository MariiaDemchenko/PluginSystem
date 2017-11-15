using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Dps.PluginManager;

namespace Dps.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new List<PluginMeta>();

            model.AddRange(PluginManager.PluginManager.Plugins.
                Select(p => new PluginMeta
                {
                    Name = p.Module.Name,
                    Enabled = p.Enabled,
                    Parameter = p.Parameter
                }));
            return View(model);
        }

        public ActionResult Plugins(List<PluginMeta> model)
        {
            return PartialView(model);
        }

        public ActionResult Examinations()
        {
            return PartialView();
        }
    }
}