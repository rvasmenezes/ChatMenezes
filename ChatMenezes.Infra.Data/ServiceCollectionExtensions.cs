using ChatMenezes.Domain.Interfaces;
using ChatMenezes.Infra.Data.Repositories.EntityFramework;
using ChatMenezes.Infra.Data.Repositories.UnitOfWork;
using Microsoft.Extensions.DependencyInjection;

namespace ChatMenezes.Infra.Data
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfraDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IBaseEFRepository<>), typeof(BaseEFRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
