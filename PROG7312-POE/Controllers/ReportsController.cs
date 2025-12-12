using Azure.Data.Tables;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;

namespace PROG7312_POE.Controllers
{
    public class ReportsController : Controller
    {
        private readonly TableClient _tableClient;
        private readonly BlobContainerClient _blobContainerClient;
        private static ReportIssueLinkedList _linkedList = new ReportIssueLinkedList();

        public ReportsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("StorageAccount")["AzureStorage"];

            // Table Storage
            _tableClient = new TableClient(connectionString, "ReportIssues");
            _tableClient.CreateIfNotExists();

            // Blob Storage container for media
            _blobContainerClient = new BlobContainerClient(connectionString, "reportfiles");
            _blobContainerClient.CreateIfNotExists();

            // Get the last report's ID
            var lastReport = _tableClient.Query<ReportIssue>()
                                 .OrderByDescending(r => r.Date)
                                 .FirstOrDefault();

            if (lastReport != null)
            {
                // Parse the last ID
                var lastIdNumber = int.Parse(lastReport.Id.Split('-')[1]);
                ReportIssueLinkedList.SetCounter(lastIdNumber);
            }
        }


        // ReportIssues view to display Report Issue form
        [HttpGet] 
        public IActionResult ReportIssues() 
        { 
            return View();
        }

        // POST method to add a Report Issue
        [HttpPost]
        public async Task<IActionResult> ReportIssues(ReportIssue issue, IFormFile mediaFile)
        {

            // Upload file/media to Blob
            if (mediaFile != null && mediaFile.Length > 0)
            {
                var blobClient = _blobContainerClient.GetBlobClient(mediaFile.FileName);
                await blobClient.UploadAsync(mediaFile.OpenReadStream(), overwrite: true);
                issue.Media = blobClient.Uri.ToString();
            }

            // Generate unique Id
            _linkedList.Add(issue);
            issue.RowKey = issue.Id;
            issue.PartitionKey = "ReportIssue";
            issue.Status = "Pending";
            issue.Date = DateTime.UtcNow;

            // Save to Azure Table Storage
            await _tableClient.AddEntityAsync(issue);

            // Create success message
            TempData["Message"] = "Issue submitted successfully!";

            // Redirect to Home/Index
            return RedirectToAction("Index", "Home");
        }

        // ReportIssueList view to display the list of all Report Issues made/created
        [HttpGet]
        public IActionResult ReportIssuesList()
        {
            var issues = _tableClient.Query<ReportIssue>().OrderBy(r => int.Parse(r.Id.Split('-')[1])).ToList();
            return View(issues);
        }
    }
}