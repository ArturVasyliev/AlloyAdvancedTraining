using AlloyAdvanced.Models;
using AlloyAdvanced.Models.Pages;
using EPiServer.DataAnnotations;

namespace AlloyAdvanced.Features.VisitorLocation
{
    [SiteContentType(DisplayName = "Visitor Location", 
        GUID = "436dd4b1-4887-48e3-9fce-828e1f62d7d4", 
        Description = "Use this page to show information about the visitor's location.")]
    [SiteImageUrl]
    [AvailableContentTypes(IncludeOn = new[] { typeof(StartPage) })]
    public class VisitorLocationPage : SitePageData
    {
    }
}