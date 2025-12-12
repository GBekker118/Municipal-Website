namespace PROG7312_POE.Models
{
    // Event Queues for adding/enqueue an Announcement
    public class AnnouncementQueue
    {
        private Queue<Announcement> _aQueue = new Queue<Announcement>();

        // Create an Announcement
        public void Enqueue(Announcement evt)
        {
            _aQueue.Enqueue(evt);
        }

        // Delete an Announcement
        public Announcement? Dequeue()
        {
            return _aQueue.Count > 0 ? _aQueue.Dequeue() : null;
        }

        // Get the first announcement in the Queue
        public Announcement? Peek()
        {
            return _aQueue.Count > 0 ? _aQueue.Peek() : null;
        }

        // Get all Announcements
        public IEnumerable<Announcement> GetAll()
        {
            return _aQueue.ToArray();
        }

        public int Count => _aQueue.Count;

        public void ReorderByDate()
        {
            _aQueue = new Queue<Announcement>(_aQueue.OrderByDescending(a => a.Date));
        }
    }
}
