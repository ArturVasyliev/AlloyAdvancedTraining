using AlloyAdvanced.Models.ViewModels;
using EPiServer.Personalization;
using System.Net;

namespace AlloyAdvanced.Features.VisitorLocation
{
    public class VisitorLocationPageViewModel : PageViewModel<VisitorLocationPage>
    {
        public VisitorLocationPageViewModel(VisitorLocationPage currentPage) : base(currentPage)
        {
        }

        public IPAddress IPAddress { get; set; }
        public IGeolocationResult GeolocationResult { get; set; }
        public string GoogleMapSrc { get; set; }
    }
}