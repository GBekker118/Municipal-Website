namespace PROG7312_POE.Models
{
    // Announcement Set to search based off category, date, and location
    public class AnnouncementSet
    {
        private HashSet<string> _categories = new HashSet<string>();
        private HashSet<string> _locations = new HashSet<string>();
        private HashSet<DateTime> _dates = new HashSet<DateTime>();

        // Add category, date and location from created announcement
        public void Add(Announcement ann)
        {
            if (!string.IsNullOrEmpty(ann.AnnouncementCategory))
                _categories.Add(ann.AnnouncementCategory);
            if (!string.IsNullOrEmpty(ann.AnnouncementLocation))
                _locations.Add(ann.AnnouncementLocation);
        }

        // Get unique categories
        public IEnumerable<string> Categories => _categories.OrderBy(c => c);

        // Get unique locations
        public IEnumerable<string> Locations => _locations.OrderBy(l => l);

        // Get unique dates
        public IEnumerable<DateTime> Dates => _dates.OrderBy(d => d);
    }
}
