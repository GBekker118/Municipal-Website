namespace PROG7312_POE.Models
{
    // Event Dictionary for adding and searching an Event
    public class EventDictionary
    {
        private Dictionary<string, Event> _dict = new Dictionary<string, Event>();

        // Create an Event
        public void Add(Event evt)
        {
            if (!_dict.ContainsKey(evt.EventId))
            {
                _dict[evt.EventId] = evt;
            }
        }

        // Search event by title 
        public IEnumerable<Event> SearchByTitle(string search)
        {
            return _dict.Values
                        .Where(e => e.EventTitle.Contains(search, System.StringComparison.OrdinalIgnoreCase));
        }

        // Get all events
        public IEnumerable<Event> GetAll()
        {
            return _dict.Values;
        }
    }
}
