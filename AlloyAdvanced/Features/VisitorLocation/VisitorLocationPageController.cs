using AlloyAdvanced.Controllers;
using EPiServer.Personalization;
using System;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace AlloyAdvanced.Features.VisitorLocation
{
    public class VisitorLocationPageController : PageControllerBase<VisitorLocationPage>
    {
        private readonly IGeolocationProvider geolocationProvider;

        public VisitorLocationPageController(IGeolocationProvider geolocationProvider)
        {
            this.geolocationProvider = geolocationProvider;
        }

        public ActionResult Index(VisitorLocationPage currentPage)
        {
            var viewmodel = new VisitorLocationPageViewModel(currentPage);

            viewmodel.IPAddress = GetIPAddress(Request);
            viewmodel.GeolocationResult = geolocationProvider.Lookup(viewmodel.IPAddress);

            viewmodel.GoogleMapSrc = "https://www.google.com/maps/embed/v1/view" +
                "?key=YOUR_API_KEY" +
                "&center=" +
                viewmodel.GeolocationResult.Location.Latitude + "," +
                viewmodel.GeolocationResult.Location.Longitude +
                "&zoom=18" +
                "&maptype=satellite";

            if (viewmodel.GeolocationResult != null)
            {
                Response.Cookies.Add(new HttpCookie("epi-visitor-country-code",
                    viewmodel.GeolocationResult.CountryCode)
                    { Expires = DateTime.Now.AddDays(90) });
            }

            return View("~/Features/VisitorLocation/VisitorLocationPage.cshtml", viewmodel);
        }

        private IPAddress GetIPAddress(HttpRequestBase request)
        {
            string episerverWorld = "217.114.90.249"; // world.episerver.com host server
            string ipAddress;
            string ipAddresses = request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (request.IsLocal)
            {
                return IPAddress.Parse(episerverWorld);
            }

            if (!string.IsNullOrEmpty(ipAddresses))
            {
                string[] addresses = ipAddresses.Split(',');
                ipAddress = addresses[0];
            }
            else
            {
                ipAddress = request.ServerVariables["REMOTE_ADDR"];
            }

            if (IPAddress.TryParse(ipAddress, out IPAddress address))
            {
                return address;
            }
            else
            {
                return null;
            }
        }
    }
}