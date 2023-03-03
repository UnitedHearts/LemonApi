using Contracts.Mail;
using EmailService;
using LemonDB;
using Microsoft.EntityFrameworkCore;

namespace LemonApi.Extensions;

public static class ServiceProviderExtensions
{
    ///<summary>
    ///Подключает базу данных
    ///</summary>
    ///<paramname="services"></param>
    public static void AddDatabaseModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<LemonDbContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        });

    }

    /// <summary>
    /// подключает сервис по пересылке почты
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    public static void AddEmailModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
        services.AddSingleton<IMailSender, SmtpMailSender>();
    }

}
