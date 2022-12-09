namespace dotnet_worker.Interfaces
{
    public interface IQueuePublisherService
    {
        void SendMessage(object myObject);
    }
}