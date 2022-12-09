using app.Common.Interfaces;
using app.Common.Models;
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
            Channel = ConnectChannel();
        }
        public IModel ConnectChannel()
        {
            TryConnect();

            if (!IsConnected || Connection == null)
                throw new InvalidOperationException("No RabbitMQ connections are available to perform this action");

            return Channel;
        }

        public void TryConnect()
        {
            lock (semaphore)
            {
                if (Connection is null || !Connection.IsOpen)
                {
                    var factory = new ConnectionFactory()
                    {
                        HostName = _options.HostName,
                        UserName = _options.UserName,
                        Password = _options.Password,
                        Port = AmqpTcpEndpoint.UseDefaultPort
                    };

                    Connection = factory.CreateConnection();
                    Channel = Connection.CreateModel();
                    Connection.ConnectionShutdown += (s, e) => TryConnect();
                    Connection.CallbackException += (s, e) => TryConnect();
                    Connection.ConnectionBlocked += (s, e) => TryConnect();
                    IsConnected = Connection.IsOpen;
                }
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
            if (Connection is not null)
                Connection.Close();
        }

    }


}