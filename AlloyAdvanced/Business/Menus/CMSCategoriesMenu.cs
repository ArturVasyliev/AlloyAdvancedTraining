using EPiServer.Security;
using EPiServer.Shell.Navigation;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.Menus
{
    [MenuProvider]
    public class CMSCategoriesMenu : IMenuProvider
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return new MenuItem[]
            {
                new UrlMenuItem(
                    text: "Categories", 
                    path: "/global/cms/categories",
                    url: "/EPiServer/CMS/admin/Categories.aspx")
                {
                    IsAvailable = httpRequest => PrincipalInfo.HasEditAccess,
                    SortIndex = 100
                }
            };
        }
    }
}