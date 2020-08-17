using EPiServer;
using EPiServer.Cms.Shell.UI.Rest.ContentQuery;
using EPiServer.Core;
using EPiServer.Framework.Localization;
using EPiServer.Globalization;
using EPiServer.ServiceLocation;
using EPiServer.Shell.ContentQuery;
using EPiServer.UI.Report.Reports;
using System;
using System.Collections.Generic;

namespace AlloyAdvanced.Business.Tasks
{
    [ServiceConfiguration(typeof(IContentQuery))]
    public class MyTaskPlugInQuery : ContentQueryBase
    {
        private readonly LocalizationService _localizationService;
        private readonly IContentRepository _contentRepository;
        private readonly ExpiredPagesData _expiredPagesData;

        public MyTaskPlugInQuery(LocalizationService localizationService,
              IContentQueryHelper queryHelper,
              IContentRepository contentRepository,
              ExpiredPagesData expiredPagesData) : base(contentRepository, queryHelper)
        {
            _localizationService = localizationService;
            _contentRepository = contentRepository;
            _expiredPagesData = expiredPagesData;
        }

        public override string Name
        {
            get { return "expiredpages"; }
        }

        public override string DisplayName
        {
            get
            {
                return "Expired pages";
                //return _localizationService.GetString("/add/your/path/to/translation");
            }
        }

        public override IEnumerable<string> PlugInAreas
        {
            get { return new string[] { "editortasks" }; }
        }

        protected override IEnumerable<IContent> GetContent(ContentQueryParameters parameters)
        {
            var expiredPages = _expiredPagesData.GetPages(
                ContentReference.StartPage,
               DateTime.MinValue,
               DateTime.Now,
               ContentLanguage.PreferredCulture.Name,
               false,
               String.Empty,
               50,
               0,
               out _);

            var list = new List<IContent>();
            list.AddRange(expiredPages);

            return list;
        }
    }
}