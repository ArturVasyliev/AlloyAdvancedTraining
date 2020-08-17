using AlloyAdvanced.Models.Pages;
using EPiServer.Find.UnifiedSearch;

namespace AlloyAdvanced.Models.ViewModels
{
    public class FindSearchPageViewModel : PageViewModel<SearchPage>
    {
        public FindSearchPageViewModel(SearchPage currentPage,
            string searchQuery) : base(currentPage)
        {
            SearchQuery = searchQuery;
        }

        public string SearchQuery { get; private set; }

        public UnifiedSearchResults Results { get; set; }
    }
}