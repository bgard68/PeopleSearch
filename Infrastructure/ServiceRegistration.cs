using Microsoft.Extensions.DependencyInjection;
using Domain.Interface;
using Infrastructure.Repositories;

namespace Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<IPeopleRepository, PeopleRepository>();
            services.AddScoped<IAddressRepository, AddressRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            // Add other infrastructure services/repositories here
            return services;
        }
    }
}