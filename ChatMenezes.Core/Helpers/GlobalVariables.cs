using Microsoft.Extensions.Configuration;

namespace ChatMenezes.Core.Helpers
{
    public class GlobalVariables
    {
        public readonly IConfigurationSection _sectionRabbitMQ;

        public GlobalVariables(IConfiguration configuration)
        {
            _sectionRabbitMQ = configuration.GetSection("RabbitMQ");
        }

        public string RabbitMQHostName => _sectionRabbitMQ["HostName"]!;
        public int RabbitMQPort => Convert.ToInt32(_sectionRabbitMQ["Port"]!);
        public string RabbitMQUserName => _sectionRabbitMQ["UserName"]!;
        public string RabbitMQPassword => _sectionRabbitMQ["Password"]!;
    }
}
