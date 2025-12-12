namespace PROG7312_POE.Models
{
    // Feedback Node
    public class FeedbackNode
    {
        public Feedback Data { get; set; }

        public FeedbackNode Next { get; set; }

        public FeedbackNode(Feedback feedback)
        {
            Data = feedback;
            Next = null;
        }
    }
}
