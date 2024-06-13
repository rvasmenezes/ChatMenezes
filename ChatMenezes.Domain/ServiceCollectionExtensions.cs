using ChatMenezes.Core.ApplicationContext;
using ChatMenezes.Core.RabbitMQ;
using ChatMenezes.Domain.Aggregates.MessageAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.MessageAgg.Services;
using ChatMenezes.Domain.Aggregates.RoomAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.RoomAgg.Services;
using ChatMenezes.Domain.Aggregates.UserAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.UserAgg.Services;
using ChatMenezes.Domain.Host;
using Microsoft.Extensions.DependencyInjection;

namespace ChatMenezes.Domain
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddServicesDependencies(this IServiceCollection services)
        {
            services.AddScoped<IChatWebContext, ChatWebContext>();

            services.AddScoped<IUserServices, UserServices>();
            services.AddScoped<IRoomServices, RoomServices>();
            services.AddScoped<IMessageServices, MessageServices>();

            services.AddScoped<RabbitMQServices>();
            services.AddHostedService<QueueConsumer>();

            return services;
        }
    }
}
