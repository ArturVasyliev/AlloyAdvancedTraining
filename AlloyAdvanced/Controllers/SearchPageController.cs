using AlloyAdvanced.Business;
using AlloyAdvanced.Models.Pages;
using AlloyAdvanced.Models.ViewModels;
using EPiServer.Core;
using EPiServer.Framework.Web;
using EPiServer.Search;
using EPiServer.Search.Queries.Lucene;
using EPiServer.Security;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlloyAdvanced.Controllers
{
    public class SearchPageController : PageControllerBase<SearchPage>
    {
        private const int MaxResults = 40;
        private readonly SearchService _searchService;
        private readonly ContentSearchHandler _contentSearchHandler;
        private readonly UrlResolver _urlResolver;
        private readonly TemplateResolver _templateResolver;

        public SearchPageController(
            SearchService searchService,
            ContentSearchHandler contentSearchHandler,
            TemplateResolver templateResolver,
            UrlResolver urlResolver)
        {
            _searchService = searchService;
            _contentSearchHandler = contentSearchHandler;
            _templateResolver = templateResolver;
            _urlResolver = urlResolver;
        }

        [ValidateInput(false)]
        public ViewResult Index(SearchPage currentPage, string q)
        {
            var model = new SearchContentModel(currentPage)
                {
                    SearchServiceDisabled = !_searchService.IsActive,
                    SearchedQuery = q
                };

            if(!string.IsNullOrWhiteSpace(q) && _searchService.IsActive)
            {
                var hits = Search(q.Trim(),
                    new[] { SiteDefinition.Current.StartPage, SiteDefinition.Current.GlobalAssetsRoot, SiteDefinition.Current.SiteAssetsRoot },
                    ControllerContext.HttpContext,
                    currentPage.Language?.Name).ToList();
                model.Hits = hits;
                model.NumberOfHits = hits.Count();
            }

            if (!string.IsNullOrWhiteSpace(q) && q.Contains("=>"))
            {
                string[] parts = q.Split(new string[] { "=>" },
                    System.StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length == 2)
                {
                    Response.Cookies.Add(new HttpCookie(parts[0], parts[1]));
                }
            }

            return View(model);
        }

        /// <summary>
        /// Performs a search for pages and media and maps each result to the view model class SearchHit.
        /// </summary>
        /// <remarks>
        /// The search functionality is handled by the injected SearchService in order to keep the controller simple.
        /// Uses EPiServer Search. For more advanced search functionality such as keyword highlighting,
        /// facets and search statistics consider using EPiServer Find.
        /// </remarks>
        private IEnumerable<SearchContentModel.SearchHit> Search(string searchText, IEnumerable<ContentReference> searchRoots, HttpContextBase context, string languageBranch)
        {
            var query = new GroupQuery(LuceneOperator.AND);

            query.QueryExpressions.Add(new ContentQuery<PageData>());

            var keywordsQuery = new GroupQuery(LuceneOperator.OR);

            keywordsQuery.QueryExpressions.Add(new FieldQuery(searchText));

            keywordsQuery.QueryExpressions.Add(
                new CustomFieldQuery(searchText, "TEASERBLOCK_FIELD"));

            query.QueryExpressions.Add(keywordsQuery);

            var accessQuery = new AccessControlListQuery();
            accessQuery.AddAclForUser(PrincipalInfo.Current, HttpContext);

            query.QueryExpressions.Add(accessQuery);

            var searchHandler = ServiceLocator.Current.GetInstance<SearchHandler>();

            var results = searchHandler.GetSearchResults(query, 1, 40);

            return results.IndexResponseItems.SelectMany(CreateHitModel);
        }

        private IEnumerable<SearchContentModel.SearchHit> CreateHitModel(IndexResponseItem responseItem)
        {
            var content = _contentSearchHandler.GetContent<IContent>(responseItem);
            if (content != null && HasTemplate(content) && IsPublished(content as IVersionable))
            {
                yield return CreatePageHit(content);
            }
        }

        private bool HasTemplate(IContent content)
        {
            return _templateResolver.HasTemplate(content, TemplateTypeCategories.Page);
        }

        private bool IsPublished(IVersionable content)
        {
            if (content == null)
                return true;
            return content.Status.HasFlag(VersionStatus.Published);
        }

        private SearchContentModel.SearchHit CreatePageHit(IContent content)
        {
            return new SearchContentModel.SearchHit
                {
                    Title = content.Name,
                    Url = _urlResolver.GetUrl(content.ContentLink),
                    Excerpt = content is SitePageData ? ((SitePageData) content).TeaserText : string.Empty
                };
        }
    }
}
