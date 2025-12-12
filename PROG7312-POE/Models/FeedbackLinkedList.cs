namespace PROG7312_POE.Models
{
    public class FeedbackLinkedList
    {
        // Head node to display start of the list of Feedback/Survey
        private FeedbackNode head;

        // Counter used to generate survey IDs
        private static int counter = 0;

        public static void SetCounter(int lastCounter)
        {
            counter = lastCounter;
        }

        // Add method to create new Feedback report
        public void Add(Feedback feedback)
        {
            // Generating ID
            counter++;
            feedback.FeedbackId = $"F-{counter}";

            // Creates a new survey when the next node is null
            FeedbackNode newNode = new FeedbackNode(feedback);
            if (head == null)
            {
                // If the next node is null it becomes the head node
                head = newNode;
            }
            else
            {
                FeedbackNode current = head;
                // Iterates through the list of nodes until it is null
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode;
            }
        }

        // ToList method to convert LinkedList into a List to display list of surveys In FeedbackList View
        public List<Feedback> ToList()
        {
            List<Feedback> feedbacks = new List<Feedback>();
            FeedbackNode current = head;
            while (current != null)
            {
                feedbacks.Add(current.Data);
                current = current.Next;
            }
            return feedbacks;
        }
    }
}