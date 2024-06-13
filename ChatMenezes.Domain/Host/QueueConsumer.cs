using ChatMenezes.Domain.Aggregates.StockAgg.Dtos;
using ChatMenezes.Domain.Aggregates.StockAgg.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;

namespace ChatMenezes.Domain.Host
{
    public class QueueConsumer : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public QueueConsumer(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

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

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            ConsumeMessage("find_stock_code", ProcessMessageAsync);

            return Task.CompletedTask;
        }

        public void ConsumeMessage(string queueName, Func<string, Task> handleMessage)
        {
            _channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await Task.Run(() => handleMessage(message));
            };

            _channel.BasicConsume(queue: queueName,
                                 autoAck: true,
                                 consumer: consumer);
        }

        private async Task ProcessMessageAsync(string message)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var _notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();

                var messageObject = JsonSerializer.Deserialize<FindStockDto>(message);

                if (messageObject != null)
                    await _notificationService.NotifyAsync(messageObject);
            }
        }
    }
}
