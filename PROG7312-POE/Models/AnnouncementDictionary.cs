namespace PROG7312_POE.Models
{
    // Announcement Dictionary for adding and searching an Announcement
    public class AnnouncementDictionary
    {
        private Dictionary<string, Announcement> _aDict = new Dictionary<string, Announcement>();

        // Create an Announcement
        public void Add(Announcement evt)
        {
            if (!_aDict.ContainsKey(evt.AnnouncementId))
            {
                _aDict[evt.AnnouncementId] = evt;
            }
        }

        // Search announcement by title
        public IEnumerable<Announcement> SearchByTitle(string search)
        {
            return _aDict.Values
                        .Where(e => e.AnnouncementTitle.Contains(search, System.StringComparison.OrdinalIgnoreCase));
        }

        // Get all announcements
        public IEnumerable<Announcement> GetAll()
        {
            return _aDict.Values;
        }
    }
}
