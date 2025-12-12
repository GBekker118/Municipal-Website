using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace PROG7312_POE.Models
{
    // Event Model
    public class Event
    {
        public string EventId { get; set; }

        [Required(ErrorMessage = "Event Name is required")]
        [Display(Name = "Event Name")]
        public string EventTitle { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string EventDescription { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Please enter the start date of the event")]
        [Display(Name = "Event Starting Date")]
        public DateTime EventStartDate { get; set; }

        [Required(ErrorMessage = "Event EndDate is required")]
        public DateTime EventEndDate { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        public DateTime Date { get; set; }
        public string? Media { get; set; }

        public int Priority { get; set; } = 0;
    }
}
