using LemonDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace LemonApi.Extensions;

public static class ServiceProviderExtensions
{
    /// <summary>
    /// Подключает базу данных
    /// </summary>
    /// <param name="services"></param>
    public static void AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LemonDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

    }

}
