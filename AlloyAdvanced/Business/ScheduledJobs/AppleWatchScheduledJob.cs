using AlloyAdvanced.Models.Pages;
using EPiServer;
using EPiServer.Core;
using EPiServer.Filters;
using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;

namespace AlloyAdvanced.Business.ScheduledJobs
{
    [ScheduledPlugIn(DisplayName = "Apple Watch Fixer",
        GUID = "88E83CA0-94ED-46FD-A338-7938C8D0FDF9", SortIndex = -1)]
    public class AppleWatchFixerScheduledJob : ScheduledJobBase
    {
        private const string jobName = "Apple Watch Fixer";
        private bool _stopSignaled;

        public AppleWatchFixerScheduledJob()
        {
            IsStoppable = true;
        }

        public override void Stop()
        {
            _stopSignaled = true;
        }

        public override string Execute()
        {
            OnStatusChanged($"Starting execution of {jobName} job...");

            var finder = ServiceLocator.Current.GetInstance<IPageCriteriaQueryService>();

            var criteria = new PropertyCriteriaCollection();
            criteria.Add(new PropertyCriteria
            {
                Type = PropertyDataType.LongString,
                Name = "PageName", // you cannot use Name
                Condition = CompareCondition.Contained,
                Value = "iWatch"
            });
            criteria.Add(new PropertyCriteria
            {
                Type = PropertyDataType.LongString,
                Name = "MetaTitle",
                Condition = CompareCondition.Contained,
                Value = "iWatch"
            });
            criteria.Add(new PropertyCriteria
            {
                Type = PropertyDataType.LongString,
                Name = "MetaDescription",
                Condition = CompareCondition.Contained,
                Value = "iWatch"
            });

            PageDataCollection matches = finder.FindPagesWithCriteria(
                ContentReference.RootPage, criteria);

            int total = matches.Count;
            int count = 0;

            var repo = ServiceLocator.Current.GetInstance<IContentRepository>();

            foreach (PageData page in matches)
            {
                if (_stopSignaled)
                {
                    return $"{jobName} job was stopped. {count} of {total} pieces of content were corrected.";
                }

                SitePageData sitepage = page.CreateWritableClone() as SitePageData;
                if (sitepage != null)
                {
                    sitepage.Name = sitepage.Name.Replace("iWatch", "Apple Watch");
                    sitepage.MetaDescription = sitepage.MetaDescription.Replace("iWatch", "Apple Watch");
                    sitepage.MetaTitle = sitepage.MetaTitle.Replace("iWatch", "Apple Watch");

                    repo.Save(content: sitepage, 
                        action: EPiServer.DataAccess.SaveAction.Publish, 
                        access: EPiServer.Security.AccessLevel.NoAccess);

                    count++;
                }

                OnStatusChanged($"{jobName} is {count/(double)total*100:0.0}% complete. Please wait...");

                // pause for five seconds to simulate work, and
                // to allow us to test the Stop functionality ;)
                System.Threading.Thread.Sleep(5000);
            }

            return $"{jobName} job completed successfully. {count} of {total} pieces of content were corrected.";
        }
    }
}
