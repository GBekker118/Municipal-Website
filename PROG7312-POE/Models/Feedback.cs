using Azure;
using Azure.Data.Tables;

namespace PROG7312_POE.Models
{
    // Feedback/Survey Model
    public class Feedback : ITableEntity
    {
        public string PartitionKey { get; set; } = "Feedback";
        public string RowKey { get; set; }
        public DateTimeOffset? Timestamp { get; set; }
        public ETag ETag { get; set; }

        public string FeedbackId { get; set; }

        public string Satisfaction { get; set; }

        public string? Improvements { get; set; }

        public string? Comments { get; set; }

        public int Rating { get; set; }
        }

    }
