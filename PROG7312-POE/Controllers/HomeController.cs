using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PROG7312_POE.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        // Home Page
        public IActionResult Index()
        {
            return View();
        }

        // Service Request Page (Coming Soon)
        public IActionResult ServiceRequest()
        {
            return View();
        }

        // Admin View
        public IActionResult Admin()
        {
            return View();
        }
    }
}
