using ChatMenezes.Core.Helpers;
using RabbitMQ.Client;
using System.Text;

namespace ChatMenezes.Core.RabbitMQ
{
    public class RabbitMQServices
    {

        public readonly GlobalVariables _globalVariables;

        public RabbitMQServices(GlobalVariables globalVariables)
        {
            _globalVariables = globalVariables;
        }

        public void SendMessage(string queueName, string message)
        {
            var factory = new ConnectionFactory()
            {
                HostName = _globalVariables.RabbitMQHostName,
                Port = _globalVariables.RabbitMQPort,
                UserName = _globalVariables.RabbitMQUserName,
                Password = _globalVariables.RabbitMQPassword
            };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: queueName,
                                     basicProperties: null,
                                     body: body);
            }
        }
    }
}
