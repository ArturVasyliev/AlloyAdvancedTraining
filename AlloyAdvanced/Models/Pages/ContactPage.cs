using System.ComponentModel.DataAnnotations;
using AlloyAdvanced.Business.Rendering;
using EPiServer.Web;
using EPiServer.Core;
using AlloyAdvanced.Business.Selectors;
using EPiServer.Shell.ObjectEditing;

namespace AlloyAdvanced.Models.Pages
{
    /// <summary>
    /// Represents contact details for a contact person
    /// </summary>
    [SiteContentType(
        GUID = "F8D47655-7B50-4319-8646-3369BA9AF05B",
        GroupName = Global.GroupNames.Specialized)]
    [SiteImageUrl(Global.StaticGraphicsFolderPath + "page-type-thumbnail-contact.png")]
    public class ContactPage : SitePageData, IContainerPage
    {
        [Display(GroupName = Global.GroupNames.Contact)]
        [UIHint(UIHint.Image)]
        public virtual ContentReference Image { get; set; }

        [Display(GroupName = Global.GroupNames.Contact)]
        public virtual string Phone { get; set; }

        [Display(GroupName = Global.GroupNames.Contact)]
        [EmailAddress]
        public virtual string Email { get; set; }

        [Display(
            Name = "Region",
            GroupName = Global.GroupNames.Contact,
            Order = 10)]
        [SelectOneEnum(typeof(Region))]
        public virtual Region Region { get; set; }

        [Display(
            Name = "YouTube video",
            GroupName = Global.GroupNames.Contact,
            Order = 20)]
        [SelectOne(SelectionFactoryType = typeof(YouTubeSelectionFactory))]
        public virtual string YouTubeVideo { get; set; }

        [Display(
            Name = "Home city",
            GroupName = Global.GroupNames.Contact,
            Order = 30)]
        [SelectOne(SelectionFactoryType = typeof(CitySelectionFactory))]
        public virtual string HomeCity { get; set; }

        [Display(
            Name = "Other cities",
            GroupName = Global.GroupNames.Contact,
            Order = 40)]
        [SelectMany(SelectionFactoryType = typeof(CitySelectionFactory))]
        public virtual string OtherCities { get; set; }
    }
}
