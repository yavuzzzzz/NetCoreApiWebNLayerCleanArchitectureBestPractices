using App.Repositories.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Repositories.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionsString = configuration.GetSection
                    (ConnectionStringOption.Key).Get<ConnectionStringOption>();

                options.UseSqlServer(connectionsString!.SqlServer, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);

                });


            });

            services.AddScoped <IProductRepository, ProductRepository>(); // ProductRepository is added to the DI container
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>)); // GenericRepository is added to the DI container
            return services;
        }
    }
}
