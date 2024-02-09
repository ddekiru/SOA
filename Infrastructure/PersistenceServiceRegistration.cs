using Domain.Repository;
using Infrastructure.DataAccess;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<InternshipsContext>((options) => options
                .UseSqlServer(connectionString: configuration.GetConnectionString("AppConnectionString")));

            // System.Configuration.ConfigurationManager.AppSettings["connectionString"]

            services.AddScoped<IAdminUserRepo, AdminUserDbRepo>();
            services.AddScoped<IRegularUserRepo, RegularUserDbRepo>();
            services.AddScoped<IInternshipRepo, InternshipDbRepo>();
            services.AddScoped<IInternshipApplicationRepo, InternshipApplicationDbRepo>();

            return services;
        }
    }
}
