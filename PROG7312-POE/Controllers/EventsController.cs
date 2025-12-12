using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;

namespace PROG7312_POE.Controllers
{
    public class EventsController : Controller
    {
        // Event and Announcement Queues, Dictionaries, and Sets
        private readonly EventQueue _eventQueue;
        private readonly EventDictionary _eventDict = new EventDictionary();
        private readonly EventSet _eventSet = new EventSet();
        private readonly AnnouncementQueue _aQueue;
        private readonly AnnouncementDictionary _aDict = new AnnouncementDictionary();
        private readonly AnnouncementSet _aSet = new AnnouncementSet();
        public static int counter = 1;

        public EventsController(EventQueue queue, AnnouncementQueue aQueue)
        {
            _eventQueue = queue;
            _aQueue = aQueue;

        }

        // EventIndex Page to show all events in the queue
        public IActionResult EventIndex(string? search, string? categoryFilter, string? locationFilter, DateTime? startDateFilter, DateTime? endDateFilter, string? activeTab)
        {
            // Set default to events tab
            var active = activeTab ?? "events";

            // Add events to dictionary, and set
            foreach (var evt in _eventQueue.GetAll())
            {
                _eventSet.Add(evt);
                _eventDict.Add(evt);
            }

            // Add announcements to dictionary, and set
            foreach (var ann in _aQueue.GetAll())
            {
                _aSet.Add(ann);
                _aDict.Add(ann);
            }

            // Get all items from events
            IEnumerable<Event> events = _eventQueue.GetAll();

            // Get all items from announcements
            IEnumerable<Announcement> announcements = _aQueue.GetAll()
                .OrderByDescending(a => a.Date);

            // Apply filters based on active tab (events/announcements)
            if (active == "events")
            {
                if (!string.IsNullOrEmpty(search))
                {
                    events = _eventDict.SearchByTitle(search);
                }
                if (!string.IsNullOrEmpty(categoryFilter))
                {
                    events = events.Where(e => e.Category == categoryFilter);

                    // Save recent category in Session
                    var recentCategories = HttpContext.Session.GetString("RecentCategories");
                    var list = string.IsNullOrEmpty(recentCategories)
                        ? new List<string>()
                        : recentCategories.Split(',').ToList();

                    if (!list.Contains(categoryFilter))
                    {
                        list.Add(categoryFilter);
                        if (list.Count > 5) 
                            list = list.Skip(list.Count - 5).ToList();
                    }

                    HttpContext.Session.SetString("RecentCategories", string.Join(",", list));
                }
                if (!string.IsNullOrEmpty(locationFilter))
                {
                    events = events.Where(e => e.Location == locationFilter);
                }
                // Filter by date range (start - end)
                if (startDateFilter.HasValue && endDateFilter.HasValue)
                {
                    var s = startDateFilter.Value.Date;
                    var e = endDateFilter.Value.Date;

                    // Include events that overlap the selected range
                    events = events.Where(ev =>
                        ev.EventStartDate.Date <= e && ev.EventEndDate.Date >= s
                    );
                }
                else if (startDateFilter.HasValue)
                {
                    var s = startDateFilter.Value.Date;

                    // Include events that end on or after the start date
                    events = events.Where(ev => ev.EventEndDate.Date >= s);
                }
                else if (endDateFilter.HasValue)
                {
                    var e = endDateFilter.Value.Date;

                    // Include events that start on or before the end date
                    events = events.Where(ev => ev.EventStartDate.Date <= e);
                }
            }
            else if (active == "announcements")
            {
                // Search filter
                if (!string.IsNullOrEmpty(search))
                {
                    announcements = announcements.Where(a =>
                        a.AnnouncementTitle.Contains(search, StringComparison.OrdinalIgnoreCase));
                }

                // Category filter
                if (!string.IsNullOrEmpty(categoryFilter))
                {
                    announcements = announcements.Where(a => a.AnnouncementCategory == categoryFilter);
                }

                // Location filter
                if (!string.IsNullOrEmpty(locationFilter))
                {
                    announcements = announcements.Where(a => a.AnnouncementLocation == locationFilter);
                }

                // Date range filter
                if (startDateFilter.HasValue && endDateFilter.HasValue)
                {
                    var s = startDateFilter.Value.Date;
                    var e = endDateFilter.Value.Date;
                    announcements = announcements.Where(a => a.Date.Date >= s && a.Date.Date <= e);
                }
                else if (startDateFilter.HasValue)
                {
                    var s = startDateFilter.Value.Date;
                    announcements = announcements.Where(a => a.Date.Date >= s);
                }
                else if (endDateFilter.HasValue)
                {
                    var e = endDateFilter.Value.Date;
                    announcements = announcements.Where(a => a.Date.Date <= e);
                }
            }

            // Recommended Events (based on recent categories)
            List<Event> recommendedEvents = new List<Event>();
            var recent = HttpContext.Session.GetString("RecentCategories");
            if (!string.IsNullOrEmpty(recent))
            {
                var categoryCounts = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, int>>(recent);

                // Set recommended events
                var sortedCategories = categoryCounts
                    .OrderByDescending(kvp => kvp.Value)
                    .Select(kvp => kvp.Key)
                    .ToList();

                // Recommended events based on most viewed categories
                recommendedEvents = _eventQueue.GetAll()
                    .Where(e => sortedCategories.Contains(e.Category))
                    .OrderByDescending(e => categoryCounts[e.Category])
                    .ThenByDescending(e => e.Priority)
                    .Take(3)
                    .ToList();

                // Display top 3 recommended event categories only
                ViewBag.SuggestedCategories = sortedCategories.Take(3).ToList();
            }

            ViewBag.RecommendedEvents = recommendedEvents;

            // Create ViewModel
            var viewModel = new EventsAndAnnouncements
            {
                Events = events.ToList(),
                Announcements = announcements.ToList()
            };

            // Pass dropdown data and selected values
            ViewBag.ActiveTab = active;
            ViewBag.SelectedCategory = categoryFilter ?? "";
            ViewBag.SelectedLocation = locationFilter ?? "";
            ViewBag.StartDateFilter = startDateFilter?.ToString("yyyy-MM-dd") ?? "";
            ViewBag.EndDateFilter = endDateFilter?.ToString("yyyy-MM-dd") ?? "";
            ViewBag.SearchQuery = search ?? "";

            // Show dropdowns based on active tab
            ViewBag.Categories = active == "announcements" ? _aSet.Categories : _eventSet.Categories;
            ViewBag.Locations = active == "announcements" ? _aSet.Locations : _eventSet.Locations;
            ViewBag.Dates = active == "announcements" ? _aSet.Dates : _eventSet.Dates;

            return View(viewModel);
        }

        // CreateEvent form to create an event 
        public IActionResult CreateEvent()
        {
            return View();
        }

        // POST method to Create new event
        [HttpPost]
        public IActionResult CreateEvent(Event evt, IFormFile mediaFile)
        {
            if (!ModelState.IsValid)
                return View(evt);

            // Handle file upload
            if (mediaFile != null && mediaFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                var filePath = Path.Combine(uploadsFolder, mediaFile.FileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                    mediaFile.CopyTo(fileStream);

                evt.Media = $"/uploads/{mediaFile.FileName}";
            }
            evt.EventId = $"Event-{counter++}";
            evt.Date = DateTime.UtcNow;

            // Add to queue 
            _eventQueue.Enqueue(evt);

            // Add to dictionary for search
            _eventDict.Add(evt);

            // Add to set for category/date/location filters
            _eventSet.Add(evt);

            TempData["Message"] = "Event created successfully!";
            return RedirectToAction("EventIndex");
        }

        // CreateAnnouncement form to create an announcement 
        public IActionResult CreateAnnouncement()
        {
            return View();
        }

        // POST method to Create new Announcement
        [HttpPost]
        public IActionResult CreateAnnouncement(Announcement ann)
        {
            if (!ModelState.IsValid)
                return View(ann);

            ann.Date = DateTime.UtcNow;

            // Add to queue 
            _aQueue.Enqueue(ann);

            // Add to dictionary for search
            _aDict.Add(ann);

            // Add to set for category/date/location filters
            _aSet.Add(ann);

            TempData["Message"] = "Announcement created successfully!";
            return RedirectToAction("EventIndex", new { activeTab = "announcements" });
        }

        // Remove next event 
        public IActionResult Dequeue()
        {
            var nextEvent = _eventQueue.Dequeue();
            if (nextEvent != null)
            {
                TempData["Message"] = $"Event '{nextEvent.EventTitle}' dequeued.";
            }
            else
            {
                TempData["Error"] = "No events to dequeue.";
            }

            return RedirectToAction("EventIndex");
        }

        // SelectEvent marks a select event as more important (doesn't reorder it)
        public IActionResult SelectEvent(string eventTitle)
        {
            var evt = _eventQueue.GetAll().FirstOrDefault(e => e.EventTitle == eventTitle);
            if (evt != null)
            {
                // Increase priority
                evt.Priority++; 
            }

            // Redirect back to event list
            return RedirectToAction("EventIndex");
        }

        // IncreasePriority method to increase an events priority and reorders it based on highest priority
        public IActionResult IncreasePriority(string id)
        {
            var evt = _eventQueue.GetAll().FirstOrDefault(e => e.EventId.Equals(id));
            if (evt != null)
            {
                // Increase priority
                evt.Priority++;
                // Reorder events based on highest priority 
                _eventQueue.ReorderByPriority();

                // Add category to recent categories in session
                var recentCategories = HttpContext.Session.GetString("RecentCategories");
                Dictionary<string, int> categoryCounts;

                if (string.IsNullOrEmpty(recentCategories))
                {
                    categoryCounts = new Dictionary<string, int>();
                }
                else
                {
                    categoryCounts = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, int>>(recentCategories);
                }

                // Increase the count for the current event’s category
                if (categoryCounts.ContainsKey(evt.Category))
                {
                    categoryCounts[evt.Category]++;
                }
                else
                {
                    categoryCounts[evt.Category] = 1;
                }

                // Keep only top 5 most interacted categories
                categoryCounts = categoryCounts
                    .OrderByDescending(kvp => kvp.Value)
                    .Take(5)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);

                // Save back to Session as JSON content
                HttpContext.Session.SetString("RecentCategories",
                    System.Text.Json.JsonSerializer.Serialize(categoryCounts));

            }
            return Ok();
        }

        // EventOverview Page to view a selected event
        public IActionResult EventOverview(string id)
        {
            var evt = _eventQueue.GetAll().FirstOrDefault(e => e.EventId == id);
            if (evt == null)
                return NotFound();

            return View(evt);
        }
    }
}
