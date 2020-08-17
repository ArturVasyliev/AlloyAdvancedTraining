using System;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Shell.UI;

namespace AlloyMvcTemplates
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    internal class DateFormatModifier : IInitializableModule
    {
        private void DojoConfigOnSerializing(object sender, EventArgs eventArgs)
        {
            var config = (DojoConfig) sender;
            if (config.Locale.StartsWith("en"))
            {
                config.Locale = "en-gb";
            }
        }

        public void Initialize(InitializationEngine context)
        {
            DojoConfig.Serializing += DojoConfigOnSerializing;
        }

        public void Uninitialize(InitializationEngine context)
        {
            DojoConfig.Serializing -= DojoConfigOnSerializing;
        }
    }
}
