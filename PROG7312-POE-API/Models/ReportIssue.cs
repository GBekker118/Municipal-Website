using Azure;
using Azure.Data.Tables;
using System.ComponentModel.DataAnnotations;

namespace PROG7312_POE_API.Models
{
    // ReportIssue Model
    public class ReportIssue : ITableEntity
    {
        public string PartitionKey { get; set; } = "ReportIssue";
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string Id { get; set; }

        [Required(ErrorMessage = "Location is required")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Category is required")]
        public string Category { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public string Media { get; set; }
    }
}
