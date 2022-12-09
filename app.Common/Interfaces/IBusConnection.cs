using RabbitMQ.Client;

namespace app.Common.Interfaces
{
    public interface IBusConnection
    {
        IModel ConnectChannel();
        void TryConnect();
    }

}