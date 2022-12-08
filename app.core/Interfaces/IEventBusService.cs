namespace dotnet_worker.Interfaces
{
    public interface IEventBusService
    {
        void SendMessage(object myObject);
    }
}