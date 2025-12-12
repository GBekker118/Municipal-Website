using PROG7312_POE_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace PROG7312_POE_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MunicipalServicesController : ControllerBase
    {
        // POST method to add an Issue Report 
        [HttpPost]
        public async Task<ActionResult<ReportIssue>> PostReport(ReportIssue report)
        {
            return report;
        }
    }
}