namespace PROG7312_POE.Models
{
    // Events and Announcements Model
    public class EventsAndAnnouncements
    {
        public IEnumerable<Event> Events { get; set; }
        public IEnumerable<Announcement> Announcements { get; set; }
        
    }
}
