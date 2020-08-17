using EPiServer.Personalization.VisitorGroups;
using System;
using System.Security.Principal;
using System.Web;

namespace AlloyAdvanced.Business.VisitorGroups
{
    [VisitorGroupCriterion(Category = "Site Criteria", Description = 
        "Checks for the existence of a named cookie with a set value.",
        DisplayName = "Cookie")]
    public class CookieCriterion : CriterionBase<CookieCriterionModel>
    {
        public override bool IsMatch(IPrincipal principal, 
            HttpContextBase httpContext)
        {
            HttpCookie cookie = httpContext.Request.Cookies.Get(Model.Name);
            if (cookie == null) return false;
            return (cookie.Value.Equals(Model.Value, 
                StringComparison.InvariantCultureIgnoreCase));
        }
    }
}