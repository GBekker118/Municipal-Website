namespace PROG7312_POE.Models
{
    class ServiceRequestsTree<T>
    {
        public ServiceRequestNode<T> Root { get; set; }

        public ServiceRequestNode<T> FindNode(ServiceRequestNode<T> node, T value)
        {
            if (node == null) return null;
            if (node.Data.Equals(value))
                return node;
            foreach (var child in node.Children)
            {
                var result = FindNode(child, value);
                if (result != null) return result;
            }
            return null;
        }
    

        // Breadth-First Search (BFS)
        public List<T> BreadthFirstSearch()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            var visited = new List<T>();
            var queue = new Queue<ServiceRequestNode<T>>(); 
            queue.Enqueue(Root);

            while (queue.Count > 0)
            {
                ServiceRequestNode<T> currentNode = queue.Dequeue(); 
                visited.Add(currentNode.Data); 

                foreach (var child in currentNode.Children)
                {
                    queue.Enqueue(child);
                }
            }
            return visited;
        }

        // Helper method for the DFS logic
        private void DFSHelper(ServiceRequestNode<T> node, List<T> visited)
        {
            if (node == null) return;

            visited.Add(node.Data); 

            foreach (var child in node.Children)
            {
                DFSHelper(child, visited);
            }
        }

        // Depth-First Search (DFS)
        public List<T> DepthFirstSearch()
        {
            if (Root == null)
            {
                return new List<T>();
            }

            var visited = new List<T>();
            DFSHelper(Root, visited); 
            return visited;
        }

        public ServiceRequestNode<T> FindByReportID(string reportId)
        {
            if (Root == null) return null;

            ServiceRequestNode<T> result = null;

            void Search(ServiceRequestNode<T> node)
            {
                if (node.Data is ServiceRequest sr && sr.ReportID == reportId)
                {
                    result = node;
                    return;
                }
                foreach (var child in node.Children)
                {
                    if (result == null) Search(child);
                }
            }

            Search(Root);
            return result;
        }

    }
}