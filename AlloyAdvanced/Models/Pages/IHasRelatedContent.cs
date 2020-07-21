using EPiServer.Core;

namespace AlloyAdvanced.Models.Pages
{
    public interface IHasRelatedContent
    {
        ContentArea RelatedContentArea { get; }
    }
}
