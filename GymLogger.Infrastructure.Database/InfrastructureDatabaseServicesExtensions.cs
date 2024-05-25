﻿using GymLogger.Infrastructure.Database.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymLogger.Infrastructure.Database;
public static class InfrastructureDatabaseServicesExtensions
{
    public static IServiceCollection AddInfrastructureDatabase(this IServiceCollection services,
        IConfiguration configuration, bool isProduction, bool isTestEnv = false)
    {
        //TODO: Add registration of repositories here

        var connectionStringKey = isProduction ? "ProductionConnection" : "DefaultConnection";
        var connectionString = configuration.GetConnectionString(connectionStringKey);
        services.AddDbContext<GymLoggerDbContext>(
            options => options.UseSqlServer(connectionString),
            isTestEnv ? ServiceLifetime.Transient : ServiceLifetime.Scoped);
        services.AddDatabaseDeveloperPageExceptionFilter();
        
        services.AddIdentityCore<DbApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedEmail = true;
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 6;
                options.Password.RequireDigit = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            })            
            .AddEntityFrameworkStores<GymLoggerDbContext>()
            .AddSignInManager()
            .AddDefaultTokenProviders();

        return services;
    }
}
