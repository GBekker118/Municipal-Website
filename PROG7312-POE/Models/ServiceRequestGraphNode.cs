namespace PROG7312_POE.Models
{
    public class ServiceRequestGraphNode<T>
    {
        public T Data { get; set; }
        public List<ServiceRequestGraphNode<T>> Neighbors { get; set; } = new List<ServiceRequestGraphNode<T>>();

        public ServiceRequestGraphNode(T data)
        {
            Data = data;
        }
    }
}
