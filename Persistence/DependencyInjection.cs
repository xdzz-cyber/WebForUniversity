﻿using Application.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>();
        services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>()!);
        return services;
    }
}