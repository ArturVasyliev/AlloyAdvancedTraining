using EPiServer.Personalization.VisitorGroups;
using System.ComponentModel.DataAnnotations;

namespace AlloyAdvanced.Business.VisitorGroups
{
    public class CookieCriterionModel : CriterionModelBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Value { get; set; }

        public override ICriterionModel Copy()
        {
            return base.ShallowCopy();
        }
    }
}