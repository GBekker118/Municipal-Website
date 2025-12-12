namespace PROG7312_POE.Models
{
    // Report Issues Node
    public class ReportIssueNode
    {
        public ReportIssue Data { get; set; }

        public ReportIssueNode Next { get; set; }

        public ReportIssueNode(ReportIssue issue)
        {
            Data = issue;
            Next = null;
        }
    }
}
