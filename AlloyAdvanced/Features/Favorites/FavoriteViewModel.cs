using EPiServer.Core;
using System.Collections.Generic;

namespace AlloyAdvanced.Features.Favorites
{
    public class FavoriteViewModel
    {
        public List<Favorite> Favorites { get; set; }
        public ContentReference CurrentPageContentReference { get; set; }
    }
}