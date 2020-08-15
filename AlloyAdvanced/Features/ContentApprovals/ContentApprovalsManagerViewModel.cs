using AlloyAdvanced.Models.ViewModels;
using EPiServer.Approvals.ContentApprovals;

namespace AlloyAdvanced.Features.ContentApprovals
{
    public class ContentApprovalsManagerViewModel : PageViewModel<ContentApprovalsManagerPage>
    {
        public ContentApprovalDefinition ApprovalDefinition { get; set; }
        public ContentApproval Approval { get; set; }

        public ContentApprovalsManagerViewModel(
            ContentApprovalsManagerPage currentPage) : base(currentPage)
        {
        }
    }
}