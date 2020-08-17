using EPiServer;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Web.Routing;
using System.Web.Routing;

namespace AlloyAdvanced.Features.NorthwindPartialRouter
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class CategoryPartialRouterRegistration : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            RouteTable.Routes.RegisterPartialRouter(
                new CategoryPartialRouter(
                    context.Locate.Advanced.GetInstance<IContentLoader>()));
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}