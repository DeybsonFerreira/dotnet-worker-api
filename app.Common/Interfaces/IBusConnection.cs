using RabbitMQ.Client;

namespace app.Common.Interfaces
{
    public interface IBusConnection
    {
        IModel CreateChannel();
        IModel GetChannel();
    }

}