using System;
using System.Collections.Generic;
using System.Linq;

namespace PROG7312_POE.Models
{
    public class ServiceRequestGraph<T>
    {
        public List<ServiceRequestGraphNode<T>> Nodes { get; set; } = new List<ServiceRequestGraphNode<T>>();

        public ServiceRequestGraphNode<T> AddNode(T data)
        {
            var node = new ServiceRequestGraphNode<T>(data);
            Nodes.Add(node);
            return node;
        }

        public void AddEdge(ServiceRequestGraphNode<T> node1, ServiceRequestGraphNode<T> node2, bool bidirectional = true)
        {
            node1.Neighbors.Add(node2);
            if (bidirectional)
            {
                node2.Neighbors.Add(node1);
            }
        }

        // BFS traversal
        public List<T> BreadthFirstSearch(ServiceRequestGraphNode<T> start)
        {
            var visited = new HashSet<ServiceRequestGraphNode<T>>();
            var queue = new Queue<ServiceRequestGraphNode<T>>();
            var result = new List<T>();

            queue.Enqueue(start);
            visited.Add(start);

            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                result.Add(node.Data);

                foreach (var neighbor in node.Neighbors)
                {
                    if (!visited.Contains(neighbor))
                    {
                        visited.Add(neighbor);
                        queue.Enqueue(neighbor);
                    }
                }
            }

            return result;
        }

        // DFS traversal
        public List<T> DepthFirstSearch(ServiceRequestGraphNode<T> start)
        {
            var visited = new HashSet<ServiceRequestGraphNode<T>>();
            var result = new List<T>();
            DFSHelper(start, visited, result);
            return result;
        }

        private void DFSHelper(ServiceRequestGraphNode<T> node, HashSet<ServiceRequestGraphNode<T>> visited, List<T> result)
        {
            visited.Add(node);
            result.Add(node.Data);

            foreach (var neighbor in node.Neighbors)
            {
                if (!visited.Contains(neighbor))
                {
                    DFSHelper(neighbor, visited, result);
                }
            }
        }
    }
}
