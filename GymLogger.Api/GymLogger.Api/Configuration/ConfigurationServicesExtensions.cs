using GymLogger.Application;
using GymLogger.Infrastructure.Database;
using Microsoft.AspNetCore.Identity;
using Serilog;

namespace GymLogger.Api.Configuration;

public static class ConfigurationServicesExtensions
{
    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services.AddAuthorization();
        services
            .AddAuthentication()
            .AddBearerToken(IdentityConstants.BearerScheme);
        services.AddAuthorizationBuilder();

        
        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.SameSite = SameSiteMode.None;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        return services;
    }

    public static IServiceCollection AddCustomAuoMapper(this IServiceCollection services)
    {
        services.AddAutoMapper(
            typeof(ApiMapperProfile).Assembly,
            typeof(ApplicationMapperProfile).Assembly,
            typeof(InfrastructureDatabaseMapperProfile).Assembly);

        return services;
    }

    public static IServiceCollection AddCustomSerilog(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSerilog((services, lc) => lc.ReadFrom.Configuration(configuration));

        return services;
    }

    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        return services;
    }

    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder.WithOrigins("https://localhost:5001", "http://localhost:5000", "https://localhost:3000", "https://gymlogger-pink.vercel.app", "https://192.168.1.65:3000")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .Build());
        });

        return services;
    }

    public static IApplicationBuilder UseCustomSwagger(this IApplicationBuilder app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            options.RoutePrefix = "api-docs";
        });

        return app;
    }
}
