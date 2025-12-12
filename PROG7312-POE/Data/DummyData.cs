using PROG7312_POE.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PROG7312_POE.Data
{
    // Events and Announcements Dummy Data to populate empty list
    public static class DummyData
    {
        public static void EventsAndAnnouncementsData(EventQueue eventQueue, AnnouncementQueue announcementQueue)
        {
            // Event data if queue is empty
            if (!eventQueue.GetAll().Any())
            {


                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-001",
                    EventTitle = "Local Farmers Market",
                    EventDescription = "Fresh produce and handmade crafts available this weekend at the town square.",
                    Location = "Town Square",
                    EventStartDate = DateTime.Now.AddDays(-5),
                    EventEndDate = DateTime.Now.AddDays(2),
                    Date = DateTime.Now.AddDays(-7),
                    Category = "Community",
                    Media = "/img/farmersMarket.jpg",
                    Priority = 16
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-002",
                    EventTitle = "Library Book Fair",
                    EventDescription = "Explore new books and meet authors at the central library book fair.",
                    Location = "Central Library",
                    EventStartDate = DateTime.Now.AddDays(-5),
                    EventEndDate = DateTime.Now.AddDays(4),
                    Date = DateTime.Now.AddDays(-6),
                    Category = "Education",
                    Media = "/img/libraryFair.jpg",
                    Priority = 15
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-003",
                    EventTitle = "Music Festival",
                    EventDescription = "Live performances from local bands and artists. Food stalls available.",
                    Location = "Riverside Park",
                    EventStartDate = DateTime.Now.AddDays(-4),
                    EventEndDate = DateTime.Now.AddDays(8),
                    Date = DateTime.Now.AddDays(-5),
                    Category = "Entertainment",
                    Media = "/img/musicFestival.jpg",
                    Priority = 15
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-004",
                    EventTitle = "Road Cleanup Initiative",
                    EventDescription = "Community cleanup of 5th Avenue following the weekend festival.",
                    Location = "5th Avenue",
                    EventStartDate = DateTime.Now.AddDays(-4),
                    EventEndDate = DateTime.Now.AddDays(3),
                    Date = DateTime.Now.AddDays(-5),
                    Category = "Sanitation",
                    Media = "/img/roadCleanup.jpg",
                    Priority = 11
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-005",
                    EventTitle = "City Marathon",
                    EventDescription = "Participate in the annual city marathon. Registration required in advance.",
                    Location = "City Center",
                    EventStartDate = DateTime.Now.AddDays(-3),
                    EventEndDate = DateTime.Now.AddDays(5),
                    Date = DateTime.Now.AddDays(-4),
                    Category = "Sports",
                    Media = "/img/marathon.jpg",
                    Priority = 11
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-006",
                    EventTitle = "Community Health Check",
                    EventDescription = "Free health check-ups and consultations at the community center.",
                    Location = "Community Center",
                    EventStartDate = DateTime.Now.AddDays(-3),
                    EventEndDate = DateTime.Now.AddDays(5),
                    Date = DateTime.Now.AddDays(-4),
                    Category = "Health",
                    Media = "/img/communityHealth.jpg",
                    Priority = 9
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-007",
                    EventTitle = "Tree Planting Drive",
                    EventDescription = "Join us in planting trees across the city parks to improve the environment.",
                    Location = "City Park",
                    EventStartDate = DateTime.Now.AddDays(-2),
                    EventEndDate = DateTime.Now.AddDays(7),
                    Date = DateTime.Now.AddDays(-3),
                    Category = "Environment",
                    Media = "/img/treePlanting.jpg",
                    Priority = 9
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-008",
                    EventTitle = "Water Pipe Repair",
                    EventDescription = "Repairing burst pipes on Main Street. Expected completion in 3 days.",
                    Location = "Downtown",
                    EventStartDate = DateTime.Now.AddDays(-1),
                    EventEndDate = DateTime.Now.AddDays(1),
                    Date = DateTime.Now.AddDays(-2),
                    Category = "Utilities",
                    Media = "/img/pipeRepair.jpg",
                    Priority = 8
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-009",
                    EventTitle = "Road Safety Workshop",
                    EventDescription = "Learn essential road safety measures in this free community workshop.",
                    Location = "Downtown Hall",
                    EventStartDate = DateTime.Now.AddDays(-1),
                    EventEndDate = DateTime.Now.AddDays(6),
                    Date = DateTime.Now.AddDays(-2),
                    Category = "Education",
                    Media = "/img/roadSafety.jpg",
                    Priority = 8
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-010",
                    EventTitle = "Streetlight Maintenance",
                    EventDescription = "Replacing broken streetlights in the northern district.",
                    Location = "North District",
                    EventStartDate = DateTime.Now.AddDays(-1),
                    EventEndDate = DateTime.Now.AddDays(2),
                    Date = DateTime.Now.AddDays(-1),
                    Category = "Utilities",
                    Media = "/img/streetlightMaintenance.jpg",
                    Priority = 7
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-011",
                    EventTitle = "Community Art Exhibition",
                    EventDescription = "A showcase of local artists’ work at the downtown gallery. Free entry.",
                    Location = "Downtown Gallery",
                    EventStartDate = DateTime.Now,
                    EventEndDate = DateTime.Now.AddDays(4),
                    Date = DateTime.Now.AddDays(-1),
                    Category = "Culture",
                    Media = "/img/artExhibition.jpg",
                    Priority = 7
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-012",
                    EventTitle = "Blood Donation Drive",
                    EventDescription = "Help save lives by donating blood at the central hospital.",
                    Location = "Central Hospital",
                    EventStartDate = DateTime.Now.AddDays(1),
                    EventEndDate = DateTime.Now.AddDays(1),
                    Date = DateTime.Now,
                    Category = "Health",
                    Media = "/img/bloodDonation.jpg",
                    Priority = 7
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-013",
                    EventTitle = "Tech Innovation Expo",
                    EventDescription = "Discover cutting-edge technology and local startups at the expo center.",
                    Location = "Expo Center",
                    EventStartDate = DateTime.Now.AddDays(2),
                    EventEndDate = DateTime.Now.AddDays(5),
                    Date = DateTime.Now,
                    Category = "Technology",
                    Media = "/img/techExpo.jpg",
                    Priority = 5
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-014",
                    EventTitle = "Food Truck Festival",
                    EventDescription = "Taste the best street food from local vendors this weekend.",
                    Location = "Harbor Front",
                    EventStartDate = DateTime.Now.AddDays(3),
                    EventEndDate = DateTime.Now.AddDays(4),
                    Date = DateTime.Now,
                    Category = "Food",
                    Media = "/img/foodFestival.jpg",
                    Priority = 4
                });

                eventQueue.Enqueue(new Event
                {
                    EventId = "Event-015",
                    EventTitle = "Wildlife Photography Contest",
                    EventDescription = "Submit your best nature shots to win exciting prizes.",
                    Location = "City Hall",
                    EventStartDate = DateTime.Now.AddDays(4),
                    EventEndDate = DateTime.Now.AddDays(7),
                    Date = DateTime.Now,
                    Category = "Photography",
                    Media = "/img/wildlifeContest.jpg",
                    Priority = 2
                });
            }

            // Announcement data if queue is empty
            if (!announcementQueue.GetAll().Any())
            {
                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-001",
                    AnnouncementTitle = "Weekly Cleanup Drive",
                    AnnouncementMessage = "Join us this Saturday for a community cleanup event across all major parks.",
                    AnnouncementLocation = "City Park",
                    AnnouncementCategory = "Community",
                    Date = DateTime.Now.AddDays(-10)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-002",
                    AnnouncementTitle = "Water Supply Alert",
                    AnnouncementMessage = "Temporary water outage expected in the northern district from 9am to 2pm tomorrow.",
                    AnnouncementLocation = "North District",
                    AnnouncementCategory = "Water",
                    Date = DateTime.Now.AddDays(-7)
                });

                // Additional dummy announcements
                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-003",
                    AnnouncementTitle = "Library Renovation",
                    AnnouncementMessage = "The central library will be closed for renovation starting next Monday. Services will resume in 3 weeks.",
                    AnnouncementLocation = "Central Library",
                    AnnouncementCategory = "Education",
                    Date = DateTime.Now.AddDays(-7)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-004",
                    AnnouncementTitle = "Traffic Advisory",
                    AnnouncementMessage = "Expect delays on Main Street due to roadworks. Alternate routes are recommended.",
                    AnnouncementLocation = "Main Street",
                    AnnouncementCategory = "Traffic",
                    Date = DateTime.Now.AddDays(-6)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-005",
                    AnnouncementTitle = "Local Farmers Market",
                    AnnouncementMessage = "Come and enjoy fresh produce and crafts at the weekend farmers market.",
                    AnnouncementLocation = "Town Square",
                    AnnouncementCategory = "Community",
                    Date = DateTime.Now.AddDays(-5)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-006",
                    AnnouncementTitle = "Power Maintenance Notice",
                    AnnouncementMessage = "Scheduled maintenance in the east district will result in temporary power outages from 8am to 5pm.",
                    AnnouncementLocation = "East District",
                    AnnouncementCategory = "Utilities",
                    Date = DateTime.Now.AddDays(-3)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-007",
                    AnnouncementTitle = "Health Awareness Campaign",
                    AnnouncementMessage = "Free health check-ups will be available at the community center this Thursday.",
                    AnnouncementLocation = "Community Center",
                    AnnouncementCategory = "Health",
                    Date = DateTime.Now.AddDays(-1)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-008",
                    AnnouncementTitle = "Public Holiday Notice",
                    AnnouncementMessage = "Municipal offices will be closed on Friday for the national holiday.",
                    AnnouncementLocation = "Citywide",
                    AnnouncementCategory = "General",
                    Date = DateTime.Now.AddDays(-1)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-009",
                    AnnouncementTitle = "Tree Planting Volunteers Needed",
                    AnnouncementMessage = "We are looking for volunteers to assist in the city’s annual tree planting program.",
                    AnnouncementLocation = "City Park",
                    AnnouncementCategory = "Environment",
                    Date = DateTime.Now.AddDays(-1)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-010",
                    AnnouncementTitle = "Recycling Initiative Launch",
                    AnnouncementMessage = "New recycling bins have been placed across neighborhoods to encourage waste sorting.",
                    AnnouncementLocation = "All Districts",
                    AnnouncementCategory = "Environment",
                    Date = DateTime.Now.AddDays(-1)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-011",
                    AnnouncementTitle = "Tech Workshop for Students",
                    AnnouncementMessage = "Free coding and robotics workshop open to high school students next weekend.",
                    AnnouncementLocation = "Central Library",
                    AnnouncementCategory = "Education",
                    Date = DateTime.Now.AddDays(-2)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-012",
                    AnnouncementTitle = "Streetlight Repairs Completed",
                    AnnouncementMessage = "Streetlight maintenance in the northern district has been successfully completed.",
                    AnnouncementLocation = "North District",
                    AnnouncementCategory = "Utilities",
                    Date = DateTime.Now.AddDays(-2)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-013",
                    AnnouncementTitle = "Food Festival Reminder",
                    AnnouncementMessage = "Don’t miss this weekend’s food truck festival at Harbor Front!",
                    AnnouncementLocation = "Harbor Front",
                    AnnouncementCategory = "Entertainment",
                    Date = DateTime.Now.AddDays(-2)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-014",
                    AnnouncementTitle = "Blood Donation Call",
                    AnnouncementMessage = "The central hospital invites residents to donate blood and save lives.",
                    AnnouncementLocation = "Central Hospital",
                    AnnouncementCategory = "Health",
                    Date = DateTime.Now.AddDays(-1)
                });

                announcementQueue.Enqueue(new Announcement
                {
                    AnnouncementId = "Ann-015",
                    AnnouncementTitle = "Community Art Showcase",
                    AnnouncementMessage = "Visit the downtown gallery this weekend to see artworks by talented locals.",
                    AnnouncementLocation = "Downtown Gallery",
                    AnnouncementCategory = "Culture",
                    Date = DateTime.Now.AddDays(-1)
                });

                // Reorder announcements by date
                announcementQueue.ReorderByDate();
            }
        }
    }
}