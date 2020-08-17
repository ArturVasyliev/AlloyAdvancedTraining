using AlloyAdvanced.Models.Pages;
using EPiServer.Cms.TinyMce.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.ServiceLocation;

namespace AlloyAdvanced.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(TinyMceInitialization))]
    public class ConfigureTinyMCEToolbarInitializationModule : IConfigurableModule
    {
        public void ConfigureContainer(ServiceConfigurationContext context)
        {
            context.Services.Configure<TinyMceConfiguration>(config =>
            {
                config.Default()
                    .ContentCss("/static/css/editor.css");

                TinyMceSettings settings = config
                    .For<ProductPage>(page => page.MainBody, 
                    copyFrom: config.Empty());

                settings.DisableMenubar()
                    .AddPlugin("image charmap emoticons paste epi-link epi-image-editor epi-personalized-content fullscreen")
                    .Toolbar("formatselect | bold italic | epi-link image epi-image-editor epi-personalized-content | outdent indent | charmap emoticons paste removeformat | fullscreen")
                    .AddSetting("charmap_append", new[]
                    {
                        new object[] { 9861, "Dice number 6" },
                        new object[] { 9925, "Sun behind cloud" }
                    });
            });
        }

        public void Initialize(InitializationEngine context) { }

        public void Uninitialize(InitializationEngine context) { }
    }
}