using Contracts.Mail;
using EmailService;
using LemonDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

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
    ///
    public static void AddEmailModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
        services.AddSingleton<IMailSender, SmtpMailSender>();
    }

}
