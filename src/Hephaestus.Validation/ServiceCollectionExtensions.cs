using Microsoft.Extensions.DependencyInjection;

namespace Hephaestus.Validation
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddValidation(this IServiceCollection services)
        {
            services.AddScoped<IValidator, Validator>();

            return services;
        }
    }
}
