using EPiServer.Core;
using EPiServer.Find;
using EPiServer.Find.Cms;
using EPiServer.Find.Framework;
using EPiServer.Find.UnifiedSearch;
using EPiServer.Framework;
using EPiServer.Framework.Initialization;

namespace AlloyFind.Business.Initialization
{
    [InitializableModule]
    [ModuleDependency(typeof(EPiServer.Find.Cms.Module.IndexingModule))]
    public class MultisiteFindInitializationModule : IInitializableModule
    {
        public void Initialize(InitializationEngine context)
        {
            var setup = new CmsUnifiedSearchSetUp();
            IUnifiedSearchRegistry registry = SearchClient.Instance.Conventions.UnifiedSearchRegistry;

            registry.Add<PageData>()
                .PublicSearchFilter((IClient c, ISearchContext ctx) => c.BuildFilter<IContentData>()
                    .FilterForVisitor(
                        ctx.ContentLanguage == null || ctx.ContentLanguage == Language.None ? 
                        Languages.AllLanguagesSuffix : ctx.ContentLanguage.FieldSuffix)
                    .ExcludeContainerPages()
                    .ExcludeContentFolders()
                    // .FilterOnCurrentSite() // this is what the default does
                    )
                .CustomizeIndexProjection(setup.CustomizeIndexProjectionForPageData)
                .CustomizeProjection(setup.CustomizeProjectionForPageData);

            registry.Add<MediaData>()
                .PublicSearchFilter((c, ctx) => c.BuildFilter<IContentData>()
                    .FilterForVisitor(
                        ctx.ContentLanguage == null || ctx.ContentLanguage == Language.None ?
                        Languages.AllLanguagesSuffix : ctx.ContentLanguage.FieldSuffix)
                    .ExcludeContentFolders()
                    // .FilterOnCurrentSite() // this is what the default does
                    )
                .CustomizeIndexProjection(setup.CustomizeIndexProjectionForMediaData)
                .CustomizeProjection(setup.CustomizeProjectionForMediaData);
        }

        public void Uninitialize(InitializationEngine context) { }
    }
}