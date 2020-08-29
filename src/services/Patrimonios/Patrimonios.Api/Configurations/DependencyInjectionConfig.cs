using Microsoft.Extensions.DependencyInjection;
using Patrimonios.Domain.Repositories;
using Patrimonios.Domain.Repositories.Events;
using Patrimonios.Infra.Repositories;
using Patrimonios.Infra.Repositories.Events;

namespace Patrimonios.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IMarcaRepository), typeof(MarcaRepository));
            services.AddScoped(typeof(IPatrimonioRepository), typeof(PatrimonioRepository));

            services.AddScoped(typeof(IMarcaLogEventRepository), typeof(MarcaLogEventRepository));
            services.AddScoped(typeof(IPatrimonioLogEventRepository), typeof(PatrimonioLogEventRepository));

            return services;
        }
    }
}
