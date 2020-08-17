using EPiServer.PlugIn;
using EPiServer.Scheduler;
using EPiServer.Web;
using System;
using System.Net.Http;

namespace AlloyAdvanced.Business.ScheduledJobs
{
    [ScheduledPlugIn(DisplayName = "Ping Site",
        GUID = "88E83CA0-93ED-46FD-A338-7938C8D0FDF9", SortIndex = -1)]
    public class PingSiteScheduledJob : ScheduledJobBase
    {
        public PingSiteScheduledJob()
        {
            IsStoppable = false; // hide the Stop button
        }

        public override string Execute()
        {
            var client = new HttpClient();
            client.BaseAddress = SiteDefinition.Current.SiteUrl;
            HttpResponseMessage response = client.GetAsync("").Result;
            return $"Pinged {client.BaseAddress} at {DateTime.Now} => {(int)response.StatusCode} {response.ReasonPhrase}";
        }
    }
}
