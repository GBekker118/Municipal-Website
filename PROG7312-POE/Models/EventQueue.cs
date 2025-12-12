namespace PROG7312_POE.Models
{
    // Event Queues for adding/enqueue an Event
    public class EventQueue
    {
        private Queue<Event> _queue = new Queue<Event>();

        // Create an Event
        public void Enqueue(Event evt)
        {
            _queue.Enqueue(evt);
        }

        // Delete an Event
        public Event? Dequeue()
        {
            return _queue.Count > 0 ? _queue.Dequeue() : null;
        }

        // Get the first event in the Queue
        public Event? Peek()
        {
            return _queue.Count > 0 ? _queue.Peek() : null;
        }

        // Get all Events
        public IEnumerable<Event> GetAll()
        {
            return _queue.ToArray();
        }

        // Get all events sorted by priority 
        public IEnumerable<Event> GetAllByPriority()
        {
            return _queue.ToArray()
                         .OrderByDescending(e => e.Priority)
                         .ThenBy(e => e.EventStartDate); 
        }

        public int Count => _queue.Count;

        // Increment priority for a specific event
        public void IncrementPriority(int id) 
        {
            foreach (var evt in _queue)
            {
                if (evt.EventId.Equals(id)) 
                {
                    evt.Priority++;
                    break;
                }
            }
        }

        // Reorder Events based off highest priority
        public void ReorderByPriority()
        {
            _queue = new Queue<Event>(_queue.OrderByDescending(e => e.Priority).ThenBy(e => e.EventStartDate));
        }

    }
}
