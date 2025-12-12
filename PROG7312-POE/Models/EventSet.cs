namespace PROG7312_POE.Models
{
    // Event Set to search based off category, date, and location
    public class EventSet
    {
        private HashSet<string> _categories = new HashSet<string>();
        private HashSet<string> _locations = new HashSet<string>();
        private HashSet<DateTime> _dates = new HashSet<DateTime>();

        // Add category, date and location from created event
        public void Add(Event evt)
        {
            if (!string.IsNullOrEmpty(evt.Category))
                _categories.Add(evt.Category);
            if (!string.IsNullOrEmpty(evt.Location))
                _locations.Add(evt.Location);

            _dates.Add(evt.EventStartDate.Date);
            _dates.Add(evt.EventEndDate.Date);
        }

        // Get unique categories
        public IEnumerable<string> Categories => _categories.OrderBy(c => c);

        // Get unique locations
        public IEnumerable<string> Locations => _locations.OrderBy(l => l);

        // Get unique dates
        public IEnumerable<DateTime> Dates => _dates.OrderBy(d => d);
    }
}
