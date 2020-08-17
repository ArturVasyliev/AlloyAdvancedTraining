using EPiServer;
using EPiServer.Core;
using EPiServer.ServiceLocation;
using Lucene.Net.Documents;
using System;

namespace AlloyAdvanced.Helpers
{
    public static class DocumentExtensions
    {
        public static T GetContent<T>(this Document doc) where T : IContent
        {
            const string fieldName = "EPISERVER_SEARCH_ID";

            string fieldValue = doc.Get(fieldName);

            if (string.IsNullOrWhiteSpace(fieldValue))
            {
                throw new NotSupportedException(
                    $"Specified document did not have a '{fieldName}' value.");
            }

            string[] fieldFragments = fieldValue.Split('|');

            Guid contentGuid;

            if (!Guid.TryParse(fieldFragments[0], out contentGuid))
            {
                throw new NotSupportedException(
                    "Expected first part of ID field to be a valid GUID.");
            }

            return ServiceLocator.Current
                .GetInstance<IContentLoader>()
                .Get<T>(contentGuid);
        }
    }
}