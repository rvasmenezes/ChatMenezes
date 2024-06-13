using RabbitMQ.Client;
using System.Text;
using IConnection = RabbitMQ.Client.IConnection;
using IModel = RabbitMQ.Client.IModel;

namespace ChatMenezes.Core.RabbitMQ
{
    public class RabbitMQServices
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public RabbitMQServices()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "rabbitmq",
                Port = 5672,
                UserName = "admin",
                Password = "admin"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
        }

        public void SendMessage(string queueName, string message)
        {
            _channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(exchange: "",
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);
        }
    }
}
