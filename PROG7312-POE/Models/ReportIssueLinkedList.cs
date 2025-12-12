namespace PROG7312_POE.Models
{
    public class ReportIssueLinkedList
    {
        // Head node to display start of the list of Report Issues
        private ReportIssueNode head;

        // Counter used to generate report issue IDs
        private static int counter = 0;

        public static void SetCounter(int lastCounter)
        {
            counter = lastCounter;
        }

        // Add method to create new Report Issues
        public void Add(ReportIssue issue)
        {
            // Generating ID
            counter++;
            issue.Id = $"REP-{counter}";

            // Creates a new report issue when the next node is null
            ReportIssueNode newNode = new ReportIssueNode(issue);
            if (head == null)
            {
                // If the next node is null it becomes the head node
                head = newNode;
            }
            else
            {
                ReportIssueNode current = head;
                // Iterates through the list of nodes until it is null
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        // ToList method to convert LinkedList into a List to display list of reports In ReportIssueList View
        public List<ReportIssue> ToList()
        {
            List<ReportIssue> issues = new List<ReportIssue>();
            ReportIssueNode current = head;
            while (current != null)
            {
                issues.Add(current.Data);
                current = current.Next;
            }
            return issues;
        }
    }
}
