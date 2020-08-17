using EPiServer.DataAnnotations;

namespace AlloyAdvanced.Models.Pages
{
    [SiteContentType(DisplayName = "Shippers", 
        Description = "Displays a list of imported shippers.")]
    [SiteImageUrl]
    [AvailableContentTypes(
        Availability = EPiServer.DataAbstraction.Availability.Specific,
        Include = new[] { typeof(ShipperPage) },
        IncludeOn = new[] { typeof(StartPage) })]
    public class ShippersPage : SitePageData
    {
        public virtual int DefaultShipper { get; set; }
    }
}