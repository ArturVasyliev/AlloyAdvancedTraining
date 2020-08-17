using AlloyAdvanced.Models;
using EPiServer.Shell.ObjectEditing;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.Selectors
{
    public class YouTubeSelectionFactory : ISelectionFactory
    {
        public IEnumerable<ISelectItem> GetSelections(ExtendedMetadata metadata)
        {
            return EpiserverYouTube.Videos;
        }
    }
}