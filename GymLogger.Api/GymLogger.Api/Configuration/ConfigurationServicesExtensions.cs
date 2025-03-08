using GymLogger.Api.Components;
using GymLogger.Api.Components.Account;
using GymLogger.Application;
using GymLogger.Infrastructure.Database;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.FluentUI.AspNetCore.Components;
using Serilog;

namespace GymLogger.Api.Configuration;

public static class ConfigurationServicesExtensions
{
    public static IServiceCollection AddCustomRazorComponents(this IServiceCollection services)
    {
        services.AddRazorComponents()
            .AddInteractiveWebAssemblyComponents();
        services.AddFluentUIComponents();

        return services;
    }

    public static IServiceCollection AddCustomAuthentication(this IServiceCollection services)
    {
        services.AddCascadingAuthenticationState();
        services.AddScoped<IdentityUserAccessor>();
        services.AddScoped<IdentityRedirectManager>();
        services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

        services.AddAuthorization();
        // services.AddAuthentication(options =>
        //     {
        //         options.DefaultScheme = IdentityConstants.ApplicationScheme;
        //         options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
        //     })
        // .AddIdentityCookies();
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
                builder => builder.WithOrigins("https://localhost:5001", "http://localhost:5000", "https://localhost:3000", "https://gymlogger-pink.vercel.app")
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

    public static IEndpointRouteBuilder UseCustomComponents(this IEndpointRouteBuilder app)
    {
        app.MapRazorComponents<App>()
            .AddInteractiveWebAssemblyRenderMode()
            .AddAdditionalAssemblies(typeof(GymLogger.Api.Client._Imports).Assembly);

        return app;
    }
}
