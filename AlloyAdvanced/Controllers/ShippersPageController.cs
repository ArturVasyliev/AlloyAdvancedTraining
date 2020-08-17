using AlloyAdvanced.Models.Pages;
using AlloyAdvanced.Models.ViewModels;
using EPiServer;
using System.Web.Mvc;

namespace AlloyAdvanced.Controllers
{
    public class ShippersPageController : PageControllerBase<ShippersPage>
    {
        private readonly IContentLoader loader;

        public ShippersPageController(IContentLoader loader)
        {
            this.loader = loader;
        }

        public ActionResult Index(ShippersPage currentPage)
        {
            var model = new ShippersPageViewModel(currentPage);
            model.Shippers = loader.GetChildren<ShipperPage>(currentPage.ContentLink);
            return View(model);
        }
    }
}