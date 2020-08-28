using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Patrimonios.Domain.PipelineBehaviors;
using Patrimonios.Domain.Repositories;
using Patrimonios.Infra.Repositories;

namespace Patrimonios.Api.Configurations
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(FailFastRequestBehavior<,>));


            services.AddScoped(typeof(IMarcaRepository), typeof(MarcaRepository));
            services.AddScoped(typeof(IPatrimonioRepository), typeof(PatrimonioRepository));

            return services;
        }
    }
}
