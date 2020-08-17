using AlloyAdvanced.Controllers;
using AlloyAdvanced.Models.NorthwindEntities;
using AlloyAdvanced.Models.ViewModels;
using EPiServer.Web.Routing;
using System.Collections.Generic;
using System.Web.Mvc;

namespace AlloyAdvanced.Features.NorthwindPartialRouter
{
    public class CategoriesPageController : PageControllerBase<CategoriesPage>
    {
        private readonly UrlResolver urlResolver;

        public CategoriesPageController(UrlResolver urlResolver)
        {
            this.urlResolver = urlResolver;
        }

        public ActionResult Index(CategoriesPage currentPage)
        {
            var model = PageViewModel.Create(currentPage);

            // connect to the Northwind database through the domain model
            // to fetch a list of all categories and pass it to the Northwind page
            // instance to show a list of category names and links generated
            // by the partial router. 

            var db = new Northwind();

            // we do not need to track changes or 
            // automatically load related entities
            db.Configuration.ProxyCreationEnabled = false;
            db.Configuration.LazyLoadingEnabled = false;

            model.CurrentPage.CategoryLinks = new Dictionary<string, string>();

            foreach (Category category in db.Categories)
            {
                string name = category.CategoryName;

                string url = urlResolver.GetVirtualPathForNonContent(
                    partialRoutedObject: category, language: null,
                    virtualPathArguments: null).GetUrl();

                model.CurrentPage.CategoryLinks.Add(name, url);
            }

            return View("~/Features/NorthwindPartialRouter/CategoriesPage.cshtml", model);
        }
    }
}