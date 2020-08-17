using AlloyAdvanced.Helpers;
using AlloyAdvanced.Models.Blocks;
using AlloyAdvanced.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;
using EPiServer.Search.IndexingService;
using EPiServer.ServiceLocation;
using Lucene.Net.Documents;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AlloyAdvanced.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Web.InitializationModule))]
    public class SearchBlocksInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            IndexingService.DocumentAdding += IndexingService_DocumentAdding;
        }

        private void IndexingService_DocumentAdding(object sender, EventArgs e)
        {
            var args = e as AddUpdateEventArgs;

            if (args == null) return;

            Document doc = args.Document;

            PageData page = doc.GetContent<IContent>() as PageData;

            if (page != null && page is ProductPage)
            {
                ProductPage product = page as ProductPage;

                if (product.MainContentArea == null) return;

                var loader = ServiceLocator.Current
                    .GetInstance<IContentLoader>();

                IEnumerable<IContent> items = product.MainContentArea.FilteredItems
                    .Select(item => loader.Get<IContent>(item.ContentLink));

                foreach (IContent item in items)
                {
                    if (item is TeaserBlock)
                    {
                        var teaser = item as TeaserBlock;
                        doc.Add(new Field("TEASERBLOCK_FIELD", teaser.Text,
                            Field.Store.NO, Field.Index.ANALYZED));
                    }
                }
            }
        }

        public void Uninitialize(InitializationEngine context)
        {
            IndexingService.DocumentAdding -= IndexingService_DocumentAdding;
        }
    }
}