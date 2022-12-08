using app.Common.Interfaces;
using app.Common.Models.Options;
using RabbitMQ.Client;

namespace app.Common.Configurations
{
    public class RabbitMqConnection : IDisposable, IBusConnection
    {
        private IModel Channel;
        public IConnection? Connection = null;
        private readonly object semaphore = new object();
        public bool IsConnected = false;
        protected RabbitMqOptions _options;

        public RabbitMqConnection(IConnectionFactory connectionFactory, RabbitMqOptions options)
        {
            _options = options;
            Connection = connectionFactory.CreateConnection();
            IsConnected = Connection.IsOpen;
            Channel = CreateChannel();
        }

        public IModel GetChannel() => Channel;

        public IModel CreateChannel()
        {
            TryConnect();

            if (!IsConnected || Connection == null)
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

            return Connection.CreateModel();
        }

        public void TryConnect()
        {
            lock (semaphore)
            {
                if (IsConnected)
                    return;

                var factory = new ConnectionFactory()
                {
                    HostName = _options.HostName,
                    UserName = _options.UserName,
                    Password = _options.Password,
                    Port = AmqpTcpEndpoint.UseDefaultPort
                };

                Connection = factory.CreateConnection();
                Connection.ConnectionShutdown += (s, e) => TryConnect();
                Connection.CallbackException += (s, e) => TryConnect();
                Connection.ConnectionBlocked += (s, e) => TryConnect();
                IsConnected = Connection.IsOpen;
            }
        }

        public void Dispose()
        {
            Channel.Close();
            CloseConnection();
            GC.SuppressFinalize(this);
        }
        private void CloseConnection()
        {
            if (Connection is not null && !Connection.IsOpen)
                Connection.Close();
        }

    }


}