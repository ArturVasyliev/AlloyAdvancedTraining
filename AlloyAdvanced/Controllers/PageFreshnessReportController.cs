using AlloyAdvanced.Models.ViewModels;
using EPiServer;
using EPiServer.Core;
using EPiServer.DataAbstraction;
using EPiServer.PlugIn;
using EPiServer.ServiceLocation;
using EPiServer.Shell.Security;
using EPiServer.Web.Mvc.Html;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AlloyAdvanced.Controllers
{
    [Authorize(Roles = "CmsAdmins,CmsEditors")]
    [GuiPlugIn(
        Area = PlugInArea.ReportMenu,
        Url = "~/pagefreshnessreport",
        Category = "Training Reports",
        DisplayName = "Page Freshness")]
    public class PageFreshnessReportController : Controller
    {
        public ActionResult Index(string changedBy, string showReport, string exportReport)
        {
            var roles = ServiceLocator.Current.GetInstance<UIRoleProvider>();

            var model = new PageFreshnessReportViewModel
            {
                Administrators = roles.GetUsersInRole("WebAdmins").ToArray(),
                Editors = roles.GetUsersInRole("ContentEditors").ToArray(),
                SelectedUser = changedBy,
                ShowReport = !string.IsNullOrWhiteSpace(showReport)
            };

            if (model.ShowReport || (!string.IsNullOrWhiteSpace(exportReport)))
            {
                var loader = ServiceLocator.Current.GetInstance<IContentLoader>();
                var versionRepo = ServiceLocator.Current.GetInstance<IContentVersionRepository>();

                var childRefs = loader.GetDescendents(ContentReference.StartPage)
                    .Union(new ContentReference[] { ContentReference.StartPage });

                var children = childRefs.Select(r => versionRepo.LoadPublished(r));

                if (string.IsNullOrWhiteSpace(model.SelectedUser) || model.SelectedUser == "Anyone")
                {
                    model.Top10FreshestPages = children
                        .OrderByDescending(c => c.Saved).Take(10);

                    model.Top10LeastFreshPages = children
                        .OrderBy(c => c.Saved).Take(10);
                }
                else
                {
                    model.Top10FreshestPages = children
                        .Where(c => c.SavedBy == model.SelectedUser)
                        .OrderByDescending(c => c.Saved).Take(10);

                    model.Top10LeastFreshPages = children
                        .Where(p => p.SavedBy == model.SelectedUser)
                        .OrderBy(c => c.Saved).Take(10);
                }

                model.HeroOfTheWeek = children
                    .Where(p => p.Saved.AddDays(7) >= DateTime.Now)
                    .OrderByDescending(p => p.Saved).FirstOrDefault()?.SavedBy;

                model.HeroOfTheMonth = children
                    .Where(p => p.Saved.AddMonths(1) >= DateTime.Now)
                    .OrderByDescending(p => p.Saved).FirstOrDefault()?.SavedBy;

                model.HeroOfTheYear = children
                    .Where(p => p.Saved.AddYears(1) >= DateTime.Now)
                    .OrderByDescending(p => p.Saved).FirstOrDefault()?.SavedBy;

                if (!string.IsNullOrWhiteSpace(exportReport))
                {
                    Export(model, HttpContext.Response);
                }
            }

            return View(model);
        }

        private void Export(PageFreshnessReportViewModel model, HttpResponseBase response)
        {
            using (var package = new ExcelPackage())
            {
                AddWorksheet(package, "Top10FreshestPages", model.Top10FreshestPages);
                AddWorksheet(package, "Top10LeastFreshPages", model.Top10LeastFreshPages);

                response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                response.AddHeader("content-disposition", string.Format("attachment; filename=pages{0}.xlsx", DateTime.Now.ToString("yyyyMMdd")));
                response.BinaryWrite(package.GetAsByteArray());
                response.Flush();
                response.End();
            }
        }

        private void AddWorksheet(ExcelPackage package, string name, IEnumerable<ContentVersion> pages)
        {
            ExcelWorksheet ws = package.Workbook.Worksheets.Add(name);

            ws.Cells[1, 1].Value = "PageId";
            ws.Cells[1, 2].Value = "PageName";
            ws.Cells[1, 3].Value = "PageUrl";
            ws.Cells[1, 4].Value = "Published Date";

            ws.Row(1).Style.Font.Bold = true;
            ws.Row(1).Style.Locked = true;

            int row = 2;

            foreach (var page in pages)
            {
                ws.Cells[row, 1].Value = page.ContentLink.ID;
                ws.Cells[row, 2].Value = page.Name;
                ws.Cells[row, 3].Value = Url.ContentUrl(page.ContentLink);
                ws.Cells[row, 4].Value = page.Saved.ToString("yyyy-MM-dd HH:mm");
                row++;
            }
        }
    }
}