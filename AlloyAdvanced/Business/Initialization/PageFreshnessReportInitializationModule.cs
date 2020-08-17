using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlloyAdvanced.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class PageFreshnessReportInitializationModule 
        : IInitializableModule
    {
        private bool initialized = false;

        public void Initialize(InitializationEngine context)
        {
            if (!initialized)
            {
                RouteTable.Routes.MapRoute(
                    name: "PageFreshnessReport",
                    url: "pagefreshnessreport/{action}",
                    defaults: new { controller = "PageFreshnessReport", action = "Index" });

                initialized = true;
            }
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}