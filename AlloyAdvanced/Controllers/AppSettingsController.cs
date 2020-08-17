using AlloyAdvanced.Models.ViewModels;
using EPiServer.PlugIn;
using System.Configuration;
using System.Web.Configuration;
using System.Web.Mvc;

namespace AlloyAdvanced.Controllers
{
    [Authorize(Roles = "CmsAdmins")]
    [GuiPlugIn(Area = PlugInArea.AdminMenu,
        Url = "~/appsettings", DisplayName = "App Settings")]
    public class AppSettingsController : Controller
    {
        // keys for the appSettings
        private const string dvKey = "EPi.DebugView.Enabled";
        private const string wsKey = "EPi.WebSockets.Enabled";

        // GET: AppSettings
        public ActionResult Index(string save, string webSockets, string debugView)
        {
            if (!string.IsNullOrWhiteSpace(save))
            {
                Configuration config = WebConfigurationManager.OpenWebConfiguration("/");

                AddOrUpdateSetting(config.AppSettings.Settings, dvKey, debugView);
                AddOrUpdateSetting(config.AppSettings.Settings, wsKey, webSockets);

                config.Save();
            }

            var model = new AppSettingsViewModel
            {
                Choices = new[] {
                    new SelectListItem { Text = "true", Value = "true" },
                    new SelectListItem { Text = "false", Value = "false" }
                },
                DebugView = WebConfigurationManager.AppSettings.Get(dvKey) ?? "false",
                WebSockets = WebConfigurationManager.AppSettings.Get(wsKey) ?? "true"
            };

            return View(model);
        }

        private void AddOrUpdateSetting(KeyValueConfigurationCollection settings, 
            string key, string value)
        {
            var existing = settings[key];
            if (existing == null)
            {
                settings.Add(key, value);
            }
            else
            {
                existing.Value = value;
            }
        }
    }
}