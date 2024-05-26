﻿using GymLogger.Application.User.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace GymLogger.Infrastructure.Http;
public static class ServiceExtensions
{
    public static IServiceCollection RegisterInfrastructureHttpServices(this IServiceCollection services)
    {
        services.AddScoped<ICurrentUserProvider, CurrentUserProvider>();

        return services;
    }
}
