namespace PROG7312_POE.Models
{
    class ServiceRequestNode<T>
    {
        public T Data { get; set; }
        public ServiceRequestNode<T> Parent { get; set; }
        public List<ServiceRequestNode<T>> Children { get; set; }
            = new List<ServiceRequestNode<T>>();

        public ServiceRequestNode(T data)
        {
            this.Data = data;
        }
    }
}
