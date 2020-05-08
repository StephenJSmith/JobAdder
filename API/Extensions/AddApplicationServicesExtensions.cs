using Core.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace API.Extensions
{
    public static class AddApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services) {
            services.AddScoped<ICandidateService, CandidateService>();
            services.AddScoped<IJobService, JobService>();

            return services;
        }
    }
}