using System.ComponentModel.DataAnnotations;

namespace PROG7312_POE.Models
{
    // Announcement Model
    public class Announcement
    {
        public string AnnouncementId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        public string AnnouncementTitle { get; set; }

        [Required(ErrorMessage = "Message is required")]
        public string AnnouncementMessage { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string AnnouncementLocation { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string AnnouncementCategory { get; set; }

        public DateTime Date { get; set; }
    }
}
