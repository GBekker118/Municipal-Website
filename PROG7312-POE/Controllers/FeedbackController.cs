using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;

namespace PROG7312_POE.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly TableClient _tableClient;
        private static FeedbackLinkedList _linkedList = new FeedbackLinkedList();

        public FeedbackController(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("StorageAccount")["AzureStorage"];

            // Table Storage
            _tableClient = new TableClient(connectionString, "Feedback");
            _tableClient.CreateIfNotExists();


            // Get the last survey's ID
            var lastSurvey = _tableClient.Query<Feedback>()
                                 .OrderByDescending(r => r.Timestamp)
                                 .FirstOrDefault();

            if (lastSurvey != null)
            {
                // Parse the last ID
                var lastIdNumber = int.Parse(lastSurvey.FeedbackId.Split('-')[1]);
                FeedbackLinkedList.SetCounter(lastIdNumber);
            }
        }


        // FeedbackSurvey view to display a Survey form to gather feedback 
        [HttpGet]
        public IActionResult FeedbackSurvey()
        {
            return View();
        }

        // POST method to add a Survey/Feedback
        [HttpPost]
        public async Task<IActionResult> FeedbackSurvey(Feedback feedback)
        {
            feedback.Satisfaction = feedback.Satisfaction?.Trim();

            if (string.IsNullOrEmpty(feedback.Satisfaction))
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(feedback);
            }

            // Generate unique Id
            _linkedList.Add(feedback);
            feedback.RowKey = feedback.FeedbackId;
            feedback.PartitionKey = "Feedback";

            // Save to Azure Table Storage
            await _tableClient.AddEntityAsync(feedback);

            // Create success message
            TempData["Message"] = "Feedback successfully sent!";

            // Redirect to Home/Index
            return RedirectToAction("Index", "Home");
        }

        // FeedbackList view to display the list of all the surveys made/created
        [HttpGet]
        public IActionResult FeedbackList()
        {
            var feedback = _tableClient.Query<Feedback>().OrderBy(f => int.Parse(f.FeedbackId.Split('-')[1])).ToList();
            return View(feedback);
        }
    }
}
