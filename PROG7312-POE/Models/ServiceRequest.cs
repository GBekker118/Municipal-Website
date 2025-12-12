namespace PROG7312_POE.Models
{
    public class ServiceRequest
    {
        public int RequestID { get; set; }
        public string RequestTitle { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string Location { get; set; }
        public int LocationTotal { get; set; }
        public string ReportID { get; set; }

        public DateTime? Date { get; set; }

        public override string ToString()
        {
            return $"{RequestTitle} - {Status}";
        }
    }
}
