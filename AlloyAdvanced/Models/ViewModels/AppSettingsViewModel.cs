using System.Collections.Generic;
using System.Web.Mvc;

namespace AlloyAdvanced.Models.ViewModels
{
    public class AppSettingsViewModel
    {
        public IEnumerable<SelectListItem> Choices { get; set; }
        public string DebugView { get; set; }
        public string WebSockets { get; set; }
    }
}