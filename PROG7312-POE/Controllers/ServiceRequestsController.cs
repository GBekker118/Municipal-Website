using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using PROG7312_POE.Models;

namespace PROG7312_POE.Controllers
{
    public class ServiceRequestsController : Controller
    {
        private readonly TableClient _tableClient;

        public ServiceRequestsController(IConfiguration configuration)
        {
            var connectionString = configuration.GetSection("StorageAccount")["AzureStorage"];
            _tableClient = new TableClient(connectionString, "ReportIssues");
            _tableClient.CreateIfNotExists();
        }

        // Service Request Index Page (User)
        public IActionResult ServiceRequestIndex()
        {
            // Fetch all reports from Azure Table Storage
            var allReports = _tableClient.Query<ReportIssue>().ToList();

            // Populate dropdowns
            ViewBag.Locations = allReports.Select(r => r.Location).Distinct().ToList();
            ViewBag.Statuses = allReports.Select(r => r.Status).Distinct().ToList();
            ViewBag.SelectedLocation = null;
            ViewBag.SelectedStatus = null;
            ViewBag.SelectedDate = null;

            // Root node
            var rootRequest = new ServiceRequest
            {
                RequestID = 1,
                RequestTitle = "Service Requests"
            };
            var rootNode = new ServiceRequestNode<ServiceRequest>(rootRequest);

            int requestCounter = 2;

            // Group reports by Status (first-level children)
            var groupedByStatus = allReports.GroupBy(r => r.Status);

            foreach (var statusGroup in groupedByStatus)
            {
                // Create a status node (first-level child)
                var statusNode = new ServiceRequestNode<ServiceRequest>(
                    new ServiceRequest
                    {
                        RequestID = requestCounter++,
                        RequestTitle = statusGroup.Key 
                    }
                );

                // Add each report as a child of the status node (second-level child)
                foreach (var report in statusGroup)
                {
                    var childNode = new ServiceRequestNode<ServiceRequest>(
                        new ServiceRequest
                        {
                            RequestID = requestCounter++,
                            ReportID = report.Id,
                            RequestTitle = report.Category,
                            Description = report.Description,
                            Status = report.Status,
                            Location = report.Location,
                            Date = report.Date
                        }
                    );

                    statusNode.Children.Add(childNode);
                }

                // Add the status node to the root
                rootNode.Children.Add(statusNode);
            }

            return View(rootNode);
        }

        // Service Request Index Page (Admin)
        public IActionResult ServiceRequestAdmin()
        {
            // Fetch all reports from Azure Table Storage
            var allReports = _tableClient.Query<ReportIssue>().ToList();

            // Populate dropdowns
            ViewBag.Locations = allReports.Select(r => r.Location).Distinct().ToList();
            ViewBag.Statuses = allReports.Select(r => r.Status).Distinct().ToList();
            ViewBag.SelectedLocation = null;
            ViewBag.SelectedStatus = null;
            ViewBag.SelectedDate = null;

            // Root node
            var rootRequest = new ServiceRequest
            {
                RequestID = 1,
                RequestTitle = "Service Requests"
            };
            var rootNode = new ServiceRequestNode<ServiceRequest>(rootRequest);

            int requestCounter = 2;

            // Group reports by Status (first-level children)
            var groupedByStatus = allReports.GroupBy(r => r.Status);

            foreach (var statusGroup in groupedByStatus)
            {
                // Create a status node (first-level child)
                var statusNode = new ServiceRequestNode<ServiceRequest>(
                    new ServiceRequest
                    {
                        RequestID = requestCounter++,
                        RequestTitle = statusGroup.Key
                    }
                );

                // Add each report as a child of the status node (second-level child)
                foreach (var report in statusGroup)
                {
                    var childNode = new ServiceRequestNode<ServiceRequest>(
                        new ServiceRequest
                        {
                            RequestID = requestCounter++,
                            ReportID = report.Id,
                            RequestTitle = report.Category,
                            Description = report.Description,
                            Status = report.Status,
                            Location = report.Location,
                            Date = report.Date
                        }
                    );

                    statusNode.Children.Add(childNode);
                }

                // Add the status node to the root
                rootNode.Children.Add(statusNode);
            }

            return View(rootNode);
        }

        public IActionResult UpdateServiceRequest(string requestId)
        {
            if (string.IsNullOrEmpty(requestId))
                return BadRequest();

            // Fetch the report directly from the table
            var report = _tableClient.Query<ReportIssue>(r => r.Id == requestId).FirstOrDefault();
            if (report == null)
                return NotFound();

            return View(report);
        }

        // Post method to update a service request's status
        [HttpPost]
        public async Task<IActionResult> UpdateStatusUsingTree(string reportId, string newStatus)
        {
            if (string.IsNullOrEmpty(reportId) || string.IsNullOrEmpty(newStatus))
                return BadRequest("Report ID or status missing.");

            // Fetch all reports from Azure Table Storage
            var allReports = _tableClient.Query<ReportIssue>().ToList();

            // Build tree
            var rootRequest = new ServiceRequest
            {
                RequestID = 1,
                RequestTitle = "Service Requests"
            };
            var rootNode = new ServiceRequestNode<ServiceRequest>(rootRequest);

            int requestCounter = 2;
            var groupedByLocation = allReports.GroupBy(r => r.Location);
            foreach (var locationGroup in groupedByLocation)
            {
                var locationNode = new ServiceRequestNode<ServiceRequest>(
                    new ServiceRequest
                    {
                        RequestID = requestCounter++,
                        RequestTitle = locationGroup.Key
                    }
                );

                foreach (var report in locationGroup)
                {
                    var childNode = new ServiceRequestNode<ServiceRequest>(
                        new ServiceRequest
                        {
                            RequestID = requestCounter++,
                            ReportID = report.Id,
                            RequestTitle = report.Category,
                            Description = report.Description,
                            Status = report.Status
                        }
                    );
                    locationNode.Children.Add(childNode);
                }

                rootNode.Children.Add(locationNode);
            }

            // Wrap it in the tree class
            var tree = new ServiceRequestsTree<ServiceRequest> { Root = rootNode };

            // Search for the target node using the tree's FindByReportID method
            var targetNode = tree.FindByReportID(reportId); 

            if (targetNode == null)
                return NotFound("Report not found in the tree.");

            // Update the status in the tree node
            targetNode.Data.Status = newStatus;

            // Update Azure Table Storage
            var reportEntity = allReports.First(r => r.Id == reportId);
            reportEntity.Status = newStatus;
            await _tableClient.UpdateEntityAsync(reportEntity, reportEntity.ETag, TableUpdateMode.Replace);

            TempData["Message"] = $"Status for {reportId} updated to {newStatus}";
            return RedirectToAction("ServiceRequestAdmin");
        }

        // Search Feature (User)
        [HttpGet]
        public IActionResult ServiceRequestSearch(string query, string organizeBy)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                TempData["Error"] = "Please enter a search term.";
                return RedirectToAction("ServiceRequestIndex");
            }

            var allReports = _tableClient.Query<ReportIssue>().ToList();

            var filteredReports = allReports
                .Where(r => (r.Category ?? "").Contains(query, StringComparison.OrdinalIgnoreCase)
                         || (r.Location ?? "").Contains(query, StringComparison.OrdinalIgnoreCase)
                         || (r.Id ?? "").Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filteredReports.Any())
            {
                TempData["Error"] = $"No reports found matching '{query}'.";
                return RedirectToAction("ServiceRequestIndex");
            }

            var rootNode = BuildTree(filteredReports, organizeBy);
            ViewBag.OrganizeBy = organizeBy;

            return View("ServiceRequestIndex", rootNode);
        }

        // Search Feature (Admin)
        [HttpGet]
        public IActionResult ServiceRequestAdminSearch(string query, string organizeBy)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                TempData["Error"] = "Please enter a search term.";
                return RedirectToAction("ServiceRequestAdmin");
            }

            var allReports = _tableClient.Query<ReportIssue>().ToList();

            var filteredReports = allReports
                .Where(r => (r.Category ?? "").Contains(query, StringComparison.OrdinalIgnoreCase)
                         || (r.Location ?? "").Contains(query, StringComparison.OrdinalIgnoreCase)
                         || (r.Id ?? "").Contains(query, StringComparison.OrdinalIgnoreCase))
                .ToList();

            if (!filteredReports.Any())
            {
                TempData["Error"] = $"No reports found matching '{query}'.";
                return RedirectToAction("ServiceRequestAdmin");
            }

            var rootNode = BuildTree(filteredReports, organizeBy);
            ViewBag.OrganizeBy = organizeBy;

            return View("ServiceRequestAdmin", rootNode);
        }

        // Helper to build the tree for search results
        private ServiceRequestNode<ServiceRequest> BuildTree(List<ReportIssue> reports, string organizeBy = "Status")
        {
            var rootRequest = new ServiceRequest { RequestID = 1, RequestTitle = "Service Requests" };
            var rootNode = new ServiceRequestNode<ServiceRequest>(rootRequest);

            int requestCounter = 2;
            IEnumerable<IGrouping<string, ReportIssue>> groupedReports;

            switch (organizeBy?.ToLower())
            {
                case "location":
                    groupedReports = reports.GroupBy(r => r.Location ?? "Unknown");
                    break;
                case "date":
                    groupedReports = reports.GroupBy(r => r.Date != default(DateTime)? r.Date.ToString("d MMMM yyyy"): "Unknown Date");
                    break;
                case "status":
                default:
                    groupedReports = reports.GroupBy(r => r.Status ?? "Unknown");
                    break;
            }

            foreach (var group in groupedReports)
            {
                var parentNode = new ServiceRequestNode<ServiceRequest>(
                    new ServiceRequest
                    {
                        RequestID = requestCounter++,
                        RequestTitle = group.Key
                    });

                foreach (var report in group)
                {
                    var childNode = new ServiceRequestNode<ServiceRequest>(
                        new ServiceRequest
                        {
                            RequestID = requestCounter++,
                            ReportID = report.Id,
                            RequestTitle = report.Category,
                            Description = report.Description,
                            Status = report.Status,
                            Location = report.Location,
                            Date = report.Date
                        });

                    parentNode.Children.Add(childNode);
                }

                rootNode.Children.Add(parentNode);
            }

            return rootNode;
        }

        // Graph Traversal for filtering and sorting
        public IActionResult ServiceRequestGraphTraversal()
        {
            // Fetch all reports
            var allReports = _tableClient.Query<ReportIssue>().ToList();

            // Build graph
            var graph = new ServiceRequestGraph<ReportIssue>();

            // Create nodes for each report
            var nodes = allReports.ToDictionary(r => r.Id, r => graph.AddNode(r));

            // Connect nodes: connect reports in the same location
            foreach (var group in allReports.GroupBy(r => r.Location))
            {
                var groupList = group.ToList();
                for (int i = 0; i < groupList.Count; i++)
                {
                    for (int j = i + 1; j < groupList.Count; j++)
                    {
                        graph.AddEdge(nodes[groupList[i].Id], nodes[groupList[j].Id]);
                    }
                }
            }

            // Pick a start node (first report)
            var startNode = nodes[allReports.First().Id];

            // Perform BFS and DFS
            var bfsResult = graph.BreadthFirstSearch(startNode);
            var dfsResult = graph.DepthFirstSearch(startNode);

            // Pass results to the view
            ViewBag.BFS = bfsResult;
            ViewBag.DFS = dfsResult;

            return View();
        }

        // Filter Feature (User)
        // Filter Feature (User)
        [HttpGet]
        public IActionResult ServiceRequestFilter(string location, string status, DateTime? date, string organizeBy)
        {
            var allReports = _tableClient.Query<ReportIssue>().ToList();

            // Filter reports based on user's selection
            var filteredReports = allReports
                .Where(r =>
                    (string.IsNullOrEmpty(location) || r.Location == location) &&
                    (string.IsNullOrEmpty(status) || r.Status == status) &&
                    (!date.HasValue || r.Date.Date == date.Value.Date)
                ).ToList();

            // Build tree from filtered reports
            var rootNode = BuildTree(filteredReports, organizeBy);

            // Get leaf nodes from the tree
            var leafNodes = GetAllLeafNodes(rootNode);

            // Build graph from leaf nodes
            var graph = new ServiceRequestGraph<ServiceRequest>();
            var nodeDict = leafNodes.ToDictionary(n => n.Data.RequestID, n => graph.AddNode(n.Data));

            // Link nodes based on their organizing property (location/status/date)
            foreach (var group in leafNodes.GroupBy(n =>
                organizeBy?.ToLower() == "status" ? n.Data.Status :
                organizeBy?.ToLower() == "location" ? n.Data.Location :
                organizeBy?.ToLower() == "date" ? n.Data.Date?.ToString("yyyy-MM-dd") :
                n.Data.Status))
            {
                var list = group.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        graph.AddEdge(nodeDict[list[i].Data.RequestID], nodeDict[list[j].Data.RequestID]);
                    }
                }
            }

            // Run BFS and DFS starting from the first node
            ViewBag.BFS = leafNodes.Any() ? graph.BreadthFirstSearch(nodeDict[leafNodes[0].Data.RequestID]) : new List<ServiceRequest>();
            ViewBag.DFS = leafNodes.Any() ? graph.DepthFirstSearch(nodeDict[leafNodes[0].Data.RequestID]) : new List<ServiceRequest>();

            // Keep filter options visible in view
            ViewBag.OrganizeBy = organizeBy;
            ViewBag.SelectedLocation = location;
            ViewBag.SelectedStatus = status;
            ViewBag.SelectedDate = date;
            ViewBag.Locations = allReports.Select(r => r.Location).Distinct().ToList();
            ViewBag.Statuses = allReports.Select(r => r.Status).Distinct().ToList();

            return View("ServiceRequestIndex", rootNode);
        }

        // Filter Feature (Admin)
        [HttpGet]
        public IActionResult ServiceRequestAdminFilter(string location, string status, DateTime? date, string organizeBy)
        {
            var allReports = _tableClient.Query<ReportIssue>().ToList();

            // Filter reports based on admin's selection
            var filteredReports = allReports
                .Where(r =>
                    (string.IsNullOrEmpty(location) || r.Location == location) &&
                    (string.IsNullOrEmpty(status) || r.Status == status) &&
                    (!date.HasValue || r.Date.Date == date.Value.Date)
                ).ToList();

            // Build tree from filtered reports
            var rootNode = BuildTree(filteredReports, organizeBy);

            // Get leaf nodes from the tree
            var leafNodes = GetAllLeafNodes(rootNode);

            // Build graph from leaf nodes
            var graph = new ServiceRequestGraph<ServiceRequest>();
            var nodeDict = leafNodes.ToDictionary(n => n.Data.RequestID, n => graph.AddNode(n.Data));

            // Link nodes based on their organizing property (location/status/date)
            foreach (var group in leafNodes.GroupBy(n =>
                organizeBy?.ToLower() == "status" ? n.Data.Status :
                organizeBy?.ToLower() == "location" ? n.Data.Location :
                organizeBy?.ToLower() == "date" ? n.Data.Date?.ToString("yyyy-MM-dd") :
                n.Data.Status))
            {
                var list = group.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        graph.AddEdge(nodeDict[list[i].Data.RequestID], nodeDict[list[j].Data.RequestID]);
                    }
                }
            }

            // Run BFS and DFS starting from the first node
            ViewBag.BFS = leafNodes.Any() ? graph.BreadthFirstSearch(nodeDict[leafNodes[0].Data.RequestID]) : new List<ServiceRequest>();
            ViewBag.DFS = leafNodes.Any() ? graph.DepthFirstSearch(nodeDict[leafNodes[0].Data.RequestID]) : new List<ServiceRequest>();

            // Keep filter options visible in view
            ViewBag.OrganizeBy = organizeBy;
            ViewBag.SelectedLocation = location;
            ViewBag.SelectedStatus = status;
            ViewBag.SelectedDate = date;
            ViewBag.Locations = allReports.Select(r => r.Location).Distinct().ToList();
            ViewBag.Statuses = allReports.Select(r => r.Status).Distinct().ToList();

            return View("ServiceRequestAdmin", rootNode);
        }

        // Organize Requests Feature (User)
        public IActionResult ServiceRequestOrganize(string organizeBy)
        {
            var allReports = _tableClient.Query<ReportIssue>().ToList();

            // Build tree
            var rootNode = BuildTree(allReports, organizeBy);

            // Get leaf nodes
            var leafNodes = GetAllLeafNodes(rootNode);

            // Build graph from leaf nodes
            var graph = new ServiceRequestGraph<ServiceRequest>();
            var nodeDict = leafNodes.ToDictionary(n => n.Data.RequestID, n => graph.AddNode(n.Data));

            // Connect nodes based on organizeBy property
            foreach (var group in leafNodes.GroupBy(n =>
                organizeBy?.ToLower() == "status" ? n.Data.Status :
                organizeBy?.ToLower() == "location" ? n.Data.Location :
                organizeBy?.ToLower() == "date" ? n.Data.Date?.ToString("yyyy-MM-dd") :
                n.Data.Status))
            {
                var list = group.ToList();
                for (int i = 0; i < list.Count; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        graph.AddEdge(nodeDict[list[i].Data.RequestID], nodeDict[list[j].Data.RequestID]);
                    }
                }
            }

            // Pass tree to view
            ViewBag.OrganizeBy = organizeBy;
            ViewBag.Locations = allReports.Select(r => r.Location).Distinct().ToList();
            ViewBag.Statuses = allReports.Select(r => r.Status).Distinct().ToList();

            return View("ServiceRequestIndex", rootNode);
        }

        // Organize Requests Feature (Admin)
        public IActionResult ServiceRequestAdminOrganize(string organizeBy)
        {
            var allReports = _tableClient.Query<ReportIssue>().ToList();

            var rootNode = BuildTree(allReports, organizeBy);

            ViewBag.OrganizeBy = organizeBy; 
            ViewBag.Locations = allReports.Select(r => r.Location).Distinct().ToList();
            ViewBag.Statuses = allReports.Select(r => r.Status).Distinct().ToList();

            return View("ServiceRequestAdmin", rootNode);
        }

        // Get all Leaf Nodes from tree
        private List<ServiceRequestNode<T>> GetAllLeafNodes<T>(ServiceRequestNode<T> root)
        {
            var leaves = new List<ServiceRequestNode<T>>();

            void Traverse(ServiceRequestNode<T> node)
            {
                if (node.Children.Count == 0)
                {
                    leaves.Add(node);
                }
                else
                {
                    foreach (var child in node.Children)
                        Traverse(child);
                }
            }
            Traverse(root);
            return leaves;
        }
    }
}
