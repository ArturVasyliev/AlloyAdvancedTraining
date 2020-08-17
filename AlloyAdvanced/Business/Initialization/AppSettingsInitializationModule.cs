using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using System.Web.Mvc;
using System.Web.Routing;

namespace AlloyAdvanced.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class AppSettingsInitializationModule : IInitializableModule
    {
        private bool initialized = false;

        public void Initialize(InitializationEngine context)
        {
            if (!initialized)
            {
                RouteTable.Routes.MapRoute(
                    name: "AppSettings",
                    url: "appsettings/{action}",
                    defaults: new
                    {
                        controller = "AppSettings",
                        action = "Index"
                    });

                initialized = true;
            }
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}