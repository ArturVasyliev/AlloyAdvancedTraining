using AlloyAdvanced.Models.NorthwindEntities;
using AlloyAdvanced.Models.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.Web;
using EPiServer.Web.Routing;
using System.Linq;
using System.Web.Mvc;

namespace AlloyAdvanced.Features.NorthwindPartialRouter
{
    // by implementing IRenderTemplate<Category>, this becomes
    // the "page template" for a Category entity
    public class CategoryController : Controller, IRenderTemplate<Category>
    {
        private readonly IContentLoader contentLoader;

        public CategoryController(IContentLoader contentLoader)
        {
            this.contentLoader = contentLoader;
        }

        public ActionResult Index()
        {
            // Note: the GetRoutedData extension method uses the partial router to 
            // convert a URL segment into a Category instance. 
            var category = Request.RequestContext.GetRoutedData<Category>();

            var categoriesPages = contentLoader
                .GetChildren<CategoriesPage>(ContentReference.StartPage);

            CategoriesPage currentPage = null;
            if (categoriesPages.Count() > 0)
            {
                currentPage = categoriesPages.First();
            }

            var model = PageViewModel.Create(currentPage);
            model.CurrentPage.NorthwindCategory = category;

            return View("~/Features/NorthwindPartialRouter/Category.cshtml", model);
        }
    }
}