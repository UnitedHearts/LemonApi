using Contracts.Mail;
using EmailService;
using LemonDB;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Text;
using LemonApi.Middlewares;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using api_avia.Extensions;
using LemonApi.Models;

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
        services.AddSingleton(configuration.GetSection("Envelope").Get<Envelope>());
        services.AddSingleton(configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>());
        services.AddSingleton<IMailSender, SmtpMailSender>();
    }


    /// <summary>
    /// Настраивает систему авторизации по JWT-токену
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureAuthorization(this IServiceCollection services, IConfiguration configuration)
    {
        var config = configuration.GetSection("JWTConfig")
                                       .Get<JWTConfig>();
        services.AddSingleton(config);

        services.AddAuthorization();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    // указывает, будет ли валидироваться издатель при валидации токена
                    ValidateIssuer = false,
                    // строка, представляющая издателя
                    ValidIssuer = config.Issuer,
                    // будет ли валидироваться потребитель токена
                    ValidateAudience = true,
                    // установка потребителя токена
                    ValidAudience = config.Audience,
                    // будет ли валидироваться время существования
                    ValidateLifetime = config.UseLifeTime,
                    // установка ключа безопасности
                    IssuerSigningKey = JWTExtansion.GetSymmetricSecurityKey(),
                    // валидация ключа безопасности
                    ValidateIssuerSigningKey = true,
                };
            });
    }

    /// <summary>
    /// добавляет wrapper для ответов
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureServices(this IServiceCollection services)
    {
        services.AddSingleton<IActionResultExecutor<ObjectResult>, ResponseEnvelopeResultExecutor>();
    }

    /// <summary>
    /// Настраивает swagger UI
    /// </summary>
    /// <param name="services"></param>
    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(option =>
        {
            option.CustomSchemaIds(type => type.ToString());
            option.SchemaFilter<SwaggerSchemaFilter>();
            option.SwaggerDoc("v1", new OpenApiInfo { Title = "Lemon API", Version = "v1" });
            option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Please enter a valid token",
                Name = "Authorization",
                Type = SecuritySchemeType.Http,
                BearerFormat = "JWT",
                Scheme = "Bearer"
            });
            option.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
        });
    }
}
