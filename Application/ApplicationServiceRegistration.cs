using Application.Services;
using Application.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IAdminUserService, AdminUserService>();
            services.AddScoped<IRegularUserService, RegularUserService>();
            services.AddScoped<IInternshipService, InternshipService>();
            services.AddScoped<IInternshipApplicationService, InternshipApplicationService>();

            return services;
        }
    }
}
