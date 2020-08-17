using NorthwindWebApi.Models;
using System;
using System.IO;
using System.Web.Http;

namespace NorthwindWebApi.Controllers
{
    public class ShipperFormController : ApiController
    {
        public string Get()
        {
            return "ShipperFormController says, 'Hi!'";
        }

        public void Post(Shipper shipper)
        {
            string add_data = System.Web.HttpContext.Current.Server.MapPath(@"~\App_Data");

            string path = Path.Combine(add_data, 
                $"shipperform_{DateTime.Now.ToString("yyyy-MM-dd_hh-mm-ss")}");

            File.WriteAllText(path, $"{shipper.ShipperID}: {shipper.CompanyName}");
        }
    }
}
