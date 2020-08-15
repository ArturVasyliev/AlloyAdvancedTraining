using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using EPiServer;
using EPiServer.Core;
using EPiServer.Marketing.KPI.Manager.DataClass;
using EPiServer.Marketing.KPI.Results;
using EPiServer.Personalization.VisitorGroups;
using EPiServer.Framework.Localization;
using System.Web;
using EPiServer.Security;

namespace AlloyAdvanced.Business.KPI
{
    // Customer KPI goal according to David Knipe’s blog
    // https://www.david-tec.com/2016/11/creating-a-custom-conversion-goal-with-episerver-ab-testing/
    [DataContract]
    public class VisitorGroupKpi : Kpi
    {
        private readonly IVisitorGroupRepository _visitorGroupRepo;
        private readonly LocalizationService _localization;
        private readonly IVisitorGroupRoleRepository _visitorGroupRoleRepository;

        public VisitorGroupKpi()
        {
            _visitorGroupRepo = _servicelocator.GetInstance<IVisitorGroupRepository>();
            _localization = _servicelocator.GetInstance<LocalizationService>();
            _visitorGroupRoleRepository = _servicelocator.GetInstance<IVisitorGroupRoleRepository>();

        }

        [DataMember]
        public override string UiMarkup
        {
            get
            {
                // This is used when setting up the custom Kpi in the UI
                StringBuilder sb = new StringBuilder();

                sb.Append("<div>");
                sb.Append("Users who match this visitor group will be marked as a conversion goal: ");
                sb.Append("<select name=\"SelectedVisitorGroup\" id=\"SelectedVisitorGroup\">");
                foreach (var visitorGroup in _visitorGroupRepo.List())
                {
                    sb.Append("<option value=\"" + WebUtility.HtmlEncode(visitorGroup.Name) + "\">" + WebUtility.HtmlEncode(visitorGroup.Name) + "</option>");
                }
                sb.Append("</select>");
                sb.Append("</div>");

                return sb.ToString();
            }
        }

        [DataMember]
        public override string UiReadOnlyMarkup
        {
            get
            {
                return "<div>Selected visitor group: <strong>" + WebUtility.HtmlEncode(SelectedVisitorGroup) + "</strong></div>";
            }
        }

        [DataMember]
        public string SelectedVisitorGroup;

        public override void Validate(Dictionary<string, string> responseData)
        {
            // Used to validate the data when saving the Kpi
            if (responseData["SelectedVisitorGroup"] != null)
            {
                SelectedVisitorGroup = responseData["SelectedVisitorGroup"];
                return;
            }
            throw new Exception("Selected visitor group cannot be empty");
        }

        public override IKpiResult Evaluate(object sender, EventArgs e)
        {
            // Used to evalauate if the user has converted 
            // Note: this is called as a result of the event defined in EvaluateProxyEvent
            var result = new KpiConversionResult { HasConverted = false, KpiId = Id };

            if (HttpContext.Current.User != null)
            {
                VisitorGroupRole role;
                if (_visitorGroupRoleRepository.TryGetRole(SelectedVisitorGroup, out role))
                {
                    // Check if the user has matched the selected visitor group
                    result.HasConverted = role.IsMatch(PrincipalInfo.CurrentPrincipal, new HttpContextWrapper(HttpContext.Current));
                }
            }

            return result;
        }

        private EventHandler<ContentEventArgs> _eventHander;

        /// <summary>
        /// EventHandler that tells the code using this KPI what CMS event this KPI wants to attach to in order to properly evaluate
        /// </summary>
        public override event EventHandler EvaluateProxyEvent
        {
            add
            {
                _eventHander = new EventHandler<ContentEventArgs>(value);
                var service = _servicelocator.GetInstance<IContentEvents>();
                service.LoadedContent += _eventHander;
            }
            remove
            {
                var service = _servicelocator.GetInstance<IContentEvents>();
                service.LoadedContent -= _eventHander;
            }
        }

        [DataMember]
        public new string FriendlyName => _localization.GetString(
            "/visitorgroupkpi/friendlyname",
            "Matches Visitor Group");

        [DataMember]
        public override string Description => _localization.GetString(
            "/visitorgroupkpi/description",
            "Coversion goal is activated if a visitor matches the visitor group.");
    }
}