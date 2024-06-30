﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Simple.App;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(config => { config.RegisterServicesFromAssembly(assembly); });
        services.AddValidatorsFromAssembly(assembly);
        services.AddScoped<IExecutionContext, ExecutionContext>();
        return services;
    }
}